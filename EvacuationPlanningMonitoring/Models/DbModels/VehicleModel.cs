namespace EvacuationPlanningMonitoring.Models.DbModels
{
    public class VehicleModel
    {
        public int ID { get; set; }
        public string VehicleID { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Speed { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Status { get; set; } = VehicleStatus.Available;
    }
}
