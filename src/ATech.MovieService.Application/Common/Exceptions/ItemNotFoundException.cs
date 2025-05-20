using System;

namespace ATech.MovieService.Application.Common.Exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string? message) : base(message)
    {
    }
    public ItemNotFoundException()
    {
    }

    public ItemNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

}