namespace FirstProject.Services
{
    public interface ICRUDService<T>
    {
        Task<T> AddUpdateAsync(T model);
        Task<T> DeleteAsync(int id);
        Task<T?> GetAsync(int id);
        Task<List<T>> GetAllAsync();
    }
}
