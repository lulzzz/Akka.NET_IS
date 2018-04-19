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
        /// </summary>
        private readonly string connectionString = "Data Source=.;Initial Catalog=tradedb;Integrated Security=SSPI;";

        /// <summary>
        /// вставка позиции в бд
        /// </summary>
        /// <param name="position"></param>
        public void InsertQuery(Position position)
        {
            //ClearTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertQuery = "INSERT INTO Positions(AccountId, Money, Instrument, Lot, LotNumber, Price, OpenCote, PositionState) VALUES (@AccountId, @Money, @Instrument, @Lot, @LotNumber, @Price, @OpenCote, @PositionState)";
                using (SqlCommand command = new SqlCommand(insertQuery))
                {
                    command.Connection = connection;
                    command.Parameters.Add("@AccountId", SqlDbType.Int).Value = position.AccountId;
                    command.Parameters.Add("@Money", SqlDbType.Decimal).Value = position.AccountMoney;
                    command.Parameters.Add("@Instrument", SqlDbType.NChar).Value = position.Instrument.ToString();
                    command.Parameters.Add("@PositionState", SqlDbType.NChar).Value = position.PositionState.ToString();
                    command.Parameters.Add("@Lot", SqlDbType.Float).Value = position.Lot;
                    command.Parameters.Add("@LotNumber", SqlDbType.Float).Value = position.LotNumber;
                    command.Parameters.Add("@Price", SqlDbType.Float).Value = position.Price;
                    command.Parameters.Add("@OpenCote", SqlDbType.Float).Value = position.CoteOnOpenPostion;
                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("Добавлено {0} записей", recordsAffected);
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
        private void ClearTable()
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
