using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Data.Entity;
using MSSQL_Programmability.Models;
using System.Collections.Specialized;
using System.Web;
using System.Data.SqlClient;

namespace SQLProgrammability.Controllers
{
    /// <summary>
    /// FunAndSP CRUD
    /// </summary>
    public class SQLProCRUDController : ApiController
    {

        /// <summary>
        /// List
        /// </summary>
        /// <returns>回傳所有資料</returns>
        public IHttpActionResult GetList()
        {
            Exception exception = null;
            List<FunAndSP> fun_and_sps = new List<FunAndSP>();
            try
            {
                using (SQL_Programmability db = new SQL_Programmability())
                {
                    fun_and_sps = db.FunAndSP.OrderByDescending(r => r.CreatedTime).ToList();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            

            if (exception == null)
            {
                return Ok(fun_and_sps);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// List 查詢功能
        /// </summary>
        /// <returns>回傳所有資料</returns>
        [Route("api/SQLProCRUD/search")]
        public IHttpActionResult GetListSearch()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            string Name = nvc["Name"];
            string EXECUTE = nvc["EXECUTE"];
            string content = nvc["content"];
            string remark = nvc["remark"];
            string tags = nvc["tags"];

            Exception exception = null;
            List<FunAndSP> fun_and_sps = new List<FunAndSP>();
            try
            {
                using (SQL_Programmability db = new SQL_Programmability())
                {
                    db.Database.Log = (SqlLOG) =>
                    {
                        System.Diagnostics.Debug.WriteLine(SqlLOG);
                    };

                    List<SqlParameter> parameters = new List<SqlParameter>();
                    if (!string.IsNullOrEmpty(Name))
                    {
                        parameters.Add(new SqlParameter("Name", "%" + Name + "%"));
                    }
                    if (!string.IsNullOrEmpty(EXECUTE))
                    {
                        parameters.Add(new SqlParameter("EXECUTE", "%" + EXECUTE + "%"));
                    }
                    if (!string.IsNullOrEmpty(content))
                    {
                        parameters.Add(new SqlParameter("content", "%" + content + "%"));
                    }
                    if (!string.IsNullOrEmpty(remark))
                    {
                        parameters.Add(new SqlParameter("remark", "%" + remark + "%"));
                    }
                    if (!string.IsNullOrEmpty(tags))
                    {
                        parameters.Add(new SqlParameter("tags", "%" + tags + "%"));
                    }

                    string sql = "select * from [FunAndSP]";
                    sql += parameterString(parameters);

                    var IQ_fun_and_sps = db.Database.SqlQuery<FunAndSP>(sql, parameters.ToArray());

                    fun_and_sps = IQ_fun_and_sps.ToList();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }


            if (exception == null)
            {
                return Ok(fun_and_sps);
            }
            else
            {
                return NotFound();
            }
        }

        private string parameterString(List<SqlParameter> parameters)
        {
            string sql = "";
            if (parameters.Count > 0)
            {
                sql += " where ";

                int i = 0;
                foreach (SqlParameter item in parameters)
                {
                    if (i > 0)
                    {
                        sql += " and ";
                    }
                    sql += "[" + item.ParameterName + "] like " + "@" + item.ParameterName + "";

                    i++;
                }
            }

            return sql;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="fun_and_sp"></param>
        /// <returns>回傳新增的資料</returns>
        public IHttpActionResult PostAdd(FunAndSP fun_and_sp)
        {
            DateTime dNow = DateTime.Now;
            fun_and_sp.CreatedTime = dNow;
            fun_and_sp.UpdateTime = dNow;

            Exception exception = null;
            try
            {
                using (SQL_Programmability db = new SQL_Programmability())
                {
                    db.FunAndSP.Add(fun_and_sp);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return NotFound();
            }

            if (exception == null)
            {
                return Created(Url.Link("DefaultApi", new { id = fun_and_sp.Id }), fun_and_sp);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="fun_and_sp"></param>
        /// <returns>回傳修改的資料</returns>
        public IHttpActionResult PutEdit(FunAndSP fun_and_sp)
        {

            DateTime dNow = DateTime.Now;

            FunAndSP new_fun_and_sp = new FunAndSP();
            Exception exception = null;
            try
            {
                using (SQL_Programmability db = new SQL_Programmability())
                {
                    new_fun_and_sp = db.FunAndSP.Find(fun_and_sp.Id);
                    if (new_fun_and_sp != null)
                    {
                        new_fun_and_sp.Name = fun_and_sp.Name;
                        new_fun_and_sp.Execute = fun_and_sp.Execute;
                        new_fun_and_sp.Content = fun_and_sp.Content;
                        new_fun_and_sp.Remark = fun_and_sp.Remark;
                        new_fun_and_sp.Tags = fun_and_sp.Tags;
                        new_fun_and_sp.UpdateTime = dNow;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                return NotFound();
            }

            if (exception == null && new_fun_and_sp != null)
            {
                return Created(Url.Link("DefaultApi", new { id = new_fun_and_sp.Id }), new_fun_and_sp);
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>回傳 OK 成功刪除</returns>
        public IHttpActionResult Delete(List<int> ids)
        {
            Exception exception = null;

            List<FunAndSP> proSPs = new List<FunAndSP>();
            try
            {

                using (SQL_Programmability db = new SQL_Programmability())
                {
                    proSPs = db.FunAndSP.Where(r => ids.Any(_id => _id.Equals(r.Id))).ToList();

                    foreach (var proSP in proSPs)
                    {
                        db.Entry(proSP).State = EntityState.Deleted;
                    }

                    if (proSPs.Count > 0)
                    {
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception == null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
