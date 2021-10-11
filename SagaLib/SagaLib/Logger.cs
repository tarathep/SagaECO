namespace SagaLib
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="Logger" />.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Defines the SQLLogLevel.
        /// </summary>
        public static BitMask<Logger.EventType> SQLLogLevel = new BitMask<Logger.EventType>(new BitMask(68712));

        /// <summary>
        /// Defines the CurrentLogger.
        /// </summary>
        public static Logger CurrentLogger = Logger.defaultlogger;

        /// <summary>
        /// Defines the LogLevel.
        /// </summary>
        public Logger.LogContent LogLevel = Logger.LogContent.Info | Logger.LogContent.Warning | Logger.LogContent.Error | Logger.LogContent.SQL | Logger.LogContent.Debug;

        /// <summary>
        /// Defines the defaultSql.
        /// </summary>
        public static MySQLConnectivity defaultSql;

        /// <summary>
        /// Defines the defaultlogger.
        /// </summary>
        public static Logger defaultlogger;

        /// <summary>
        /// Defines the path.
        /// </summary>
        private string path;

        /// <summary>
        /// Defines the filename.
        /// </summary>
        private string filename;

        /// <summary>
        /// The GetStackTrace.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GetStackTrace()
        {
            string[] strArray = new StackTrace(2, false).ToString().Split('\n');
            string str1 = "";
            foreach (string str2 in strArray)
            {
                if (!str2.Contains(" System."))
                    str1 += str2.Replace("\r", "\r\n");
            }
            int length = 1024;
            if (length > str1.Length)
                length = str1.Length;
            str1.Substring(0, length);
            return str1;
        }

        /// <summary>
        /// The LogItemGet.
        /// </summary>
        /// <param name="type">The type<see cref="Logger.EventType"/>.</param>
        /// <param name="pc">The pc<see cref="string"/>.</param>
        /// <param name="item">The item<see cref="string"/>.</param>
        /// <param name="detail">The detail<see cref="string"/>.</param>
        /// <param name="stack">The stack<see cref="bool"/>.</param>
        public static void LogItemGet(Logger.EventType type, string pc, string item, string detail, bool stack)
        {
            if (type < Logger.EventType.ItemGolemGet || type > Logger.EventType.ItemGMGet || (Logger.defaultSql == null || !Logger.SQLLogLevel.Test(type)))
                return;
            string detail1 = detail;
            if (stack)
                detail1 = detail1 + "\r\n" + Logger.GetStackTrace();
            Logger.SQLLog(type, pc, item, detail1);
        }

        /// <summary>
        /// The LogItemLost.
        /// </summary>
        /// <param name="type">The type<see cref="Logger.EventType"/>.</param>
        /// <param name="pc">The pc<see cref="string"/>.</param>
        /// <param name="item">The item<see cref="string"/>.</param>
        /// <param name="detail">The detail<see cref="string"/>.</param>
        /// <param name="stack">The stack<see cref="bool"/>.</param>
        public static void LogItemLost(Logger.EventType type, string pc, string item, string detail, bool stack)
        {
            if (type < Logger.EventType.ItemGolemLost || type > Logger.EventType.ItemDropLost || (Logger.defaultSql == null || !Logger.SQLLogLevel.Test(type)))
                return;
            string detail1 = detail;
            if (stack)
                detail1 = detail1 + "\r\n" + Logger.GetStackTrace();
            Logger.SQLLog(type, pc, item, detail1);
        }

        /// <summary>
        /// The LogGoldChange.
        /// </summary>
        /// <param name="pc">The pc<see cref="string"/>.</param>
        /// <param name="amount">The amount<see cref="int"/>.</param>
        public static void LogGoldChange(string pc, int amount)
        {
            if (Logger.defaultSql == null || !Logger.SQLLogLevel.Test(Logger.EventType.GoldChange))
                return;
            Logger.SQLLog(Logger.EventType.GoldChange, pc, amount.ToString(), Logger.GetStackTrace());
        }

        /// <summary>
        /// The LogWarehouseGet.
        /// </summary>
        /// <param name="pc">The pc<see cref="string"/>.</param>
        /// <param name="item">The item<see cref="string"/>.</param>
        /// <param name="detail">The detail<see cref="string"/>.</param>
        public static void LogWarehouseGet(string pc, string item, string detail)
        {
            if (Logger.defaultSql == null || !Logger.SQLLogLevel.Test(Logger.EventType.WarehouseGet))
                return;
            Logger.SQLLog(Logger.EventType.WarehouseGet, pc, item, detail);
        }

        /// <summary>
        /// The LogWarehousePut.
        /// </summary>
        /// <param name="pc">The pc<see cref="string"/>.</param>
        /// <param name="item">The item<see cref="string"/>.</param>
        /// <param name="detail">The detail<see cref="string"/>.</param>
        public static void LogWarehousePut(string pc, string item, string detail)
        {
            if (Logger.defaultSql == null || !Logger.SQLLogLevel.Test(Logger.EventType.WarehouseGet))
                return;
            Logger.SQLLog(Logger.EventType.WarehousePut, pc, item, detail);
        }

        /// <summary>
        /// The LogGMCommand.
        /// </summary>
        /// <param name="pc">The pc<see cref="string"/>.</param>
        /// <param name="item">The item<see cref="string"/>.</param>
        /// <param name="detail">The detail<see cref="string"/>.</param>
        public static void LogGMCommand(string pc, string item, string detail)
        {
            if (Logger.defaultSql == null || !Logger.SQLLogLevel.Test(Logger.EventType.GMCommand))
                return;
            Logger.SQLLog(Logger.EventType.GMCommand, pc, item, detail);
        }

        /// <summary>
        /// The SQLLog.
        /// </summary>
        /// <param name="type">The type<see cref="Logger.EventType"/>.</param>
        /// <param name="src">The src<see cref="string"/>.</param>
        /// <param name="dst">The dst<see cref="string"/>.</param>
        /// <param name="detail">The detail<see cref="string"/>.</param>
        private static void SQLLog(Logger.EventType type, string src, string dst, string detail)
        {
            DateTime now = DateTime.Now;
            Logger.defaultSql.CheckSQLString(ref src);
            string sqlstr = string.Format("INSERT INTO `log`(`eventType`,`eventTime`,`src`,`dst`,`detail`) VALUES ('{0}','{1}','{2}','{3}','{4}');", (object)type, (object)Logger.defaultSql.ToSQLDateTime(now), (object)Logger.defaultSql.CheckSQLString(src), (object)Logger.defaultSql.CheckSQLString(dst), (object)Logger.defaultSql.CheckSQLString(detail));
            try
            {
                Logger.defaultSql.SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="filename">The filename<see cref="string"/>.</param>
        public Logger(string filename)
        {
            this.filename = filename;
            this.path = this.GetLogFile();
            if (File.Exists(this.path))
                return;
            File.Create(this.path);
        }

        /// <summary>
        /// The WriteLog.
        /// </summary>
        /// <param name="p">The p<see cref="string"/>.</param>
        public void WriteLog(string p)
        {
            try
            {
                this.path = this.GetLogFile();
                StreamWriter streamWriter = new StreamWriter((Stream)new FileStream(this.path, FileMode.Append));
                string str = this.GetDate() + "|" + p;
                streamWriter.WriteLine(str);
                streamWriter.Close();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The WriteLog.
        /// </summary>
        /// <param name="prefix">The prefix<see cref="string"/>.</param>
        /// <param name="p">The p<see cref="string"/>.</param>
        public void WriteLog(string prefix, string p)
        {
            try
            {
                this.path = this.GetLogFile();
                p = string.Format("{0}->{1}", (object)prefix, (object)p);
                StreamWriter streamWriter = new StreamWriter((Stream)new FileStream(this.path, FileMode.Append));
                string str = this.GetDate() + "|" + p;
                streamWriter.WriteLine(str);
                streamWriter.Close();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The ShowInfo.
        /// </summary>
        /// <param name="ex">The ex<see cref="Exception"/>.</param>
        /// <param name="log">The log<see cref="Logger"/>.</param>
        public static void ShowInfo(Exception ex, Logger log)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Info) != Logger.defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]");
            Console.ResetColor();
            Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            log?.WriteLog(ex.Message);
        }

        /// <summary>
        /// The ShowInfo.
        /// </summary>
        /// <param name="ex">The ex<see cref="string"/>.</param>
        public static void ShowInfo(string ex)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Info) != Logger.defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]");
            Console.ResetColor();
            Console.WriteLine(ex);
        }

        /// <summary>
        /// The ShowInfo.
        /// </summary>
        /// <param name="ex">The ex<see cref="string"/>.</param>
        /// <param name="log">The log<see cref="Logger"/>.</param>
        public static void ShowInfo(string ex, Logger log)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Info) != Logger.defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]");
            Console.ResetColor();
            Console.WriteLine(ex);
            log?.WriteLog(ex);
        }

        /// <summary>
        /// The ShowWarning.
        /// </summary>
        /// <param name="ex">The ex<see cref="Exception"/>.</param>
        public static void ShowWarning(Exception ex)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Warning) != Logger.defaultlogger.LogLevel)
                return;
            Logger.ShowWarning(ex, Logger.defaultlogger);
        }

        /// <summary>
        /// The ShowWarning.
        /// </summary>
        /// <param name="ex">The ex<see cref="string"/>.</param>
        public static void ShowWarning(string ex)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Warning) != Logger.defaultlogger.LogLevel)
                return;
            Logger.ShowWarning(ex, Logger.defaultlogger);
        }

        /// <summary>
        /// The ShowDebug.
        /// </summary>
        /// <param name="ex">The ex<see cref="Exception"/>.</param>
        /// <param name="log">The log<see cref="Logger"/>.</param>
        public static void ShowDebug(Exception ex, Logger log)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Debug) != Logger.defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Debug]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            Console.ResetColor();
            log?.WriteLog("[Debug]" + ex.Message + "\r\n" + ex.StackTrace);
        }

        /// <summary>
        /// The ShowDebug.
        /// </summary>
        /// <param name="ex">The ex<see cref="string"/>.</param>
        /// <param name="log">The log<see cref="Logger"/>.</param>
        public static void ShowDebug(string ex, Logger log)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Debug) != Logger.defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Debug]");
            Console.ForegroundColor = ConsoleColor.White;
            StackTrace stackTrace = new StackTrace(1, true);
            string input = ex;
            foreach (StackFrame frame in stackTrace.GetFrames())
                input = input + "\r\n      at " + frame.GetMethod().ReflectedType.FullName + "." + frame.GetMethod().Name + " " + frame.GetFileName() + ":" + (object)frame.GetFileLineNumber();
            string str = Logger.FilterSQL(input);
            Console.WriteLine(str);
            Console.ResetColor();
            log?.WriteLog("[Debug]" + str);
        }

        /// <summary>
        /// The ShowSQL.
        /// </summary>
        /// <param name="ex">The ex<see cref="Exception"/>.</param>
        /// <param name="log">The log<see cref="Logger"/>.</param>
        public static void ShowSQL(Exception ex, Logger log)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.SQL) != Logger.defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("[SQL]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex.Message + "\r\n" + Logger.FilterSQL(ex.StackTrace));
            Console.ResetColor();
            log?.WriteLog("[SQL]" + ex.Message + "\r\n" + Logger.FilterSQL(ex.StackTrace));
        }

        /// <summary>
        /// The FilterSQL.
        /// </summary>
        /// <param name="input">The input<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string FilterSQL(string input)
        {
            string[] strArray = input.Split('\n');
            string str1 = "";
            foreach (string str2 in strArray)
            {
                if (!str2.Contains(" MySql.") && !str2.Contains(" System."))
                    str1 = str1 + str2 + "\n";
            }
            return str1;
        }

        /// <summary>
        /// The ShowSQL.
        /// </summary>
        /// <param name="ex">The ex<see cref="string"/>.</param>
        /// <param name="log">The log<see cref="Logger"/>.</param>
        public static void ShowSQL(string ex, Logger log)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.SQL) != Logger.defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("[SQL]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex);
            Console.ResetColor();
            log?.WriteLog("[SQL]" + ex);
        }

        /// <summary>
        /// The ShowWarning.
        /// </summary>
        /// <param name="ex">The ex<see cref="Exception"/>.</param>
        /// <param name="log">The log<see cref="Logger"/>.</param>
        public static void ShowWarning(Exception ex, Logger log)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Warning) != Logger.defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[Warning]");
            Console.ResetColor();
            Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            log?.WriteLog("Warning:" + ex.ToString());
        }

        /// <summary>
        /// The ShowWarning.
        /// </summary>
        /// <param name="ex">The ex<see cref="string"/>.</param>
        /// <param name="log">The log<see cref="Logger"/>.</param>
        public static void ShowWarning(string ex, Logger log)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Warning) != Logger.defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[Warning]");
            Console.ResetColor();
            Console.WriteLine(ex);
            log?.WriteLog("Warning:" + ex);
        }

        /// <summary>
        /// The ShowError.
        /// </summary>
        /// <param name="ex">The ex<see cref="Exception"/>.</param>
        /// <param name="log">The log<see cref="Logger"/>.</param>
        public static void ShowError(Exception ex, Logger log)
        {
            try
            {
                if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Error) != Logger.defaultlogger.LogLevel)
                    return;
                if (log == null)
                    log = Logger.defaultlogger;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[Error]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                Console.ResetColor();
                log.WriteLog("[Error]" + ex.Message + "\r\n" + ex.StackTrace);
            }
            catch
            {
            }
        }

        /// <summary>
        /// The ShowError.
        /// </summary>
        /// <param name="ex">The ex<see cref="string"/>.</param>
        /// <param name="log">The log<see cref="Logger"/>.</param>
        public static void ShowError(string ex, Logger log)
        {
            try
            {
                if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Error) != Logger.defaultlogger.LogLevel)
                    return;
                if (log == null)
                    log = Logger.defaultlogger;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[Error]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(ex);
                Console.ResetColor();
                log.WriteLog("[Error]" + ex);
            }
            catch
            {
            }
        }

        /// <summary>
        /// The ShowError.
        /// </summary>
        /// <param name="ex">The ex<see cref="string"/>.</param>
        public static void ShowError(string ex)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Error) != Logger.defaultlogger.LogLevel)
                return;
            Logger.ShowError(ex, Logger.defaultlogger);
        }

        /// <summary>
        /// The ShowError.
        /// </summary>
        /// <param name="ex">The ex<see cref="Exception"/>.</param>
        public static void ShowError(Exception ex)
        {
            if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Error) != Logger.defaultlogger.LogLevel)
                return;
            Logger.ShowError(ex, Logger.defaultlogger);
        }

        /// <summary>
        /// The GetLogFile.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetLogFile()
        {
            if (!Directory.Exists("Log"))
                Directory.CreateDirectory("Log");
            return "Log/[" + string.Format("{0}-{1}-{2}", (object)DateTime.Today.Year, (object)DateTime.Today.Month, (object)DateTime.Today.Day) + "]" + this.filename;
        }

        /// <summary>
        /// The GetDate.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetDate()
        {
            return DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        /// <summary>
        /// The ProgressBarShow.
        /// </summary>
        /// <param name="progressPos">The progressPos<see cref="uint"/>.</param>
        /// <param name="progressTotal">The progressTotal<see cref="uint"/>.</param>
        /// <param name="label">The label<see cref="string"/>.</param>
        public static void ProgressBarShow(uint progressPos, uint progressTotal, string label)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\r[Info]");
            Console.ResetColor();
            Console.Write(string.Format("{0} [", (object)label));
            StringBuilder stringBuilder = new StringBuilder();
            uint num = progressPos * 40U / progressTotal + 1U;
            for (uint index = 0; index < num; ++index)
                stringBuilder.AppendFormat("#");
            for (uint index = num; index < 40U; ++index)
                stringBuilder.AppendFormat(" ");
            stringBuilder.AppendFormat("] {0}%\r", (object)(progressPos * 100U / progressTotal));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(stringBuilder.ToString());
            Console.ResetColor();
        }

        /// <summary>
        /// The ProgressBarHide.
        /// </summary>
        /// <param name="label">The label<see cref="string"/>.</param>
        public static void ProgressBarHide(string label)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\r[Info]");
            Console.ResetColor();
            Console.Write(string.Format("{0}                                                                                            \r", (object)label));
        }

        /// <summary>
        /// Defines the EventType.
        /// </summary>
        public enum EventType
        {
            /// <summary>
            /// Defines the ItemGolemGet.
            /// </summary>
            ItemGolemGet = 1,

            /// <summary>
            /// Defines the ItemLootGet.
            /// </summary>
            ItemLootGet = 2,

            /// <summary>
            /// Defines the ItemWareGet.
            /// </summary>
            ItemWareGet = 4,

            /// <summary>
            /// Defines the ItemNPCGet.
            /// </summary>
            ItemNPCGet = 8,

            /// <summary>
            /// Defines the ItemVShopGet.
            /// </summary>
            ItemVShopGet = 16, // 0x00000010

            /// <summary>
            /// Defines the ItemTradeGet.
            /// </summary>
            ItemTradeGet = 32, // 0x00000020

            /// <summary>
            /// Defines the ItemGMGet.
            /// </summary>
            ItemGMGet = 64, // 0x00000040

            /// <summary>
            /// Defines the ItemGolemLost.
            /// </summary>
            ItemGolemLost = 128, // 0x00000080

            /// <summary>
            /// Defines the ItemUseLost.
            /// </summary>
            ItemUseLost = 256, // 0x00000100

            /// <summary>
            /// Defines the ItemWareLost.
            /// </summary>
            ItemWareLost = 512, // 0x00000200

            /// <summary>
            /// Defines the ItemNPCLost.
            /// </summary>
            ItemNPCLost = 1024, // 0x00000400

            /// <summary>
            /// Defines the ItemTradeLost.
            /// </summary>
            ItemTradeLost = 2048, // 0x00000800

            /// <summary>
            /// Defines the ItemDropLost.
            /// </summary>
            ItemDropLost = 4096, // 0x00001000

            /// <summary>
            /// Defines the GoldChange.
            /// </summary>
            GoldChange = 8192, // 0x00002000

            /// <summary>
            /// Defines the WarehouseGet.
            /// </summary>
            WarehouseGet = 16384, // 0x00004000

            /// <summary>
            /// Defines the WarehousePut.
            /// </summary>
            WarehousePut = 32768, // 0x00008000

            /// <summary>
            /// Defines the GMCommand.
            /// </summary>
            GMCommand = 65536, // 0x00010000
        }

        /// <summary>
        /// Defines the LogContent.
        /// </summary>
        public enum LogContent
        {
            /// <summary>
            /// Defines the Info.
            /// </summary>
            Info = 1,

            /// <summary>
            /// Defines the Warning.
            /// </summary>
            Warning = 2,

            /// <summary>
            /// Defines the Error.
            /// </summary>
            Error = 4,

            /// <summary>
            /// Defines the SQL.
            /// </summary>
            SQL = 8,

            /// <summary>
            /// Defines the Debug.
            /// </summary>
            Debug = 16, // 0x00000010
        }
    }
}
