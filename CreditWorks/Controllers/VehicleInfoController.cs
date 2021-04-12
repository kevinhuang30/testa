using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreditWorks.Models;

namespace CreditWorks.Controllers
{
    public class VehicleInfoController : Controller
    {
        public ActionResult index()
        {
            CreditWorksEntities db = new CreditWorksEntities();
            ViewData.Model = db.VehicleInfo.ToList();

            return View();
        }

        public ActionResult create()
        {
            CreditWorksEntities db = new CreditWorksEntities();
            ViewData["manufacturer"] = db.Vehicle_Manufacturer.ToList();
            return View();
        }

        public ActionResult submitForm(VehicleInfo info)
        {
            try
            {               
                CreditWorksEntities db = new CreditWorksEntities();
                List<Vehicle_Category> categoryList = db.Vehicle_Category.ToList();

                //use range to find categoryId
                foreach (var category in categoryList) {
                    if (category.RangeMin <= info.WeightOfVehicle && category.RangeMax >= info.WeightOfVehicle) {
                        info.CategoryId = category.Id;
                    }
                }
                //can not be created if can not find match Category Id
                if (info.OwnerName !=null && info.YearOfManufacture != null && info.WeightOfVehicle != null && info.CategoryId != 0)
                {
                    db.VehicleInfo.Add(new VehicleInfo()
                    {
                        ManufacturerId = info.ManufacturerId,
                        OwnerName = info.OwnerName,
                        CategoryId = info.CategoryId,
                        WeightOfVehicle = info.WeightOfVehicle,
                        YearOfManufacture = info.YearOfManufacture
                    });

                    db.SaveChanges();
                }
                return RedirectToAction("Create", "VehicleInfo");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}