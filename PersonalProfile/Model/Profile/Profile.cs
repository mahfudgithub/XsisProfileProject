using PersonalProfile.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Model.Profile
{
    public class Profile
    {
        [Key]
        [ForeignKey("Id")]
        public ApplicationUser applicationUser { get; set; }
        public string Id { get; set; }
        [StringLength(2)]
        public string IdType { get; set; }
        [StringLength(100)]
        public string IdNo { get; set; }        
        [DefaultValue(false)]
        public bool IsActive { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
