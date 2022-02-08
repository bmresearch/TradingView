using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingView.Models
{
    /// <summary>
    /// Represents a timescale mark.
    /// </summary>
    public class TvTimescaleMark
    {
        /// <summary>
        /// The timescale mark identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The time of the mark.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// The timescale mark color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// The timescale mark label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The timescale mark tooltip.
        /// </summary>
        public string[]? Tooltip { get; set; }
    }
}
