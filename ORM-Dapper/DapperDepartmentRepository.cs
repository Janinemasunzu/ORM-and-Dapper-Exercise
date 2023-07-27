using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ORM_Dapper
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _connection;
        public DapperDepartmentRepository(IDbConnection connection) 
        { 
            _connection = connection;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            return _connection.Query<Department>("Select * from Departments;");
            
        }

        public void InsertDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
         new { departmentName = newDepartmentName });
        }
    }
}
