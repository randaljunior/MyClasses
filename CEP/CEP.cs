using MyExtensions;

namespace MyClasses;

public readonly record struct CEP
{
    public required uint Codigo { get; init; }

    public readonly string ToStrinFormated()
    {
        return Codigo.ToString("00000-000");
    }

    public override readonly string ToString()
    {
        return Codigo.ToString("00000000");
    }

    public static implicit operator uint(CEP cep)
    {
        return cep.Codigo;
    }

    public static implicit operator CEP(uint cep)
    {
        if (cep > 99999999)
            throw new ArgumentOutOfRangeException(nameof(cep), "CEP must be less than or equal to 8 digits.");
        
        return new CEP { Codigo = cep };
    }

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
