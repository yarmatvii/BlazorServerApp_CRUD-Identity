using FirstProject.Models;

namespace FirstProject.Services
{
    public interface ICRUDService<T>
    {
        bool AddUpdate(T t);
        bool Delete(int id);
        T? Get(int id);
        List<T> GetAll();
    }
}
