using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.Threading.Tasks;
using MyappModels;
using System.Data.Entity.Validation;



namespace MyappDB.DB_Operations
{
    public class ApplicantsRepository
    {

        public List<CountryListModel> GetCountyList()
        {

            using (var context = new AbhijeetEntities())
            {
                var list = context.CountryList.Select(x => new CountryListModel()
                {
                    CId = x.CId,
                    CName = x.CName,
                }).ToList();
                return list;
            }

        }
        public string AddApplicant(ApplicantsModel model)
        {
            using (var context = new AbhijeetEntities())
            {
                
                Applicants apl = new Applicants()
                {
                    Apl_Name = model.Apl_Name
                    ,Age = model.Age
                    ,Gender = model.Gender
                    ,Email = model.Email
                    ,PhoneNo = model.PhoneNo                    
                    ,Country =  model.Country
                    ,State = model.State
                    
                };

                
                try
                {
                    context.Applicants.Add(apl);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                context.SaveChanges();
                return apl.Apl_Name;
                 

            }

        }
        public List<ApplicantsModel> GetAllApplicants()
        {
            using (var context = new AbhijeetEntities())
            {

                var apl = context.Applicants.Select(x => new ApplicantsModel()
                {
                    Apl_Name = x.Apl_Name,
                    Age = x.Age,
                    Gender = x.Gender,
                    Email = x.Email,
                    PhoneNo = x.PhoneNo,
                    Country = x.Country,
                    State  = x.State,
                    Id= x.Id
                    

                }).ToList();
                return apl;



            }

        }
        //public List<ApplicantsModel> GetAllApplicants()
        //{
        //    using (var context = new AbhijeetEntities())
        //    {

        //        var apl = context.Applicants.Select(x => new ApplicantsModel()
        //        {
        //            Apl_Name = x.Apl_Name,
        //            Age = x.Age,
        //            Gender = x.Gender,
        //            Email = x.Email,
        //            PhoneNo = x.PhoneNo,
        //            Country = x.Country,
        //            State = x.State,
        //            Id = x.Id


        //        }).ToList();
        //        return apl;



        //    }

        //}
        public ApplicantsModel GetAllApplicantById(int id)
        {
            using (var context = new AbhijeetEntities())
            {

                var apl = context.Applicants.Where(x=> x.Id==id).Select(x => new ApplicantsModel()
                {
                    Apl_Name = x.Apl_Name,
                    Age = x.Age,
                    Gender = x.Gender,
                    Email = x.Email,
                    PhoneNo = x.PhoneNo,
                    Country = x.Country,
                    State = x.State,
                    Id = x.Id


                }).FirstOrDefault();
                return apl;



            }

        }
        public ApplicantsModel GetApplicantByIdForEdit(int id)
        {
            using (var context = new AbhijeetEntities())
            {
                
                var apl = context.Applicants.Where(x => x.Id == id).Select(x => new ApplicantsModel()
                {
                    Apl_Name = x.Apl_Name,
                    Age = x.Age,
                    Gender = x.Gender,
                    Email = x.Email,
                    PhoneNo = x.PhoneNo,
                    Country = x.Country,
                    //State = x.State,
                    Id = x.Id
               




                }).FirstOrDefault();
                return apl;



            }

        }
        public int EditApplicantMethod(int id, ApplicantsModel model)
        {
            using (var context = new AbhijeetEntities())
            {
                
                var apl = context.Applicants.FirstOrDefault(x => x.Id == id);
                if (apl != null)
                {
                    apl.Apl_Name = model.Apl_Name;
                    apl.Age = model.Age;
                    apl.Gender = model.Gender;
                    apl.Email = model.Email;                    
                    apl.PhoneNo = model.PhoneNo;
                    apl.Country = model.Country;
                    apl.State = model.State;



                }
                context.SaveChanges();
                int UpdatedId = apl.Id;
                return UpdatedId;
            }


            
        }
        public bool DeleteApplicantMethod(int id)
        {
            using (var context = new AbhijeetEntities())
            {
                var apl = context.Applicants.FirstOrDefault(x => x.Id == id);
                if (apl !=null)
                {
                    context.Applicants.Remove(apl);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

    }
}
