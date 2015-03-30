using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SteamTrade.Model.csgo;

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

        public Item GetItem(ulong id)
        {
            return (Items == null ? null : Items.FirstOrDefault(item => item.ClassId == id));

        }

    }
}
