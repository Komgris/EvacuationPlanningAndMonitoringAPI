using EvacuationPlanningMonitoring.Validators.Interfaces;

namespace EvacuationPlanningMonitoring.Validators
{
    public class BaseValidator : IBaseValidator
    {
        public bool IsValidCoordinates(double latitude, double longitude)
        {
            return (latitude >= -90 && latitude <= 90) && (longitude >= -180 && longitude <= 180);
        }
    }
}
