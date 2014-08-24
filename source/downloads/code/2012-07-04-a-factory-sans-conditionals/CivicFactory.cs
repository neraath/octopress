public class CivicFactory : IMakeVehicles
{
    public Vehicle CreateVehicle(string color, int numDoors, string[] options = string[0])
    {
        Civic vehicle = new Civic();
        vehicle.Doors = numDoors;
        vehicle.Color = color;
        vehicle.Options = options;
        return vehicle;
    }

    public bool CanCreateModel(string model) 
    {
        return model.Equals("Civic");
    }
}
