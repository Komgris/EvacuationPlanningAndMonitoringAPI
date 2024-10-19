using System.ComponentModel.DataAnnotations;

namespace EvacuationPlanningMonitoring.Models.DbModels
{
    public class BaseDbModel
    {
        [Key]
        public int Id { get; set; }
        public bool Valid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public BaseDbModel()
        {
            Valid = true;
        }
    }
}
