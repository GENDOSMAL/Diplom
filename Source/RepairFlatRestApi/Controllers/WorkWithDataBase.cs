using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RepairFlatRestApi.Controllers
{
    public class WorkWithDataBase
    {
        public static List<Tuple<string, string>> SelectSmallVolumeOfData(string query, SqlParameter[] sqlParameters, string[] nameOfSelectColumns)
        {
            Logger.WriteToLog(Logger.TypeOfRecord.Information, nameof(WorkWithDataBase), nameof(SelectSmallVolumeOfData), $"Строка для подключения <{Properties.Settings.Default.ConnectionString}> ");
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.ConnectionString))
                {
                    sqlConnection.Open();
                    Logger.WriteToLog(Logger.TypeOfRecord.Information, nameof(WorkWithDataBase), nameof(SelectSmallVolumeOfData), $"Подключение к серверу  состоялось по пути <{Properties.Settings.Default.ConnectionString}> ");
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        if (sqlParameters != null)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddRange(sqlParameters);
                        }
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                if (reader.HasRows)
                                {
                                    List<Tuple<string, string>> result = new List<Tuple<string, string>>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        for (int j = 0; j < nameOfSelectColumns.Length; j++)
                                        {
                                            if (reader.GetName(i) == nameOfSelectColumns[j])
                                            {
                                                result.Add(Tuple.Create(nameOfSelectColumns[j], reader[reader.GetName(i)].ToString()));
                                            }
                                        }
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
                                return null;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Произошла ошибка в модуле Controllers{nameof(Controllers)}::{nameof(SelectSmallVolumeOfData)}. Ошибка <{ex.ToString().Replace(Environment.NewLine, "")}>");
            }
        }
        public static bool MakeUpdateAndInsert(string query, SqlParameter[] sqlParameters)
        {
            Logger.WriteToLog(Logger.TypeOfRecord.Information, nameof(WorkWithDataBase), nameof(SelectSmallVolumeOfData), $"Строка для подключения <{Properties.Settings.Default.ConnectionString}> ");
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.ConnectionString))
                {
                    sqlConnection.Open();
                    Logger.WriteToLog(Logger.TypeOfRecord.Information, nameof(WorkWithDataBase), nameof(SelectSmallVolumeOfData), $"Подключение к серверу  состоялось по пути <{Properties.Settings.Default.ConnectionString}> ");
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        if (sqlParameters != null)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddRange(sqlParameters);
                        }
                        int ff = command.ExecuteNonQuery();
                        if (ff != -1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Произошла ошибка в модуле Controllers{nameof(Controllers)}::{nameof(MakeUpdateAndInsert)}. Ошибка <{ex.ToString().Replace(Environment.NewLine, "")}>");
            }
        }
    }
}