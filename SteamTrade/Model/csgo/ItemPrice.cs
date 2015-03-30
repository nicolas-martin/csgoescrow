using Newtonsoft.Json;

namespace SteamTrade.Model.csgo
{
    public class ItemPrice
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("lowest_price")]
        public float LowestPrice { get; set; }

        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("median_price")]
        public float MedianPrice { get; set; }
    }
}
