using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreditWorks.Models;

namespace CreditWorks.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult index()
        {
            CreditWorksEntities db = new CreditWorksEntities();
            ViewData.Model = db.Vehicle_Category.ToList();

            return View();
        }

        public ActionResult edit(int id)
        {
            try
            {
                CreditWorksEntities db = new CreditWorksEntities();
                Vehicle_Category category = db.Vehicle_Category.Where(t => t.Id == id).FirstOrDefault();

                if (category == null)
                    return RedirectToAction("error", "Category");

                bool isOverlap = overlapRange(category.RangeMin, category.RangeMax);

                if (isOverlap)
                    return RedirectToAction("error", "Category");

                return View(category);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            try
            {
                CreditWorksEntities db = new CreditWorksEntities();
                Vehicle_Category category = db.Vehicle_Category.Where(t => t.Id == id).FirstOrDefault();

                if (category == null)
                    return RedirectToAction("Index", "Category");

                category.CategoryName = Request.Form["CategoryName"];
                category.RangeMin = Convert.ToInt32(Request.Form["RangeMin"]);
                category.RangeMax = Convert.ToInt32(Request.Form["RangeMax"]);
                category.CategoryIcon = Request.Form["CategoryIcon"];
                db.SaveChanges();

                return RedirectToAction("Index", "Category");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult error()
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            try
            {
                CreditWorksEntities db = new CreditWorksEntities();
                var categoryList = db.Vehicle_Category;

                Vehicle_Category category = categoryList.Where(t => t.Id == id).FirstOrDefault();
                int count = categoryList.Count();

                if (category == null)
                    return RedirectToAction("Index", "Category");
                VehicleInfo vehicle = db.VehicleInfo.Where(t => t.CategoryId == id).FirstOrDefault();

                //Cannot delete if categoryId has been used by VehicleInfo
                if (vehicle != null)
                    return RedirectToAction("error", "Category");

                //Cannot delete if less than 2 category
                if (count <= 2) {
                    return RedirectToAction("Index", "Category");
                }

                categoryList.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult create()
        {
            return View();
        }

        public ActionResult submitForm(Vehicle_Category category)
        {
            try
            {
                CreditWorksEntities db = new CreditWorksEntities();

                bool isOverlap = overlapRange(category.RangeMin, category.RangeMax);

                if (isOverlap)
                    return RedirectToAction("error", "Category");


                db.Vehicle_Category.Add(new Vehicle_Category()
                {
                    CategoryName = category.CategoryName,
                    CategoryIcon = category.CategoryIcon,
                    RangeMax = category.RangeMax,
                    RangeMin = category.RangeMin
                });

                db.SaveChanges();

                return RedirectToAction("Create", "Category");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool overlapRange(int rangeMin, int rangeMax) {
            if (rangeMax <= rangeMin) 
                return true;
            
            CreditWorksEntities db = new CreditWorksEntities();
            int categoryId = db.VehicleInfo.Where(x => x.WeightOfVehicle >= rangeMin)
                          .Where(y => y.WeightOfVehicle <= rangeMax).FirstOrDefault().CategoryId;

            if (categoryId == 0)
            {
                return false;
            }
            else {
                return true;
            }        
        }
    }
}