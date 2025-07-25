using MyExtensions;
using System.Runtime.CompilerServices;

namespace MyClasses;

public readonly record struct Telefone
{
    public uint? DDD
    {
        get;
        init
        {
            if (value is null)
            {
                field = null;
                return;
            }

            ArgumentOutOfRangeException.ThrowIfGreaterThan<uint>(value.Value, 99);

            field = value;
        }
    }

    public ulong? Numero
    {
        get;
        init
        {
            if (value is null)
            {
                field = null;
                return;
            }

            ArgumentOutOfRangeException.ThrowIfGreaterThan<ulong>(value.Value, 9_9999_9999);

            if (value / 1_0000_0000 != 9 && value / 1_0000_0000 != 0)
            {
                throw new ArgumentException(nameof(value), "O numero de teleone não é válido.");
            }

            field = value;
        }
    }

    public ulong NumeroCompleto
    {
        get
        {
            if (Numero > 9999_9999)
            {
                return (DDD ?? 0)  * 10_0000_0000 + (Numero ?? 0);
            }
            else
            {
                return (DDD ?? 0) * 1_0000_0000 + (Numero ?? 0);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"{DDD:00}{Numero:00000000}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToStringFormated()
    {
        return $"({DDD:00}) {Numero:#0000-0000}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return NumeroCompleto.ToString(format, formatProvider);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ulong(Telefone telefone)
    {
        return telefone.NumeroCompleto;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(Telefone telefone)
    {
        return telefone.ToString();
    }

    public static implicit operator Telefone(ulong telefone)
    {
        var _tel = telefone.GetDigits().TrimStart<uint>(0);

        if (_tel.Length == 10)
            return new Telefone
            {
                DDD = _tel.Slice(0, 2).ToUint() ?? 0,
                Numero = _tel.Slice(2, 8).ToUlong() ?? 0
            };

        if (_tel.Length == 11 && _tel[_tel.Length - 9] == '9')
            return new Telefone
            {
                DDD = _tel.Slice(0, 2).ToUint() ?? 0,
                Numero = _tel.Slice(2, 9).ToUlong() ?? 0
            };

        throw new ArgumentOutOfRangeException(nameof(telefone), "Telefone com DDD inválido.");
    }

    public static implicit operator Telefone(string telefone)
    {
        var _tel = telefone.GetDigits().TrimStart<uint>(0);

        if (_tel.Length == 10)
            return new Telefone
            {
                DDD = _tel.Slice(0, 2).ToUint() ?? 0,
                Numero = _tel.Slice(2, 8).ToUint() ?? 0
            };

        if (_tel.Length == 11 && _tel[_tel.Length - 9] == 9)
            return new Telefone
            {
                DDD = _tel.Slice(0, 2).ToUint() ?? 0,
                Numero = _tel.Slice(2, 9).ToUint() ?? 0
            };

        throw new ArgumentOutOfRangeException(nameof(telefone), "Telefone com DDD inválido.");
    }
}
