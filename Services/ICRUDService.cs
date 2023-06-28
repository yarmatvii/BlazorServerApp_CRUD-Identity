using FirstProject.Models;

namespace FirstProject.Services
{
    public interface ICRUDService<T>
    {
        Task<bool> AddUpdateAsync(T t);
        Task<bool> DeleteAsync(int id);
        Task<T?> GetAsync(int id);
        Task<List<T>> GetAllAsync();
    }
}
