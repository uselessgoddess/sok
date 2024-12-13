namespace Identity.Core.Exceptions;

public class UnauthorizedException(string? message = "") : Exception(message);