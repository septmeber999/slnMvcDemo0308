using prjMauiDemo.Models;
using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class AController : Controller
    {
        public string testQuery()
        {
            CCustomerFactory cc = new CCustomerFactory();
            return cc.queryByAll().Count().ToString()pp[;
        }
        public string testUpdate()
        {
            CCustomer x = new CCustomer()
            {
                fId = 5,
                fName = "kevin",
                fPassword = "4321"
            };
            (new CCustomerFactory()).update(x);
            return "修改成功";
        }
        public string testDELETE(int id)
		{
			(new CCustomerFactory()).delete(id);
			return "刪除成功";
		}
		public string testInner()
		{
			CCustomer x = new CCustomer()
			{
				fAddress = "Kaoshung",
                fEmail = "tom@gmail.com",
                fName = "Tom",
                fPassword = "1234",
                fPhone = "0913572468"
			};
			(new CCustomerFactory()).ctreat(x);
			return "成功";
		}

		public ActionResult bindingById(int? id)
		{
			CCustomer x = null;

			if (id != null)
			{

				SqlConnection con = new SqlConnection();
				con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
				con.Open();

				SqlCommand cmd = new SqlCommand(
					"SELECT * FROM tCustomer WHERE fId=" + id.ToString(),
					con);
				SqlDataReader reader = cmd.ExecuteReader();

				if (reader.Read())
				{
					x = new CCustomer()
					{
						fId = (int)reader["fId"],
						fName = reader["fName"].ToString(),
						fPhone = reader["fPhone"].ToString()
					};
				}
				con.Close();
			}

			return View(x);
		}
		public ActionResult showById(int? id)
        {
            if (id != null)
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM tCustomer WHERE fId=" + id.ToString(),
                    con);
                SqlDataReader reader = cmd.ExecuteReader();
               
                if (reader.Read())
                {
                    CCustomer x = new CCustomer()
                    {
                        fId=(int)reader["fId"],
                        fName=reader["fName"].ToString(),
                        fPhone=reader["fPhone"].ToString()
                    };
                    ViewBag.KK = x;
                }
                con.Close();
            }

            return View();
        }
        public string queryById(int? id)
        {
            if (id == null)
                return "沒有指定id";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM tCustomer WHERE fId=" + id.ToString(),
                con);
            SqlDataReader reader = cmd.ExecuteReader();
            string s = "查無任何資料";
            if (reader.Read())
                s = reader["fName"].ToString() + "<br/>" + reader["fPhone"].ToString();
            con.Close();
            return s;
        }
        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }
        public string demoParameter(int? id)
        {

            if (id == null)
                return "沒有指定id";
            if (id == 0)
                return "XBox 加入購物車成功";
            else if (id == 1)
                return "PS5 加入購物車成功";
            else if (id == 2)
                return "Nintendo Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoRequest()
        {
            string id = Request.QueryString["pid"];
            if (id == "0")
                return "XBox 加入購物車成功";
            else if (id == "1")
                return "PS5 加入購物車成功";
            else if (id == "2")
                return "Nintendo Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\QNote\8000.jpg");
            Response.End();
            return "";
        }

        public string sayHello()
        {
            return "Hello Asp.Net MVC";
        }
        [NonAction]

        public string lotto()
        {
            return (new CLottoGen()).getNumber();
        }
        // GET: A
        public ActionResult Index()
        {
            return View();
        }
    }
}