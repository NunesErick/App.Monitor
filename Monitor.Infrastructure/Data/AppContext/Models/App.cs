using System.ComponentModel.DataAnnotations;

namespace Monitor.Infrastructure.Data.AppContext.Models
{
    public class App
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(255)]
        public string InteractionLink { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
