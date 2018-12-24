using AOISystem.Utilities;
using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Modules;
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Threading;

namespace AOISystem.MMFHandshake
{
    public class MMFServer
    {
        private static long MMF_CAPACITY_SIZE = 1024;

        private static long MMF_RECEIVE_OFFSET = 0;

        private static long MMF_RECEIVE_SIZE = 512;

        private static long MMF_SEND_OFFSET = 512;

        private static long MMF_SEND_SIZE = 512;

        private MemoryMappedFile _mmf;

        private Mutex _mutex;

        public MMFServerBlock ServerBlock;

        public MMFClientBlock ClientBlock;

        public MMFServer()
        {
            MMF_SEND_OFFSET = 0;
            MMF_SEND_SIZE = sizeof(int) + Marshal.SizeOf(typeof(MMFServerBlock));
            MMF_RECEIVE_OFFSET = MMF_SEND_SIZE;
            MMF_RECEIVE_SIZE = sizeof(int) + Marshal.SizeOf(typeof(MMFClientBlock));
            MMF_CAPACITY_SIZE = MMF_RECEIVE_SIZE + MMF_SEND_SIZE;

            _mmf = MemoryMappedFile.CreateOrOpen(MMFDefine.MAP_NAME, MMF_CAPACITY_SIZE, MemoryMappedFileAccess.ReadWrite);
            this.ServerBlock = new MMFServerBlock();
            this.ClientBlock = new MMFClientBlock();
            _mutex = new Mutex(false, MMFDefine.MUTEX_NAME);
            ModulesFactory.FlowControlHelper.GetFlowControl("MMF").AddFlowBase(new FlowBase("MMF_Blinding", FlowAction));
        }

        ~MMFServer()
        {
            Stop();
            _mmf.Dispose();
        }

        public event Action<Exception> ErrorRaised;

        private void OnErrorRaised(Exception ex)
        {
            if (ErrorRaised != null)
            {
                ErrorRaised(ex);
            }
        }

        public void Start()
        {
            ModulesFactory.FlowControlHelper.GetFlowControl("MMF").StartAll();
        }

        public void Stop()
        {
            ModulesFactory.FlowControlHelper.GetFlowControl("MMF").StopAll();
        }

        private void FlowAction(FlowVar flowVar)
        {
            Blinding();
        }

        private void Blinding()
        {
            if (_mutex.WaitOne(new TimeSpan(100)))
            {
                try
                {
                    using (MemoryMappedViewStream streamReader = _mmf.CreateViewStream(MMF_RECEIVE_OFFSET, MMF_RECEIVE_SIZE))
                    {
                        using (BinaryReader reader = new BinaryReader(streamReader))
                        {
                            int len = reader.ReadInt32();
                            if (len > 0)
                            {
                                if (len != (MMF_RECEIVE_SIZE - sizeof(int)))
                                {
                                    throw new Exception("接收結構大小與MMF分配容量不符");
                                }
                                this.ClientBlock = (MMFClientBlock)MarshalEx.BytesToStuct(reader.ReadBytes(len), typeof(MMFClientBlock));
                            }
                        }
                    }
                    using (MemoryMappedViewStream streamWriter = _mmf.CreateViewStream(MMF_SEND_OFFSET, MMF_SEND_SIZE))
                    {
                        using (BinaryWriter writer = new BinaryWriter(streamWriter))
                        {
                            byte[] data = MarshalEx.StructToBytes(this.ServerBlock);
                            if (data.Length > (MMF_SEND_SIZE - sizeof(int)))
                            {
                                throw new Exception("傳送結構大小大於MMF分配容量");
                            }
                            writer.Write(data.Length);
                            writer.Write(data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnErrorRaised(ex);
                }
                finally
                {
                    _mutex.ReleaseMutex();
                }
            }
        }
    }
}
