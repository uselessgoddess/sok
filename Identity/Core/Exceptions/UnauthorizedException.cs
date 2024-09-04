namespace Identity.Core.Exceptions;

public class UnauthorizedException(string? message = default) : Exception(message);