using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prueba_banco_guayaquil.Models
{
    public class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Status { get; set; }

        [ForeignKey("EventId")]
        public int EventId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
