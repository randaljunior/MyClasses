using MyExtensions;

namespace MyClasses;

internal class Telefone
{
    public required uint DDD
    {
        get;
        init
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan<uint>(value, 99);

            field = value;
        }
    }

    public required uint Numero
    {
        get;
        init
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan<uint>(value, 9_9999_9999);

            if (value / 1_0000_0000 != 9 || value / 1_0000_0000 != 0)
            {
                throw new ArgumentException(nameof(value), "O numero de teleone não é válido.");
            }

            field = value;
        }
    }

    public uint NumeroCompleto
    {
        get
        {
            if (Numero > 9999_9999)
            {
                return DDD * 10_0000_0000 + Numero;
            }
            else
            {
                return DDD * 1_0000_0000 + Numero;
            }
        }
    }

    public override string ToString()
    {
        return $"{DDD:00}{Numero:00000000}";
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return NumeroCompleto.ToString(format, formatProvider);
    }

    public static implicit operator uint(Telefone telefone)
    {
        return telefone.NumeroCompleto;
    }

    public static implicit operator int(Telefone telefone)
    {
        return (int)(telefone.NumeroCompleto);
    }

    public static implicit operator Telefone(uint telefone)
    {
        var _tel = telefone.GetDigits().TrimStart<uint>(0);

        if (_tel.Length == 10)
            return new Telefone
            {
                DDD = _tel.Slice(0, 2).ToUint() ?? 0,
                Numero = _tel.Slice(2, 8).ToUint() ?? 0
            };

        if ( _tel.Length == 11 && _tel[_tel.Length - 9] == '9')
            return new Telefone
            {
                DDD = _tel.Slice(0, 2).ToUint() ?? 0,
                Numero = _tel.Slice(2, 9).ToUint() ?? 0
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

        if (_tel.Length == 11 && _tel[_tel.Length - 9] == '9')
            return new Telefone
            {
                DDD = _tel.Slice(0, 2).ToUint() ?? 0,
                Numero = _tel.Slice(2, 9).ToUint() ?? 0
            };

        throw new ArgumentOutOfRangeException(nameof(telefone), "Telefone com DDD inválido.");
    }
}
