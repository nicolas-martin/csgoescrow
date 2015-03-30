using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SteamTrade;

namespace Inventories.Tf2Inventory
{
    public class CSGOInventory
    {
        /// <summary>
        /// Fetches the inventory for the given Steam ID using the Steam API.
        /// </summary>
        /// <returns>The give users inventory.</returns>
        /// <param name='steamId'>Steam identifier.</param>
        /// <param name='apiKey'>The needed Steam API key.</param>
        public static CSGOInventory FetchInventory(ulong steamId, string apiKey, SteamWeb steamWeb)
        {
            var url = "http://api.steampowered.com/IEconItems_730/GetPlayerItems/v0001/?key=" + apiKey + "&steamid=" + steamId;
            var response = steamWeb.Fetch(url, "GET", null, false);
            var result = JsonConvert.DeserializeObject<InventoryResponse>(response);
            return new CSGOInventory(result.Result);
        }

        public List<Item> Items { get; set; }
        public bool IsPrivate { get; private set; }
        public bool IsGood { get; private set; }

        protected CSGOInventory(InventoryResult apiInventory)
        {
            Items = apiInventory.Items;
            IsPrivate = (apiInventory.Status == "15");
            IsGood = (apiInventory.Status == "1");
        }

        public Item GetItem(ulong id)
        {
            return (Items == null ? null : Items.FirstOrDefault(item => item.Id == id));
        }

        public List<Item> GetItemsByDefindex(int defindex)
        {
            return Items.Where(item => item.Defindex == defindex).ToList();
        }

        public class Item
        {
            public int AppId = 730;
            public long ContextId = 2;

            [JsonProperty("id")]
            public ulong Id { get; set; }

            [JsonProperty("original_id")]
            public ulong OriginalId { get; set; }

            [JsonProperty("defindex")]
            public ushort Defindex { get; set; }

            [JsonProperty("level")]
            public byte Level { get; set; }

            [JsonProperty("quality")]
            public string Quality { get; set; }

            [JsonProperty("quantity")]
            public int RemainingUses { get; set; }

            [JsonProperty("origin")]
            public int Origin { get; set; }

            [JsonProperty("custom_name")]
            public string CustomName { get; set; }

            [JsonProperty("custom_desc")]
            public string CustomDescription { get; set; }

            [JsonProperty("flag_cannot_craft")]
            public bool IsNotCraftable { get; set; }

            [JsonProperty("flag_cannot_trade")]
            public bool IsNotTradeable { get; set; }

            [JsonProperty("attributes")]
            public ItemAttribute[] Attributes { get; set; }

            [JsonProperty("contained_item")]
            public Item ContainedItem { get; set; }
        }

        public class ItemAttribute
        {
            [JsonProperty("defindex")]
            public ushort Defindex { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            [JsonProperty("float_value")]
            public float FloatValue { get; set; }

            [JsonProperty("account_info")]
            public AccountInfo AccountInfo { get; set; }
        }

        public class AccountInfo
        {
            [JsonProperty("steamid")]
            public ulong SteamId { get; set; }

            [JsonProperty("personaname")]
            public string PersonaName { get; set; }
        }

        protected class InventoryResult
        {
            public string Status { get; set; }

            public List<Item> Items { get; set; }
        }

        protected class InventoryResponse
        {
            public InventoryResult Result;
        }

    }
}
