namespace SagaDB
{
    using SagaLib;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="AccessAccountDB" />.
    /// </summary>
    public class AccessAccountDB : AccessConnectivity, AccountDB
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
        /// Defines the Source.
        /// </summary>
        private string Source;

        /// <summary>
        /// Defines the isconnected.
        /// </summary>
        private bool isconnected;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessAccountDB"/> class.
        /// </summary>
        /// <param name="Source">The Source<see cref="string"/>.</param>
        public AccessAccountDB(string Source)
        {
            this.Source = Source;
            this.isconnected = false;
            try
            {
                db = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", (object)this.Source));
                dbinactive = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", (object)this.Source));
                db.Open();
            }
            catch (OleDbException ex)
            {
                Logger.ShowSQL((Exception)ex, (Logger)null);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, (Logger)null);
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
            if (this.db.State == ConnectionState.Open)
            {
                this.isconnected = true;
                return true;
            }
            this.isconnected = false;
            return false;
        }

        /// <summary>
        /// The WriteUser.
        /// </summary>
        /// <param name="user">The user<see cref="Account"/>.</param>
        public void WriteUser(Account user)
        {
            if (user == null || !this.isConnected())
                return;
            string sqlstr = string.Format("UPDATE `login` SET `username`='{0}',`password`='{1}',`deletepass`='{2}' WHERE account_id='{3}'", (object)user.Name, (object)user.Password, (object)user.DeletePassword, (object)user.AccountID);
            try
            {
                this.SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
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
            string sqlstr = "SELECT top 1 * FROM `login` WHERE username='" + name + "'";
            DataRowCollection dataRowCollection;
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return (Account)null;
            }
            if (dataRowCollection.Count == 0)
                return (Account)null;
            return new Account()
            {
                AccountID = (int)dataRowCollection[0]["account_id"],
                Name = name,
                Password = (string)dataRowCollection[0]["password"],
                DeletePassword = (string)dataRowCollection[0]["deletepass"],
                GMLevel = (byte)dataRowCollection[0]["gmlevel"]
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
            string sqlstr = "SELECT top 1 * FROM `login` WHERE username='" + user + "'";
            DataRowCollection dataRowCollection;
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
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
            string sqlstr = "SELECT top 1 * FROM `login` WHERE username='" + user + "'";
            DataRowCollection dataRowCollection;
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return -1;
            }
            if (dataRowCollection.Count == 0)
                return -1;
            return (int)dataRowCollection[0]["account_id"];
        }
    }
}
