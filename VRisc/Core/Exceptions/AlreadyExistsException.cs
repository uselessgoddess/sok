﻿namespace VRisc.Core.Exceptions;

public class AlreadyExistsException(string message = "") : Exception(message);