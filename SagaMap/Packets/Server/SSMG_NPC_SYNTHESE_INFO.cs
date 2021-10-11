namespace SagaMap.Packets.Server
{
    using SagaDB.Synthese;
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_SYNTHESE_INFO" />.
    /// </summary>
    public class SSMG_NPC_SYNTHESE_INFO : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_SYNTHESE_INFO"/> class.
        /// </summary>
        public SSMG_NPC_SYNTHESE_INFO()
        {
            this.data = new byte[42];
            this.offset = (ushort)2;
            this.ID = (ushort)5046;
        }

        /// <summary>
        /// Sets the Synthese.
        /// </summary>
        public SyntheseInfo Synthese
        {
            set
            {
                if (value.Materials.Count > 4 || value.Products.Count > 4)
                    throw new ArgumentOutOfRangeException();
                this.PutByte((byte)4, (ushort)2);
                int num1 = 0;
                foreach (ItemElement material in value.Materials)
                {
                    this.PutUInt(material.ID, (ushort)(3 + num1 * 4));
                    ++num1;
                }
                this.PutByte((byte)4, (ushort)19);
                int num2 = 0;
                foreach (ItemElement material in value.Materials)
                {
                    this.PutUShort(material.Count, (ushort)(20 + num2 * 2));
                    ++num2;
                }
                this.PutUInt(value.RequiredTool, (ushort)28);
                this.PutUInt(value.ID, (ushort)32);
                this.PutUInt(value.Gold, (ushort)36);
                this.PutByte((byte)1, (ushort)40);
            }
        }
    }
}
