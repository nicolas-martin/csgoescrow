using System;
using System.Collections.Generic;
using SteamKit2;

namespace SteamTrade.Lottery
{
    public class Round<T>
    {
        public int Id { get; set; }
        public Dictionary<SteamID, List<T>> ItemsPerPlayer { get; set; }
        public int Timelimit { get; set; }
        public DateTime StartTime { get; set; }
        public int ItemLimit { get; set; }
        public int BetMinimum { get; set; }
        public SteamID Winner { get; set; }
        public T House { get; set; }
        public List<T> Pot { get; set; }

        public bool IsCurrent { get; set; }

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

        public Round(int timelimit, int itemLimit, int betMinimum)
        {
            Timelimit = timelimit;
            ItemLimit = itemLimit;
            BetMinimum = betMinimum;
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
            var total = 0;
            foreach (var item in items)
            {
                //TODO: Get item value
                
                
            }

            return total;
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
