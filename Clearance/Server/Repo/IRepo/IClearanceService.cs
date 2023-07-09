using Clearance.Shared;

namespace Clearance.Server.Repo.IRepo
{
    public interface IClearanceService
    {
        Task<List<ClearanceDTO>> GetAll();
        Task<List<ClearanceDTO>> GetAllByUserId(Guid Id);
        Task<List<ClearanceDirectionsDTO>> GetAllByDirection(Guid Id);
        Task<List<ClearanceDirectionsDTO>> GetAllByDirectionState(Guid Id,bool state);
       
        Task<ClearanceDTO?> GetById(int id);
        Task<ClearanceDirectionsDTO?> GetByDirectionId(int id);
         Task<List<ClearanceDirectionsDTO>?> GetByclId(int id);

        Task<List<ClearanceDTO>> Search(Guid Id,string Name);
        Task<List<ClearanceDTO>> Search(string Name);
        Task<List<ClearanceDirectionsDTO>> SearchbyDirection(Guid Id,string Name);
        Task<List<ClearanceDirectionsDTO>> SearchbyDirectionState(Guid Id,string Name,bool state);

        Task<bool> Insert(ClearanceDTO clearanceDTO);

        Task<bool> Update(ClearanceDTO clearanceDTO, int Id);
        Task<bool> RenewOrder(ClearanceDTO clearanceDTO, int Id);
        Task<bool> UpdateDirection(ClearanceDirectionsDTO clearanceDirectionsDTO, int Id, Guid UserId);

        Task<bool> Delete(int id);
    }
}
