using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace demo1
{
    /// <summary>
    /// Summary description for webs
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class webs : System.Web.Services.WebService
    {
        //[WebMethod]
        //public DataSet GetDataInfo()
        //{
        //    string QueryString = @"select ProductName from Products where ProductID <=10";
        //    SqlConnection conn = new SqlConnection(@"data source=DESKTOP-CEQSS3O\SQLEXPRESS;initial catalog=Northwind;integrated security=True;MultipleActiveResultSets=True");
        //    conn.Open();
        //    SqlCommand QueryCommand = new SqlCommand(QueryString, conn);
        //    //QueryCommand.Parameters.Add(new SqlParameter("@ProductName", _ProductName));
        //    SqlDataAdapter da = new SqlDataAdapter(QueryCommand);
        //    DataSet dt = new DataSet();
        //    da.Fill(dt);
        //    conn.Close();
        //    return dt;
        //}
        [WebMethod]
        public tblEmployee GetEmployee(int EmployeeID)
        {
            
            tblEmployee employee = new tblEmployee();
            string databaseserver = @"data source=DESKTOP-CEQSS3O\SQLEXPRESS;initial catalog=demo;integrated security=True;MultipleActiveResultSets=True";
            using (SqlConnection conn = new SqlConnection(databaseserver))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployeeById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.Value = EmployeeID;

                cmd.Parameters.Add(parameter);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employee.Id = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                }
            }
            return employee;
        }
    }
}
