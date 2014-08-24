public class VehicleController
{
    public ActionResult buildVehicle(string make, string model, string color, int numDoors, string[] options = string[0])
    {
        Vehicle vehicle = null;
        if (make.Equals("Honda")) {
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
        } else if (make.Equals("Chevrolet")) {
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
        } else if (make.Equals("BMW")) {
            // Pompus luxury vehicle.
        } else if (make.Equals("Ferrari")) {
            // Not likely in my lifetime.
        } else if (make.Equals("Lamborghini")) {
            // I wish.
        }

        // Save.
        this.vehicleContext.Attach(vehicle);
        this.vehicleContext.SaveAll();

        // Display our newly created vehicle.
        return View("BuildVehicle", vehicle);
    }
}
