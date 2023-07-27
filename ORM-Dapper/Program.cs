using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using Org.BouncyCastle.Asn1;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            var instance = new DapperDepartmentRepository(conn);

            Console.WriteLine("Please Enter in your new department");
            string userInput = Console.ReadLine();

            instance.InsertDepartment(userInput);

            var departmentCollection = instance.GetAllDepartments();

            foreach (var item in departmentCollection)
            {
                Console.WriteLine($"{item.DepartmentID} {item.Name}");
            }


        }
    }
}
