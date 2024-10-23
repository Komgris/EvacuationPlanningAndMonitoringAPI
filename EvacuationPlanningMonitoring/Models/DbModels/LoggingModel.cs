using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvacuationPlanningMonitoring.Models.DbModels
{
    [Table("logging")]
    public class LoggingModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string VehicleID { get; set; } = string.Empty;
        public string ZoneID { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        [Column(TypeName = "jsonb")]
        public string Data { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
    }
}
