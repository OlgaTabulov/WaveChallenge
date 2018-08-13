using Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataAccess
    {
        //TODO: move this to a secure/configurable location. (registry)
        private static string ConnectionSelect = @"Data Source=DESKTOP-KC59S8C\SQLEXPRESS; Initial Catalog=Wave; User id=sa; Password=111;";
        private static string ConnectionInsert = @"Data Source=DESKTOP-KC59S8C\SQLEXPRESS; Initial Catalog=Wave; User id=sa; Password=111;";
        private static string ConnectionUpdate = @"Data Source=DESKTOP-KC59S8C\SQLEXPRESS; Initial Catalog=Wave; User id=sa; Password=111;";
        private static string ConnectionDelete = @"Data Source=DESKTOP-KC59S8C\SQLEXPRESS; Initial Catalog=Wave; User id=sa; Password=111;";

        public static ResultObject SqlSelect<T>(string query, SqlParameter[] parameters) where T : new()
        {
            var result = new ResultObject() { Success = true };

            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    conn.ConnectionString = ConnectionSelect;
                    conn.Open();
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddRange(parameters);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<T> rowList = new List<T>();
                        while (reader.Read())
                        {
                            T row = new T();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var objectProperty = row.GetType().GetProperty(reader.GetName(i));
                                if (objectProperty != null)
                                {
                                    if (row.GetType() == typeof(Int32))
                                        objectProperty.SetValue(row, reader.GetInt32(i));
                                    if (row.GetType() == typeof(Decimal))
                                        objectProperty.SetValue(row, reader.GetDecimal(i));
                                    if (row.GetType() == typeof(DateTime))
                                        objectProperty.SetValue(row, reader.GetDateTime(i));
                                    if (row.GetType() == typeof(string))
                                        objectProperty.SetValue(row, reader.GetString(i));
                                    else
                                        objectProperty.SetValue(row, reader.GetValue(i));
                                }
                            }
                            rowList.Add(row);
                        };
                        result.Value = rowList;
                    }
                }
                catch (SqlException er)
                {
                    result.Success = false;
                    result.Message = er.Message;
                }
            }

            return result;
        }
        public static ResultObject SqlNonQuery(string query, SqlParameter[] parameters)
        {
            var result = new ResultObject() { Success = true };

            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    conn.ConnectionString = ConnectionSelect;
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    sqlCommand.Parameters.AddRange(parameters);
                    var rowsAffected = sqlCommand.ExecuteNonQuery();
                    result.Value = rowsAffected;
                }
                catch (SqlException er)
                {
                    result.Success = false;
                    result.Message = er.Message;
                }
            }

            return result;
        }

    }
}
