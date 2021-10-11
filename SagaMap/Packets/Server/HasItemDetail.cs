namespace SagaMap.Packets.Server
{
    using SagaDB.Item;
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="HasItemDetail" />.
    /// </summary>
    public abstract class HasItemDetail : Packet
    {
        /// <summary>
        /// Defines the price.
        /// </summary>
        private uint price = 0;

        /// <summary>
        /// Defines the shopCount.
        /// </summary>
        private ushort shopCount = 0;

        /// <summary>
        /// Sets the Price.
        /// </summary>
        public uint Price
        {
            set
            {
                this.price = value;
            }
        }

        /// <summary>
        /// Sets the ShopCount.
        /// </summary>
        public ushort ShopCount
        {
            set
            {
                this.shopCount = value;
            }
        }

        /// <summary>
        /// Sets the ItemDetail.
        /// </summary>
        protected SagaDB.Item.Item ItemDetail
        {
            set
            {
                this.PutUInt(value.ItemID);
                this.PutUInt(value.PictID);
                ++this.offset;
                int identified = (int)value.identified;
                if (value.Locked)
                    identified |= 32;
                this.PutInt(identified);
                this.PutUShort(value.Durability);
                this.PutUShort(value.maxDurability);
                if (value.BaseData.itemType != ItemType.PET && value.BaseData.itemType != ItemType.PET_NEKOMATA && value.BaseData.itemType != ItemType.RIDE_PET && value.BaseData.itemType != ItemType.BACK_DEMON)
                    this.PutUShort(value.Refine);
                else
                    this.PutUShort((ushort)0);
                if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga9_Iris)
                {
                    this.PutUShort((ushort)value.CurrentSlot);
                    for (int index = 0; index < 10; ++index)
                    {
                        if (index < value.Cards.Count)
                            this.PutUInt(value.Cards[index].ID);
                        else
                            this.PutUInt(0U);
                    }
                }
                this.PutByte((byte)0);
                this.PutUShort(value.Stack);
                if (this.price == 0U)
                    this.PutUInt(value.BaseData.price);
                else
                    this.PutUInt(this.price);
                this.PutUShort(this.shopCount);
                this.PutUShort(value.BaseData.possessionWeight);
                this.offset += (ushort)8;
                this.PutUShort(value.BaseData.passiveSkill);
                this.PutUShort(value.BaseData.possessionSkill);
                this.PutUShort(value.BaseData.possessionPassiveSkill);
                this.PutShort((short)((int)value.BaseData.str + (int)value.Str));
                this.PutShort((short)((int)value.BaseData.mag + (int)value.Mag));
                this.PutShort((short)((int)value.BaseData.vit + (int)value.Vit));
                this.PutShort((short)((int)value.BaseData.dex + (int)value.Dex));
                this.PutShort((short)((int)value.BaseData.agi + (int)value.Agi));
                this.PutShort((short)((int)value.BaseData.intel + (int)value.Int));
                this.PutShort(value.BaseData.luk);
                this.PutShort(value.BaseData.cha);
                this.PutShort((short)((int)value.BaseData.hp + (int)value.HP));
                this.PutShort((short)((int)value.BaseData.sp + (int)value.SP));
                this.PutShort((short)((int)value.BaseData.mp + (int)value.MP));
                this.PutShort((short)((int)value.BaseData.speedUp + (int)value.SpeedUp));
                this.PutShort((short)((int)value.BaseData.atk1 + (int)value.Atk1));
                this.PutShort((short)((int)value.BaseData.atk2 + (int)value.Atk2));
                this.PutShort((short)((int)value.BaseData.atk3 + (int)value.Atk3));
                this.PutShort((short)((int)value.BaseData.matk + (int)value.MAtk));
                this.PutShort((short)((int)value.BaseData.def + (int)value.Def));
                this.PutShort((short)((int)value.BaseData.mdef + (int)value.MDef));
                this.PutShort((short)((int)value.BaseData.hitMelee + (int)value.HitMelee));
                this.PutShort((short)((int)value.BaseData.hitRanged + (int)value.HitRanged));
                this.PutShort((short)((int)value.BaseData.hitMagic + (int)value.HitMagic));
                this.PutShort((short)((int)value.BaseData.avoidMelee + (int)value.AvoidMelee));
                this.PutShort((short)((int)value.BaseData.avoidRanged + (int)value.AvoidRanged));
                this.PutShort((short)((int)value.BaseData.avoidMagic + (int)value.AvoidMagic));
                this.PutShort((short)((int)value.BaseData.hitCritical + (int)value.HitCritical));
                this.PutShort((short)((int)value.BaseData.avoidCritical + (int)value.AvoidCritical));
                this.PutShort(value.BaseData.hpRecover);
                this.PutShort(value.BaseData.mpRecover);
                this.offset += (ushort)2;
                for (int index = 0; index < 7; ++index)
                {
                    if (value.BaseData.element.ContainsKey((Elements)index))
                        this.PutShort(value.BaseData.element[(Elements)index]);
                }
                for (int index = 1; index <= 9; ++index)
                {
                    if (value.BaseData.abnormalStatus.ContainsKey((AbnormalStatus)index))
                        this.PutShort(value.BaseData.abnormalStatus[(AbnormalStatus)index]);
                }
                this.PutShort((short)0);
                this.PutShort((short)0);
                this.PutShort((short)0);
                this.PutInt(0);
                this.PutInt(0);
                this.PutInt(0);
                this.PutShort((short)0);
                this.PutInt(0);
                this.PutShort((short)0);
                this.PutShort((short)0);
                this.PutByte((byte)0);
                this.PutByte((byte)0);
                if (Singleton<Configuration>.Instance.Version < SagaLib.Version.Saga9_Iris)
                    return;
                if (value.Rental)
                {
                    this.PutInt((int)(value.RentalTime - DateTime.Now).TotalSeconds);
                    this.PutByte((byte)1);
                }
                else
                {
                    this.PutInt(-1);
                    this.PutByte((byte)0);
                }
            }
        }
    }
}
