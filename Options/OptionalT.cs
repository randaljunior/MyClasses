using System.Runtime.CompilerServices;

namespace MyClasses.Options;

public record Optional<T>
{
    public IError? Error { get; init; }

    public bool IsError => Error is not null;

    public T? Value { get; init; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Optional(T value)
    {
        Value = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Optional(IError error)
    {
        Error = error;
        Value = default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Optional<T>(T value)
    {
        return new Optional<T>(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T?(Optional<T> value)
    {
        return value.Value;
    }
}
