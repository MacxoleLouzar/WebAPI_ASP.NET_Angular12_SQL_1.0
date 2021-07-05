using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                            select DepartmentId,DpartmentName from dbo.Department
                      ";
            DataTable table = new DataTable();
            using(var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using(var cmd = new SqlCommand(query,con))
                using(var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post([FromUri] Department dep)
        {
            try
            {
                string query = @"
                                 insert into dbo.Department values
                               (
                                '"+ dep.DpartmentName +@"'
                               )
                                ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successfully !!!";
            }
            catch (Exception)
            {
               // return "Failed to Add";
               throw;
            }
           
        }
        public string Put([FromUri] Department dep)
        {
            try
            {
                string query = @"update dbo.Department set DpartmentName = '" + dep.DpartmentName+ @"'
                                 where DepartmentId ="+dep.DepartmentId+@" ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully !!!";
            }
            catch (Exception)
            {
                // return "Failed to Updated";
                throw;
            }

        }

        public string Delete(int id)
        {
            try
            {
                string query = @" delete from dbo.Department
                                  where DepartmentId ="+ id +@" ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                    using(var cmd = new SqlCommand(query,con))
                    using(var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted successfully !!!";
            }
            catch (Exception)
            {
                return "Failed to Delete";
            }
        }
    }
}
