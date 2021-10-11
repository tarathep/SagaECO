namespace SagaDB
{
    using SagaLib;
    using System;
    using System.Data;
    using System.Data.OleDb;

    /// <summary>
    /// Defines the <see cref="AccessConnectivity" />.
    /// </summary>
    public abstract class AccessConnectivity
    {
        /// <summary>
        /// Defines the db.
        /// </summary>
        internal OleDbConnection db;

        /// <summary>
        /// Defines the dbinactive.
        /// </summary>
        internal OleDbConnection dbinactive;

        /// <summary>
        /// The SQLExecuteNonQuery.
        /// </summary>
        /// <param name="sqlstr">The sqlstr<see cref="string"/>.</param>
        public void SQLExecuteNonQuery(string sqlstr)
        {
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            try
            {
                new OleDbCommand(sqlstr, this.db).ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sqlstr, (Logger)null);
                Logger.ShowSQL(ex, (Logger)null);
            }
            DatabaseWaitress.LeaveCriticalArea();
            if (!blocked)
                return;
            ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// The SQLExecuteScalar.
        /// </summary>
        /// <param name="sqlstr">The sqlstr<see cref="string"/>.</param>
        /// <param name="index">The index<see cref="uint"/>.</param>
        public void SQLExecuteScalar(string sqlstr, ref uint index)
        {
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            try
            {
                if (sqlstr.Substring(sqlstr.Length - 1) != ";")
                    sqlstr += ";";
                sqlstr += "SELECT LAST_INSERT_ID();";
                OleDbCommand oleDbCommand = new OleDbCommand(sqlstr, this.db);
                index = Convert.ToUInt32(oleDbCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sqlstr, (Logger)null);
                Logger.ShowSQL(ex, (Logger)null);
            }
            DatabaseWaitress.LeaveCriticalArea();
            if (!blocked)
                return;
            ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// The SQLExecuteQuery.
        /// </summary>
        /// <param name="sqlstr">The sqlstr<see cref="string"/>.</param>
        /// <returns>The <see cref="DataRowCollection"/>.</returns>
        public DataRowCollection SQLExecuteQuery(string sqlstr)
        {
            DataSet dataSet = new DataSet();
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            try
            {
                new OleDbDataAdapter(sqlstr, this.db).Fill(dataSet);
                if (dataSet.Tables.Count == 0)
                    throw new Exception("Unexpected Empty Query Result!");
                DataRowCollection rows = dataSet.Tables[0].Rows;
                DatabaseWaitress.LeaveCriticalArea();
                if (blocked)
                    ClientManager.EnterCriticalArea();
                return rows;
            }
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sqlstr, (Logger)null);
                Logger.ShowSQL(ex, (Logger)null);
                DatabaseWaitress.LeaveCriticalArea();
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
        internal string ToSQLDateTime(DateTime date)
        {
            return string.Format("{0}-{1}-{2} {3}:{4}:{5}", (object)date.Year, (object)date.Month, (object)date.Day, (object)date.Hour, (object)date.Minute, (object)date.Second);
        }

        /// <summary>
        /// The CheckSQLString.
        /// </summary>
        /// <param name="str">The str<see cref="string"/>.</param>
        internal void CheckSQLString(ref string str)
        {
            str = str.Replace("'", "\\'");
        }
    }
}
