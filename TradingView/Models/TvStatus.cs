using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TradingView.Converters;

namespace BlockMountain.TradingView.Models
{
    /// <summary>
    /// Represents the status of a response.
    /// </summary>
    [JsonConverter(typeof(TvStatusJsonConverter))]
    public enum TvStatus
    {
        Error = 0,
        Ok = 1,
        NoData = 2
    }
}
