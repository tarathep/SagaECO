using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaDB.Item;

namespace SagaScript
{
    public abstract class GuildMerchant : Event
    {
        uint shopID;
        int wareFee;
        WarehousePlace warePlace;

        public GuildMerchant()
        {
        
        }

        /// <summary>
        /// 初始化GuildMerchant
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="shop">商店ID</param>
        /// <param name="wareFee">倉庫費用</param>
        /// <param name="place">倉庫地點</param>

        protected void Init(uint eventID, uint shop, int wareFee, WarehousePlace place)
        {
            this.EventID = eventID;
            this.shopID = shop;
            this.wareFee = wareFee;
            this.warePlace = place;
        }

        public override void OnEvent(ActorPC pc)
        {
            string fee;

            if (wareFee > 0)
            {
                fee = wareFee.ToString() + "Gold";
            }
            else
            {
                fee = "Free";
            }

            switch (Select(pc, "What do you want to do?", "", "Buy something", "Sell something", "Deposit in bank", "Withdraw money in bank", "Use warehouse (" + fee + ") ", "[Treasure Gold] Handling", "Do nothing"))
            {
                case 1:
                    OpenShopBuy(pc, shopID);
                    break;

                case 2:
                    OpenShopSell(pc, shopID);
                    break;

                case 3:
                    BankDeposit(pc);
                    break;

                case 4:
                    BankWithdraw(pc);
                    break;

                case 5:
                    if (pc.Gold >= wareFee)
                    {
                        pc.Gold -= wareFee;

                        OpenWareHouse(pc, warePlace);
                    }
                    else
                    {
                        Say(pc, 131, "Insufficient funds! $R;", "Guild Merchant");
                    }
                    break;
                case 6:
                    switch (Select(pc, "How to handle [Treasure Gold Coins]?", "", "[Treasure Gold Coins] purchase", "[Treasure Gold Coins] sales", "What is [Treasure Gold Coins]?", "Nothing"))
                    {
                        case 1:
                            Say(pc, 11000050, 131, "Ahhh! This distinguished guest...$R;" +
                            "Because I prepared a lot of $R;" +
                            "Please come and buy points. $R;" +
                            "$P『Treasure Gold Coin』$R;" +
                            "When buying $R;" +
                            "You will be charged a 1% handling fee, $R;" +
                            "Is this all right? $R;", "Guild Merchant");
                            if (Select(pc, "How about the 1% fee?", "", "good", "bad") == 1)
                            {
                                string temp = InputBox(pc, "How many to buy?", InputType.Bank);
                                if (temp != "")
                                {
                                    ushort count = ushort.Parse(temp);
                                    if (count <= 99 && pc.Gold >= (count * 1010000))
                                    {
                                        Say(pc, 11000050, 131, "『Treasure Gold Coin』$R;" +
                                            count + "Purchases. $R;" +
                                            "Please smile...$R;", "Guild Merchant");
                                        PlaySound(pc, 2040, false, 100, 50);
                                        pc.Gold -= (count * 1010000);
                                        GiveItem(pc, 10037600, count);

                                        Say(pc, 0, 131, "『Treasure Gold Coin』$R;" +
                                        count + "个$R;" +
                                        "Get! $R;", "");
                                    }
                                    else
                                    {
                                        Say(pc, 0, 131, "It seems that the money is not enough! $R;", "");
                                    }
                                }
                            }
                            break;
                        case 2:
                            if (CountItem(pc, 10037600) >= 1)
                            {
                                if (Select(pc, "Do you want to exchange it?", "", "Yes", "No") == 1)
                                {
                                    string temp = InputBox(pc, "How many to exchange?", InputType.Bank);
                                    if (temp != "temp")
                                    {
                                        int count = int.Parse(temp);
                                        if (CountItem(pc, 10037600) >= count)
                                        {
                                            if (pc.Gold + (uint)(count * 1000000) <= 99999999)
                                            {
                                                Say(pc, 0, 131, "lost" + count + "a [treasure gold coin]. $R;", "guild merchant");
                                                TakeItem(pc, 10037600, (ushort)count);
                                                pc.Gold += (count * 1000000);
                                                Say(pc, 0, 131, "Get" + (count * 1000000) + "G.$R;", "Guild Merchant");
                                            }
                                            else
                                            {
                                                Say(pc, 0, 131, "I have too much money with me. $R;", "Guild Merchant");
                                            }
                                        }
                                        else
                                        {
                                            Say(pc, 0, 131, "[Treasure Gold Coins] does not seem to be enough. $R;", "Guild Merchant");
                                        }
                                    }
                                }
                            }
                            else
                                Say(pc, 0, 131, "You don't seem to have a [treasure gold coin] in your hand! $R;", "Guild Merchant");
                            break;
                        case 3:
                            Say(pc, 11000050, 131, "We are accepting a $R;" +
                            "Special gem. $R;" +
                            "$R is like that, pickup truck! $R;" +
                            "$ can be purchased with 1 million gold coins, $R;" +
                            "You can also go to our guild merchant again, $R;" +
                            "Exchange the equivalent of 1 million gold coins. $R;" +
                            "$P a [treasure gold coin] is equivalent to $R;" +
                            "The value of one million gold coins...$R;" +
                            "$R2 is equivalent to 2 million gold coins! $R;" +
                            "Three of them are equivalent to 3 million gold coins! $R;" +
                            "What about $P10? One, ten million gold coins... $R;" +
                            "$R, ah, ah... so much money? $R;" +
                            "$P wants to ask why this $R;" +
                            "Something...$R;" +
                            "$P For example, your wallet cannot be $R;" +
                            "Put in 999999 $R;" +
                            "The above gold coins? $R;" +
                            "$P needs to be with friends and partners, $R;" +
                            "Securely trade 99,999,999$R;" +
                            "Please consider using the above. $R;" +
                            "$P is traded with [Treasure Gold Coins] $R;" +
                            "One transaction is enough! $R;" +
                            "After $R, you can exchange for gold coins with peace of mind. $R;" +
                            "$P In addition, because it is a special gem, $R;" +
                            "So you can only use $R; here in our guild merchants" +
                            "$P will also be $R when buying;" +
                            "A 1% handling fee is charged. $R;" +
                            "So please pay attention. $R;" +
                            "That means 1 [Treasure Gold Coin] $R;" +
                            "If you do, you will be charged a handling fee of 10,000 gold coins. $R;" +
                            "$P If necessary, please feel free to $R;" +
                            "All come to patronize our guild merchants. $R;", "guild merchants");
                            break;
                    }
                    break;
            }

            Say(pc, 131, "Welcome to visit next time! $R;", "Guild Merchant");
        }
    }
}
