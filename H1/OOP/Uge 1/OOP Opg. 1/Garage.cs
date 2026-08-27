namespace OOP_Opg_1
{
    internal class Garage
    {
        private List<Car> cars = new List<Car>();

        public void AddCar(Car car)
        {
            //Check if the car is already in the garage
            try
            {
                cars.Add(car);
                Console.WriteLine("Car added to the garage");
            }
            catch (Exception e)
            {
                Console.WriteLine("Car is already in the garage");
            }
        }

        public void ShowCars()
        {
            foreach (Car car in cars)
            {
                car.ShowInfo();
            }
        }
        
    }
}
