using Clearance.Server.Data;
using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.EntityFrameworkCore;

namespace Clearance.Server.Repo
{
    public class CollageDirectionService : ICollageDirectionService
    {
        DataContext db;
        public CollageDirectionService(DataContext db)
        {
            this.db = db;
        }
        public async Task<bool> Delete(int id)
        {
            var data = await db.CollageDirections.FindAsync(id);
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

        public async Task<List<CollageDirectionDTO>> GetAll(int collageId)
        {
            return await (
               from a in db.CollageDirections
               where a.CollageId == collageId
               select new CollageDirectionDTO
               {
                   Id = a.Id,
                   CollageId = a.CollageId,
                   DirectionId = a.DirectionId,
                   DirectionName = a.Direction.Name
               }
                  ).ToListAsync();
        }

        public async Task<CollageDirectionDTO?> GetById(int id)
        {
            return await (
              from a in db.CollageDirections
              select new CollageDirectionDTO
              {
                  Id = a.Id,
                  CollageId = a.CollageId,
                  DirectionId = a.DirectionId,
                  DirectionName = a.Direction.Name
              }
                 ).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Insert(CollageDirectionDTO collageDirectionDTO)
        {
            await db.CollageDirections.AddAsync(new CollageDirection
            {
                Id = collageDirectionDTO.Id,
                CollageId = collageDirectionDTO.CollageId,
                DirectionId = collageDirectionDTO.DirectionId
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
