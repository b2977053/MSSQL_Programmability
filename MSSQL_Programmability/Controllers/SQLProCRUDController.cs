using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Data.Entity;
using MSSQL_Programmability.Models;

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
        /// Read
        /// </summary>
        /// <param name="id"></param>
        /// <returns>回傳單一資料</returns>
        public IHttpActionResult Get(int id)
        {
            Exception exception = null;
            FunAndSP fun_and_sp = new FunAndSP();

            try
            {
                using (SQL_Programmability db = new SQL_Programmability())
                {
                    fun_and_sp = db.FunAndSP.Find(id);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            


            if (exception == null)
            {
                return Ok(fun_and_sp);
            }
            else
            {
                return NotFound();
            }
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
                        new_fun_and_sp = fun_and_sp;
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
