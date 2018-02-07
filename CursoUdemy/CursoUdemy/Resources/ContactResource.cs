using System.ComponentModel.DataAnnotations;

namespace CursoUdemy.Resources
{
    public class ContactResource
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Emailo { get; set; }

        [Required]
        [StringLength(255)]
        public string Phone { get; set; }
    }

}
