﻿namespace Identity.Core.Exceptions;

public class NotFoundException(string? message = "") : Exception(message);