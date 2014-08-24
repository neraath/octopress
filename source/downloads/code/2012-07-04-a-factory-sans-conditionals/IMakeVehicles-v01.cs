public interface IMakeVehicles<TModelType> where TModelType : Vehicle
{
    TModelType CreateVehicle(string color, int numDoors, string[] options = string[0]);
    bool CanCreateModel(string model);
}
