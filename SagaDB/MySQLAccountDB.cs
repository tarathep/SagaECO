namespace SagaDB
{
    using MySql.Data.MySqlClient;
    using SagaLib;
    using System;
    using System.Data;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="MySQLAccountDB" />.
    /// </summary>
    public class MySQLAccountDB : MySQLConnectivity, AccountDB
    {
        /// <summary>
        /// Defines the encoder.
        /// </summary>
        private Encoding encoder = Encoding.UTF8;

        /// <summary>
        /// Defines the tick.
        /// </summary>
        private DateTime tick = DateTime.Now;

        /// <summary>
        /// Defines the host.
        /// </summary>
        private string host;

        /// <summary>
        /// Defines the port.
        /// </summary>
        private string port;

        /// <summary>
        /// Defines the database.
        /// </summary>
        private string database;

        /// <summary>
        /// Defines the dbuser.
        /// </summary>
        private string dbuser;

        /// <summary>
        /// Defines the dbpass.
        /// </summary>
        private string dbpass;

        /// <summary>
        /// Defines the isconnected.
        /// </summary>
        private bool isconnected;

        /// <summary>
        /// Initializes a new instance of the <see cref="MySQLAccountDB"/> class.
        /// </summary>
        /// <param name="host">The host<see cref="string"/>.</param>
        /// <param name="port">The port<see cref="int"/>.</param>
        /// <param name="database">The database<see cref="string"/>.</param>
        /// <param name="user">The user<see cref="string"/>.</param>
        /// <param name="pass">The pass<see cref="string"/>.</param>
        public MySQLAccountDB(string host, int port, string database, string user, string pass)
        {
            this.host = host;
            this.port = port.ToString();
            this.dbuser = user;
            this.dbpass = pass;
            this.database = database;
            this.isconnected = false;
            try
            {
                this.db = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;SslMode=none;", (object)database, (object)host, (object)port, (object)user, (object)pass));
                this.dbinactive = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;SslMode=none;", (object)database, (object)host, (object)port, (object)user, (object)pass));
                this.db.Open();
            }
            catch (MySqlException ex)
            {
                SagaLib.Logger.ShowSQL((Exception)ex, (SagaLib.Logger)null);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex, (SagaLib.Logger)null);
            }
            if (this.db == null)
                return;
            if (this.db.State != ConnectionState.Closed)
                this.isconnected = true;
            else
                Console.WriteLine("SQL Connection error");
        }

        /// <summary>
        /// The Connect.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Connect()
        {
            if (!this.isconnected)
            {
                if (this.db.State == ConnectionState.Open)
                {
                    this.isconnected = true;
                    return true;
                }
                try
                {
                    this.db.Open();
                }
                catch (Exception ex)
                {
                }
                if (this.db != null && this.db.State == ConnectionState.Closed)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// The isConnected.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool isConnected()
        {
            if (this.isconnected)
            {
                if ((DateTime.Now - this.tick).TotalMinutes > 5.0)
                {
                    SagaLib.Logger.ShowSQL("AccountDB:Pinging SQL Server to keep the connection alive", (SagaLib.Logger)null);
                    bool blocked = ClientManager.Blocked;
                    if (blocked)
                        ClientManager.LeaveCriticalArea();
                    DatabaseWaitress.EnterCriticalArea();
                    MySqlConnection mySqlConnection = this.dbinactive;
                    if (mySqlConnection.State == ConnectionState.Open)
                        mySqlConnection.Close();
                    try
                    {
                        mySqlConnection.Open();
                    }
                    catch (Exception ex)
                    {
                        mySqlConnection = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};", (object)this.database, (object)this.host, (object)this.port, (object)this.dbuser, (object)this.dbpass));
                        mySqlConnection.Open();
                    }
                    this.dbinactive = this.db;
                    this.db = mySqlConnection;
                    this.tick = DateTime.Now;
                    DatabaseWaitress.LeaveCriticalArea();
                    if (blocked)
                        ClientManager.EnterCriticalArea();
                }
                if (this.db.State == ConnectionState.Broken || this.db.State == ConnectionState.Closed)
                    this.isconnected = false;
            }
            return this.isconnected;
        }

        /// <summary>
        /// The WriteUser.
        /// </summary>
        /// <param name="user">The user<see cref="Account"/>.</param>
        public void WriteUser(Account user)
        {
            if (user == null || !this.isConnected())
                return;
            byte num = !user.Banned ? (byte)0 : (byte)1;
            string sqlstr = string.Format("UPDATE `login` SET `username`='{0}',`password`='{1}',`deletepass`='{2}',`bank`='{4}',`banned`='{5}',`lastip`='{6}' WHERE account_id='{3}' LIMIT 1", (object)user.Name, (object)user.Password, (object)user.DeletePassword, (object)user.AccountID, (object)user.Bank, (object)num, (object)user.LastIP);
            try
            {
                this.SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The GetUser.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="Account"/>.</returns>
        public Account GetUser(string name)
        {
            this.CheckSQLString(ref name);
            string sqlstr = "SELECT * FROM `login` WHERE `username`='" + name + "' LIMIT 1";
            DataRowCollection dataRowCollection;
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                return (Account)null;
            }
            if (dataRowCollection.Count == 0)
                return (Account)null;
            return new Account()
            {
                AccountID = (int)(uint)dataRowCollection[0]["account_id"],
                Name = name,
                Password = (string)dataRowCollection[0]["password"],
                DeletePassword = (string)dataRowCollection[0]["deletepass"],
                GMLevel = (byte)dataRowCollection[0]["gmlevel"],
                Bank = (uint)dataRowCollection[0]["bank"],
                Banned = (byte)dataRowCollection[0]["banned"] == (byte)1
            };
        }

        /// <summary>
        /// The CheckPassword.
        /// </summary>
        /// <param name="user">The user<see cref="string"/>.</param>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <param name="frontword">The frontword<see cref="uint"/>.</param>
        /// <param name="backword">The backword<see cref="uint"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CheckPassword(string user, string password, uint frontword, uint backword)
        {
            SHA1 shA1 = SHA1.Create();
            string sqlstr = "SELECT * FROM `login` WHERE `username`='" + this.CheckSQLString(user) + "' LIMIT 1";
            DataRowCollection dataRowCollection;
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                return false;
            }
            if (dataRowCollection.Count == 0)
                return false;
            string s = string.Format("{0}{1}{2}", (object)frontword, (object)((string)dataRowCollection[0][nameof(password)]).ToLower(), (object)backword);
            byte[] hash = shA1.ComputeHash(Encoding.ASCII.GetBytes(s));
            return password == Conversions.bytes2HexString(hash).ToLower();
        }

        /// <summary>
        /// The GetAccountID.
        /// </summary>
        /// <param name="user">The user<see cref="string"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetAccountID(string user)
        {
            string sqlstr = "SELECT * FROM `login` WHERE `username`='" + user + "' LIMIT 1";
            DataRowCollection dataRowCollection;
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                return -1;
            }
            if (dataRowCollection.Count == 0)
                return -1;
            return (int)dataRowCollection[0]["account_id"];
        }
    }
}
