using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SteamKit2;
using SteamTrade.Model;
using SteamTrade.Model.csgo;

namespace SteamTrade.Lottery
{
    public class Round<T> where T : ITradable
    {

        #region Properties

        public int Id { get; set; }
        public Dictionary<SteamID, List<T>> ItemsPerPlayer { get; set; }
        public int Timelimit { get; set; }
        public DateTime StartTime { get; set; }
        public int ItemLimit { get; set; }
        public int BetMinimum { get; set; }
        public SteamWeb SteamWeb { get; set; }
        public SteamID Winner { get; set; }
        public T House { get; set; }
        public List<T> Pot { get; set; }

        //TODO: Move this into it's own function?
        public bool IsCurrent { get; set; }
        //{
        //    get { return true; //Gotta sort this out 
        //    }
        //    set
        //    {

        //        //ERROR CHECK select count(roundId) from round where roundActive = 'true'; MUST = 1 or we're boned

        //        //Get round
        //        //var int roundId = ###SQL ---select count(roundId) from round where roundActive = 'true';
        //        //Get Winner

        //        //GetPotValue
        //        //??
        //        //GetpotJson
        //        //??
        //        //GetSkim
        //        //??
        //        var endTime = DateTime.Now;
        //        IsCurrent = false;
        //        var skim = "{item}";
        //        // update round set skimItemsJson = '{skim}', winnerSteamId = #winnnerSteamId, winnerSteamTradeId = 1111110, timeEnded = NOW() where roundId = #roundId;

        //        //Send Winner Winnings
        //        //Get winnerSteaemTradeId


        //        //insert into round (timeStarted) values (Now());

        //    }
        //}

        #endregion

		public int ItemsInRound {
			get {
				var itemsInRound = 0;
				foreach (var player in ItemsPerPlayer) {
					itemsInRound += player.Value.Count;

				}
				return itemsInRound;
			}
		}

        protected float PotValue
        {
            get
            {
                return GetItemsValue(Pot);
            }
        }

        public Round(int timelimit, int itemLimit, int betMinimum, SteamWeb steamWeb)
        {
            Timelimit = timelimit;
            ItemLimit = itemLimit;
            BetMinimum = betMinimum;
            SteamWeb = steamWeb;
            IsCurrent = true;
            ItemsPerPlayer = new Dictionary<SteamID, List<T>>();
            Pot = new List<T>();
            StartRound();

        }

        public void StartRound()
        {
            StartTime = DateTime.Now;
        }

        public float GetOdds(SteamID player)
        {
            var itemsFromPlayer = ItemsPerPlayer[player];

            var valueForPlayer = GetItemsValue(itemsFromPlayer);

            return valueForPlayer / PotValue;
        }

        public float GetItemsValue(List<T> items)
        {
            var total = 0.0;
            foreach (var item in items)
            {
                var response = SteamWeb.Fetch(
                    string.Format(
                        "http://steamcommunity.com/market/priceoverview/?country=US&currency=3&appid={0}&market_hash_name={1}",
                        730, item.MarketHashName),"get");

                //TODO: Not generic at all.. must find a better way
                var obj = JsonConvert.DeserializeObject<ItemPrice>(response);
                total += obj.MedianPrice;
            }

            return (float) total;
        }

        public SteamID GetWinner()
        {
            var random = new Random();
            var winner = random.Next(0, 100);

            var oddPerPlayer = new Dictionary<SteamID, float>();

            foreach (var player in ItemsPerPlayer.Keys)
            {
                oddPerPlayer.Add(player, GetOdds(player));
            }

            var rangePerPlayer = new Dictionary<SteamID, double[]>();
            var bottomCounter = 0.0;

            foreach (var player in oddPerPlayer)
            {

                rangePerPlayer.Add(player.Key, new[] { bottomCounter, bottomCounter+player.Value });
                bottomCounter += player.Value;
            }

            foreach (var player in rangePerPlayer)
            {
                if (player.Value[0] < winner && player.Value[1] < winner)
                {
                    return player.Key;
                }
            }

            return null;


        }


    }
}
