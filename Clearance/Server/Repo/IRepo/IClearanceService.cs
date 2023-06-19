using Clearance.Shared;

namespace Clearance.Server.Repo.IRepo
{
    public interface IClearanceService
    {
        Task<List<ClearanceDTO>> GetAll();
        Task<List<ClearanceDTO>> GetAllByUserId(Guid Id);
        Task<List<ClearanceDTO>> GetAllByState(string State);
        Task<ClearanceDTO?> GetById(int id);
        Task<List<ClearanceDTO>> Search(Guid Id,string? Name);
        Task<List<ClearanceDTO>> Search(string? Name);
        Task<bool> Insert(ClearanceDTO clearanceDTO);

        Task<bool> Update(ClearanceDTO clearanceDTO, int Id);

        Task<bool> Delete(int id);
    }
}
