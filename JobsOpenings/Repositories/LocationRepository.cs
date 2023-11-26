using JobsOpenings.Data;
using JobsOpenings.Interfaces;
using JobsOpenings.Models;
using Microsoft.EntityFrameworkCore;
using static JobsOpenings.Models.JobsModel;

namespace JobsOpenings.Repositories
{
    public class LocationRepository : ILocationsRepository
    {
        private readonly AppDbContext _dataContext;

        public LocationRepository(AppDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool AddLocation(Location loc)
        {
            bool success = false;
            try
            {
                if (loc != null)
                {
                    _dataContext.Location.Add(loc);
                    _dataContext.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex) { success = false; }
            return success;
        }

        public bool UpdateLocations(int id, Location request)
        {
            bool success = false;
            try
            {
                var existingLoc = _dataContext.Location.FirstOrDefault(t => t.Id == id);
                if (existingLoc != null)
                {
                    Location location = DataMapper.location(request, existingLoc);
                    _dataContext.Entry(location).State = EntityState.Modified;
                    _dataContext.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex) { success = false; }
            return success;
        }

        public List<Models.Location> GetLocations()
        {
            List<Location> loc = null;
            try
            {
                loc = _dataContext.Location.ToList();
            }
            catch (Exception ex) { return loc; }
            return loc;
        }
    }
}
