namespace SagaDB
{
    using SagaDB.Actor;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Account" />.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Defines the account_id.
        /// </summary>
        private int account_id = -1;

        /// <summary>
        /// Defines the lastIP.
        /// </summary>
        private string lastIP = "";

        /// <summary>
        /// Defines the chars.
        /// </summary>
        private List<ActorPC> chars = new List<ActorPC>();

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the password.
        /// </summary>
        private string password;

        /// <summary>
        /// Defines the deletepass.
        /// </summary>
        private string deletepass;

        /// <summary>
        /// Defines the gmlevel.
        /// </summary>
        private byte gmlevel;

        /// <summary>
        /// Defines the bank.
        /// </summary>
        private uint bank;

        /// <summary>
        /// Defines the banned.
        /// </summary>
        private bool banned;

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        /// <summary>
        /// Gets or sets the DeletePassword.
        /// </summary>
        public string DeletePassword
        {
            get
            {
                return this.deletepass;
            }
            set
            {
                this.deletepass = value;
            }
        }

        /// <summary>
        /// Gets or sets the AccountID.
        /// </summary>
        public int AccountID
        {
            get
            {
                return this.account_id;
            }
            set
            {
                this.account_id = value;
            }
        }

        /// <summary>
        /// Gets or sets the Characters.
        /// </summary>
        public List<ActorPC> Characters
        {
            get
            {
                return this.chars;
            }
            set
            {
                this.chars = value;
            }
        }

        /// <summary>
        /// Gets or sets the GMLevel.
        /// </summary>
        public byte GMLevel
        {
            get
            {
                return this.gmlevel;
            }
            set
            {
                this.gmlevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the Bank.
        /// </summary>
        public uint Bank
        {
            get
            {
                return this.bank;
            }
            set
            {
                this.bank = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Banned.
        /// </summary>
        public bool Banned
        {
            get
            {
                return this.banned;
            }
            set
            {
                this.banned = value;
            }
        }

        /// <summary>
        /// Gets or sets the LastIP.
        /// </summary>
        public string LastIP
        {
            get
            {
                return this.lastIP;
            }
            set
            {
                this.lastIP = value;
            }
        }
    }
}
