using Clearance.Shared;

namespace Clearance.Server.Repo.IRepo
{
    public interface IDepartmentDirectionService
    {
        Task<DepartmentDirectionDTO?> GetById(int id);
        Task<List<DepartmentDirectionDTO>> GetAll(int DepId);
        Task<bool> Insert(DepartmentDirectionDTO collageDirectionDTO);
        Task<bool> Delete(int id);
    }
}
