using Clearance.Shared;

namespace Clearance.Server.Repo.IRepo
{
    public interface ICollageDirectionService
    {
        Task<CollageDirectionDTO?> GetById(int id);
        Task<List<CollageDirectionDTO>> GetAll(int CollageId);
        Task<bool> Insert(CollageDirectionDTO collageDirectionDTO);
        Task<bool> Delete(int id);
    }
}
