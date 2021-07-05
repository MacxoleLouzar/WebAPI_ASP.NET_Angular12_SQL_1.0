using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @" select EmployeeId,EmployeeName,Department,
                              convert(varchar(10),DateOfJoining,120) as DateOfJoining,PhotoFilename
                              from dbo.Employee
                            ";


            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        public string Post([FromUri] Employee emp)
        {
            try
            {
                string query = @"
                                  insert into dbo.Employee values
                                    (
                                     '" + emp.EmployeeName + @"'
                                     ,'" + emp.Department + @"'
                                     ,'" + emp.DateOfJoining + @"'
                                     ,'" + emp.PhotoFileName + @"'
                                     )";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successful!!!";

            }
            catch (Exception)
            {
                return "Failed to Add";
            }
        }
        public string Put([FromUri] Employee emp)
        {
            try
            {
                string query = @"
                                 update dbo.Employee set
                                 EmployeeName = '" + emp.EmployeeName + @"'
                                 ,Department = '" + emp.Department + @"'
                                 ,DateOfJoining = '" + emp.DateOfJoining + @"'
                                 ,PhotoFileName = '" + emp.PhotoFileName + @"'
                                 where EmployeeId =" + emp.EmployeeId + @"
                                 ";

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
                return "Failed to Updated";
            }

        }
        public string Delete(int id)
        {
            try
            {
                string query = @" 
                                  delete from dbo.Employee
                                  where EmployeeId=" + id + @" ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
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

        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @" select DpartmentName from dbo.Department";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        
        [Route("api/Employee/SaveFile")]
        [HttpPost]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);

                postedFile.SaveAs(physicalPath);

                return filename;
            }
            catch(Exception)
            {
                /// return "anonymous.png";
                throw;
            }
        }
    }
}

