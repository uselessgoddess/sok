namespace Identity.Core.Exceptions;

public class NotFoundException(string? message = default) : Exception(message);