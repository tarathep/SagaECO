namespace SagaLib
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="MySQLConnectivity" />.
    /// </summary>
    public abstract class MySQLConnectivity
    {
        /// <summary>
        /// Defines the waitQueue.
        /// </summary>
        private List<MySQLConnectivity.MySQLCommand> waitQueue = new List<MySQLConnectivity.MySQLCommand>();

        /// <summary>
        /// Defines the db.
        /// </summary>
        protected MySqlConnection db;

        /// <summary>
        /// Defines the dbinactive.
        /// </summary>
        protected MySqlConnection dbinactive;

        /// <summary>
        /// Defines the mysqlPool.
        /// </summary>
        private Thread mysqlPool;

        /// <summary>
        /// Defines the cuurentCount.
        /// </summary>
        internal int cuurentCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="MySQLConnectivity"/> class.
        /// </summary>
        public MySQLConnectivity()
        {
            this.mysqlPool = new Thread(new ThreadStart(this.ProcessMysql));
            this.mysqlPool.Start();
        }

        /// <summary>
        /// Gets a value indicating whether CanClose.
        /// </summary>
        public bool CanClose
        {
            get
            {
                lock (this.waitQueue)
                    return this.waitQueue.Count == 0 && this.cuurentCount == 0;
            }
        }

        /// <summary>
        /// The ProcessMysql.
        /// </summary>
        private void ProcessMysql()
        {
            while (true)
            {
                try
                {
                    MySQLConnectivity.MySQLCommand[] mySqlCommandArray;
                    lock (this.waitQueue)
                    {
                        if (this.waitQueue.Count > 0)
                        {
                            mySqlCommandArray = this.waitQueue.ToArray();
                            this.waitQueue.Clear();
                            this.cuurentCount = mySqlCommandArray.Length;
                        }
                        else
                            mySqlCommandArray = new MySQLConnectivity.MySQLCommand[0];
                    }
                    if (mySqlCommandArray.Length > 0)
                    {
                        List<MySQLConnectivity.MySQLCommand> mySqlCommandList = new List<MySQLConnectivity.MySQLCommand>();
                        DatabaseWaitress.EnterCriticalArea();
                        foreach (MySQLConnectivity.MySQLCommand mySqlCommand in mySqlCommandArray)
                        {
                            try
                            {
                                mySqlCommand.Command.Connection = this.db;
                                switch (mySqlCommand.Type)
                                {
                                    case MySQLConnectivity.MySQLCommand.CommandType.NonQuery:
                                        mySqlCommand.Command.ExecuteNonQuery();
                                        continue;
                                    case MySQLConnectivity.MySQLCommand.CommandType.Query:
                                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand.Command);
                                        DataSet dataSet = new DataSet();
                                        mySqlDataAdapter.Fill(dataSet);
                                        mySqlCommand.DataRows = dataSet.Tables[0].Rows;
                                        continue;
                                    case MySQLConnectivity.MySQLCommand.CommandType.Scalar:
                                        mySqlCommand.Scalar = Convert.ToUInt32(mySqlCommand.Command.ExecuteScalar());
                                        continue;
                                    default:
                                        continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowSQL("Error on query:" + this.command2String(mySqlCommand.Command), Logger.defaultlogger);
                                Logger.ShowSQL(ex, Logger.defaultlogger);
                                ++mySqlCommand.ErrorCount;
                                if (mySqlCommand.ErrorCount > 10)
                                    Logger.ShowSQL("Error to many times, dropping command", Logger.defaultlogger);
                                else
                                    mySqlCommandList.Add(mySqlCommand);
                            }
                        }
                        DatabaseWaitress.LeaveCriticalArea();
                        if (mySqlCommandList.Count > 0)
                        {
                            lock (this.waitQueue)
                            {
                                foreach (MySQLConnectivity.MySQLCommand mySqlCommand in mySqlCommandList)
                                    this.waitQueue.Add(mySqlCommand);
                            }
                        }
                    }
                    mySqlCommandArray = (MySQLConnectivity.MySQLCommand[])null;
                    this.cuurentCount = 0;
                    Thread.Sleep(10);
                }
                catch (ThreadAbortException ex)
                {
                    DatabaseWaitress.LeaveCriticalArea();
                }
            }
        }

        /// <summary>
        /// The SQLExecuteNonQuery.
        /// </summary>
        /// <param name="sqlstr">The sqlstr<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool SQLExecuteNonQuery(string sqlstr)
        {
            lock (this.waitQueue)
                this.waitQueue.Add(new MySQLConnectivity.MySQLCommand(new MySqlCommand(sqlstr)));
            return true;
        }

        /// <summary>
        /// The command2String.
        /// </summary>
        /// <param name="cmd">The cmd<see cref="MySqlCommand"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string command2String(MySqlCommand cmd)
        {
            string str1 = cmd.CommandText;
            if (cmd.Parameters.Count > 0)
            {
                string str2 = "";
                foreach (MySqlParameter parameter in (DbParameterCollection)cmd.Parameters)
                    str2 += string.Format("{0}={1},", (object)parameter.ParameterName, (object)this.value2String(parameter.Value));
                string str3 = str2.Substring(0, str2.Length - 1);
                str1 = string.Format("{0} VALUES({1})", (object)str1, (object)str3);
            }
            return str1;
        }

        /// <summary>
        /// The value2String.
        /// </summary>
        /// <param name="val">The val<see cref="object"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string value2String(object val)
        {
            if (val.GetType() == typeof(byte[]))
                return "0x" + Conversions.bytes2HexString((byte[])val);
            return val.ToString();
        }

        /// <summary>
        /// The SQLExecuteNonQuery.
        /// </summary>
        /// <param name="cmd">The cmd<see cref="MySqlCommand"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool SQLExecuteNonQuery(MySqlCommand cmd)
        {
            lock (this.waitQueue)
                this.waitQueue.Add(new MySQLConnectivity.MySQLCommand(cmd));
            return true;
        }

        /// <summary>
        /// The SQLExecuteScalar.
        /// </summary>
        /// <param name="sqlstr">The sqlstr<see cref="string"/>.</param>
        /// <param name="index">The index<see cref="uint"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool SQLExecuteScalar(string sqlstr, out uint index)
        {
            bool blocked = ClientManager.Blocked;
            bool flag = true;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            try
            {
                if (sqlstr.Substring(sqlstr.Length - 1) != ";")
                    sqlstr += ";";
                sqlstr += "SELECT LAST_INSERT_ID();";
                MySQLConnectivity.MySQLCommand mySqlCommand = new MySQLConnectivity.MySQLCommand(new MySqlCommand(sqlstr), MySQLConnectivity.MySQLCommand.CommandType.Scalar);
                lock (this.waitQueue)
                    this.waitQueue.Add(mySqlCommand);
                while (mySqlCommand.Scalar == uint.MaxValue)
                    Thread.Sleep(10);
                index = mySqlCommand.Scalar;
            }
            catch (Exception ex)
            {
                Logger.ShowSQL(ex, Logger.defaultlogger);
                flag = false;
                index = 0U;
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
            return flag;
        }

        /// <summary>
        /// The SQLExecuteQuery.
        /// </summary>
        /// <param name="sqlstr">The sqlstr<see cref="string"/>.</param>
        /// <returns>The <see cref="DataRowCollection"/>.</returns>
        public DataRowCollection SQLExecuteQuery(string sqlstr)
        {
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            try
            {
                MySQLConnectivity.MySQLCommand mySqlCommand = new MySQLConnectivity.MySQLCommand(new MySqlCommand(sqlstr), MySQLConnectivity.MySQLCommand.CommandType.Query);
                lock (this.waitQueue)
                    this.waitQueue.Add(mySqlCommand);
                while (mySqlCommand.DataRows == null)
                    Thread.Sleep(10);
                DataRowCollection dataRows = mySqlCommand.DataRows;
                if (blocked)
                    ClientManager.EnterCriticalArea();
                return dataRows;
            }
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sqlstr, Logger.defaultlogger);
                Logger.ShowSQL(ex, Logger.defaultlogger);
                if (blocked)
                    ClientManager.EnterCriticalArea();
                return (DataRowCollection)null;
            }
        }

        /// <summary>
        /// The ToSQLDateTime.
        /// </summary>
        /// <param name="date">The date<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToSQLDateTime(DateTime date)
        {
            return string.Format("{0}-{1}-{2} {3}:{4}:{5}", (object)date.Year, (object)date.Month, (object)date.Day, (object)date.Hour, (object)date.Minute, (object)date.Second);
        }

        /// <summary>
        /// The CheckSQLString.
        /// </summary>
        /// <param name="str">The str<see cref="string"/>.</param>
        public void CheckSQLString(ref string str)
        {
            str = str.Replace("\\", "").Replace("'", "\\'");
        }

        /// <summary>
        /// The CheckSQLString.
        /// </summary>
        /// <param name="str">The str<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string CheckSQLString(string str)
        {
            return str.Replace("\\", "").Replace("'", "\\'");
        }

        /// <summary>
        /// Defines the <see cref="MySQLCommand" />.
        /// </summary>
        private class MySQLCommand
        {
            /// <summary>
            /// Defines the scalar.
            /// </summary>
            private uint scalar = uint.MaxValue;

            /// <summary>
            /// Defines the cmd.
            /// </summary>
            private MySqlCommand cmd;

            /// <summary>
            /// Defines the reader.
            /// </summary>
            private DataRowCollection reader;

            /// <summary>
            /// Defines the type.
            /// </summary>
            private MySQLConnectivity.MySQLCommand.CommandType type;

            /// <summary>
            /// Defines the errorCount.
            /// </summary>
            private int errorCount;

            /// <summary>
            /// Gets the Command.
            /// </summary>
            public MySqlCommand Command
            {
                get
                {
                    return this.cmd;
                }
            }

            /// <summary>
            /// Gets or sets the DataRows.
            /// </summary>
            public DataRowCollection DataRows
            {
                get
                {
                    return this.reader;
                }
                set
                {
                    this.reader = value;
                }
            }

            /// <summary>
            /// Gets the Type.
            /// </summary>
            public MySQLConnectivity.MySQLCommand.CommandType Type
            {
                get
                {
                    return this.type;
                }
            }

            /// <summary>
            /// Gets or sets the Scalar.
            /// </summary>
            public uint Scalar
            {
                get
                {
                    return this.scalar;
                }
                set
                {
                    this.scalar = value;
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="MySQLCommand"/> class.
            /// </summary>
            /// <param name="cmd">The cmd<see cref="MySqlCommand"/>.</param>
            public MySQLCommand(MySqlCommand cmd)
            {
                this.cmd = cmd;
                this.type = MySQLConnectivity.MySQLCommand.CommandType.NonQuery;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="MySQLCommand"/> class.
            /// </summary>
            /// <param name="cmd">The cmd<see cref="MySqlCommand"/>.</param>
            /// <param name="type">The type<see cref="MySQLConnectivity.MySQLCommand.CommandType"/>.</param>
            public MySQLCommand(MySqlCommand cmd, MySQLConnectivity.MySQLCommand.CommandType type)
            {
                this.cmd = cmd;
                this.type = type;
            }

            /// <summary>
            /// Gets or sets the ErrorCount.
            /// </summary>
            public int ErrorCount
            {
                get
                {
                    return this.errorCount;
                }
                set
                {
                    this.errorCount = value;
                }
            }

            /// <summary>
            /// Defines the CommandType.
            /// </summary>
            public enum CommandType
            {
                /// <summary>
                /// Defines the NonQuery.
                /// </summary>
                NonQuery,

                /// <summary>
                /// Defines the Query.
                /// </summary>
                Query,

                /// <summary>
                /// Defines the Scalar.
                /// </summary>
                Scalar,
            }
        }
    }
}
