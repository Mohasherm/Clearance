using Clearance.Shared;

namespace Clearance.Server.Repo.IRepo
{
    public interface IDepartmentService
    {
        Task<DepartmentDTO?> GetById(int id);
        Task<List<DepartmentDTO>> GetAll(int collageId);
        Task<List<DepartmentDTO>> Search(string? Name);
        Task<bool> Insert(DepartmentDTO departmentDTO);

        Task<bool> Update(DepartmentDTO departmentDTO, int Id);

        Task<bool> Delete(int id);
    }
}
