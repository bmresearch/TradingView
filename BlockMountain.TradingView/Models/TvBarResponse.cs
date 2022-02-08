using BlockMountain.TradingView.Converters;
using System;
using System.Text.Json.Serialization;
using TradingView.Converters;

namespace BlockMountain.TradingView.Models
{
    /// <summary>
    /// Represents a bar response with OHLCV data.
    /// </summary>
    public class TvBarResponse
    {
        /// <summary>
        /// Status code. 
        /// Expected values: <see cref="TvStatus.Ok"/> | <see cref="TvStatus.Error"/> | <see cref="TvStatus.NoData"/>.
        /// </summary>
        /// 
        [JsonPropertyName("s")]
        [JsonConverter(typeof(TvStatusJsonConverter))]
        public TvStatus Status { get; set; }

        /// <summary>
        /// Error message. Should be present only when status is <see cref="TvStatus.Error"/>.
        /// </summary>
        [JsonPropertyName("errmsg")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The array of data regarding the open of the bars.
        /// </summary>
        [JsonPropertyName("o")]
        [JsonConverter(typeof(GenericDecimalArrayJsonConverter))]
        public decimal[] Open { get; set; }

        /// <summary>
        /// The array of data regarding the close of the bars.
        /// </summary>
        [JsonPropertyName("c")]
        [JsonConverter(typeof(GenericDecimalArrayJsonConverter))]
        public decimal[] Close { get; set; }

        /// <summary>
        /// The array of data regarding the high of the bars.
        /// </summary>
        [JsonPropertyName("h")]
        [JsonConverter(typeof(GenericDecimalArrayJsonConverter))]
        public decimal[] High { get; set; }

        /// <summary>
        /// The array of data regarding the low of the bars.
        /// </summary>
        [JsonPropertyName("l")]
        [JsonConverter(typeof(GenericDecimalArrayJsonConverter))]
        public decimal[] Low { get; set; }

        /// <summary>
        /// The array of data regarding the volume of the bars.
        /// </summary>
        [JsonPropertyName("v")]
        [JsonConverter(typeof(GenericDecimalArrayJsonConverter))]
        public decimal[] Volume { get; set; }

        /// <summary>
        /// The array of data regarding the timestamp of the bars.
        /// </summary>
        [JsonPropertyName("t")]
        public int[] Timestamp { get; set; }

        /// <summary>
        /// Should be the time of the closest available bar in the past if there is no data (status is <see cref="TvStatus.NoData"/>.) in the requested period.
        /// </summary>
        [JsonPropertyName("nb")]
        public int? NextBar { get; set; }

        /// <summary>
        /// Whether the requested symbol is valid.
        /// </summary>
        public bool? ValidSymbol { get; set; }

        /// <summary>
        /// Whether the requested resolution is valid.
        /// </summary>
        public bool? ValidResolution { get; set; }

        /// <summary>
        /// Whether the requested start time is valid.
        /// </summary>
        public bool? ValidFrom { get; set; }
    }
}
