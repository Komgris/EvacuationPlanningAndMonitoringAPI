using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvacuationPlanningMonitoring.Models.DbModels
{
    [Table("evacuation_zone")]
    public class EvacuationZoneModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set;}
        public string ZoneID { get; set;} = string.Empty;
        public int RemainPeople { get; set;}
        public int NumberOfPeople { get; set; }
        public int UrgencyLevel { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
