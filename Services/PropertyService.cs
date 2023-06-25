using FirstProject.Data;
using FirstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Services
{
    public class PropertyService : ICRUDService<Property>
    {
        private readonly ApplicationDbContext _context;
        public PropertyService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddUpdate(Property property)
        {
            try
            {
                if (property.Id == 0)
                    _context.Properties.Add(property);
                else
                    _context.Properties.Update(property);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var property = Get(id);
                if (property == null)
                    return false;
                _context.Properties.Remove(property);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Property? Get(int id)
        {
            return _context.Properties.Find(id);

        }

        public List<Property> GetAll()
        {
            return _context.Properties.Include(x => x.Owner).ToList();
        }
    }
}
