using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication9.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]

        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile ImgFile
        {
            get; set;
        }
    }
}
