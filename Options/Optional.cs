namespace MyClasses.Options;

public record Optional : Optional<Optional>
{
    private Optional(IError value) : base(value)
    {
    }

    public static Optional FromError(IError error) 
    {
        return new Optional(error);
    }
}
