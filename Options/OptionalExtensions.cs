using System.Runtime.CompilerServices;

namespace MyClasses.Options;

    public static class OptionalExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Optional<T> Ok<T>(T value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Optional<T> ToOptional<T>(this Exception ex)
            => Optional<T>.FromError(ex);
    }