namespace Identity.Core.Exceptions;

public class AlreadyExistsException(string? message = "") : Exception(message);