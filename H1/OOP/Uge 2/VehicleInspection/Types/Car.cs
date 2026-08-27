namespace VehicleInspection 
{

    public class Car : Vehicles
    {
        public Car(string brand, string model, DateTime productionDate, DateTime inspectionDate)
        {
            this.brand = brand;
            this.model = model;
            this.productionDate = productionDate;
            this.inspectionDate = inspectionDate;
        }

        public override string InspectionStatus()
        {
            if (productionDate > DateTime.Now.AddYears(-4))
            {
                return "This vehicle is not eligible for inspection";
            }

            if (inspectionDate > DateTime.Now.AddYears(-1))
            {
                return "This vehicle is not eligible for inspection";
            }

            return "This vehicle is eligible for inspection";
        }

        public override string DisplayInfo()
        {
            return $"Car: {brand} {model}";
        }
    }
}
