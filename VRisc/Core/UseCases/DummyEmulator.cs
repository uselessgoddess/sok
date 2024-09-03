using VRisc.Core.Interfaces;

namespace VRisc.Core.UseCases;

public class DummyEmulator(byte[] src) : EmulatorBase(src)
{
}