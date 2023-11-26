using JobsOpenings.Models;

namespace JobsOpenings.Interfaces
{
    public interface ILocationsRepository
    {
        bool AddLocation(Location loc);
        bool UpdateLocations(int id, Location request);
        List<Models.Location> GetLocations();
    }
}
