using System.Runtime.CompilerServices;

namespace MyClasses.Options;

public interface IError
{
    string? Message { get; init; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    string? ToString()
    {
        return Message;
    }
}
