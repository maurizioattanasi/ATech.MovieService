namespace ATech.MovieService.Application.Common.Exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string? message) : base(message)
    {
    }
}