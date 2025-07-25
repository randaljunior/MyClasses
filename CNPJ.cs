using MyClasses.CPF_CNPJ;
using MyExtensions;
using System.Runtime.CompilerServices;

namespace MyClasses;

public readonly record struct CNPJ : IDocumento
{
    private const uint _size = 14;

    private readonly ulong _numero;

    /// <summary>
    /// Número do CNPJ.
    /// </summary>
    public ulong Numero
    {
        readonly get => _numero;
        init
        {
            if (!ValidaCNPJ.IsCNPJ(value))
            {
                throw new ArgumentException("CNPJ inválido");
            }

            _numero = value;
        }
    }

    /// <summary>
    /// Construtor que recebe um ulong com o CNPJ.
    /// </summary>
    /// <param name="numero"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CNPJ(ulong numero)
    {
        Numero = numero;
    }

    /// <summary>
    /// Construtor que recebe uma string com o CNPJ.
    /// </summary>
    /// <param name="numero"></param>
    /// <exception cref="ArgumentException"></exception>
    public CNPJ(string numero)
    {
        if (!ValidaCNPJ.IsCnpjStringRegex(numero) && !ValidaCNPJ.IsCnpjRegex(numero))
        {
            throw new ArgumentException("CNPJ inválido");
        }

        var _numeroLimpo = numero.GetDigits();

        if (!ValidaCNPJ.IsCNPJ(_numeroLimpo))
        {
            throw new ArgumentException("CNPJ inválido");
        }

        _numero = _numeroLimpo.ToUlong() ?? 0;
    }

    /// <summary>
    /// Converte o CNPJ para uma string com o tamanho padrão (14 dígitos).
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly string ToString()
    {
        return _numero.ToString('0'.Repeat((int)_size));
    }

    /// <summary>
    /// Converte o CNPJ para uma string com o tamanho especificado.
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly string ToString(int size)
    {
        return _numero.ToString('0'.Repeat(size));
    }

    /// <summary>
    /// Converte o CNPJ para uma string formatada com pontuação.
    /// </summary>
    /// <returns></returns>
    public readonly string ToStringFormated()
    {
        var digitos = _numero.GetDigits((int)_size);
        Span<char> span = stackalloc char[(int)_size + 3];

        int digitIndex = 0;
        for (int i = 0; i < span.Length; i++)
        {
            span[i] = i switch
            {
                2 or 6 => '.',
                10 => '/',
                15 => '-',
                _ => (char)(digitos[digitIndex++] + '0')
            };
        }

        return span.ToString();
    }

    public static implicit operator CNPJ(ulong numero) => new CNPJ(numero);

    public static implicit operator CNPJ(string numero) => new CNPJ(numero);

    public static implicit operator ulong(CNPJ cnpj) => cnpj._numero;

    public static implicit operator string(CNPJ cnpj) => cnpj.ToString();
}