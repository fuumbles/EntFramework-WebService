using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Lib
{
    [Table("ProvinceStates")]
    public class ProvinceState
    {
        [Key]
        [StringLength(2)]
        [Column(Order = 0)]
        public string ProvinceStateCode { get; set; }


        [Key]
        [StringLength(2)]
        [Column(Order = 1)]
        public string CountryCode { get; set; }
        [ForeignKey("CountryCode")]
        public Country Country { get; set; }


        [StringLength(100)]
        public string Name { get; set; }


        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}
