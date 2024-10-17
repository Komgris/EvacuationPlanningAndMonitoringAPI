namespace EvacuationPlanningMonitoring.Models.DbModels
{
    public class EvacuationZoneModel
    {
        public int ID { get; set;}
        public string ZoneID { get; set;} = string.Empty;
        public int NumberOfPeople { get; set; }
        public int UrgencyLevel { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
