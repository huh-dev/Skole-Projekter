namespace VehicleInspection
{
    public abstract class Vehicles
    {
        public string brand { get; set; }
        public string model { get; set; }
        public DateTime productionDate { get; set; }
        public DateTime inspectionDate { get; set; }

        public abstract string InspectionStatus();

        public virtual string DisplayInfo()
        {
            return $"Brand: {brand}, Model: {model}, Production Date: {productionDate.ToShortDateString()}, Inspection Date: {inspectionDate.ToShortDateString()}";
        }
    }
}
