using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTal.Commons.TestData
{
    //hopefully the real DB tech you use would have stored procss instead of a maintaining a query library here.
    static class TestDataDatabase
    {
        static SQLiteConnection dbConnection;

        public static void init()
        {
            SQLiteConnection.CreateFile("TestDataDatabase.sqlite");
            dbConnection = new SQLiteConnection("Data Source=TestDataDatabase.sqlite;Version=3;");
            dbConnection.Open();
            CreateTables();
            PopulateTables();
        }

        private static void CreateTables()
        {
            CreateTable(Contract.Tables.PersonsTable.tableName, Contract.Tables.PersonsTable.columns);
        }

        private static void PopulateTables()
        {
            InsertIntoTable(Contract.Tables.PersonsTable.tableName, Contract.Tables.PersonsTable.data);
        }

        private static void CreateTable(string tableName, KeyValuePair<string, Contract.DataTypes>[] columns)
        {
            if (dbConnection != null && dbConnection.State == System.Data.ConnectionState.Open)
            {
                string tableSetupSql = string.Format("create table {0} (", tableName);
                foreach (KeyValuePair<string, Contract.DataTypes> column in columns)
                {
                    tableSetupSql += string.Format("{0} {1},", column.Key, column.Value.Value);
                }
                tableSetupSql = tableSetupSql.TrimEnd(',');
                tableSetupSql += ")";
                SQLiteCommand cmd = new SQLiteCommand(tableSetupSql, dbConnection);
                cmd.ExecuteNonQuery();
            }

        }

        private static void InsertIntoTable(string tableName, string[] valuesSets)
        {
            SQLiteCommand cmd = null;
            foreach (string valueSet in valuesSets)
            {
                string[] valueSetValues = valueSet.Split(',');
                string sql = string.Format("insert into {0} values(", tableName);
                foreach (string value in valueSetValues)
                {
                    sql += string.Format("'{0}',", value);
                }
                sql = sql.TrimEnd(',');
                sql += ")";
                cmd = new SQLiteCommand(sql, dbConnection);
                cmd.ExecuteNonQuery();

            }
        }


        private static class Contract
        {
            /// <summary>
            /// Cheater class that acts similarly to an enum, but allows us to relate strings to the values easily, with the caveat that you must 
            /// drill down to .Value when referencing.
            /// </summary>
            public class DataTypes
            {
                private DataTypes(string value) { Value = value; }

                public string Value { get; set; }

                public static DataTypes Text { get { return new DataTypes("Text"); } }

            }

            public static class Tables
            {
                public static class PersonsTable
                {
                    public const string tableName = "Persons";
                    public static readonly KeyValuePair<string, DataTypes>[] columns;
                    public static string[] data = new string[2] { @"Jeremy Dunn,jeremydunn@jeremydunn.com", "Tester McTesty,test@test.com" };

                    //populates the columns KeyValuePair array.
                    static PersonsTable()
                    {
                        columns = new KeyValuePair<string, DataTypes>[]
                            {
                                new KeyValuePair<string, DataTypes>("Name", DataTypes.Text),
                                new KeyValuePair<string, DataTypes>("Email", DataTypes.Text)
                            };
                    }
                }

            }
        }

        public static class Queries
        {
            public static void Testing()
            {

            }
        }
    }
}

