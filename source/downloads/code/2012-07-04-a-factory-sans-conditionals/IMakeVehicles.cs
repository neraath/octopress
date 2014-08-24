public interface IMakeVehicles
{
    Vehicle CreateVehicle(string color, int numDoors, string[] options = string[0]);
    bool CanCreateModel(string model);
}
