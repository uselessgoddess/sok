namespace Identity.Core;

public class BadRequestException : Exception
{
    public BadRequestException(string? message = default) : base(message)
    {
    }
}

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string? message = default) : base(message)
    {
    }
}

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string? message = default) : base(message)
    {
    }
}

public class NotFoundException : Exception
{
    public NotFoundException(string? message = default) : base(message)
    {
    }
}