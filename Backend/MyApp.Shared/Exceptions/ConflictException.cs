﻿namespace MyApp.Shared.Exceptions
{
    public class ConflictException : ApplicationException
    {
        public ConflictException(string message) : base(message) { }
    }
}
