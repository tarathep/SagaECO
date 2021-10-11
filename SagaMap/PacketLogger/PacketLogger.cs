namespace SagaMap.PacketLogger
{
    using SagaLib;
    using SagaMap.Network.Client;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="PacketLogger" />.
    /// </summary>
    public class PacketLogger
    {
        /// <summary>
        /// Defines the client.
        /// </summary>
        private MapClient client;

        /// <summary>
        /// Defines the fs.
        /// </summary>
        private FileStream fs;

        /// <summary>
        /// Defines the bw.
        /// </summary>
        private BinaryWriter bw;

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketLogger"/> class.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        public PacketLogger(MapClient client)
        {
            this.client = client;
            client.netIO.OnReceivePacket += new NetIO.PacketEventArg(this.OnReceivePacket);
            client.netIO.OnSendPacket += new NetIO.PacketEventArg(this.OnSendPacket);
            if (!Directory.Exists("Log/PacketLog"))
                Directory.CreateDirectory("Log/PacketLog");
            DateTime now = DateTime.Now;
            this.fs = new FileStream(string.Format("Log/PacketLog/{0}({1})_{2}-{3}-{4} {5}-{6}-{7}.dat", (object)client.Character.Name, (object)client.Character.Account.Name, (object)now.Year, (object)now.Month, (object)now.Day, (object)now.Hour, (object)now.Minute, (object)now.Second), FileMode.Create);
            this.bw = new BinaryWriter((Stream)this.fs);
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            this.client.netIO.OnReceivePacket -= new NetIO.PacketEventArg(this.OnReceivePacket);
            this.client.netIO.OnSendPacket -= new NetIO.PacketEventArg(this.OnSendPacket);
            this.fs.Flush();
            this.fs.Close();
        }

        /// <summary>
        /// The OnReceivePacket.
        /// </summary>
        /// <param name="p">The p<see cref="Packet"/>.</param>
        private void OnReceivePacket(Packet p)
        {
            lock (this.fs)
            {
                long position = this.bw.BaseStream.Position;
                try
                {
                    if (p.ID == (ushort)4600 || p.ID == (ushort)4601 || p.ID == (ushort)4005 || p.ID == (ushort)4006)
                        return;
                    this.bw.Write((byte)0);
                    this.bw.Write(DateTime.Now.ToBinary());
                    this.bw.Write(p.ID);
                    byte[] bytes1 = Encoding.UTF8.GetBytes(p.ToString().Replace("SagaMap.Packets.Client.", ""));
                    this.bw.Write((short)bytes1.Length);
                    this.bw.Write(bytes1);
                    PropertyInfo[] properties = p.GetType().GetProperties();
                    this.bw.Write((short)(properties.Length - 1));
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        if (!(propertyInfo.Name == "ID"))
                        {
                            byte[] bytes2 = Encoding.UTF8.GetBytes(propertyInfo.Name);
                            this.bw.Write((short)bytes2.Length);
                            this.bw.Write(bytes2);
                            byte[] bytes3 = Encoding.UTF8.GetBytes(propertyInfo.GetGetMethod().Invoke((object)p, (object[])null).ToString());
                            this.bw.Write((short)bytes3.Length);
                            this.bw.Write(bytes3);
                        }
                    }
                    bool flag = this.ifNeedInventory(p);
                    this.bw.Write(flag);
                    if (flag)
                    {
                        byte[] bytes2 = this.client.Character.Inventory.ToBytes();
                        this.bw.Write((short)bytes2.Length);
                        this.bw.Write(bytes2);
                    }
                }
                catch
                {
                    this.bw.BaseStream.Position = position;
                }
            }
        }

        /// <summary>
        /// The OnSendPacket.
        /// </summary>
        /// <param name="p">The p<see cref="Packet"/>.</param>
        private void OnSendPacket(Packet p)
        {
            lock (this.fs)
            {
                long position = this.bw.BaseStream.Position;
                try
                {
                    if (p.ID == (ushort)4600 || p.ID == (ushort)4601 || p.ID == (ushort)4005 || p.ID == (ushort)4006)
                        return;
                    this.bw.Write((byte)1);
                    this.bw.Write(DateTime.Now.ToBinary());
                    byte[] bytes1 = Encoding.UTF8.GetBytes(p.ToString().Replace("SagaMap.Packets.Server.", ""));
                    this.bw.Write((short)bytes1.Length);
                    this.bw.Write(bytes1);
                    this.bw.Write((short)p.data.Length);
                    this.bw.Write(p.data);
                    bool flag = this.ifNeedInventory(p);
                    this.bw.Write(flag);
                    if (flag)
                    {
                        byte[] bytes2 = this.client.Character.Inventory.ToBytes();
                        this.bw.Write((short)bytes2.Length);
                        this.bw.Write(bytes2);
                    }
                }
                catch
                {
                    this.bw.BaseStream.Position = position;
                }
            }
        }

        /// <summary>
        /// The ifNeedInventory.
        /// </summary>
        /// <param name="p">The p<see cref="Packet"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool ifNeedInventory(Packet p)
        {
            switch (p.ID)
            {
                case 2000:
                case 2020:
                case 2500:
                case 2530:
                case 2535:
                case 2580:
                    return true;
                default:
                    return false;
            }
        }
    }
}
