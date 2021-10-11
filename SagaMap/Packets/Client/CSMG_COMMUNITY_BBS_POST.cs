namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_COMMUNITY_BBS_POST" />.
    /// </summary>
    public class CSMG_COMMUNITY_BBS_POST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_COMMUNITY_BBS_POST"/> class.
        /// </summary>
        public CSMG_COMMUNITY_BBS_POST()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title
        {
            get
            {
                byte num = this.GetByte((ushort)2);
                return Global.Unicode.GetString(this.GetBytes((ushort)num, (ushort)3)).Replace("\0", "");
            }
        }

        /// <summary>
        /// Gets the Content.
        /// </summary>
        public string Content
        {
            get
            {
                byte num1 = this.GetByte((ushort)2);
                int num2 = (int)this.GetByte((ushort)(3U + (uint)num1));
                if (num2 != 253)
                    return Global.Unicode.GetString(this.GetBytes((ushort)num2, (ushort)(4U + (uint)num1))).Replace("\0", "");
                int num3 = this.GetInt((ushort)(4U + (uint)num1));
                return Global.Unicode.GetString(this.GetBytes((ushort)num3, (ushort)(8U + (uint)num1))).Replace("\0", "");
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_COMMUNITY_BBS_POST();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnBBSPost(this);
        }
    }
}
