using System.Linq;
using SteamKit2;
using SteamTrade;

namespace SteamBot
{
    public class SimpleUserHandler : UserHandler
    {
        public TF2Value AmountAdded;
        

        public SimpleUserHandler (Bot bot, SteamID sid) : base(bot, sid) {}

        public override bool OnGroupAdd()
        {
            return false;
        }

        public override bool OnFriendAdd () 
        {
            return true;
        }

        public override void OnLoginCompleted()
        {
        }

        public override void OnChatRoomMessage(SteamID chatID, SteamID sender, string message)
        {
            Log.Info(Bot.SteamFriends.GetFriendPersonaName(sender) + ": " + message);
            base.OnChatRoomMessage(chatID, sender, message);
        }

        public override void OnFriendRemove () {}
        
        public override void OnMessage (string message, EChatEntryType type)
        {
            //var inventory = OtherInventory;
            //var jsonIventory = JsonConvert.SerializeObject(inventory);
            //var inventory2 = JsonConvert.DeserializeObject<Inventory>(jsonIventory);
            //Log.Info(jsonIventory);
            //Console.WriteLine("test");
            //SendChatMessage(Bot.ChatResponse);

            //var trade = new CreateTra(7704618, 2838957, this.SteamWeb);

            //John 2838957
            //me 7704618
            
            //Bot.tradeManager.InitializeTrade(7704618, 2838957);
            //var trade = Bot.tradeManager.CreateTrade(7704618, 2838957);
            ////"contextid":2,"assetid":165463158,"appid":730,"amount":1
            //trade.AddItem(new GenericInventory.Item(730, 2, 165463158, "wtf"));
            //trade.AcceptTrade();
            var tradeOffer = Bot.NewTradeOffer(76561197963104685);
            //tradeOffer.Items.AddMyItem(730, 2, 165463158);
            string offerId;
            tradeOffer.Send(out offerId, "THIS IS GOING TO WORK DUDE");
        }

        public override bool OnTradeRequest()
        {
            
			//Prevent players from requesting items from the bot.
            if (Bot.Round.IsCurrent)
            {
                //Trade.AcceptTrade();
                return true;

            }
            else
            {
                //Trade.CancelTrade();
                return false;
            }
			           
            
            //return true;

        }
        
        public override void OnTradeError (string error) 
        {
            SendChatMessage("Oh, there was an error: {0}.", error);
            Log.Warn (error);
        }
        
        public override void OnTradeTimeout () 
        {
            SendChatMessage("Sorry, but you were AFK and the trade was canceled.");
            Log.Info ("User was kicked because he was AFK.");
        }
        
        public override void OnTradeInit() 
        {
            SendTradeMessage("Success. Please put up your items.");
        }
        
        public override void OnTradeAddItem (Schema.Item schemaItem, Inventory.Item inventoryItem) {}
        
        public override void OnTradeRemoveItem (Schema.Item schemaItem, Inventory.Item inventoryItem) {}
        
        public override void OnTradeMessage (string message) {}
        
        public override void OnTradeReady (bool ready) 
        {
            if (!ready)
            {
                Trade.SetReady (false);
            }
            else
            {
                if(Validate ())
                {
                    Trade.SetReady (true);
                    //if(Trade.MyOfferedItems = 0)
                    //Trade.OtherO
                    Trade.AcceptTrade();

                }
                //SendTradeMessage("Scrap: {0}", AmountAdded.ScrapTotal);
            }
        }

        public override void OnTradeSuccess()
        {

            Log.Success("Trade Complete.");
        }

        public override void OnTradeAwaitingEmailConfirmation(long tradeOfferID)
        {
            Log.Warn("Trade ended awaiting email confirmation");
            SendChatMessage("Please complete the email confirmation to finish the trade");

			Bot.Round.ItemsPerPlayer.Add(Trade.OtherSID, Trade.OtherOfferedItems.ToList());

			//TODO: We have reached the max amount of items.. Do something.
			if (Bot.Round.ItemLimit >= Bot.Round.ItemsInRound) {
				Bot.Round.IsCurrent = false;

			}

		
        }

        public override void OnTradeAccept() 
        {
            if (Validate() || IsAdmin)
            {
                //Even if it is successful, AcceptTrade can fail on
                //trades with a lot of items so we use a try-catch
                try {
                    if (Trade.AcceptTrade())
                    {
                        Log.Success("Trade Accepted!");
                        //TODO: To move out on OnTradeSuccess
                        Bot.Round.ItemsPerPlayer.Add(Trade.OtherSID, Trade.OtherOfferedItems.ToList());
                        //var jsonIventory = JsonConvert.SerializeObject(Trade.OtherOfferedItems);
                        //var inventory2 = JsonConvert.DeserializeObject<IEnumerable<TradeUserAssets>>(jsonIventory);
                        
                    }


                }
                catch {
                    Log.Warn ("The trade might have failed, but we can't be sure.");
                }
            }
        }

        public bool Validate ()
        {
            return true;
            //AmountAdded = TF2Value.Zero;

            //List<string> errors = new List<string> ();

            //foreach (TradeUserAssets asset in Trade.OtherOfferedItems)
            //{
            //    var item = Trade.OtherInventory.GetItem(asset.assetid);
            //    if (item.Defindex == 5000)
            //        AmountAdded += TF2Value.Scrap;
            //    else if (item.Defindex == 5001)
            //        AmountAdded += TF2Value.Reclaimed;
            //    else if (item.Defindex == 5002)
            //        AmountAdded += TF2Value.Refined;
            //    else
            //    {
            //        var schemaItem = Trade.CurrentSchema.GetItem (item.Defindex);
            //        errors.Add ("Item " + schemaItem.Name + " is not a metal.");
            //    }
            //}

            //if (AmountAdded == TF2Value.Zero)
            //{
            //    errors.Add ("You must put up at least 1 scrap.");
            //}

            //// send the errors
            //if (errors.Count != 0)
            //    SendTradeMessage("There were errors in your trade: ");
            //foreach (string error in errors)
            //{
            //    SendTradeMessage(error);
            //}

            //return errors.Count == 0;
        }
        
    }
 
}

