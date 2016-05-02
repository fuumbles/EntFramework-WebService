using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Lib
{
    [Table("Program")]
    public class Program
    {
        [Key]
        [Required]
        public int ProgramID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ProgramChoice> ProgramChoices { get; set; }
        public virtual ICollection<Campus> Campuses { get; set; }

    }
}
