namespace SagaLogin.Packets.Server
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAR_EQUIP" />.
    /// </summary>
    public class SSMG_CHAR_EQUIP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAR_EQUIP"/> class.
        /// </summary>
        public SSMG_CHAR_EQUIP()
        {
            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                this.data = new byte[214];
            else
                this.data = new byte[161];
            this.offset = (ushort)14;
            this.ID = (ushort)41;
            this.PutByte((byte)13, (ushort)2);
            this.PutByte((byte)13, (ushort)55);
            this.PutByte((byte)13, (ushort)108);
            if (Singleton<Configuration>.Instance.Version < SagaLib.Version.Saga10)
                return;
            this.PutByte((byte)13, (ushort)161);
        }

        /// <summary>
        /// Sets the Characters.
        /// </summary>
        public List<ActorPC> Characters
        {
            set
            {
                int num = Singleton<Configuration>.Instance.Version < SagaLib.Version.Saga10 ? 3 : 4;
                for (int i = 0; i < num; ++i)
                {
                    IEnumerable<ActorPC> source = value.Where<ActorPC>((Func<ActorPC, bool>)(p => (int)p.Slot == i));
                    if (source.Count<ActorPC>() != 0)
                    {
                        ActorPC actorPc = source.First<ActorPC>();
                        for (int index = 0; index < 13; ++index)
                        {
                            if (actorPc.Inventory.Equipments.ContainsKey((EnumEquipSlot)index))
                            {
                                SagaDB.Item.Item equipment = actorPc.Inventory.Equipments[(EnumEquipSlot)index];
                                if (equipment.PictID == 0U)
                                    this.PutUInt(equipment.ItemID, (ushort)(3 + i * 53 + index * 4));
                                else
                                    this.PutUInt(equipment.PictID, (ushort)(3 + i * 53 + index * 4));
                            }
                        }
                    }
                }
            }
        }
    }
}
