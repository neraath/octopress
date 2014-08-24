public class ChevroletFactory
{
    public Vehicle CreateVehicle(string model, string color, int numDoors, string[] options = string[0])
    {
        Vehicle vehicle = null;
        if (model.Equals("Suburban")) {
            vehicle = new Suburban();
            vehicle.Doors = numDoors;
            vehicle.Color = color;
            vehicle.Options = options;
        } else if (model.Equals("Silverado")) {
                vehicle = new Silverado();
            vehicle.Doors = numDoors;
            vehicle.Color = color;
            vehicle.Options = options;
        } else {
            throw new InvalidArgumentException("Cannot make requested model for Chevrolet");
        }

        return vehicle;
    }
}
