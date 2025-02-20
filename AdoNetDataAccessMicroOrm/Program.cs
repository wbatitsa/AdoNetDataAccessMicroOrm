using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;

namespace AdoNetDataAccessMicroOrm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDb;Database=Northwind";
            var connection = new SqlConnection(connectionString);

            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                // Insert order


                //var insertOrderCommand = connection.CreateCommand();
                //insertOrderCommand.Transaction = transaction;
                //var insertOrderSql = @"
                //    INSERT Orders(CustomerID, OrderDate)
                //    VALUES(@CustomerID, @OrderDate);
                //    SELECT SCOPE_IDENTITY();
                //";
                //insertOrderCommand.CommandText = insertOrderSql;
                //insertOrderCommand.Parameters.AddWithValue("@CustomerID", "ALFKI");
                //insertOrderCommand.Parameters.AddWithValue("@OrderDate", DateTime.UtcNow);
                //int orderId = Convert.ToInt32(insertOrderCommand.ExecuteScalar());

                // Insert order detail
                //var insertDetailCommand = connection.CreateCommand();
                //insertDetailCommand.Transaction = transaction;
                //var iserrtDetailSql = @"
                //    INSERT INTO [dbo].[Order Details]
                //               ([OrderID]
                //               ,[ProductID]
                //               ,[UnitPrice]
                //               ,[Quantity]
                //               ,[Discount])
                //         VALUES
                //               (@OrderId
                //               ,@ProductId
                //               ,@UnitPrice
                //               ,@Qty
                //               ,@Discount)
                //";
                //insertDetailCommand.CommandText = iserrtDetailSql;
                //insertDetailCommand.Parameters.AddWithValue("@OrderId", orderId);
                //insertDetailCommand.Parameters.AddWithValue("@ProductId", 1);
                //insertDetailCommand.Parameters.AddWithValue("@UnitPrice", 100);
                //insertDetailCommand.Parameters.AddWithValue("@Qty", 10);
                //insertDetailCommand.Parameters.AddWithValue("@Discount", 0);
                //insertDetailCommand.ExecuteNonQuery();



                var insertOrderSql = @"
                    INSERT Orders(CustomerID, OrderDate)
                    VALUES(@CustomerID, @OrderDate);
                    SELECT SCOPE_IDENTITY();
                ";
                var orderId = connection.ExecuteScalar<int>(insertOrderSql, new
                {
                    CustomerID = "ALFKI",
                    OrderDate = DateTime.UtcNow,
                },
                transaction: transaction);

                var insertDetailSql = @"
                    INSERT INTO [dbo].[Order Details]
                               ([OrderID]
                               ,[ProductID]
                               ,[UnitPrice]
                               ,[Quantity]
                               ,[Discount])
                         VALUES
                               (@OrderId
                               ,@ProductId
                               ,@UnitPrice
                               ,@Qty
                               ,@Discount)
                ";
                connection.Execute(insertDetailSql, new
                {
                    OrderId = orderId,
                    ProductId = 1,
                    UnitPrice = 25,
                    Qty = 300,
                    Discount = 0
                }, 
                transaction: transaction);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }

    public class Product
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }
    }
}
