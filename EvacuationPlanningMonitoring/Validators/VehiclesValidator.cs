using EvacuationPlanningMonitoring.Models.DTOs;
using EvacuationPlanningMonitoring.Validators.Interfaces;

namespace EvacuationPlanningMonitoring.Validators
{
    public class VehiclesValidator : IVehiclesValidator
    {
        public VehiclesValidator() { }
        public List<string> IsValidVehicles(List<EvacuationZoneDTO> zones)
        {
            throw new NotImplementedException();
        }
    }
}
