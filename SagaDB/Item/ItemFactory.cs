namespace SagaDB.Item
{
    using SagaDB.Actor;
    using SagaLib;
    using System;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="ItemFactory" />.
    /// </summary>
    public class ItemFactory : Factory<ItemFactory, SagaDB.Item.Item.ItemData>
    {
        /// <summary>
        /// Defines the readDesc.
        /// </summary>
        private bool readDesc;

        /// <summary>
        /// Sets a value indicating whether ReadDesc.
        /// </summary>
        public bool ReadDesc
        {
            set
            {
                this.readDesc = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemFactory"/> class.
        /// </summary>
        public ItemFactory()
        {
            this.loadingTab = "Loading item database";
            this.loadedTab = " items loaded.";
            this.databaseName = "Item";
            this.FactoryType = FactoryType.CSV;
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="SagaDB.Item.Item.ItemData"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, SagaDB.Item.Item.ItemData item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item.ItemData"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(SagaDB.Item.Item.ItemData item)
        {
            return item.id;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item.ItemData"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(SagaDB.Item.Item.ItemData item, string[] paras)
        {
            item.id = uint.Parse(paras[0]);
            item.imageID = uint.Parse(paras[1]);
            item.iconID = uint.Parse(paras[2]);
            item.name = paras[3];
            item.itemType = (ItemType)Enum.Parse(typeof(ItemType), paras[4]);
            item.price = uint.Parse(paras[5]);
            item.weight = (ushort)float.Parse(paras[6]);
            item.volume = (ushort)float.Parse(paras[7]);
            item.equipVolume = ushort.Parse(paras[8]);
            item.possessionWeight = ushort.Parse(paras[9]);
            item.repairItem = (uint)int.Parse(paras[10]);
            item.enhancementItem = (uint)int.Parse(paras[11]);
            item.events = uint.Parse(paras[13]);
            item.receipt = this.toBool(paras[14]);
            item.dye = this.toBool(paras[15]);
            item.stock = this.toBool(paras[16]);
            item.doubleHand = this.toBool(paras[17]);
            item.usable = this.toBool(paras[18]);
            item.color = byte.Parse(paras[19]);
            item.durability = ushort.Parse(paras[20]);
            item.jointJob = !(paras[21] != "0") ? PC_JOB.NONE : (PC_JOB)(int.Parse(paras[21]) + 1000);
            item.currentSlot = byte.Parse(paras[22]);
            item.maxSlot = byte.Parse(paras[23]);
            item.eventID = uint.Parse(paras[24]);
            item.effectID = uint.Parse(paras[25]);
            item.activateSkill = ushort.Parse(paras[26]);
            item.possibleSkill = ushort.Parse(paras[27]);
            item.passiveSkill = ushort.Parse(paras[28]);
            item.possessionSkill = ushort.Parse(paras[29]);
            item.possessionPassiveSkill = ushort.Parse(paras[30]);
            item.target = (TargetType)Enum.Parse(typeof(TargetType), paras[31]);
            item.activeType = (ActiveType)Enum.Parse(typeof(ActiveType), paras[32]);
            item.range = (byte)int.Parse(paras[33]);
            item.duration = uint.Parse(paras[34]);
            item.effectRange = byte.Parse(paras[35]);
            item.cast = uint.Parse(paras[37]);
            item.delay = uint.Parse(paras[38]);
            item.hp = short.Parse(paras[39]);
            item.mp = short.Parse(paras[40]);
            item.sp = short.Parse(paras[41]);
            item.weightUp = short.Parse(paras[42]);
            item.volumeUp = short.Parse(paras[43]);
            item.speedUp = short.Parse(paras[44]);
            item.str = short.Parse(paras[45]);
            item.mag = short.Parse(paras[46]);
            item.vit = short.Parse(paras[47]);
            item.dex = short.Parse(paras[48]);
            item.agi = short.Parse(paras[49]);
            item.intel = short.Parse(paras[50]);
            item.luk = short.Parse(paras[51]);
            item.cha = short.Parse(paras[52]);
            item.atk1 = short.Parse(paras[53]);
            item.atk2 = short.Parse(paras[54]);
            item.atk3 = short.Parse(paras[55]);
            item.matk = short.Parse(paras[56]);
            item.def = short.Parse(paras[57]);
            item.mdef = short.Parse(paras[58]);
            item.hitMelee = short.Parse(paras[59]);
            item.hitRanged = short.Parse(paras[60]);
            item.hitMagic = short.Parse(paras[61]);
            item.avoidMelee = short.Parse(paras[62]);
            item.avoidRanged = short.Parse(paras[63]);
            item.avoidMagic = short.Parse(paras[64]);
            item.hitCritical = short.Parse(paras[65]);
            item.avoidCritical = short.Parse(paras[66]);
            item.hpRecover = short.Parse(paras[67]);
            item.mpRecover = short.Parse(paras[68]);
            for (int index = 0; index < 7; ++index)
                item.element.Add((Elements)index, short.Parse(paras[69 + index]));
            for (int index = 0; index < 9; ++index)
                item.abnormalStatus.Add((AbnormalStatus)index, short.Parse(paras[76 + index]));
            for (int index = 0; index < 4; ++index)
                item.possibleRace.Add((PC_RACE)index, this.toBool(paras[85 + index]));
            for (int index = 0; index < 2; ++index)
                item.possibleGender.Add((PC_GENDER)index, this.toBool(paras[89 + index]));
            item.possibleLv = byte.Parse(paras[91]);
            item.possibleStr = ushort.Parse(paras[92]);
            item.possibleMag = ushort.Parse(paras[93]);
            item.possibleVit = ushort.Parse(paras[94]);
            item.possibleDex = ushort.Parse(paras[95]);
            item.possibleAgi = ushort.Parse(paras[96]);
            item.possibleInt = ushort.Parse(paras[97]);
            item.possibleLuk = ushort.Parse(paras[98]);
            item.possibleCha = ushort.Parse(paras[99]);
            string[] names = Enum.GetNames(typeof(PC_JOB));
            for (int index = 0; index < 37; ++index)
                item.possibleJob.Add((PC_JOB)Enum.Parse(typeof(PC_JOB), names[index]), this.toBool(paras[100 + index]));
            for (int index = 0; index < 4; ++index)
                item.possibleCountry.Add((Country)index, this.toBool(paras[137 + index]));
            item.possibleJob.Add(PC_JOB.BREEDER, this.toBool(paras[145]));
            item.possibleJob.Add(PC_JOB.GARDNER, this.toBool(paras[146]));
            item.marionetteID = uint.Parse(paras[152]);
            item.petID = uint.Parse(paras[153]);
            item.handMotion = byte.Parse(paras[154]);
            item.handMotion2 = byte.Parse(paras[155]);
            if (this.readDesc)
                item.desc = paras[160];
            item.noTrade = int.Parse(paras[162]) > 0;
        }

        /// <summary>
        /// The toBool.
        /// </summary>
        /// <param name="input">The input<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool toBool(string input)
        {
            return input == "1";
        }

        /// <summary>
        /// The GetItem.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaDB.Item.Item"/>.</returns>
        public SagaDB.Item.Item GetItem(uint id)
        {
            return this.GetItem(id, true);
        }

        /// <summary>
        /// The GetItem.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <param name="identified">The identified<see cref="bool"/>.</param>
        /// <returns>The <see cref="SagaDB.Item.Item"/>.</returns>
        public SagaDB.Item.Item GetItem(uint id, bool identified)
        {
            if (this.items.ContainsKey(id))
            {
                SagaDB.Item.Item obj = new SagaDB.Item.Item(this.items[id]);
                obj.Stack = (ushort)1;
                obj.Durability = obj.BaseData.durability;
                obj.Identified = identified;
                return obj;
            }
            Logger.ShowWarning("Item:" + id.ToString() + " not found! Creating dummy Item.");
            SagaDB.Item.Item obj1 = new SagaDB.Item.Item(this.items[10000000U]);
            obj1.Stack = (ushort)1;
            obj1.Durability = obj1.BaseData.durability;
            obj1.Identified = identified;
            return obj1;
        }
    }
}
