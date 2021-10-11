namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_ACTOR_EQUIP_UPDATE" />.
    /// </summary>
    public class SSMG_ITEM_ACTOR_EQUIP_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_ACTOR_EQUIP_UPDATE"/> class.
        /// </summary>
        public SSMG_ITEM_ACTOR_EQUIP_UPDATE()
        {
            this.data = new byte[77];
            this.offset = (ushort)2;
            this.ID = (ushort)2537;
        }

        /// <summary>
        /// Sets the Player.
        /// </summary>
        public ActorPC Player
        {
            set
            {
                this.PutUInt(value.ActorID, (ushort)2);
                this.PutByte((byte)13, (ushort)6);
                Dictionary<EnumEquipSlot, SagaDB.Item.Item> dictionary = value.Form == DEM_FORM.MACHINA_FORM ? value.Inventory.Parts : value.Inventory.Equipments;
                if (value.Marionette == null)
                {
                    for (int index = 0; index < 13; ++index)
                    {
                        if (dictionary.ContainsKey((EnumEquipSlot)index))
                        {
                            SagaDB.Item.Item obj = dictionary[(EnumEquipSlot)index];
                            if (obj.Stack != (ushort)0)
                            {
                                if (obj.PictID == 0U)
                                    this.PutUInt(obj.BaseData.imageID, (ushort)(7 + index * 4));
                                else
                                    this.PutUInt(obj.PictID, (ushort)(7 + index * 4));
                            }
                        }
                    }
                }
                else
                    this.PutUInt(value.Marionette.PictID, (ushort)7);
                this.PutByte((byte)3, (ushort)59);
                if (dictionary.ContainsKey(EnumEquipSlot.LEFT_HAND) && value.Marionette == null)
                {
                    SagaDB.Item.Item obj = dictionary[EnumEquipSlot.LEFT_HAND];
                    this.PutByte(obj.BaseData.handMotion, (ushort)60);
                    this.PutByte(obj.BaseData.handMotion2, (ushort)61);
                }
                this.PutByte((byte)3, (ushort)63);
                if (dictionary.ContainsKey(EnumEquipSlot.RIGHT_HAND) && value.Marionette == null)
                {
                    SagaDB.Item.Item obj = dictionary[EnumEquipSlot.RIGHT_HAND];
                    this.PutByte(obj.BaseData.handMotion, (ushort)64);
                    this.PutByte(obj.BaseData.handMotion2, (ushort)65);
                }
                this.PutByte((byte)3, (ushort)67);
                if (dictionary.ContainsKey(EnumEquipSlot.PET) && value.Pet != null && value.Pet.Ride)
                {
                    SagaDB.Item.Item obj = dictionary[EnumEquipSlot.PET];
                    this.PutByte(obj.BaseData.handMotion, (ushort)68);
                    this.PutByte(obj.BaseData.handMotion2, (ushort)69);
                }
                if (!value.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) || value.Pet == null || !value.Pet.Ride)
                    return;
                this.PutUInt(value.Inventory.Equipments[EnumEquipSlot.PET].ItemID, (ushort)71);
            }
        }
    }
}
