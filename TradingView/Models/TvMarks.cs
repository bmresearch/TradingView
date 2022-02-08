﻿using System;
using System.Text.Json.Serialization;

namespace BlockMountain.TradingView.Models
{
    /// <summary>
    /// Represents a mark.
    /// </summary>
    public class TvMarks
    {
        /// <summary>
        /// Mark unique id
        /// </summary>
        public int[] Id { get; set; }

        /// <summary>
        /// When mark occured
        /// </summary>
        [JsonPropertyName("time")]
        public int[] Timestamp { get; set; }

        /// <summary>
        /// Short always visible text
        /// </summary>
        public string[] Label { get; set; }

        /// <summary>
        /// Font color of the label
        /// </summary>
        public string[] LabelFontColor { get; set; }

        /// <summary>
        /// HTML color (name or hex)
        /// </summary>
        public string[] Color { get; set; }

        /// <summary>
        /// Long text visible on mouse hover (tooltip)
        /// </summary>
        public string[] Text { get; set; }

        /// <summary>
        /// Minimal size of the mark
        /// </summary>
        public int[] MinSize { get; set; } = { 1 };
    }
}
