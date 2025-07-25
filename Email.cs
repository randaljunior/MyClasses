using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace MyClasses;

public partial record Email
{
    /// <summary>
    /// Key = email, Value = displayName
    /// </summary>
    private readonly Dictionary<string, string> _addresses = [];
    
    private readonly List<string> _errors = [];

    public Email() { }

    public bool IsEmpty => _addresses.Count == 0;
    
    public bool HasErrors => _errors.Count > 0;

    /// <summary>
    /// Key = email, Value = displayName
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ImmutableDictionary<string, string> GetAddresses() => _addresses.ToImmutableDictionary();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ImmutableList<string> GetErrors() => _errors.ToImmutableList();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clean() => _addresses.Clear();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CleanErrors() => _errors.Clear();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetFirst() => (IsEmpty) ? String.Empty : _addresses.First().Key;

    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex EmailStringRegex();

    [GeneratedRegex(@"^(?<nome>.*?)\s*<(?<email>.+?)>")]
    private static partial Regex NomeEEnderecoStringRegex();

    public bool TryAdd(string address, string displayName = "")
    {
        if (string.IsNullOrWhiteSpace(address))
            return false;

        if (address.Contains('<'))
        {
            var _regexMatch = NomeEEnderecoStringRegex().Match(address);

            if (!_regexMatch.Success || !_regexMatch.Groups["email"].Success)
                return false;

            var _email = _regexMatch.Groups["email"].Value.Trim();

            if (!EmailStringRegex().IsMatch(_email))
                return false;

            if (_addresses.ContainsKey(_email))
                return false;

            if (string.IsNullOrWhiteSpace(displayName) && _regexMatch.Groups["nome"].Success)
                displayName = _regexMatch.Groups["nome"].Value.Trim();

            return _addresses.TryAdd(_email, displayName);
        }
        else
        {
            if (!EmailStringRegex().IsMatch(address))
                return false;

            return _addresses.TryAdd(address, displayName);
        }
    }

    public bool TryRemove(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            return false;

        if (!_addresses.ContainsKey(address))
            return false;

        return _addresses.Remove(address);
    }

    public bool Contains(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            return false;

        return _addresses.ContainsKey(address);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return ToString(','); // Default delimiter is semicolon
    }

    public virtual string ToString(char delimitador)
    {
        if (IsEmpty)
            return string.Empty;

        var sb = new StringBuilder();
        bool first = true;

        foreach (var address in _addresses)
        {
            // Adiciona o delimitador somente a partir do segundo elemento
            if (!first)
            {
                sb.Append(delimitador);
            }
            else
            {
                first = false;
            }

            if (string.IsNullOrWhiteSpace(address.Value))
                sb.Append(address.Key);
            else
                sb.Append($"{address.Value} <{address.Key}>");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Cria uma Classe de Email a partir de uma string com endereços de email separados por vírgula ou ponto e vírgula.
    /// </summary>
    /// <param name="emails"></param>
    public Email(ReadOnlySpan<char> emails)
    {
        Span<char> _delimitadores = stackalloc char[] { ',', ';' };

        while (!emails.IsEmpty)
        {
            int _index = emails.IndexOfAny(_delimitadores); // Localiza o próximo delimitador

            if (_index == -1) // Se nenhum delimitador for encontrado
            {
                var token1 = emails.ToString();
                if (!TryAdd(token1)) _errors.Add(token1);
                break;
            }

            ReadOnlySpan<char> _parte = emails.Slice(0, _index); // Extrai a parte antes do delimitador

            var token2 = _parte.ToString();
            if (!TryAdd(token2))
                _errors.Add(token2); // Mostra a parte extraída

            emails = emails.Slice(_index + 1); // Move para o próximo segmento
        }
    }
    
    public static implicit operator string(Email email) => email.ToString();
    
    public static implicit operator Email(ReadOnlySpan<char> emails)
    {
        var e = new Email(emails);
        return e;
    }

    public static implicit operator Email(string emails)
    {
        var e = new Email(emails);
        return e;
    }
}

