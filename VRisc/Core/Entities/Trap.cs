namespace VRisc.Core.Entities;

public enum Trap : uint
{
    Contained,
    Requested,
    Invisible,
    Fatal,
}