using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Lib
{
    [Table("Applications")]
    public class Application
    {
        [Key]
        [Required]
        public int ApplicationId { get; set; }


        [Required]
        public string ApplicationDate { get; set; }

        [Required]
        public int ApplicantId { get; set; }
        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }

        public int ApplicationFee { get; set; }

        [Required]
        public bool Paid { get; set; }

        
               

        public virtual ICollection<AcademicYear> AcademicYears { get; set; }
    }
}
