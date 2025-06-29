using MyExtensions;

namespace MyClasses;

public readonly record struct Telefone
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

    public required ulong Numero
    {
        get;
        init
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan<ulong>(value, 9_9999_9999);

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

    public string ToStringFormated()
    {
        //var str = Numero.ToString("00000000").AsSpan();

        //if(str.Length == 8) 
        //    return $"({DDD:00}) {str.Slice(0, 4)}-{str.Slice(4, 4)}";

        //if(str.Length == 9 && str[0] == '9')
        //    return $"({DDD:00}) 9{str.Slice(1, 4)}-{str.Slice(5, 4)}";

        //throw new ArgumentException("Número de Telefone Inválido");

        return $"({DDD:00}) {Numero:#0000-0000}";
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return NumeroCompleto.ToString(format, formatProvider);
    }

    public static implicit operator ulong(Telefone telefone)
    {
        return telefone.NumeroCompleto;
    }

    public static implicit operator int(Telefone telefone)
    {
        return (int)(telefone.NumeroCompleto);
    }

    public static implicit operator string(Telefone telefone)
    {
        return telefone.ToString();
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

        if (_tel.Length == 11 && _tel[_tel.Length - 9] == 9)
            return new Telefone
            {
                DDD = _tel.Slice(0, 2).ToUint() ?? 0,
                Numero = _tel.Slice(2, 9).ToUint() ?? 0
            };

        throw new ArgumentOutOfRangeException(nameof(telefone), "Telefone com DDD inválido.");
    }
}
