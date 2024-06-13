using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        public bool IsMain { get; set; }
        [Required]
        public string PublicId { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}