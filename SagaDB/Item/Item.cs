namespace SagaDB.Item
{
    using SagaDB.Actor;
    using SagaDB.Iris;
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="Item" />.
    /// </summary>
    [Serializable]
    public class Item
    {
        /// <summary>
        /// Defines the Version.
        /// </summary>
        private static ushort Version = 3;

        /// <summary>
        /// Defines the rentalTime.
        /// </summary>
        private DateTime rentalTime = DateTime.Now;

        /// <summary>
        /// Defines the cards.
        /// </summary>
        private List<IrisCard> cards = new List<IrisCard>();

        /// <summary>
        /// Defines the db_id.
        /// </summary>
        private uint db_id;

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the stack.
        /// </summary>
        private ushort stack;

        /// <summary>
        /// Defines the durability.
        /// </summary>
        private ushort durability;

        /// <summary>
        /// Defines the identified.
        /// </summary>
        public byte identified;

        /// <summary>
        /// Defines the locked.
        /// </summary>
        private bool locked;

        /// <summary>
        /// Defines the slot.
        /// </summary>
        private uint slot;

        /// <summary>
        /// Defines the hp.
        /// </summary>
        private short hp;

        /// <summary>
        /// Defines the mp.
        /// </summary>
        private short mp;

        /// <summary>
        /// Defines the sp.
        /// </summary>
        private short sp;

        /// <summary>
        /// Defines the weightUp.
        /// </summary>
        private short weightUp;

        /// <summary>
        /// Defines the volumeUp.
        /// </summary>
        private short volumeUp;

        /// <summary>
        /// Defines the speedUp.
        /// </summary>
        private short speedUp;

        /// <summary>
        /// Defines the str.
        /// </summary>
        private short str;

        /// <summary>
        /// Defines the dex.
        /// </summary>
        private short dex;

        /// <summary>
        /// Defines the intel.
        /// </summary>
        private short intel;

        /// <summary>
        /// Defines the vit.
        /// </summary>
        private short vit;

        /// <summary>
        /// Defines the agi.
        /// </summary>
        private short agi;

        /// <summary>
        /// Defines the mag.
        /// </summary>
        private short mag;

        /// <summary>
        /// Defines the luk.
        /// </summary>
        private short luk;

        /// <summary>
        /// Defines the cha.
        /// </summary>
        private short cha;

        /// <summary>
        /// Defines the atk1.
        /// </summary>
        private short atk1;

        /// <summary>
        /// Defines the atk2.
        /// </summary>
        private short atk2;

        /// <summary>
        /// Defines the atk3.
        /// </summary>
        private short atk3;

        /// <summary>
        /// Defines the matk.
        /// </summary>
        private short matk;

        /// <summary>
        /// Defines the def.
        /// </summary>
        private short def;

        /// <summary>
        /// Defines the mdef.
        /// </summary>
        private short mdef;

        /// <summary>
        /// Defines the hitMelee.
        /// </summary>
        private short hitMelee;

        /// <summary>
        /// Defines the hitRanged.
        /// </summary>
        private short hitRanged;

        /// <summary>
        /// Defines the hitMagic.
        /// </summary>
        private short hitMagic;

        /// <summary>
        /// Defines the avoidMelee.
        /// </summary>
        private short avoidMelee;

        /// <summary>
        /// Defines the avoidRanged.
        /// </summary>
        private short avoidRanged;

        /// <summary>
        /// Defines the avoidMagic.
        /// </summary>
        private short avoidMagic;

        /// <summary>
        /// Defines the hitCritical.
        /// </summary>
        private short hitCritical;

        /// <summary>
        /// Defines the avoidCritical.
        /// </summary>
        private short avoidCritical;

        /// <summary>
        /// Defines the hpRecover.
        /// </summary>
        private short hpRecover;

        /// <summary>
        /// Defines the mpRecover.
        /// </summary>
        private short mpRecover;

        /// <summary>
        /// Defines the aspd.
        /// </summary>
        private short aspd;

        /// <summary>
        /// Defines the cspd.
        /// </summary>
        private short cspd;

        /// <summary>
        /// Defines the pict_id.
        /// </summary>
        private uint pict_id;

        /// <summary>
        /// Defines the rental.
        /// </summary>
        private bool rental;

        /// <summary>
        /// Defines the currentSlot.
        /// </summary>
        private byte currentSlot;

        /// <summary>
        /// Defines the maxSlot.
        /// </summary>
        private byte maxSlot;

        /// <summary>
        /// Defines the refine.
        /// </summary>
        private ushort refine;

        /// <summary>
        /// Defines the possessionedActor.
        /// </summary>
        [NonSerialized]
        private ActorPC possessionedActor;

        /// <summary>
        /// Defines the possessionOwner.
        /// </summary>
        [NonSerialized]
        private ActorPC possessionOwner;

        /// <summary>
        /// Gets or sets the HP.
        /// </summary>
        public short HP
        {
            get
            {
                return this.hp;
            }
            set
            {
                this.hp = value;
            }
        }

        /// <summary>
        /// Gets or sets the MP.
        /// </summary>
        public short MP
        {
            get
            {
                return this.mp;
            }
            set
            {
                this.mp = value;
            }
        }

        /// <summary>
        /// Gets or sets the SP.
        /// </summary>
        public short SP
        {
            get
            {
                return this.sp;
            }
            set
            {
                this.sp = value;
            }
        }

        /// <summary>
        /// Gets or sets the WeightUp.
        /// </summary>
        public short WeightUp
        {
            get
            {
                return this.weightUp;
            }
            set
            {
                this.weightUp = value;
            }
        }

        /// <summary>
        /// Gets or sets the VolumeUp.
        /// </summary>
        public short VolumeUp
        {
            get
            {
                return this.volumeUp;
            }
            set
            {
                this.volumeUp = value;
            }
        }

        /// <summary>
        /// Gets or sets the SpeedUp.
        /// </summary>
        public short SpeedUp
        {
            get
            {
                return this.speedUp;
            }
            set
            {
                this.speedUp = value;
            }
        }

        /// <summary>
        /// Gets or sets the Str.
        /// </summary>
        public short Str
        {
            get
            {
                return this.str;
            }
            set
            {
                this.str = value;
            }
        }

        /// <summary>
        /// Gets or sets the Dex.
        /// </summary>
        public short Dex
        {
            get
            {
                return this.dex;
            }
            set
            {
                this.dex = value;
            }
        }

        /// <summary>
        /// Gets or sets the Int.
        /// </summary>
        public short Int
        {
            get
            {
                return this.intel;
            }
            set
            {
                this.intel = value;
            }
        }

        /// <summary>
        /// Gets or sets the Vit.
        /// </summary>
        public short Vit
        {
            get
            {
                return this.vit;
            }
            set
            {
                this.vit = value;
            }
        }

        /// <summary>
        /// Gets or sets the Agi.
        /// </summary>
        public short Agi
        {
            get
            {
                return this.agi;
            }
            set
            {
                this.agi = value;
            }
        }

        /// <summary>
        /// Gets or sets the Mag.
        /// </summary>
        public short Mag
        {
            get
            {
                return this.mag;
            }
            set
            {
                this.mag = value;
            }
        }

        /// <summary>
        /// Gets or sets the Atk1.
        /// </summary>
        public short Atk1
        {
            get
            {
                return this.atk1;
            }
            set
            {
                this.atk1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the Atk2.
        /// </summary>
        public short Atk2
        {
            get
            {
                return this.atk2;
            }
            set
            {
                this.atk2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the Atk3.
        /// </summary>
        public short Atk3
        {
            get
            {
                return this.atk3;
            }
            set
            {
                this.atk3 = value;
            }
        }

        /// <summary>
        /// Gets or sets the MAtk.
        /// </summary>
        public short MAtk
        {
            get
            {
                return this.matk;
            }
            set
            {
                this.matk = value;
            }
        }

        /// <summary>
        /// Gets or sets the Def.
        /// </summary>
        public short Def
        {
            get
            {
                return this.def;
            }
            set
            {
                this.def = value;
            }
        }

        /// <summary>
        /// Gets or sets the MDef.
        /// </summary>
        public short MDef
        {
            get
            {
                return this.mdef;
            }
            set
            {
                this.mdef = value;
            }
        }

        /// <summary>
        /// Gets or sets the HitMelee.
        /// </summary>
        public short HitMelee
        {
            get
            {
                return this.hitMelee;
            }
            set
            {
                this.hitMelee = value;
            }
        }

        /// <summary>
        /// Gets or sets the HitMagic.
        /// </summary>
        public short HitMagic
        {
            get
            {
                return this.hitMagic;
            }
            set
            {
                this.hitMagic = value;
            }
        }

        /// <summary>
        /// Gets or sets the HitRanged.
        /// </summary>
        public short HitRanged
        {
            get
            {
                return this.hitRanged;
            }
            set
            {
                this.hitRanged = value;
            }
        }

        /// <summary>
        /// Gets or sets the AvoidMelee.
        /// </summary>
        public short AvoidMelee
        {
            get
            {
                return this.avoidMelee;
            }
            set
            {
                this.avoidMelee = value;
            }
        }

        /// <summary>
        /// Gets or sets the AvoidMagic.
        /// </summary>
        public short AvoidMagic
        {
            get
            {
                return this.avoidMagic;
            }
            set
            {
                this.avoidMagic = value;
            }
        }

        /// <summary>
        /// Gets or sets the AvoidRanged.
        /// </summary>
        public short AvoidRanged
        {
            get
            {
                return this.avoidRanged;
            }
            set
            {
                this.avoidRanged = value;
            }
        }

        /// <summary>
        /// Gets or sets the HitCritical.
        /// </summary>
        public short HitCritical
        {
            get
            {
                return this.hitCritical;
            }
            set
            {
                this.hitCritical = value;
            }
        }

        /// <summary>
        /// Gets or sets the AvoidCritical.
        /// </summary>
        public short AvoidCritical
        {
            get
            {
                return this.avoidCritical;
            }
            set
            {
                this.avoidCritical = value;
            }
        }

        /// <summary>
        /// Gets or sets the HPRecover.
        /// </summary>
        public short HPRecover
        {
            get
            {
                return this.hpRecover;
            }
            set
            {
                this.hpRecover = value;
            }
        }

        /// <summary>
        /// Gets or sets the MPRecover.
        /// </summary>
        public short MPRecover
        {
            get
            {
                return this.mpRecover;
            }
            set
            {
                this.mpRecover = value;
            }
        }

        /// <summary>
        /// Gets or sets the ASPD.
        /// </summary>
        public short ASPD
        {
            get
            {
                return this.aspd;
            }
            set
            {
                this.aspd = value;
            }
        }

        /// <summary>
        /// Gets or sets the CSPD.
        /// </summary>
        public short CSPD
        {
            get
            {
                return this.cspd;
            }
            set
            {
                this.cspd = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Rental.
        /// </summary>
        public bool Rental
        {
            get
            {
                return this.rental;
            }
            set
            {
                this.rental = value;
            }
        }

        /// <summary>
        /// Gets or sets the RentalTime.
        /// </summary>
        public DateTime RentalTime
        {
            get
            {
                return this.rentalTime;
            }
            set
            {
                this.rentalTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the PictID.
        /// </summary>
        public uint PictID
        {
            get
            {
                return this.pict_id;
            }
            set
            {
                this.pict_id = value;
            }
        }

        /// <summary>
        /// Gets the ItemID.
        /// </summary>
        public uint ItemID
        {
            get
            {
                return this.id;
            }
        }

        /// <summary>
        /// Gets or sets the DBID.
        /// </summary>
        public uint DBID
        {
            get
            {
                return this.db_id;
            }
            set
            {
                this.db_id = value;
            }
        }

        /// <summary>
        /// Gets the maxDurability.
        /// </summary>
        public ushort maxDurability
        {
            get
            {
                return this.BaseData.durability;
            }
        }

        /// <summary>
        /// Gets or sets the Slot.
        /// </summary>
        public uint Slot
        {
            get
            {
                return this.slot;
            }
            set
            {
                this.slot = value;
            }
        }

        /// <summary>
        /// Gets the BaseData.
        /// </summary>
        public SagaDB.Item.Item.ItemData BaseData
        {
            get
            {
                return (SagaDB.Item.Item.ItemData)null ?? (!Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.Items.ContainsKey(this.id) ? new SagaDB.Item.Item.ItemData() : Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.Items[this.id]);
            }
        }

        /// <summary>
        /// Gets or sets the Stack.
        /// </summary>
        public ushort Stack
        {
            get
            {
                return this.stack;
            }
            set
            {
                this.stack = value;
            }
        }

        /// <summary>
        /// Gets or sets the Durability.
        /// </summary>
        public ushort Durability
        {
            get
            {
                return this.durability;
            }
            set
            {
                this.durability = value;
            }
        }

        /// <summary>
        /// Gets or sets the PossessionedActor.
        /// </summary>
        public ActorPC PossessionedActor
        {
            get
            {
                return this.possessionedActor;
            }
            set
            {
                this.possessionedActor = value;
            }
        }

        /// <summary>
        /// Gets or sets the PossessionOwner.
        /// </summary>
        public ActorPC PossessionOwner
        {
            get
            {
                return this.possessionOwner;
            }
            set
            {
                this.possessionOwner = value;
            }
        }

        /// <summary>
        /// Gets or sets the CurrentSlot.
        /// </summary>
        public byte CurrentSlot
        {
            get
            {
                if (this.currentSlot == (byte)0 && (int)this.currentSlot != (int)this.BaseData.currentSlot)
                    this.currentSlot = this.BaseData.currentSlot;
                return this.currentSlot;
            }
            set
            {
                this.currentSlot = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxSlot.
        /// </summary>
        public byte MaxSlot
        {
            get
            {
                if (this.maxSlot == (byte)0 && (int)this.maxSlot != (int)this.BaseData.maxSlot)
                    this.maxSlot = this.BaseData.maxSlot;
                return this.maxSlot;
            }
            set
            {
                this.maxSlot = value;
            }
        }

        /// <summary>
        /// Gets the Cards.
        /// </summary>
        public List<IrisCard> Cards
        {
            get
            {
                if (this.cards == null)
                    this.cards = new List<IrisCard>();
                return this.cards;
            }
        }

        /// <summary>
        /// Gets or sets the Refine.
        /// </summary>
        public ushort Refine
        {
            get
            {
                return this.refine;
            }
            set
            {
                this.refine = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Identified.
        /// </summary>
        public bool Identified
        {
            get
            {
                return this.identified != (byte)0;
            }
            set
            {
                if (value)
                    this.identified = (byte)1;
                else
                    this.identified = (byte)0;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Locked.
        /// </summary>
        public bool Locked
        {
            get
            {
                return this.locked;
            }
            set
            {
                this.locked = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="baseData">The baseData<see cref="SagaDB.Item.Item.ItemData"/>.</param>
        public Item(SagaDB.Item.Item.ItemData baseData)
        {
            this.id = baseData.id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="InputStream">The InputStream<see cref="Stream"/>.</param>
        public Item(Stream InputStream)
        {
            this.FromStream(InputStream);
        }

        /// <summary>
        /// The ToStream.
        /// </summary>
        /// <param name="ms">The ms<see cref="Stream"/>.</param>
        public void ToStream(Stream ms)
        {
            BinaryWriter binaryWriter = new BinaryWriter(ms);
            binaryWriter.Write(SagaDB.Item.Item.Version);
            binaryWriter.Write(this.id);
            binaryWriter.Write(this.durability);
            binaryWriter.Write(this.stack);
            binaryWriter.Write(this.identified);
            binaryWriter.Write(this.agi);
            binaryWriter.Write(this.atk1);
            binaryWriter.Write(this.atk2);
            binaryWriter.Write(this.atk3);
            binaryWriter.Write(this.avoidCritical);
            binaryWriter.Write(this.avoidMagic);
            binaryWriter.Write(this.avoidMelee);
            binaryWriter.Write(this.avoidRanged);
            binaryWriter.Write(this.cha);
            binaryWriter.Write(this.def);
            binaryWriter.Write(this.dex);
            binaryWriter.Write(this.hitCritical);
            binaryWriter.Write(this.hitMagic);
            binaryWriter.Write(this.hitMelee);
            binaryWriter.Write(this.hitRanged);
            binaryWriter.Write(this.hp);
            binaryWriter.Write(this.hpRecover);
            binaryWriter.Write(this.intel);
            binaryWriter.Write(this.luk);
            binaryWriter.Write(this.mag);
            binaryWriter.Write(this.matk);
            binaryWriter.Write(this.mdef);
            binaryWriter.Write(this.mp);
            binaryWriter.Write(this.mpRecover);
            binaryWriter.Write(this.sp);
            binaryWriter.Write(this.speedUp);
            binaryWriter.Write(this.str);
            binaryWriter.Write(this.vit);
            binaryWriter.Write(this.volumeUp);
            binaryWriter.Write(this.weightUp);
            binaryWriter.Write(this.aspd);
            binaryWriter.Write(this.cspd);
            binaryWriter.Write(this.refine);
            binaryWriter.Write(this.pict_id);
            binaryWriter.Write(this.slot);
            binaryWriter.Write(this.currentSlot);
            if (this.cards == null)
                this.cards = new List<IrisCard>();
            binaryWriter.Write((byte)this.cards.Count);
            foreach (IrisCard card in this.cards)
                binaryWriter.Write(card.ID);
            binaryWriter.Write(this.locked);
            binaryWriter.Write(this.rental);
            binaryWriter.Write(this.rentalTime.ToBinary());
        }

        /// <summary>
        /// The FromStream.
        /// </summary>
        /// <param name="InputStream">The InputStream<see cref="Stream"/>.</param>
        public void FromStream(Stream InputStream)
        {
            try
            {
                BinaryReader binaryReader = new BinaryReader(InputStream);
                ushort num1 = binaryReader.ReadUInt16();
                if (num1 >= (ushort)1)
                {
                    this.id = binaryReader.ReadUInt32();
                    this.durability = binaryReader.ReadUInt16();
                    this.stack = binaryReader.ReadUInt16();
                    this.identified = binaryReader.ReadByte();
                    this.agi = binaryReader.ReadInt16();
                    this.atk1 = binaryReader.ReadInt16();
                    this.atk2 = binaryReader.ReadInt16();
                    this.atk3 = binaryReader.ReadInt16();
                    this.avoidCritical = binaryReader.ReadInt16();
                    this.avoidMagic = binaryReader.ReadInt16();
                    this.avoidMelee = binaryReader.ReadInt16();
                    this.avoidRanged = binaryReader.ReadInt16();
                    this.cha = binaryReader.ReadInt16();
                    this.def = binaryReader.ReadInt16();
                    this.dex = binaryReader.ReadInt16();
                    this.hitCritical = binaryReader.ReadInt16();
                    this.hitMagic = binaryReader.ReadInt16();
                    this.hitMelee = binaryReader.ReadInt16();
                    this.hitRanged = binaryReader.ReadInt16();
                    this.hp = binaryReader.ReadInt16();
                    this.hpRecover = binaryReader.ReadInt16();
                    this.intel = binaryReader.ReadInt16();
                    this.luk = binaryReader.ReadInt16();
                    this.mag = binaryReader.ReadInt16();
                    this.matk = binaryReader.ReadInt16();
                    this.mdef = binaryReader.ReadInt16();
                    this.mp = binaryReader.ReadInt16();
                    this.mpRecover = binaryReader.ReadInt16();
                    this.sp = binaryReader.ReadInt16();
                    this.speedUp = binaryReader.ReadInt16();
                    this.str = binaryReader.ReadInt16();
                    this.vit = binaryReader.ReadInt16();
                    this.volumeUp = binaryReader.ReadInt16();
                    this.weightUp = binaryReader.ReadInt16();
                    this.aspd = binaryReader.ReadInt16();
                    this.cspd = binaryReader.ReadInt16();
                    this.refine = binaryReader.ReadUInt16();
                    this.pict_id = binaryReader.ReadUInt32();
                    this.slot = binaryReader.ReadUInt32();
                }
                if (num1 >= (ushort)2)
                {
                    this.currentSlot = binaryReader.ReadByte();
                    int num2 = (int)binaryReader.ReadByte();
                    for (int index = 0; index < num2; ++index)
                    {
                        uint key = binaryReader.ReadUInt32();
                        if (Factory<IrisCardFactory, IrisCard>.Instance.Items.ContainsKey(key))
                            this.cards.Add(Factory<IrisCardFactory, IrisCard>.Instance.Items[key]);
                    }
                    this.locked = binaryReader.ReadBoolean();
                }
                if (num1 < (ushort)3)
                    return;
                this.rental = binaryReader.ReadBoolean();
                this.rentalTime = DateTime.FromBinary(binaryReader.ReadInt64());
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The Clear.
        /// </summary>
        public void Clear()
        {
            this.agi = (short)0;
            this.atk1 = (short)0;
            this.atk2 = (short)0;
            this.atk3 = (short)0;
            this.avoidCritical = (short)0;
            this.avoidMagic = (short)0;
            this.avoidMelee = (short)0;
            this.avoidRanged = (short)0;
            this.cha = (short)0;
            this.def = (short)0;
            this.dex = (short)0;
            this.hitCritical = (short)0;
            this.hitMagic = (short)0;
            this.hitMelee = (short)0;
            this.hitRanged = (short)0;
            this.hp = (short)0;
            this.hpRecover = (short)0;
            this.intel = (short)0;
            this.luk = (short)0;
            this.mag = (short)0;
            this.matk = (short)0;
            this.mdef = (short)0;
            this.mp = (short)0;
            this.mpRecover = (short)0;
            this.sp = (short)0;
            this.speedUp = (short)0;
            this.str = (short)0;
            this.vit = (short)0;
            this.volumeUp = (short)0;
            this.weightUp = (short)0;
            this.aspd = (short)0;
            this.cspd = (short)0;
        }

        /// <summary>
        /// The Clone.
        /// </summary>
        /// <returns>The <see cref="SagaDB.Item.Item"/>.</returns>
        public SagaDB.Item.Item Clone()
        {
            SagaDB.Item.Item obj = new SagaDB.Item.Item()
            {
                id = this.id,
                db_id = this.db_id,
                durability = this.durability,
                stack = this.stack,
                identified = this.identified,
                PossessionedActor = this.PossessionedActor,
                PossessionOwner = this.PossessionOwner,
                agi = this.agi,
                atk1 = this.atk1,
                atk2 = this.atk2,
                atk3 = this.atk3,
                avoidCritical = this.avoidCritical,
                avoidMagic = this.avoidMagic,
                avoidMelee = this.avoidMelee,
                avoidRanged = this.avoidRanged,
                cha = this.cha,
                def = this.def,
                dex = this.dex,
                hitCritical = this.hitCritical,
                hitMagic = this.hitMagic,
                hitMelee = this.hitMelee,
                hitRanged = this.hitRanged,
                hp = this.hp,
                hpRecover = this.hpRecover,
                intel = this.intel,
                luk = this.luk,
                mag = this.mag,
                matk = this.matk,
                mdef = this.mdef,
                mp = this.mp,
                mpRecover = this.mpRecover,
                sp = this.sp,
                speedUp = this.speedUp
            };
            obj.stack = this.stack;
            obj.str = this.str;
            obj.vit = this.vit;
            obj.volumeUp = this.volumeUp;
            obj.weightUp = this.weightUp;
            obj.refine = this.refine;
            obj.aspd = this.aspd;
            obj.cspd = this.cspd;
            obj.pict_id = this.pict_id;
            obj.currentSlot = this.currentSlot;
            obj.maxSlot = this.maxSlot;
            obj.locked = this.locked;
            foreach (IrisCard card in this.cards)
                obj.cards.Add(card);
            obj.rental = this.rental;
            obj.rentalTime = this.rentalTime;
            return obj;
        }

        /// <summary>
        /// Gets a value indicating whether Stackable.
        /// </summary>
        public bool Stackable
        {
            get
            {
                return this.BaseData.stock;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsEquipt.
        /// </summary>
        public bool IsEquipt
        {
            get
            {
                int itemType = (int)this.BaseData.itemType;
                return itemType >= 22 && itemType <= 70;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsParts.
        /// </summary>
        public bool IsParts
        {
            get
            {
                int itemType = (int)this.BaseData.itemType;
                return itemType >= 71 && itemType <= 78;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsWeapon.
        /// </summary>
        public bool IsWeapon
        {
            get
            {
                switch (this.BaseData.itemType)
                {
                    case ItemType.CLAW:
                    case ItemType.HAMMER:
                    case ItemType.STAFF:
                    case ItemType.SWORD:
                    case ItemType.AXE:
                    case ItemType.SPEAR:
                    case ItemType.BOW:
                    case ItemType.HANDBAG:
                    case ItemType.GUN:
                    case ItemType.ETC_WEAPON:
                    case ItemType.SHORT_SWORD:
                    case ItemType.RAPIER:
                    case ItemType.STRINGS:
                    case ItemType.BOOK:
                    case ItemType.DUALGUN:
                    case ItemType.RIFLE:
                    case ItemType.ROPE:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsArmor.
        /// </summary>
        public bool IsArmor
        {
            get
            {
                switch (this.BaseData.itemType)
                {
                    case ItemType.ARMOR_UPPER:
                    case ItemType.ONEPIECE:
                    case ItemType.COSTUME:
                    case ItemType.BODYSUIT:
                    case ItemType.WEDDING:
                    case ItemType.OVERALLS:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Gets the EquipSlot.
        /// </summary>
        public List<EnumEquipSlot> EquipSlot
        {
            get
            {
                List<EnumEquipSlot> enumEquipSlotList = new List<EnumEquipSlot>();
                if (!this.IsEquipt && !this.IsParts)
                    Logger.ShowDebug("Cannot equip a non equipment item!", Logger.defaultlogger);
                switch (this.BaseData.itemType)
                {
                    case ItemType.ACCESORY_HEAD:
                        enumEquipSlotList.Add(EnumEquipSlot.HEAD_ACCE);
                        enumEquipSlotList.Add(EnumEquipSlot.HEAD);
                        break;
                    case ItemType.ACCESORY_FACE:
                        enumEquipSlotList.Add(EnumEquipSlot.FACE_ACCE);
                        break;
                    case ItemType.HELM:
                    case ItemType.PARTS_HEAD:
                        enumEquipSlotList.Add(EnumEquipSlot.HEAD);
                        break;
                    case ItemType.BOOTS:
                        enumEquipSlotList.Add(EnumEquipSlot.SHOES);
                        break;
                    case ItemType.CLAW:
                    case ItemType.STRINGS:
                    case ItemType.DUALGUN:
                        enumEquipSlotList.Add(EnumEquipSlot.RIGHT_HAND);
                        enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                        break;
                    case ItemType.HAMMER:
                        if (this.BaseData.doubleHand)
                        {
                            enumEquipSlotList.Add(EnumEquipSlot.RIGHT_HAND);
                            enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                            break;
                        }
                        enumEquipSlotList.Add(EnumEquipSlot.RIGHT_HAND);
                        break;
                    case ItemType.ARMOR_UPPER:
                    case ItemType.PARTS_BODY:
                        enumEquipSlotList.Add(EnumEquipSlot.UPPER_BODY);
                        break;
                    case ItemType.FULLFACE:
                        enumEquipSlotList.Add(EnumEquipSlot.HEAD);
                        enumEquipSlotList.Add(EnumEquipSlot.FACE);
                        enumEquipSlotList.Add(EnumEquipSlot.HEAD_ACCE);
                        enumEquipSlotList.Add(EnumEquipSlot.FACE_ACCE);
                        break;
                    case ItemType.LONGBOOTS:
                        enumEquipSlotList.Add(EnumEquipSlot.SHOES);
                        enumEquipSlotList.Add(EnumEquipSlot.LOWER_BODY);
                        break;
                    case ItemType.SHIELD:
                    case ItemType.LEFT_HANDBAG:
                        enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                        break;
                    case ItemType.ONEPIECE:
                        enumEquipSlotList.Add(EnumEquipSlot.UPPER_BODY);
                        enumEquipSlotList.Add(EnumEquipSlot.LOWER_BODY);
                        break;
                    case ItemType.COSTUME:
                        enumEquipSlotList.Add(EnumEquipSlot.UPPER_BODY);
                        enumEquipSlotList.Add(EnumEquipSlot.HEAD_ACCE);
                        enumEquipSlotList.Add(EnumEquipSlot.HEAD);
                        enumEquipSlotList.Add(EnumEquipSlot.FACE);
                        enumEquipSlotList.Add(EnumEquipSlot.FACE_ACCE);
                        enumEquipSlotList.Add(EnumEquipSlot.LOWER_BODY);
                        enumEquipSlotList.Add(EnumEquipSlot.SHOES);
                        enumEquipSlotList.Add(EnumEquipSlot.SOCKS);
                        break;
                    case ItemType.BODYSUIT:
                    case ItemType.FACEBODYSUIT:
                        enumEquipSlotList.Add(EnumEquipSlot.UPPER_BODY);
                        enumEquipSlotList.Add(EnumEquipSlot.LOWER_BODY);
                        enumEquipSlotList.Add(EnumEquipSlot.SHOES);
                        enumEquipSlotList.Add(EnumEquipSlot.SOCKS);
                        break;
                    case ItemType.STAFF:
                    case ItemType.SWORD:
                    case ItemType.AXE:
                    case ItemType.SPEAR:
                    case ItemType.HANDBAG:
                    case ItemType.ETC_WEAPON:
                    case ItemType.SHORT_SWORD:
                    case ItemType.RAPIER:
                    case ItemType.BOOK:
                    case ItemType.THROW:
                    case ItemType.PARTS_BLOW:
                    case ItemType.PARTS_SLASH:
                    case ItemType.PARTS_STAB:
                    case ItemType.PARTS_LONGRANGE:
                    case ItemType.CARD:
                        if (this.BaseData.doubleHand)
                        {
                            enumEquipSlotList.Add(EnumEquipSlot.RIGHT_HAND);
                            enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                            break;
                        }
                        enumEquipSlotList.Add(EnumEquipSlot.RIGHT_HAND);
                        break;
                    case ItemType.BOW:
                        enumEquipSlotList.Add(EnumEquipSlot.RIGHT_HAND);
                        enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                        break;
                    case ItemType.GUN:
                        enumEquipSlotList.Add(EnumEquipSlot.RIGHT_HAND);
                        enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                        break;
                    case ItemType.ARMOR_LOWER:
                    case ItemType.SLACKS:
                    case ItemType.PARTS_LEG:
                        enumEquipSlotList.Add(EnumEquipSlot.LOWER_BODY);
                        break;
                    case ItemType.EQ_ALLSLOT:
                        enumEquipSlotList.Add(EnumEquipSlot.UPPER_BODY);
                        enumEquipSlotList.Add(EnumEquipSlot.HEAD);
                        enumEquipSlotList.Add(EnumEquipSlot.FACE);
                        enumEquipSlotList.Add(EnumEquipSlot.FACE_ACCE);
                        enumEquipSlotList.Add(EnumEquipSlot.CHEST_ACCE);
                        enumEquipSlotList.Add(EnumEquipSlot.RIGHT_HAND);
                        enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                        enumEquipSlotList.Add(EnumEquipSlot.BACK);
                        enumEquipSlotList.Add(EnumEquipSlot.LOWER_BODY);
                        enumEquipSlotList.Add(EnumEquipSlot.SHOES);
                        enumEquipSlotList.Add(EnumEquipSlot.SOCKS);
                        enumEquipSlotList.Add(EnumEquipSlot.PET);
                        break;
                    case ItemType.WEDDING:
                        enumEquipSlotList.Add(EnumEquipSlot.RIGHT_HAND);
                        enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                        enumEquipSlotList.Add(EnumEquipSlot.UPPER_BODY);
                        enumEquipSlotList.Add(EnumEquipSlot.LOWER_BODY);
                        enumEquipSlotList.Add(EnumEquipSlot.SOCKS);
                        enumEquipSlotList.Add(EnumEquipSlot.SHOES);
                        enumEquipSlotList.Add(EnumEquipSlot.PET);
                        break;
                    case ItemType.OVERALLS:
                        enumEquipSlotList.Add(EnumEquipSlot.UPPER_BODY);
                        enumEquipSlotList.Add(EnumEquipSlot.LOWER_BODY);
                        break;
                    case ItemType.SHOES:
                        enumEquipSlotList.Add(EnumEquipSlot.SHOES);
                        break;
                    case ItemType.HALFBOOTS:
                        enumEquipSlotList.Add(EnumEquipSlot.SHOES);
                        break;
                    case ItemType.ACCESORY_NECK:
                    case ItemType.JOINT_SYMBOL:
                        enumEquipSlotList.Add(EnumEquipSlot.CHEST_ACCE);
                        break;
                    case ItemType.BACKPACK:
                    case ItemType.PARTS_BACK:
                        enumEquipSlotList.Add(EnumEquipSlot.BACK);
                        break;
                    case ItemType.ACCESORY_FINGER:
                        enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                        break;
                    case ItemType.RIFLE:
                        enumEquipSlotList.Add(EnumEquipSlot.RIGHT_HAND);
                        enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                        break;
                    case ItemType.ROPE:
                        enumEquipSlotList.Add(EnumEquipSlot.RIGHT_HAND);
                        enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                        break;
                    case ItemType.SOCKS:
                        enumEquipSlotList.Add(EnumEquipSlot.SOCKS);
                        break;
                    case ItemType.BULLET:
                        enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                        break;
                    case ItemType.ARROW:
                        enumEquipSlotList.Add(EnumEquipSlot.LEFT_HAND);
                        break;
                    case ItemType.BACK_DEMON:
                        enumEquipSlotList.Add(EnumEquipSlot.PET);
                        enumEquipSlotList.Add(EnumEquipSlot.BACK);
                        break;
                    case ItemType.PET:
                    case ItemType.RIDE_PET:
                    case ItemType.PET_NEKOMATA:
                        enumEquipSlotList.Add(EnumEquipSlot.PET);
                        break;
                }
                return enumEquipSlotList;
            }
        }

        /// <summary>
        /// Gets the AttackType.
        /// </summary>
        public ATTACK_TYPE AttackType
        {
            get
            {
                switch (this.BaseData.itemType)
                {
                    case ItemType.CLAW:
                    case ItemType.SPEAR:
                    case ItemType.BOW:
                    case ItemType.GUN:
                    case ItemType.RAPIER:
                    case ItemType.DUALGUN:
                    case ItemType.RIFLE:
                    case ItemType.THROW:
                    case ItemType.ARROW:
                    case ItemType.PARTS_STAB:
                    case ItemType.PARTS_LONGRANGE:
                        return ATTACK_TYPE.STAB;
                    case ItemType.HAMMER:
                    case ItemType.STAFF:
                    case ItemType.AXE:
                    case ItemType.HANDBAG:
                    case ItemType.LEFT_HANDBAG:
                    case ItemType.ETC_WEAPON:
                    case ItemType.STRINGS:
                    case ItemType.BOOK:
                    case ItemType.PARTS_BLOW:
                        return ATTACK_TYPE.BLOW;
                    case ItemType.SWORD:
                    case ItemType.SHORT_SWORD:
                    case ItemType.PARTS_SLASH:
                    case ItemType.CARD:
                        return ATTACK_TYPE.SLASH;
                    default:
                        return ATTACK_TYPE.BLOW;
                }
            }
        }

        /// <summary>
        /// 装备所有插的卡的能力向量
        /// <param name="deck">是否是牌面</param>.
        /// </summary>
        /// <param name="deck">是否是牌面.</param>
        /// <returns>The <see cref="List{AbilityVector}"/>.</returns>
        public List<AbilityVector> AbilityVectors(bool deck)
        {
            List<AbilityVector> abilityVectorList = new List<AbilityVector>();
            List<IrisCard> irisCardList = new List<IrisCard>();
            if (deck && this.cards.Count > 0)
                irisCardList.Add(this.cards[this.cards.Count - 1]);
            else
                irisCardList = this.cards;
            foreach (IrisCard irisCard in irisCardList)
            {
                foreach (AbilityVector key in irisCard.Abilities.Keys)
                {
                    if (!abilityVectorList.Contains(key))
                        abilityVectorList.Add(key);
                }
            }
            return abilityVectorList;
        }

        /// <summary>
        /// The VectorValues.
        /// </summary>
        /// <param name="deck">是否是牌面.</param>
        /// <param name="lv">是否是取得向量等级信息而非值信息.</param>
        /// <returns>.</returns>
        public Dictionary<AbilityVector, int> VectorValues(bool deck, bool lv)
        {
            Dictionary<AbilityVector, int> dictionary1 = new Dictionary<AbilityVector, int>();
            List<IrisCard> irisCardList = new List<IrisCard>();
            if (deck && this.cards.Count > 0)
                irisCardList.Add(this.cards[this.cards.Count - 1]);
            else
                irisCardList = this.cards;
            foreach (IrisCard irisCard in irisCardList)
            {
                foreach (AbilityVector key in irisCard.Abilities.Keys)
                {
                    if (!dictionary1.ContainsKey(key))
                    {
                        dictionary1.Add(key, irisCard.Abilities[key]);
                    }
                    else
                    {
                        Dictionary<AbilityVector, int> dictionary2;
                        AbilityVector index;
                        (dictionary2 = dictionary1)[index = key] = dictionary2[index] + irisCard.Abilities[key];
                    }
                }
            }
            if (lv)
            {
                int[] numArray = new int[10]
                {
          1,
          30,
          80,
          150,
          250,
          370,
          510,
          660,
          820,
          999
                };
                foreach (AbilityVector index in dictionary1.Keys.ToArray<AbilityVector>())
                {
                    int num1 = dictionary1[index];
                    int num2 = 0;
                    foreach (int num3 in numArray)
                    {
                        if (num1 >= num3)
                            ++num2;
                        else
                            break;
                    }
                    dictionary1[index] = num2;
                }
            }
            return dictionary1;
        }

        /// <summary>
        /// The ReleaseAbilities.
        /// </summary>
        /// <param name="deck">.</param>
        /// <returns>.</returns>
        public Dictionary<ReleaseAbility, int> ReleaseAbilities(bool deck)
        {
            return SagaDB.Item.Item.ReleaseAbilities(this.VectorValues(deck, true));
        }

        /// <summary>
        /// The ReleaseAbilities.
        /// </summary>
        /// <param name="vectors">The vectors<see cref="Dictionary{AbilityVector, int}"/>.</param>
        /// <returns>The <see cref="Dictionary{ReleaseAbility, int}"/>.</returns>
        public static Dictionary<ReleaseAbility, int> ReleaseAbilities(Dictionary<AbilityVector, int> vectors)
        {
            Dictionary<ReleaseAbility, int> dictionary1 = new Dictionary<ReleaseAbility, int>();
            foreach (AbilityVector key1 in vectors.Keys)
            {
                Dictionary<ReleaseAbility, int> ability = key1.Abilities[(byte)vectors[key1]];
                foreach (ReleaseAbility key2 in ability.Keys)
                {
                    if (dictionary1.ContainsKey(key2))
                    {
                        Dictionary<ReleaseAbility, int> dictionary2;
                        ReleaseAbility index;
                        (dictionary2 = dictionary1)[index = key2] = dictionary2[index] + ability[key2];
                    }
                    else
                        dictionary1.Add(key2, ability[key2]);
                }
            }
            return dictionary1;
        }

        /// <summary>
        /// The Elements.
        /// </summary>
        /// <param name="deck">是否是牌面.</param>
        /// <returns>.</returns>
        public Dictionary<Elements, int> Elements(bool deck)
        {
            Dictionary<Elements, int> dictionary1 = new Dictionary<Elements, int>();
            List<IrisCard> irisCardList = new List<IrisCard>();
            if (deck && this.cards.Count > 0)
                irisCardList.Add(this.cards[this.cards.Count - 1]);
            else
                irisCardList = this.cards;
            dictionary1.Add(SagaLib.Elements.Neutral, 0);
            dictionary1.Add(SagaLib.Elements.Fire, 0);
            dictionary1.Add(SagaLib.Elements.Water, 0);
            dictionary1.Add(SagaLib.Elements.Wind, 0);
            dictionary1.Add(SagaLib.Elements.Earth, 0);
            dictionary1.Add(SagaLib.Elements.Holy, 0);
            dictionary1.Add(SagaLib.Elements.Dark, 0);
            foreach (IrisCard irisCard in irisCardList)
            {
                foreach (Elements key in irisCard.Elements.Keys)
                {
                    if (!dictionary1.ContainsKey(key))
                    {
                        dictionary1.Add(key, irisCard.Elements[key]);
                    }
                    else
                    {
                        Dictionary<Elements, int> dictionary2;
                        Elements index;
                        (dictionary2 = dictionary1)[index = key] = dictionary2[index] + irisCard.Elements[key];
                    }
                }
            }
            return dictionary1;
        }

        /// <summary>
        /// Defines the <see cref="ItemData" />.
        /// </summary>
        public class ItemData
        {
            /// <summary>
            /// Defines the element.
            /// </summary>
            public Dictionary<Elements, short> element = new Dictionary<Elements, short>();

            /// <summary>
            /// Defines the abnormalStatus.
            /// </summary>
            public Dictionary<AbnormalStatus, short> abnormalStatus = new Dictionary<AbnormalStatus, short>();

            /// <summary>
            /// Defines the possibleRace.
            /// </summary>
            public Dictionary<PC_RACE, bool> possibleRace = new Dictionary<PC_RACE, bool>();

            /// <summary>
            /// Defines the possibleGender.
            /// </summary>
            public Dictionary<PC_GENDER, bool> possibleGender = new Dictionary<PC_GENDER, bool>();

            /// <summary>
            /// Defines the possibleJob.
            /// </summary>
            public Dictionary<PC_JOB, bool> possibleJob = new Dictionary<PC_JOB, bool>();

            /// <summary>
            /// Defines the possibleCountry.
            /// </summary>
            public Dictionary<Country, bool> possibleCountry = new Dictionary<Country, bool>();

            /// <summary>
            /// Defines the name.
            /// </summary>
            public string name;

            /// <summary>
            /// Defines the desc.
            /// </summary>
            public string desc;

            /// <summary>
            /// Defines the id.
            /// </summary>
            public uint id;

            /// <summary>
            /// Defines the price.
            /// </summary>
            public uint price;

            /// <summary>
            /// Defines the iconID.
            /// </summary>
            public uint iconID;

            /// <summary>
            /// Defines the imageID.
            /// </summary>
            public uint imageID;

            /// <summary>
            /// Defines the equipVolume.
            /// </summary>
            public ushort equipVolume;

            /// <summary>
            /// Defines the possessionWeight.
            /// </summary>
            public ushort possessionWeight;

            /// <summary>
            /// Defines the weight.
            /// </summary>
            public ushort weight;

            /// <summary>
            /// Defines the volume.
            /// </summary>
            public ushort volume;

            /// <summary>
            /// Defines the itemType.
            /// </summary>
            public ItemType itemType;

            /// <summary>
            /// Defines the repairItem.
            /// </summary>
            public uint repairItem;

            /// <summary>
            /// Defines the enhancementItem.
            /// </summary>
            public uint enhancementItem;

            /// <summary>
            /// Defines the events.
            /// </summary>
            public uint events;

            /// <summary>
            /// Defines the receipt.
            /// </summary>
            public bool receipt;

            /// <summary>
            /// Defines the dye.
            /// </summary>
            public bool dye;

            /// <summary>
            /// Defines the stock.
            /// </summary>
            public bool stock;

            /// <summary>
            /// Defines the doubleHand.
            /// </summary>
            public bool doubleHand;

            /// <summary>
            /// Defines the usable.
            /// </summary>
            public bool usable;

            /// <summary>
            /// Defines the color.
            /// </summary>
            public byte color;

            /// <summary>
            /// Defines the durability.
            /// </summary>
            public ushort durability;

            /// <summary>
            /// Defines the jointJob.
            /// </summary>
            public PC_JOB jointJob;

            /// <summary>
            /// Defines the eventID.
            /// </summary>
            public uint eventID;

            /// <summary>
            /// Defines the effectID.
            /// </summary>
            public uint effectID;

            /// <summary>
            /// Defines the activateSkill.
            /// </summary>
            public ushort activateSkill;

            /// <summary>
            /// Defines the possibleSkill.
            /// </summary>
            public ushort possibleSkill;

            /// <summary>
            /// Defines the passiveSkill.
            /// </summary>
            public ushort passiveSkill;

            /// <summary>
            /// Defines the possessionSkill.
            /// </summary>
            public ushort possessionSkill;

            /// <summary>
            /// Defines the possessionPassiveSkill.
            /// </summary>
            public ushort possessionPassiveSkill;

            /// <summary>
            /// Defines the target.
            /// </summary>
            public TargetType target;

            /// <summary>
            /// Defines the activeType.
            /// </summary>
            public ActiveType activeType;

            /// <summary>
            /// Defines the range.
            /// </summary>
            public byte range;

            /// <summary>
            /// Defines the duration.
            /// </summary>
            public uint duration;

            /// <summary>
            /// Defines the effectRange.
            /// </summary>
            public byte effectRange;

            /// <summary>
            /// Defines the cast.
            /// </summary>
            public uint cast;

            /// <summary>
            /// Defines the delay.
            /// </summary>
            public uint delay;

            /// <summary>
            /// Defines the hp.
            /// </summary>
            public short hp;

            /// <summary>
            /// Defines the mp.
            /// </summary>
            public short mp;

            /// <summary>
            /// Defines the sp.
            /// </summary>
            public short sp;

            /// <summary>
            /// Defines the weightUp.
            /// </summary>
            public short weightUp;

            /// <summary>
            /// Defines the volumeUp.
            /// </summary>
            public short volumeUp;

            /// <summary>
            /// Defines the speedUp.
            /// </summary>
            public short speedUp;

            /// <summary>
            /// Defines the str.
            /// </summary>
            public short str;

            /// <summary>
            /// Defines the dex.
            /// </summary>
            public short dex;

            /// <summary>
            /// Defines the intel.
            /// </summary>
            public short intel;

            /// <summary>
            /// Defines the vit.
            /// </summary>
            public short vit;

            /// <summary>
            /// Defines the agi.
            /// </summary>
            public short agi;

            /// <summary>
            /// Defines the mag.
            /// </summary>
            public short mag;

            /// <summary>
            /// Defines the luk.
            /// </summary>
            public short luk;

            /// <summary>
            /// Defines the cha.
            /// </summary>
            public short cha;

            /// <summary>
            /// Defines the atk1.
            /// </summary>
            public short atk1;

            /// <summary>
            /// Defines the atk2.
            /// </summary>
            public short atk2;

            /// <summary>
            /// Defines the atk3.
            /// </summary>
            public short atk3;

            /// <summary>
            /// Defines the matk.
            /// </summary>
            public short matk;

            /// <summary>
            /// Defines the def.
            /// </summary>
            public short def;

            /// <summary>
            /// Defines the mdef.
            /// </summary>
            public short mdef;

            /// <summary>
            /// Defines the hitMelee.
            /// </summary>
            public short hitMelee;

            /// <summary>
            /// Defines the hitRanged.
            /// </summary>
            public short hitRanged;

            /// <summary>
            /// Defines the hitMagic.
            /// </summary>
            public short hitMagic;

            /// <summary>
            /// Defines the avoidMelee.
            /// </summary>
            public short avoidMelee;

            /// <summary>
            /// Defines the avoidRanged.
            /// </summary>
            public short avoidRanged;

            /// <summary>
            /// Defines the avoidMagic.
            /// </summary>
            public short avoidMagic;

            /// <summary>
            /// Defines the hitCritical.
            /// </summary>
            public short hitCritical;

            /// <summary>
            /// Defines the avoidCritical.
            /// </summary>
            public short avoidCritical;

            /// <summary>
            /// Defines the hpRecover.
            /// </summary>
            public short hpRecover;

            /// <summary>
            /// Defines the mpRecover.
            /// </summary>
            public short mpRecover;

            /// <summary>
            /// Defines the possibleLv.
            /// </summary>
            public byte possibleLv;

            /// <summary>
            /// Defines the possibleStr.
            /// </summary>
            public ushort possibleStr;

            /// <summary>
            /// Defines the possibleDex.
            /// </summary>
            public ushort possibleDex;

            /// <summary>
            /// Defines the possibleInt.
            /// </summary>
            public ushort possibleInt;

            /// <summary>
            /// Defines the possibleVit.
            /// </summary>
            public ushort possibleVit;

            /// <summary>
            /// Defines the possibleAgi.
            /// </summary>
            public ushort possibleAgi;

            /// <summary>
            /// Defines the possibleMag.
            /// </summary>
            public ushort possibleMag;

            /// <summary>
            /// Defines the possibleLuk.
            /// </summary>
            public ushort possibleLuk;

            /// <summary>
            /// Defines the possibleCha.
            /// </summary>
            public ushort possibleCha;

            /// <summary>
            /// Defines the marionetteID.
            /// </summary>
            public uint marionetteID;

            /// <summary>
            /// Defines the petID.
            /// </summary>
            public uint petID;

            /// <summary>
            /// Defines the currentSlot.
            /// </summary>
            public byte currentSlot;

            /// <summary>
            /// Defines the maxSlot.
            /// </summary>
            public byte maxSlot;

            /// <summary>
            /// Defines the handMotion.
            /// </summary>
            public byte handMotion;

            /// <summary>
            /// Defines the handMotion2.
            /// </summary>
            public byte handMotion2;

            /// <summary>
            /// Defines the noTrade.
            /// </summary>
            public bool noTrade;

            /// <summary>
            /// The ToString.
            /// </summary>
            /// <returns>The <see cref="string"/>.</returns>
            public override string ToString()
            {
                return this.name;
            }
        }
    }
}
