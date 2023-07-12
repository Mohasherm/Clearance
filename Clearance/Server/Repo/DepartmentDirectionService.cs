using Clearance.Server.Data;
using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.EntityFrameworkCore;

namespace Clearance.Server.Repo
{
    public class DepartmentDirectionService : IDepartmentDirectionService
    {
        DataContext db;
        public DepartmentDirectionService(DataContext db)
        {
            this.db = db;
        }
        public async Task<bool> Delete(int id)
        {
            var data = await db.DepartmentDirection.FindAsync(id);
            if (data == null)
            {
                return false;
            }
            db.Remove(data);
            try
            {
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<DepartmentDirectionDTO>> GetAll(int DepId)
        {
            return await (
               from a in db.DepartmentDirection
               where a.DepartmentId == DepId
               select new DepartmentDirectionDTO
               {
                   Id = a.Id,
                   DepartmentId = a.DepartmentId,
                   DirectionId = a.DirectionId,
                   DirectionName = a.Direction.Name
               }
                  ).ToListAsync();
        }

        public async Task<DepartmentDirectionDTO?> GetById(int id)
        {
            return await (
              from a in db.DepartmentDirection
              select new DepartmentDirectionDTO
              {
                  Id = a.Id,
                  DepartmentId = a.DepartmentId,
                  DirectionId = a.DirectionId,
                  DirectionName = a.Direction.Name
              }
                 ).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCollageIdByDepartment(int depId)
        {
            return db.Departments.Where(x => x.Id ==  depId).FirstOrDefault().CollageId;
        }

        public async Task<bool> Insert(DepartmentDirectionDTO departmentDirectionDTO)
        {
            if (db.DepartmentDirection.Where(x => x.DepartmentId == departmentDirectionDTO.DepartmentId
            && x.DirectionId == departmentDirectionDTO.DirectionId).Any())
            {
                return true;
            }
            else
            {

                await db.DepartmentDirection.AddAsync(new DepartmentDirection
                {
                    Id = departmentDirectionDTO.Id,
                    DepartmentId = departmentDirectionDTO.DepartmentId,
                    DirectionId = (int)departmentDirectionDTO.DirectionId
                });

                try
                {
                    await db.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }
    }
}
