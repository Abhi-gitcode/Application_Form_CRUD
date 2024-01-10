using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyappModels
{
    public class ApplicantsModel
    {
        [Required(ErrorMessage = "You must enter a name")]
        [DisplayName("Applicant's Name")]
        public string Apl_Name { get; set; }
        [Required]
        [Range (18,100)]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^([0-9]{10})$",ErrorMessage= "Invalid Mobile Number.")]
        public string PhoneNo { get; set; }
        
        public string Country { get; set; }
       
        public string State { get; set; }
        public int Id { get; set; }

        public IEnumerable<SelectListItem> ClistforEdit { get; set; }
        public IEnumerable<SelectListItem> SlistforEdit { get; set; }



    }
}
