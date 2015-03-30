using System;
using System.Collections.Generic;
using System.Linq;
using SteamKit2;
using SteamTrade;
using SteamTrade.Inventories;

namespace SteamBot
{
    public class SimpleUserHandler : UserHandler
    {
        public TF2Value AmountAdded;
        public CsgoInventory MySteamInventory { get; set; }
        public CsgoInventory OtherSteamInventory { get; set; }
        

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

        public override void OnBotCommand(string command)
        {

            var inv = CsgoInventory.FetchInventory(76561197967970346, Bot.ApiKey, SteamWeb);

        }

        public override void OnChatRoomMessage(SteamID chatID, SteamID sender, string message)
        {
            Log.Info(Bot.SteamFriends.GetFriendPersonaName(sender) + ": " + message);
            base.OnChatRoomMessage(chatID, sender, message);
        }

        public override void OnFriendRemove () {}
        
        public override void OnMessage (string message, EChatEntryType type)
        {
            //var inventory = OtherTf2Inventory;
            //var jsonIventory = JsonConvert.SerializeObject(inventory);
            //var inventory2 = JsonConvert.DeserializeObject<TF2Inventory>(jsonIventory);
            //Log.Info(jsonIventory);
            //Console.WriteLine("test");
            //SendChatMessage(Bot.ChatResponse);
            /**                         **/
            //var rand = new Random();
            //Bot.GetInventory();
            //var inventory = Bot.MyTf2Inventory.Items;
            //var randomItem = inventory[rand.Next(inventory.Length)];
            //var tradeOffer = Bot.NewTradeOffer(OtherSID);
            //tradeOffer.Items.AddMyItem(randomItem.AppId, randomItem.ContextId, (long) randomItem.Id);

            //string tradeId;
            //tradeOffer.Send(out tradeId);

            var rand = new Random();

            List<long> contextId = new List<long>();
            //contextId.Add(6);
            contextId.Add(2);


            var genericInv = new GenericInventory(Bot.SteamWeb);
            genericInv.load(730, contextId, OtherSID);

            var inventory = genericInv.items;
            foreach (var item in genericInv.items)
            {
                Bot.log.Info(genericInv.getDescription(item.Key).name);
                
            }

            //var randomItem = inventory[rand.Next(inventory.Length)];
            //var tradeOffer = Bot.NewTradeOffer(OtherSID);
            //tradeOffer.Items.AddMyItem(randomItem.AppId, randomItem.ContextId, (long)randomItem.Id);

            //string tradeId;
            //tradeOffer.Send(out tradeId);

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
            MySteamInventory = CsgoInventory.FetchInventory(Bot.SteamClient.SteamID, Bot.ApiKey, SteamWeb);
            OtherSteamInventory = CsgoInventory.FetchInventory(OtherSID, Bot.ApiKey, SteamWeb);
            //mySteamInventory.load(753, contextId, Bot.SteamClient.SteamID);
            //OtherSteamInventory.load(753, contextId, OtherSID);

            if (!MySteamInventory.Success | !OtherSteamInventory.Success)
            {
                SendTradeMessage("Couldn't open an inventory, type 'errors' for more info.");
            }
        }

        

        public override void OnTradeAddItem (Schema.Item schemaItem, Tf2Inventory.Item inventoryItem) {}
        
        public override void OnTradeRemoveItem (Schema.Item schemaItem, Tf2Inventory.Item inventoryItem) {}
        
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
            //TODO: Log in MySQL

            Log.Success("Trade Complete.");
        }

        public override void OnTradeAwaitingEmailConfirmation(long tradeOfferID)
        {
            Log.Warn("Trade ended awaiting email confirmation");
            SendChatMessage("Please complete the email confirmation to finish the trade");

            var concreteItem = Trade.OtherOfferedItems.Select(offeredItem => OtherSteamInventory.GetItem(offeredItem.assetid)).ToList();

            Bot.Round.ItemsPerPlayer.Add(Trade.OtherSID, concreteItem);

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
                        //Bot.Round.ItemsPerPlayer.Add(Trade.OtherSID, Trade.OtherOfferedItems.ToList());
                        //var jsonIventory = JsonConvert.SerializeObject(Trade.OtherOfferedItems);
                        //var inventory2 = JsonConvert.DeserializeObject<IEnumerable<TradeUserAssets>>(jsonIventory);
                        
                    }


                }
                catch {
                    Log.Warn ("The trade might have failed, but we can't be sure.");
                }
            }
        }

        //TODO: Validate that the items are of the correct type.
        public bool Validate ()
        {
            return true;
            //AmountAdded = TF2Value.Zero;

            //List<string> errors = new List<string> ();

            //foreach (TradeUserAssets asset in Trade.OtherOfferedItems)
            //{
            //    var item = Trade.OtherTf2Inventory.GetItem(asset.assetid);
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

