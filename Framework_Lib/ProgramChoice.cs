using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework_Lib
{
    [Table("ProgramChoice")]
    public class ProgramChoice
    {
        [Key]
        [Required]
        public int ProgramChoiceId { get; set; }

        [Required]
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public virtual Application ApplicationFK { get; set; }

        [Required]
        public int CampusId { get; set; }
        [ForeignKey("CampusId")]
        public virtual Campus CampusFK { get; set; }

        [Required]
        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public virtual Program ProgramFK { get; set; }
        
    }
}
