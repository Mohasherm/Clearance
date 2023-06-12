using Clearance.Shared;

namespace Clearance.Server.Repo.IRepo
{
    public interface IDirectionService
    {
        Task<DirectionDTO?> GetById(int id);
        Task<List<DirectionDTO>> GetAll();
        Task<List<DirectionDTO>> Search(string? Name);
        Task<bool> Insert(DirectionDTO directionDTO);

        Task<bool> Update(DirectionDTO directionDTO, int Id);

        Task<bool> Delete(int id);
    }
}
