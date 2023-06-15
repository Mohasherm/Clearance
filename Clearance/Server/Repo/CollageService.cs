using Clearance.Server.Data;
using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.EntityFrameworkCore;

namespace Clearance.Server.Repo
{
    public class CollageService : ICollageService
    {
        DataContext db;
        public CollageService(DataContext db)
        {
            this.db = db;
        }
        public async Task<bool> Delete(int id)
        {
            var data = await db.Collages.FindAsync(id);
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

        public async Task<List<CollageDTO>> GetAll()
        {
            return await (
                from a in db.Collages
                select new CollageDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    UserId = (Guid)a.UserId,
                    UserName = a.AppUser.FirstName + " " + a.AppUser.LastName
                }
                   ).ToListAsync();
        }

        public async Task<CollageDTO?> GetById(int id)
        {
            return await (
               from a in db.Collages
               where a.Id == id
               select new CollageDTO
               {
                   Id = a.Id,
                   Name = a.Name,
                   UserId = (Guid)a.UserId,
                   UserName = a.AppUser.FirstName + " " + a.AppUser.LastName
               }
                  ).FirstOrDefaultAsync();
        }

        public async Task<bool> Insert(CollageDTO directionDTO)
        {
            await db.Collages.AddAsync(new Collage
            {
                Id = directionDTO.Id,
                Name = directionDTO.Name,
                UserId = directionDTO.UserId
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

        public async Task<List<CollageDTO>> Search(string? Name)
        {
            return await(
           from a in db.Collages
           where a.Name.Contains(Name)
           select new CollageDTO
           {
               Id = a.Id,
               Name = a.Name,
               UserId = (Guid)a.UserId,
               UserName = a.AppUser.FirstName + " " + a.AppUser.LastName
           }
              ).ToListAsync();
        }

        public async Task<bool> Update(CollageDTO collageDTO, int Id)
        {
            if (collageDTO == null || collageDTO.Id != Id)
                return false;
            var data = await db.Collages.FindAsync(Id);
            data.Name = collageDTO.Name;
            data.UserId = collageDTO.UserId;
            db.Entry(data).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
