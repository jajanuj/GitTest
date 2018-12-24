namespace AOISystem.Utilities.Modules.Syntek
{
    public enum SpeedMode : ushort
    {
        Manual = 0,
        Auto = 1
    }

    public enum StopType
    {
        SlowDown,
        Emergency,
        CmdWait
    }

    public enum CmdStatus : ushort
    {
        OFF = 0,
        ON = 1
    }
}