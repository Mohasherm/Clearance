using Clearance.Shared;

namespace Clearance.Server.Repo.IRepo
{
    public interface ICollageService
    {
        Task<CollageDTO?> GetById(int id);
        Task<List<CollageDTO>> GetAll();
        Task<List<CollageDTO>> Search(string? Name);
        Task<bool> Insert(CollageDTO directionDTO);

        Task<bool> Update(CollageDTO collageDTO, int Id);

        Task<bool> Delete(int id);
    }
}
