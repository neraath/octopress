public class HondaFactory : IVehicleFactory
{
    private IList<IMakeVehicles<Honda>> factories = new List<IMakeVehicles<Honda>>();

    public VehicleFactory()
    {
        var discoveredFactories = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.GetInterfaces().Any(intrfce => intrfce.Equals(typeof(IVehicleFactory<Honda>))));
        this.factories.AddRange(discoveredFactories);
    }

    public Vehicle buildVehicle(string model, string color, int numDoors, string[] options = string[0])
    {
        IVehicleFactory usableFactory = this.factories.SingleOrDefault(fac => fac.CanCreateModel(model));
        if (usableFactory == null) throw new InvalidArgumentException("Cannot create vehicle of the requested model.");
        return usableFactory.CreateVehicle(color, numDoors, options);
    }

    public bool CanCreateMake(string make) 
    {
        return make.Equals("Honda");
    }
}
