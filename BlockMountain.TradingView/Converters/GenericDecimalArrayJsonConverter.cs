using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlockMountain.TradingView.Converters
{
    /// <summary>
    /// Implements a generic <see cref="JsonConverter{T}"/> to <see cref="decimal[]"/>, 
    /// using the <see cref="NumberStyles.Float"/> and <see cref="NumberFormatInfo.InvariantInfo"/> conventions.
    /// </summary>
    public class GenericDecimalArrayJsonConverter : JsonConverter<decimal[]>
    {
        /// <inheritdoc cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        public override decimal[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var successfulStartArrayTokenRead = reader.Read();

                if (!successfulStartArrayTokenRead)
                    throw new JsonException();

                List<decimal> list = new();

                while (reader.TokenType != JsonTokenType.EndArray)
                {
                    bool successful = true;
                    if (reader.TokenType == JsonTokenType.String)
                    {
                        var s = reader.GetString();
                        successful = decimal.TryParse(s, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out decimal result);
                        list.Add(result);
                    } else if (reader.TokenType == JsonTokenType.Number)
                    {
                        list.Add(reader.GetDecimal());
                    }
                    if (successful)
                    {
                        var successfulTokenRead = reader.Read();
                        if (!successfulTokenRead)
                            throw new JsonException();
                        continue;
                    }
                    throw new JsonException();
                }
                return list.ToArray();
            }
            throw new JsonException();
        }

        /// <inheritdoc cref="Write(Utf8JsonWriter, decimal[], JsonSerializerOptions)"/>
        public override void Write(Utf8JsonWriter writer, decimal[] value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
