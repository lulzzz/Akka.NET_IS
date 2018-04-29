using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEmulator.Types
{
    /// <summary>
    /// класс для работы с бд
    /// </summary>
    public class MSSQL
    {
        /// <summary>
        /// строка соединения
        /// возникли проблемы на разных машинах
        /// на десктопе connectionString = "Data Source=.;Initial Catalog=tradedb;Integrated Security=SSPI;
        /// на ноуте connectionString = "Data Source=HP\\HPSERVER;Initial Catalog=tradedb;Integrated Security=SSPI;
        /// </summary>
        private readonly string connectionString = "Data Source=.;Initial Catalog=tradedb;Integrated Security=SSPI;";
        
        /// <summary>
        /// вставка позиции в бд
        /// </summary>
        /// <param name="account"></param>
        public void InsertPositionQuery(Account account)
        {
            const string storedProc = "sp_InsertPosition";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProc))
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@AccountId", SqlDbType.Int).Value = account.Id;
                    command.Parameters.Add("@Instrument", SqlDbType.NChar).Value = account.Position.Instrument.ToString();
                    command.Parameters.Add("@Lot", SqlDbType.Float).Value = account.Position.Lot;
                    command.Parameters.Add("@LotNumber", SqlDbType.Float).Value = account.Position.LotNumber;
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        //Console.WriteLine("Открыта позиция для аккаунта {0}", account.Id);
                    }
                    catch(SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        /// <summary>
        /// очистить таблицу
        /// </summary>
        /// <param name="position"></param>
        public void ClearTable()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string clearQuery = "TRUNCATE TABLE Positions";
                using (SqlCommand command = new SqlCommand(clearQuery, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
