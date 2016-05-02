using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Lib
{
    [Table("Applicant")]
    public class Applicant
    {
        [Key]
        [Required]
        public int ApplicantId { get; set; }

        [StringLength(10)]
        public string SIN { get; set; }

        [MaxLength(10)]
        public string Prefix { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }


        [MaxLength(50)]
        [Required]
        public string MiddleName { get; set; }


        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }


        [MaxLength(50)]
        public string FirstNamePreferred { get; set; }


        [MaxLength(50)]
        public string LastNamePrevious { get; set; }


        [Required]
        public DateTime DOB { get; set; }


        [MaxLength(1)]
        [Required]
        public string Gender { get; set; }


        [MaxLength(50)]
        [Required]
        public string StreetAddress1 { get; set; }


        [MaxLength(50)]
        public string StreetAddress2 { get; set; }


        [MaxLength(50)]
        [Required]
        public string City { get; set; }


        [MaxLength(50)]
        public string Country { get; set; }


        [StringLength(2)]
        public string ProvinceStateCode { get; set; }
        [ForeignKey("ProvinceStateCode,CountryCode")]
        public virtual ProvinceState ProvinceStateFK { get; set; }


        [MaxLength(50)]
        public string ProvinceStateOther { get; set; }


        [StringLength(2)]        
        [Required]
        public string CountryCode { get; set; }
        [ForeignKey("CountryCode")]
        public virtual Country CountryFK { get; set; }


        [MaxLength(25)]
        [Required]
        public string HomePhone { get; set; }


        [MaxLength(25)]
        [Required]
        public string WorkPhone { get; set; }


        [MaxLength(25)]
        [Required]
        public string CellPhone { get; set; }


        [MaxLength(50)]
        [Required]
        public string Email { get; set; }


        [Required]
        public int Citizenship { get; set; }
        [Column(Order = 0)]
        [ForeignKey("Citizenship")]
        public virtual Citizenship CitizenshipFK { get; set; }


        [StringLength(2)]
        [Column(Order = 2)]
        public string CitizenshipOther { get; set; }
        [ForeignKey("CitizenshipOther")]
        public virtual Country CitizenshipOtherFK { get; set; }


        [MaxLength(50)]
        [Required]
        public string Password { get; set; }

        public bool HasCriminalConviction { get; set; }

        public bool OnChildAbuseRegistry { get; set; }

        public bool HasDisciplinaryAction { get; set; }

        public bool IsAfricanCanadian { get; set; }

        public bool IsFirstNations { get; set; }

        public bool IsCurrentALP { get; set; }

        public bool HasDisability { get; set; }



        public ICollection<Application> Applications { get; set; }
    }
}
