using BlockMountain.TradingView.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradingView.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class TvStatusJsonConverter : JsonConverter<TvStatus>
    {
        /// <inheritdoc cref="Read(ref Utf8JsonReader, Type, JsonSerializerOptions)"/>
        public override TvStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();

            return s switch
            {
                "ok" => TvStatus.Ok,
                "no_data" => TvStatus.NoData,
                _ => TvStatus.Error
            };
        }

        /// <inheritdoc cref="Write(Utf8JsonWriter, TvStatus, JsonSerializerOptions)"/>
        public override void Write(Utf8JsonWriter writer, TvStatus value, JsonSerializerOptions options) => throw new NotImplementedException();
    }
}
