﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvacuationPlanningMonitoring.Models.DbModels
{
    [Table("evacuation_plan")]
    public class EvacuationPlanModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string ZoneID { get; set; } = "";
        public string VehicleID { get; set; } = "";
        public string Message { get; set; } = "";
        public int ETAMin { get; set; }
        public int NumberOfPeople { get; set; }
        public string Status { get; set; } = EvacuationPlanStatus.Ready;
    }
}
