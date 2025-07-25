using MyClasses.CPF_CNPJ;
using MyExtensions;
using System.Runtime.CompilerServices;

namespace MyClasses;

public readonly record struct CPFouCNPJ
{
    private readonly IDocumento _valor;

    //private CPFouCNPJ() { }

    /// <summary>
    /// Construtor que recebe um CPF.
    /// </summary>
    /// <param name="cpf"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CPFouCNPJ(CPF cpf)
    {
        _valor = cpf;
    }

    /// <summary>
    /// Construtor que recebe um CNPJ.
    /// </summary>
    /// <param name="cnpj"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CPFouCNPJ(CNPJ cnpj)
    {
        _valor = cnpj;
    }

    /// <summary>
    /// Número do CPF ou CNPJ.
    /// </summary>
    public readonly ulong Valor => _valor?.Numero ?? 0;

    /// <summary>
    /// Documento associado ao CPFouCNPJ.
    /// </summary>
    public readonly IDocumento Documento => _valor;

    /// <summary>
    /// Retorna verdadeiro se o valor armazeando é um CPF.
    /// </summary>
    public readonly bool IsCPF => _valor is CPF;

    /// <summary>
    /// Retorna verdadeiro se o valor armazeando é um CNPJ.
    /// </summary>
    public readonly bool IsCNPJ => _valor is CNPJ;

    /// <summary>
    /// Retorna o tipo do documento (CPF ou CNPJ).
    /// </summary>
    public readonly Type? TipoDocumento => _valor?.GetType();

    /// <summary>
    /// Cria um CPFouCNPJ a partir de uma numero de CPF ou CNPJ.
    /// </summary>
    /// <param name="numero"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static CPFouCNPJ Create(ulong numero)
    {
        if (ValidaCPF.IsCPF(numero))
        {
            return new CPFouCNPJ(new CPF(numero));
        }
        else if (ValidaCNPJ.IsCNPJ(numero))
        {
            return new CPFouCNPJ(new CNPJ(numero));
        }
        else
        {
            throw new ArgumentException("CPF ou CNPJ inválido", nameof(numero));
        }
    }

    /// <summary>
    /// Cria um CPFouCNPJ a partir de uma numero de CPF ou CNPJ.
    /// </summary>
    /// <param name="numero"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static CPFouCNPJ Create(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("Número não pode ser nulo ou vazio", nameof(numero));

        var digits = numero.GetDigits();

        if (ValidaCPF.IsCPF(digits))
        {
            return new CPFouCNPJ(new CPF(digits.ToUlong() ?? 0));
        }
        else if (ValidaCNPJ.IsCNPJ(digits))
        {
            return new CPFouCNPJ(new CNPJ(digits.ToUlong() ?? 0));
        }
        else
        {
            throw new ArgumentException("CPF ou CNPJ inválido", nameof(numero));
        }
    }

    /// <summary>
    /// Retorna uma string com o CPF ou CNPJ em um tamanho específico.
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly string ToString(int size) => _valor.ToString(size);

    /// <summary>
    /// Retorna uma string com o CPF ou CNPJ em um tamanho padrão (11 ou 14 dígitos).
    /// </summary>
    /// <returns></returns>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly override string ToString() => _valor.ToString();

    /// <summary>
    /// Retorna uma string com o CPF ou CNPJ formatado.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToStringFormated() => _valor?.ToStringFormated() ?? string.Empty;

    public static implicit operator CPFouCNPJ(ulong numero) => CPFouCNPJ.Create(numero);

    public static implicit operator CPFouCNPJ(string numero) => CPFouCNPJ.Create(numero);

    public static implicit operator ulong(CPFouCNPJ cpfouCnpj) => cpfouCnpj.Valor;

    public static implicit operator string(CPFouCNPJ cpfouCnpj) => cpfouCnpj.ToString();

    public static implicit operator CPF(CPFouCNPJ cpfouCnpj)
    {
        if(cpfouCnpj.Documento is CPF cpf)
        {
            return cpf;
        }
        else
        {
            throw new InvalidCastException("O valor armazenado não é um CPF.");
        }
    }

    public static implicit operator CNPJ(CPFouCNPJ cpfouCnpj)
    {
        if (cpfouCnpj.Documento is CNPJ cnpj)
        {
            return cnpj;
        }
        else
        {
            throw new InvalidCastException("O valor armazenado não é um CNPJ.");
        }
    }
}
