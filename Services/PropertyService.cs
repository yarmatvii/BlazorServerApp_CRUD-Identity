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
        public async Task<Property> AddUpdateAsync(Property property)
        {
            try
            {
                if (property.Id == 0)
                {
                    _ = await _context.Properties.AddAsync(property);
                }
                else
                {
                    _ = await GetAsync(property.Id);
                    _ = _context.Properties.Update(property);
                }
                _ = await _context.SaveChangesAsync();
                return property;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding/updating property", ex.Message);
                throw;
            }
        }

        public async Task<Property> DeleteAsync(int id)
        {
            try
            {
                Property? property = await GetAsync(id);
                _ = _context.Properties.Remove(property);
                _ = await _context.SaveChangesAsync();
                return property;
            }
            catch (KeyNotFoundException ex)
            {
                // Log the invalid operation exception with a specific message.
                _logger.LogError(ex.Message, "Property with ID {Id} not found", id);
                throw;
            }
            catch (DbException ex)
            {
                // Log the database exception with a specific message.
                _logger.LogError(ex.Message, "Error occurred while deleting property with ID {Id}", id);
                throw;
            }
            catch (Exception ex)
            {
                // Log the general exception with a generic message.
                _logger.LogError(ex.Message, "An error occurred while deleting property with ID {Id}", id);
                throw;
            }
        }

        public async Task<Property?> GetAsync(int id)
        {
            return await _context.Properties.FindAsync(id) ??
                throw new KeyNotFoundException($"Property with ID {id} not found");
        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await _context.Properties.Include(x => x.Owner).ToListAsync();
        }
    }
}
