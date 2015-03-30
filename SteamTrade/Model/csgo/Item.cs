using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SteamTrade.Model.csgo
{
    public class Item : ITradable
    {
        public int AppId = 730;
        public long ContextId = 2;

        //[JsonProperty("appid")]
        //public string Appid { get; set; }

        [JsonProperty("classid")]
        public ulong ClassId { get; set; }

        [JsonProperty("instanceid")]
        public string InstanceId { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("icon_url_large")]
        public string IconUrlLarge { get; set; }

        [JsonProperty("icon_drag_url")]
        public string IconDragUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("market_name")]
        public string MarketName { get; set; }

        [JsonProperty("name_color")]
        public string NameColor { get; set; }

        [JsonProperty("background_color")]
        public string BackgroundColor { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tradable")]
        public int Tradable { get; set; }

        [JsonProperty("marketable")]
        public int Marketable { get; set; }

        [JsonProperty("commodity")]
        public int Commodity { get; set; }

        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; }
    }
}
