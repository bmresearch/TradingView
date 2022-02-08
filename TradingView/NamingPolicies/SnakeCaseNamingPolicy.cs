using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TradingView.NamingPolicies
{
    /// <summary>
    /// A snake case <see cref="JsonNamingPolicy"/>.
    /// </summary>
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        /// <inheritdoc cref="JsonNamingPolicy.ConvertName(string)"/>
        public override string ConvertName(string name)
        {
            var select = name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString());

            var cname = string.Concat(select);
            var final = cname.ToLower();
            return final;
        }
    }
}
