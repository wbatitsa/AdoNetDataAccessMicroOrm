using Microsoft.Data.SqlClient;
using System.Data;

namespace AdoNetDataAccessMicroOrm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var listOfProduct = new List<Product>();

            var connectionString = "Server=(localdb)\\MSSQLLocalDb;Database=Northwind";
            var connection = new SqlConnection(connectionString);
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Products";

            connection.Open();

            var dataReader = command.ExecuteReader();
            while (dataReader.Read()) { 
                var product = new Product();
                product.ProductID = (int)dataReader["ProductID"];
                product.ProductName = (string)dataReader["ProductName"];
                listOfProduct.Add(product);
            }
            connection.Close();


            foreach (var product in listOfProduct) {
                Console.WriteLine(product.ProductName);
            }
        }
    }

    public class Product
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }
    }
}
