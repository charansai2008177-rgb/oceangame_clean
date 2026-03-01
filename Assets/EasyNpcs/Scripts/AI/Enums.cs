using System;

namespace AIPackage
{
    public enum NpcState
    {
        Default,
        GoingToWork,
        Working,
        GoingHome,
        AtHome,
        Talking,
        Scared,
        Patrol,
        Chase,
        Attack,
    }

    [Flags]
    public enum Job : uint
    {
        None = 0,
        Default = 1 << 0,
        Farmer = 1 << 1,
        FisherMan = 1 << 2,
        Lumberjack = 1 << 3,
        Merchant = 1 << 4,
        Guard = 1 << 5,
        Rich = 1 << 6,
        InnKeeper = 1 << 7,
        Servant = 1 << 8
    }

    [Flags]
    public enum Gender : uint
    {
        None = 0,
        Male = 1 << 0,
        Female = 1 << 1,
        Default = 1 << 2
    }
}