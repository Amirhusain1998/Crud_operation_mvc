using Entity_CF_Approach.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Entity_CF_Approach.Controllers
{
    public class HomeController : Controller
    {
        StudentContext db = new StudentContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]    //For submit form
        public ActionResult Create(Student s)
        {
            if(ModelState.IsValid==true)
            {
                db.Students.Add(s); //Isert Data Into Database with these Lines
                int a = db.SaveChanges();  //when Insert row saveChanges Returns 1 and no insrt row resturn 0
                if (a > 0)
                {
                    //ViewBag.InsertMessage = "<script>alert('Data Inserted')</script>";
                    //TempData["InsertMessage"] = "<script>alert('Data Inserted')</script>";
                    TempData["InsertMessage"] = "Data Inserted";
                    return RedirectToAction("Index");
                    //ModelState.Clear();
                }
                else
                {
                    ViewBag.InsertMessage = "<script>alert('Data not Inserted')</script>";
                }
            }
           
          
            return View();
        }
        public ActionResult Edit(int id)
        {
            var row = db.Students.Where(model => model.Id == id).FirstOrDefault();
            return View(row);
        }
        [HttpPost] //for Edit to change 
        public ActionResult Edit(Student s)
        {
            if(ModelState.IsValid==true)
            {
                db.Entry(s).State = EntityState.Modified; //Update Row of Database
                int a = db.SaveChanges();
                if (a > 0)
                {
                    //ViewBag.UpdateMessage = "<script>alert('Data Updated')</script>";
                    TempData["UpdateRow"] = "Update Data";
                    return RedirectToAction("Index");
                    // ModelState.Clear();
                }
                else
                {
                    ViewBag.UpdateMessage = "<script>alert('Data Not Updated')</script>";
                }
            }
            
            return View();
        }
        public ActionResult Delete(int id)
        {
            var StudentIdRow = db.Students.Where(model => model.Id == id).FirstOrDefault();
            return View(StudentIdRow);
        }
        [HttpPost]
        public ActionResult Delete(Student s)
        {
            db.Entry(s).State = EntityState.Deleted;
            int a=db.SaveChanges();
            if(a>0)
            {
                TempData["DeleteRow"] = "Delete Data";
                
            }
            else
            {
                ViewBag.DeleteData = "<script>alert('Data not Data Not Deleted')</script>";
            }
            return RedirectToAction("Index");


        }
        public ActionResult Details(int id)
        {
            var DetailId = db.Students.Where(model => model.Id == id).FirstOrDefault();

            return View(DetailId);
        }

    }
}