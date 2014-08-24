public class HondaFactory : IVehicleFactory
{
    public Vehicle CreateVehicle(string model, string color, int numDoors, string[] options = string[0])
    {
        Vehicle vehicle = null;
        if (model.Equals("Civic")) {
            // Ooh! My favorite!
            vehicle = new Civic();
            vehicle.Doors = numDoors;
            vehicle.Color = color;
            vehicle.Options = options;
        } else if (model.Equals("Accord")) {
            // Only comes in 4 doors. So, ignore numDoors.
            vehicle = new Accord();
            vehicle.Color = color;
            vehicle.Options = options;
        } else if (model.Equals("CR-V")) {
            // Only comes in 4 doors. So, ignore numDoors.
            vehicle = new CRV();
            vehicle.Color = color;
            vehicle.Options = options;
        } else {
            throw new InvalidArgumentException("Cannot make requested model for Honda");
        }

        return vehicle;
    }

    public bool CanCreateMake(string make)
    {
        return make.Equals("Honda");
    }
}
