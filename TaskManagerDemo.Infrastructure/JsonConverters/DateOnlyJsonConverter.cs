using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskManagerDemo.Infrastructure.JsonConverters;

public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateTime = DateTime.Parse(reader.GetString()!).Date;
        return DateOnly.FromDateTime(dateTime);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        var date = value.ToString("O");
        writer.WriteStringValue(date);
    }
}