public interface IVehicleFactory
{
    Vehicle CreateVehicle(string model, string color, int numDoors, string[] options = string[0]);
    bool CanCreateMake(string make);
}
