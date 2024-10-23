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
    public static class ActionStatus
    {
        public const string CreateZone = "CREATE_ZONES";
        public const string CreateVehicle = "CREATE_VEHICLES";
        public const string GeneratePlan = "GENERATE_PLAN";
        public const string InCompletePlan = "INCOMPLETE_PLAN";
        public const string UpdateStatus = "UPDATE_STATUS";
        public const string ClearAll = "CLEAR_ALL";
    }
}
