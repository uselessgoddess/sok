namespace Identity.Core.Exceptions;

public class BadRequestException(string? message = default) : Exception(message);