namespace EvacuationPlanningMonitoring.Models.DTOs
{
    public class EvacuationZoneDTO
    {
        public string ZoneID { get; set; } = string.Empty;
        public int NumberOfPeople { get; set; }
        public int UrgencyLevel { get; set; }
        public LocationCoordinatesDTO LocationCoordinates { get; set; }
        public EvacuationZoneDTO()
        {
            LocationCoordinates = new LocationCoordinatesDTO();
        }

    }
}
