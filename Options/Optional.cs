using System.Runtime.CompilerServices;

namespace MyClasses.Options;

public record Optional : Optional<Optional>
{
    private Optional(IError value) : base(value)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Optional FromError(IError error) 
    {
        return new Optional(error);
    }
}
