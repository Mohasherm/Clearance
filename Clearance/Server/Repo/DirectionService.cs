using Clearance.Server.Data;
using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.EntityFrameworkCore;

namespace Clearance.Server.Repo
{
    public class DirectionService : IDirectionService
    {
        DataContext db;
        public DirectionService(DataContext db)
        {
            this.db = db;
        }
        public async Task<bool> Delete(int id)
        {
            var data = await db.Directions.FindAsync(id);
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

        public async Task<List<DirectionDTO>> GetAll()
        {
            return await (
                from a in db.Directions
                select new DirectionDTO
                {
                    Id = a.Id,
                    Name = a.Name
                }
                   ).ToListAsync();
        }

        public async Task<DirectionDTO?> GetById(int id)
        {
            return await (from a in db.Directions
                          where a.Id == id
                          select new DirectionDTO
                          {
                              Id = a.Id,
                              Name = a.Name
                          }).FirstOrDefaultAsync();
        }

        public async Task<bool> Insert(DirectionDTO directionDTO)
        {
            await db.Directions.AddAsync(new Direction { Id = directionDTO.Id, Name = directionDTO.Name });

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

        public async Task<List<DirectionDTO>> Search(string Name)
        {
            return await (
            from a in db.Directions
            where a.Name.Contains(Name)
            select new DirectionDTO
            {
                Id = a.Id,
                Name = a.Name
            }
               ).ToListAsync();
        }

        public async Task<bool> Update(DirectionDTO directionDTO, int Id)
        {
            if (directionDTO == null || directionDTO.Id != Id)
                return false;
            var data = await db.Directions.FindAsync(Id);
            data.Name = directionDTO.Name;
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
