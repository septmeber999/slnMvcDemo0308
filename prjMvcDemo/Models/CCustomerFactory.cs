using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace prjMvcDemo.Models
{
	public class CCustomerFactory
	{
		private void excuteSql(string sql, List<SqlParameter> paras)
		{
			SqlConnection con = new SqlConnection();
			con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
			con.Open();
			SqlCommand cmd = new SqlCommand(sql, con);

			if (paras != null)
			{
				cmd.Parameters.AddRange(paras.ToArray());
			}

			cmd.ExecuteNonQuery();
			con.Close();
		}

		public List<CCustomer> queryByAll()
		{
			string sql = "SELECT * FROM tCustomer";
			return queryBySql(sql, null);

		}
		public CCustomer queryByID(int id)
		{
			string sql = "SELECT * FROM tCustomer WHERE fid=@k_FID";
			List<SqlParameter> paras = new List<SqlParameter>();
			paras.Add(new SqlParameter("k_FID", (object)id));
			List<CCustomer> list = queryBySql(sql, paras);
			if (list.Count == 0)
			{
				return null;
			}
			return list[0];
		}
		public List<CCustomer> queryBySql(string sql,List<SqlParameter> paras)
		{
			List<CCustomer> list = new List<CCustomer>();
			SqlConnection con = new SqlConnection();

			con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
			con.Open();
			SqlCommand cmd = new SqlCommand(sql, con);

			if (paras != null)
			{
				cmd.Parameters.AddRange(paras.ToArray());
			}
			SqlDataReader reader= cmd.ExecuteReader();

			while (reader.Read())
			{
				CCustomer x = new CCustomer();
				x.fId = (int)reader["fId"];
				if (!DBNull.Value.Equals(reader["fName"]))
				{
					x.fName = (string)reader["fName"];
				}
				if (!DBNull.Value.Equals(reader["fPhone"]))
				{
					x.fPhone = (string)reader["fPhone"];
				}
				if (!DBNull.Value.Equals(reader["fEmail"]))
				{
					x.fEmail = (string)reader["fEmail"];
				}
				if (!DBNull.Value.Equals(reader["fAddress"]))
				{
					x.fAddress = (string)reader["fAddress"];
				}
				if (!DBNull.Value.Equals(reader["fPassword"]))
				{
					x.fPassword= (string)reader["fPassword"];
				}
				list.Add(x);
			}

			con.Close();
			return list;
		}
		public void update(CCustomer p)
		{
			List<SqlParameter> paras = new List<SqlParameter>();
			string sql = "UPDATE tCustomer SET ";

			if (!string.IsNullOrEmpty(p.fName))
			{
				sql += " fName=@k_FNAME, ";
				paras.Add(new SqlParameter("k_FNAME", (object)p.fName));
			}
			if (!string.IsNullOrEmpty(p.fPhone))
			{
				sql += " fPhone=k_FPHONE, ";
				paras.Add(new SqlParameter("k_FPHONE", (object)p.fPhone));
			}
			if (!string.IsNullOrEmpty(p.fEmail))
			{
				sql += " fEmail=@k_EMAIL, ";
				paras.Add(new SqlParameter("k_EMAIL", (object)p.fEmail));
			}
			if (!string.IsNullOrEmpty(p.fAddress))
			{
				sql += " fAddress=@k_ADDRESS, ";
				paras.Add(new SqlParameter("k_ADDRESS", (object)p.fAddress));
			}
			if (!string.IsNullOrEmpty(p.fPassword))
			{
				sql += " fPassword=@k_PASSWORD, ";
				paras.Add(new SqlParameter("k_PASSWORD", (object)p.fPassword));
			}
			if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
			{
				sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
			}
			sql += " WHERE fid=@k_FID";
			paras.Add(new SqlParameter("k_FID", (object)p.fId));
			excuteSql(sql, paras);
		}
		public void delete(int id)
		{
			string sql = "DELETE FROM tCustomer WHERE fid =@k_fid ";
			List<SqlParameter> paras = new List<SqlParameter>();
			paras.Add(new SqlParameter("k_fid", (object)id));



			excuteSql(sql, paras);
		}
		public void ctreat(CCustomer p)
		{
			List<SqlParameter> paras = new List<SqlParameter>();
			string sql = " INSERT INTO tCustomer (";

			if (!string.IsNullOrEmpty(p.fName))
			{
				sql += " fName, ";
			}
			if (!string.IsNullOrEmpty(p.fPhone))
			{
				sql += " fPhone, ";
			}
			if (!string.IsNullOrEmpty(p.fEmail))
			{
				sql += " fEmail, ";
			}
			if (!string.IsNullOrEmpty(p.fAddress))
			{
				sql += " fAddress, ";
			}
			if (!string.IsNullOrEmpty(p.fPassword))
			{
				sql += " fPassword, ";
			}
			if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
			{
				sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
			}
				sql += " )VALUES( ";
			if (!string.IsNullOrEmpty(p.fName))
			{
				sql += " @k_FMANE, ";
				paras.Add(new SqlParameter("k_FMANE", (object)p.fName));
			}
			if (!string.IsNullOrEmpty(p.fPhone))
			{
				sql += " @k_FPHONE, ";
				paras.Add(new SqlParameter("k_FPHONE", (object)p.fPhone));
			}
			if (!string.IsNullOrEmpty(p.fEmail))
			{
				sql += " @k_FEMAIL, ";
				paras.Add(new SqlParameter("k_FEMAIL", (object)p.fEmail));
			}
			if (!string.IsNullOrEmpty(p.fAddress))
			{
				sql += " @k_FADDRESS, ";
				paras.Add(new SqlParameter("k_FADDRESS", (object)p.fAddress));
			}
			if (!string.IsNullOrEmpty(p.fPassword))
			{
				sql += " @k_FPASSWORD) ";
				paras.Add(new SqlParameter("k_FPASSWORD", (object)p.fPassword));
			}
			if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
			{
				sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
				sql += ")";
			}

			excuteSql(sql, paras);

		}
	}
}
