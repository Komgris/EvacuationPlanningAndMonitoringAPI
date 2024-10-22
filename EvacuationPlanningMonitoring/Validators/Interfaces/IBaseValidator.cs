namespace EvacuationPlanningMonitoring.Validators.Interfaces
{
    public interface IBaseValidator
    {
        bool IsValidCoordinates(double latitude, double longitude);
    }
}
