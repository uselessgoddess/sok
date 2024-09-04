namespace Identity.Core.Exceptions;

public class AlreadyExistsException(string? message = default) : Exception(message);