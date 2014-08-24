public class VehicleController
{
    public ActionResult buildVehicle(string make, string model, string color, int numDoors, string[] options = string[0])
    {
        VehicleFactory factory = new VehicleFactory();
        Vehicle vehicle = factory.buildVehicle(make, model, color, numDoors, options);

        // Save.
        this.vehicleContext.Attach(vehicle);
        this.vehicleContext.SaveAll();

        // Display our newly created vehicle.
        return View("BuildVehicle", vehicle);
    }
}
