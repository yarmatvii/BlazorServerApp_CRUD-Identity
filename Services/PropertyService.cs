using FirstProject.Data;
using FirstProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace FirstProject.Services
{
    public class PropertyService : ICRUDService<Property>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PropertyService> _logger;
        public PropertyService(ApplicationDbContext context, ILogger<PropertyService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> AddUpdateAsync(Property property)
        {
            try
            {
                if (property.Id == 0)
                {
                    await _context.Properties.AddAsync(property);
                }
                else
                {
                    _context.Properties.Update(property);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding/updating property", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var property = await GetAsync(id);
                if (property == null)
                    return false;
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException ex)
            {
                // Log the database exception with a specific message.
                _logger.LogError(ex.Message, "Error occurred while deleting property with ID {Id}", id);
                return false;
            }
            catch (Exception ex)
            {
                // Log the general exception with a generic message.
                _logger.LogError(ex.Message, "An error occurred while deleting property with ID {Id}", id);
                return false;
            }
        }

        public async Task<Property?> GetAsync(int id)
        {
            return await _context.Properties.FindAsync(id);

        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await _context.Properties.Include(x => x.Owner).ToListAsync();
        }
    }
}
