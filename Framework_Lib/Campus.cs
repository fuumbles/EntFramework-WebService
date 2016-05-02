using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Lib
{
    [Table("Campus")]
    public class Campus
    {
        [Key]
        [Required]
        public int CampusId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        //o-to-m
        public virtual ICollection<ProgramChoice> ProgramChoices { get; set; }

        //m-to-m
        public virtual ICollection<Program> Programs { get; set; }
    }
}
