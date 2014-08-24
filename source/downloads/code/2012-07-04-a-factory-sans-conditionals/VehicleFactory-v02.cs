public class VehicleFactory
{
    private IList<IVehicleFactory> factories = new List<IVehicleFactory>();

    public VehicleFactory()
    {
        var discoveredFactories = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.GetInterfaces().Any(intrfce => intrfce.Equals(typeof(IVehicleFactory))));
        this.factories.AddRange(discoveredFactories);
    }

    public Vehicle buildVehicle(string make, string model, string color, int numDoors, string[] options = string[0])
    {
        IVehicleFactory usableFactory = this.factories.SingleOrDefault(fac => fac.CanCreateMake(make));
        if (usableFactory == null) throw new InvalidArgumentException("Cannot create vehicle of the requested make.");
        return usableFactory.CreateVehicle(model, color, numDoors, options);
    }
}
