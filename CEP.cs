using MyExtensions;
using System.Runtime.CompilerServices;

namespace MyClasses;

/// <summary>
/// Estrutura de CEP - Deve ser criado com conversores implicitos
/// </summary>
public readonly record struct CEP
{
    public required uint Codigo { get; init; }

    public CEP(uint cep)
    {
        if (cep > 99999999)
            throw new ArgumentOutOfRangeException(nameof(cep), "CEP must be less than or equal to 8 digits.");

        Codigo = cep;
    }

    public CEP(string cep)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(cep), "CEP cannot be null or empty.");
        
        var _cep = cep.GetOnlyDigits();
        
        if (_cep.Length > 8)
            throw new ArgumentOutOfRangeException(nameof(cep), "CEP must be less than or equal to 8 digits.");
        
        if (uint.TryParse(_cep, out uint result))
        {
            Codigo = result;
        }
        else
        {
            throw new FormatException("Invalid format for CEP.");
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly string ToStringFormatted()
    {
        return Codigo.ToString("00000-000");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly string ToString()
    {
        return Codigo.ToString("00000000");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator uint(CEP cep)
    {
        return cep.Codigo;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator CEP(uint cep)
    {
        if (cep > 99999999)
            throw new ArgumentOutOfRangeException(nameof(cep), "CEP must be less than or equal to 8 digits.");
        
        return new CEP { Codigo = cep };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(CEP cep)
    {
        return cep.ToString();
    }

    public static implicit operator CEP(string cep)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(cep), "CEP cannot be null or empty.");

        var _cep = cep.GetOnlyDigits();

        if (_cep.Length > 8)
            throw new ArgumentOutOfRangeException(nameof(cep), "CEP must be less than or equal to 8 digits.");


        if (uint.TryParse(_cep, out uint result))
        {
            return new CEP { Codigo = result };
        }

        throw new FormatException("Invalid format for CEP.");
    }
}
