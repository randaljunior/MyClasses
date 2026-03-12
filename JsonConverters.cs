using MyExtensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyClasses;

public sealed class CpfOuCnpjJsonConverter : JsonConverter<CPFouCNPJ>
{
    public override CPFouCNPJ Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var _str = reader.GetString()?.Trim();
        return (CPFouCNPJ)(_str ?? String.Empty);
    }

    public override void Write(Utf8JsonWriter writer, CPFouCNPJ value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public sealed class EmailJsonConverter : JsonConverter<Email>
{
    public override Email Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var _str = reader.GetString()?.Trim();
        return (Email)(_str ?? String.Empty);
    }

    public override void Write(Utf8JsonWriter writer, Email value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public sealed class CEPJsonConverter : JsonConverter<CEP>
{
    public override CEP Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var _str = reader.GetString()?.Trim();
        return (CEP)(_str ?? String.Empty);
    }

    public override void Write(Utf8JsonWriter writer, CEP value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public sealed class NumeroTelefoneJsonConverter : JsonConverter<ulong?>
{
    public override ulong? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {

        var _tkt = reader.TokenType;

        if (_tkt == JsonTokenType.String)
        {
            var _str = reader.GetString();
            _ = ulong.TryParse(_str?.Trim().Right(9) ?? string.Empty, out var result);
            return result;
        }
        else if (_tkt == JsonTokenType.Number)
        {
            return (uint?)reader.GetUInt32();
        }

        throw new ArgumentException("Telefone não pode ser lido.");
    }

    public override void Write(Utf8JsonWriter writer, ulong? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString("D9").Right(9));
    }
}

public sealed class DDDJsonConverter : JsonConverter<uint?>
{
    public override uint? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var _str = reader.GetString();
        _ = uint.TryParse(_str?.Trim().Right(2) ?? string.Empty, out var result);
        return result;
    }

    public override void Write(Utf8JsonWriter writer, uint? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString("D2").Right(2));
    }
}

public sealed class TelefoneJsonConverter : JsonConverter<Telefone?>
{
    public override Telefone? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {

        var _tkt = reader.TokenType;

        if (_tkt == JsonTokenType.String)
        {
            var _str = reader.GetString();
            _ = ulong.TryParse(_str?.Trim() ?? string.Empty, out var result);
            return (Telefone)result;
        }
        else if (_tkt == JsonTokenType.Number)
        {
            return (Telefone)reader.GetUInt64();
        }

        throw new ArgumentException("Telefone não pode ser lido.");
    }

    public override void Write(Utf8JsonWriter writer, Telefone? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}