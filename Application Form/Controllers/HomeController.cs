using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyappDB;
using MyappDB.DB_Operations;
using MyappModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.IO;



namespace Application_Form.Controllers
{
    public class HomeController : Controller
    {
        ApplicantsRepository AppRepo = new ApplicantsRepository();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
           List<CountryListModel> ClistFromDB = AppRepo.GetCountyList();
        //    ViewBag.ClistForEditView = new SelectList(ClistFromDB, "CName", "CName");

            ViewBag.ClistForCreateView = new SelectList (ClistFromDB,"CName", "CName");
            List <StateList> Slist = new List<StateList>();
            
            ViewBag.SlistForView = new SelectList(Slist, "SName", "SName");
            ViewBag.AddedMessage = TempData["SuccessMessage"];
            return View();
        }
        
        //public JsonResult GetCountryList()
        //{

        //    using (var context = new AbhijeetEntities())
        //    {
        //        var cntList = context.CountryList.ToList();
        //        //return new JsonResult(cntList);
        //        //var json = JsonConvert.SerializeObject(cntList);
        //        JsonConvert.SerializeObject(cntList);
        //        return Json(cntList, JsonRequestBehavior.AllowGet);



        //    }
        //}
        [HttpPost]
        public ActionResult Create(ApplicantsModel model)
        {
           

            if (ModelState.IsValid)
            {
                string Name = null;

                Name = AppRepo.AddApplicant(model);
                if (Name != "Null")
                {
                    ModelState.Clear();
                    TempData["SuccessMessage"] = "New data added for " + Name + "!";


                }

            }
            return RedirectToAction("Create");


        }
        public ActionResult ShowApplicantList()
        {
            ApplicantsRepository AR = new ApplicantsRepository();

            var AplList = AR.GetAllApplicants();
            return View(AplList);
        }
        public ActionResult GetDetails(int id)
        {
            ApplicantsRepository AR = new ApplicantsRepository();
            var apl = AR.GetAllApplicantById(id);

            return View(apl);
        }
        public ActionResult EditApplicant(int id)
        {
            ApplicantsRepository AR = new ApplicantsRepository();
            var apl = AR.GetApplicantByIdForEdit(id);
            using (var context = new AbhijeetEntities())
            {
                var Clist = new SelectList(context.CountryList.ToList(), "Cname", "Cname");
                var Slist = new SelectList(context.StateList.ToList(), "Sname", "Sname");
                apl.ClistforEdit = Clist;
                apl.SlistforEdit = Slist;
                return View(apl);
            }

            
        }
        [HttpPost]
        public ActionResult EditApplicant(ApplicantsModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicantsRepository AR = new ApplicantsRepository();
                int UpdatedId = AR.EditApplicantMethod(model.Id, model);
            }
            return RedirectToAction("ShowApplicantList");

        }

       
        public ActionResult DeleteApplicant(int id)
        {
            ApplicantsRepository AR = new ApplicantsRepository();
            bool status = AR.DeleteApplicantMethod(id);
            if (status)
            {
                return RedirectToAction("ShowApplicantList");
            }
            return View();
        }

        public JsonResult getStateListbyStateCname(string SCname)
        {
            using (var context = new AbhijeetEntities())
            {
                List<StateList> SlistFromDB = context.StateList.Where(x => x.StateCname == SCname).ToList();
                //ViewBag.SlistForView = new SelectList(SlistFromDB, "Id", "SName");
                 var list = JsonConvert.SerializeObject(SlistFromDB);
                return Json (list, JsonRequestBehavior.AllowGet);

            }
            
        }
    }


        

    
}
