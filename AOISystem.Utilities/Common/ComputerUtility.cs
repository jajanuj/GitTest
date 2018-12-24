using System;
using System.Diagnostics;

namespace AOISystem.Utilities.Common
{
    public class ComputerUtility
    {
        private static readonly ComputerUtility instance = new ComputerUtility();

        PerformanceCounter cpu;
        Microsoft.VisualBasic.Devices.ComputerInfo cinf;

        public ComputerUtility()
        {
            cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cinf = new Microsoft.VisualBasic.Devices.ComputerInfo();
            _totalPhysicalMemory = cinf.TotalPhysicalMemory;
        }

        public static ComputerUtility GetInstance()
        {
            return instance;
        }

        public double GetUsedCupPercentage()
        {
            float cupPercentage = cpu.NextValue();
            return Math.Round(cupPercentage, 2, MidpointRounding.AwayFromZero);
        }

        private ulong _totalPhysicalMemory;
        public ulong GetPhysicalMemSize()
        {
            return _totalPhysicalMemory;
        }

        public ulong GetAvailablePhysicalMemory()
        {
            return cinf.AvailablePhysicalMemory;
        }

        public ulong GetAvailablePhysicalMemory(MemoryUnitMode unit)
        {
            ulong availablePhysicalMemory = cinf.AvailablePhysicalMemory;
            return (ulong)ConvertBytes(availablePhysicalMemory, (int)unit);
        }

        public ulong GetUsedPhysicalMemory(MemoryUnitMode unit)
        {
            ulong usedPhysicalMemory = GetPhysicalMemSize() - GetAvailablePhysicalMemory();
            return (ulong)ConvertBytes(usedPhysicalMemory, (int)unit);
        }

        public double GetUsedPhysicalMemoryPercentage()
        {
            return (double)(GetUsedPhysicalMemory(MemoryUnitMode.Byte) / Convert.ToDecimal(GetPhysicalMemSize()) * 100);
        }

        public decimal ConvertBytes(ulong b, int iteration)
        {
            long iter = 1;
            for (int i = 0; i < iteration; i++)
                iter *= 1024;
            return Math.Round((Convert.ToDecimal(b)) / Convert.ToDecimal(iter), 2, MidpointRounding.AwayFromZero);
        }
    }

    public enum MemoryUnitMode
    {
        Byte,
        Kilo_Byte,
        Mega_Byte,
        Giga_Byte
    }
}
