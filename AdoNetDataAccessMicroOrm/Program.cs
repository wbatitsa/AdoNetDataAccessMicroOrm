using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AdoNetDataAccessMicroOrm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDb;Database=Northwind";
            var connection = new SqlConnection(connectionString);

            var param = new DynamicParameters(new
            {
                ProductName = "Banana",
                UnitPrice = 50.00
            });
            param.Add("@ProductID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            connection.Execute("ProductsInsert", param, commandType: CommandType.StoredProcedure);

            int insertedId = param.Get<int>("@ProductID");

            Console.WriteLine("Inserted id: " + insertedId);
        }
    }

    public class Product
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }
    }
}
