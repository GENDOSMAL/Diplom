using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace RepairFlatWPF.Controller
{
    public class MakeWorkWirthDataBase
    {
        public static string NameOfSqlFile = "MakeHelp.sqlite";
        public static string PathToDB = "";
        public static void MakeFilePathAndCheck()
        {
            string PathToDataBase = Path.Combine(Path.GetDirectoryName(Path.GetTempPath()),"Repflat" ,NameOfSqlFile);
            if (!Directory.Exists(Path.GetDirectoryName(PathToDataBase)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(PathToDataBase));
            }
            if (!File.Exists(PathToDataBase))
            {
                SQLiteConnection.CreateFile(PathToDataBase);
            }
            PathToDB = $"Data Source = {PathToDataBase}";
            CheckAndMakeTableInDB();
        }

        public static void CheckAndMakeTableInDB()
        {
            var ListOFTablesDescription = MakeSomeHelp.ReturnJsonOfTable();
            foreach (var Table in ListOFTablesDescription.Tables)
            {
                CreateTableIfNotExist(Table.NameOfTable, Table.ColumnOfTable);
            }
        }

        public static void CreateTableIfNotExist(string NameOfTable, List<WorkWithDB.ColumnOfTable> columns)
        {
            if (!IsTableExist(NameOfTable))
            {
                string NameOfPK = "";
                StringBuilder CreateTableQuery = new StringBuilder($"CREATE TABLE {NameOfTable} (");
                WorkWithDB.ColumnOfTable columnOfTableLast = columns.Last();
                foreach (var colum in columns)
                {

                    if (colum.Equals(columnOfTableLast))
                    {
                        CreateTableQuery.Append($" '{colum.NameOfCol}' {colum.TypeOfCol}");
                    }
                    else
                    {
                        CreateTableQuery.Append($" '{colum.NameOfCol}' {colum.TypeOfCol},");
                    }
                    if (colum.IsPk)
                    {
                        NameOfPK = colum.NameOfCol;
                    }
                }
                string EndOfCreate = !string.IsNullOrEmpty(NameOfPK) ? $", PRIMARY KEY(`{NameOfPK}`));" : $");";

                CreateTableQuery.Append(EndOfCreate);
                MakeSomeQueryWork(CreateTableQuery.ToString());
            }
        }

        public static bool IsTableExist(string NameOfTable)
        {
            string query = "SELECT name FROM sqlite_master WHERE type='table' AND name=@tableName;";
            SQLiteParameter[] parameters = new SQLiteParameter[1];
            parameters[0] = new SQLiteParameter("@tableName", NameOfTable);
            string[] whatSelect = new string[] { "name" };
            var makeWork = MakeSomeQueryWork(query, parameters: parameters, SelectedColumns: whatSelect);
            if (makeWork != null)
            {
                string[] dd = makeWork as string[];
                if (dd.Length != 0)
                {
                    try
                    {
                        if (dd[0] == NameOfTable)
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public static object MakeSomeQueryWork(string query, string[] SelectedColumns = null, bool WorkWithTables = false, SQLiteParameter[] parameters = null)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(PathToDB))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        if (!WorkWithTables)
                        {
                            using (SQLiteDataReader reader = command.ExecuteReader())
                            {
                                if (SelectedColumns != null)
                                {
                                    if (reader.HasRows)
                                    {
                                        string[] result = new string[reader.FieldCount];
                                        if (reader.Read())
                                        {
                                            for (int i = 0; i < reader.FieldCount; i++)
                                            {
                                                if (reader.GetName(i) == SelectedColumns[i])
                                                {

                                                    result[i] = reader[reader.GetName(i)].ToString();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            return null;
                                        }
                                        return result;
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                }
                                else
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            DataTable ResultTable = new DataTable("Result");
                            using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command))
                            {
                                dataAdapter.Fill(ResultTable);
                                if (ResultTable.Rows.Count != 0)
                                {
                                    return ResultTable;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при работе с базой данных <{ex.Message}>");
            }
            return null;
        }

        public static void Run(Action<SQLiteCommand> dbAction)
        {
            try
            {

                using (SQLiteConnection connection = new SQLiteConnection(PathToDB))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        using (var transaction = connection.BeginTransaction())
                        {
                            dbAction(command);

                            transaction.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при работе с базой данных <{ex.Message}>");
            }
        }
    }
}
