using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Lib
{
    [Table("AcademicYear")]
    public class AcademicYear
    {
        [Key]
        [Required]
        public int AcademicYearID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }



        public virtual ICollection<Application> Applications { get; set; }
    }
}
