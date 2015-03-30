using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SteamTrade.Inventories
{
    public class CsgoInventory
    {
        public List<Item> Items { get; set; }
        public bool Success { get; private set; }
        public bool More { get; private set; }
        public bool MoreStart { get; private set; }


        /// <summary>
        /// Fetches the inventory for the given Steam ID using the Steam API.
        /// </summary>
        /// <returns>The give users inventory.</returns>
        /// <param name='steamId'>Steam identifier.</param>
        /// <param name='apiKey'>The needed Steam API key.</param>
        public static CsgoInventory FetchInventory(ulong steamId, string apiKey, SteamWeb steamWeb)
        {
            //var url = "http://api.steampowered.com/IEconItems_730/GetPlayerItems/v0001/?key=" + apiKey + "&steamid=" + steamId;
            var url = string.Format("http://steamcommunity.com/profiles/{0}/inventory/json/{1}/{2}", steamId, 730, 2);
            var response = steamWeb.Fetch(url, "GET", null, false);

            dynamic obj = JsonConvert.DeserializeObject<dynamic>(response);
            bool success = obj["success"];
            bool more = obj["more"];
            bool moreStart = obj["more_start"];
            var items = new List<Item>();
            foreach (var variable in obj["rgDescriptions"].Children())
            {
                string str = variable.Value.ToString();
                var desc = JsonConvert.DeserializeObject<Item>(str);
                items.Add(desc);
            }


            //var result = JsonConvert.DeserializeObject<InventoryResponse>(response);
            return new CsgoInventory(items, success, more, moreStart);
        }


        protected CsgoInventory(List<Item> items, bool success, bool more, bool moreStart)
        {
            Items = items;
            Success = success;
            More = more;
            MoreStart = moreStart;
        }

        public Item GetItem(string id)
        {
            return (Items == null ? null : Items.FirstOrDefault(item => item.ClassId == id));

        }

        public class Item
        {
            public int AppId = 730;
            public long ContextId = 2;

            //[JsonProperty("appid")]
            //public string Appid { get; set; }

            [JsonProperty("classid")]
            public string ClassId { get; set; }

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

            [JsonProperty("market_hash_name")]
            public string MarketHashName { get; set; }

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
        }

    }
}
