using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpdateApp.Models;

namespace UpdateApp.Controllers
{
    public class EmployeeController : Controller
    {

        private EmployeeContextn db = new EmployeeContextn();

        //
        // GET: /Employee/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Employee employee)
        {
           // HttpPostedFileBase file;
            
      
            

            try
            {
                if (ModelState.IsValid)
                {
                    using (var db = new EmployeeContextn())
                    {
                      //  var path = Server.MapPath("~/Images/" + file.FileName);
                       // ViewBag.Path = "File Uploaded Successfully! Wow";
                       // employee.Photos = (Employee)path;
                        // file.SaveAs(path);
                        db.Employees.Add(employee);
                        db.SaveChanges();
                    }
                }
                ViewBag.Message = "Save Successfully";
            }
            catch (Exception)
            {

                ViewBag.Message = "Error is Occured";
            }


            return View(employee);
        }


        [HttpGet]
        public JsonResult GetAllP()
        {
            List<Employee> aList = new List<Employee>();

            using (EmployeeContextn context = new EmployeeContextn())
            {
                aList = context.Employees.ToList();
            }
            return Json(aList, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Get(int id)
        {
            //var id = emp.EmpID;

            try
            {
                using (EmployeeContextn context = new EmployeeContextn())
                {
                    var obj = context.Employees.FirstOrDefault(r => r.EmpID == id);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Error");

            }

           
           
        }



        [HttpPost]
        public JsonResult UpdateData(Employee objEmployee)
        {

            var dbList = from item in db.Employees
                         where item.EmpID == objEmployee.EmpID
                         select item;


            foreach (var employee1 in dbList)
            {
                if (employee1.EmpID == objEmployee.EmpID)
                {

                    employee1.FirstName = objEmployee.FirstName;
                    employee1.Surname = objEmployee.Surname;
                    employee1.Email = objEmployee.Email;
                    employee1.DOB = objEmployee.DOB;
                    db.SaveChanges();
                    


                }
            }

            return Json("Update Successfull");
        }


        public JsonResult DeleteRecord(int id) 
        { 
           //var id = employee.EmpID;

            try
            {
                using (EmployeeContextn context = new EmployeeContextn())
                {
                    var obj = context.Employees.FirstOrDefault(r => r.EmpID == id);
                    context.Employees.Remove(obj);
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                return Json("Error");
                
            }

            return Json("Delete Successfull");
        }


	}
}