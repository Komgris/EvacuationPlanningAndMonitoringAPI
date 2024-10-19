namespace EvacuationPlanningMonitoring.Models
{
    public static class VehicleStatus
    {
        public const string Available = "AVAILABLE"; // รถพร้อมจัดแผน
        public const string Assigned = "ASSIGNED"; // รถจัดแผนแล้ว
    }
    public static class EvacuationPlanStatus
    {
        public const string Ready = "READY";
        public const string InProgress = "INPROGRESS";
        public const string Done = "DONE";
    }
}
