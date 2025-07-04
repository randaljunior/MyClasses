namespace MyClasses.Options;

public record Optional<T>
{
    public IError? Error { get; init; }

    public bool IsError => Error is not null;

    public T? Value { get; init; }

    public Optional(T value)
    {
        Value = value;
    }

    public Optional(IError error)
    {
        Error = error;
        Value = default;
    }

    public static Optional<T> FromError<TError>(TError value) where TError : IError
    {
        return new Optional<T>(value);
    }

    public override string ToString()
    {
        if (Value is T)
            return Value.ToString() ?? string.Empty;

        if (Value is IError)
            return Value.ToString() ?? string.Empty;

        return string.Empty;
    }

    public static implicit operator Optional<T>(Optional value)
    {
        if (value.IsError)
        {
            return new Optional<T>(value.Error!);
        }
        else
        {
            return new Optional<T>(value.Value!);
        }
    }

    public static implicit operator Optional<T>(T value)
    {
        return new Optional<T>(value);
    }

    public static implicit operator T?(Optional<T> value)
    {
        return value.Value;
    }
}
