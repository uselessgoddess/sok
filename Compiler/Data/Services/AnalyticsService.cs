using GrpcServices;

namespace Compiler.Data.Services;

public class AnalyticsService
{
    private uint _success;
    private uint _failed;
    private uint _codeLenght;
    private double _compilationTime;

    public void Success(uint lenght, double time)
    {
        _success++;
        _codeLenght += lenght;
        _compilationTime += time;
    }

    public void Failed(uint lenght)
    {
        _failed++;
    }

    public Metrics GetMetrics()
    {
        var averageTime = _success != 0 ? _compilationTime / _success : 0;
        var lenght = _success != 0 ? _codeLenght / _success : 0;
        return new Metrics
        {
            Success = _success,
            Failed = _failed,
            Lenght = lenght,
            Time = averageTime,
        };
    }

    public void ResetMetrics()
    {
        _success = 0;
        _failed = 0;
        _codeLenght = 0;
        _compilationTime = 0.0;
    }
}