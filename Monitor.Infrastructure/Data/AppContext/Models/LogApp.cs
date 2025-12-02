using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monitor.Infrastructure.Data.AppContext.Models
{
    public class LogApp
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AppId { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public DateTime DateLogged { get; set; }

        [ForeignKey("AppId")]
        public App App { get; set; }
    }
}

