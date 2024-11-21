using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JobOffersApi.Infrastructure.Converters;

internal sealed class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter()
        : base(
            v => v.ToDateTime(TimeOnly.MinValue),
            v => DateOnly.FromDateTime(v))
    {
    }
}

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string DateFormat = "dd-MM-yyyy";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, DateFormat);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}