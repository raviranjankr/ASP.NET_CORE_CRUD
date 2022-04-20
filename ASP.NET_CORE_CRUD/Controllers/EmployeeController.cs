using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_CORE_CRUD.DB;
using ASP.NET_CORE_CRUD.Models;
using ASP.NET_CORE_CRUD.Library;


namespace ASP.NET_CORE_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        IDB dB = new DBCMD();
        Hashtable param;        
               
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            param = new Hashtable();
            param.Clear();
            param.Add("@Status_Int", 1);            
            DataSet ds = dB.GetData("SPEmployee", param, 0);
            if (ds != null)
            {
                employees = DT.ToList<Employee>(ds.Tables[0]);
            }
            return View(employees);
        }

        public ActionResult Details(int? id)
        {
            return View();
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    param = new Hashtable();
                    param.Add("@Status_Int", 3);
                    param.Add("@Name", collection["Name"].ToString()); // collection.Get("Name");
                    param.Add("@City", collection["City"].ToString());
                    param.Add("@State", collection["State"].ToString());
                    param.Add("@Department", collection["Department"].ToString());
                    param.Add("@Gender", collection["Gender"].ToString());
                    param.Add("@MobileNo", collection["MobileNo"].ToString());
                    param.Add("@EmailID", collection["EmailID"].ToString());
                    if (dB.Save("SPEmployee", param) > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View();
                    }
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee employee = new Employee();
            param = new Hashtable();
            param.Clear();
            param.Add("@Status_Int", 2);
            param.Add("@EmpID", id);
            DataSet ds = dB.GetData("SPEmployee", param, 0);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    employee = new Employee()
                    {
                        EmployeeId = (int)id,
                        Name = ds.Tables[0].Rows[0]["Name"].ToString(),
                        State = ds.Tables[0].Rows[0]["State"].ToString(),
                        City = ds.Tables[0].Rows[0]["City"].ToString(),
                        Department = ds.Tables[0].Rows[0]["Department"].ToString(),
                        Gender = ds.Tables[0].Rows[0]["Gender"].ToString(),
                        EmailID = ds.Tables[0].Rows[0]["EmailID"].ToString(),
                        MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString()
                    };
                    return View(employee);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, IFormCollection collection)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                param = new Hashtable();
                param.Add("@Status_Int", 4);
                param.Add("@EmpID", id);
                param.Add("@Name", collection["Name"].ToString()); 
                param.Add("@City", collection["City"].ToString());
                param.Add("@State", collection["State"].ToString());
                param.Add("@Department", collection["Department"].ToString());
                param.Add("@Gender", collection["Gender"].ToString());
                param.Add("@MobileNo", collection["MobileNo"].ToString());
                param.Add("@EmailID", collection["EmailID"].ToString());
                if (dB.Save("SPEmployee", param) > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        
       
        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                param = new Hashtable();
                param.Add("@Status_Int", 5);
                param.Add("@EmpID", id);                
                if (dB.Save("SPEmployee", param) > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
