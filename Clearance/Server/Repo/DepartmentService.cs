using Clearance.Server.Data;
using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.EntityFrameworkCore;

namespace Clearance.Server.Repo
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext db;

        public DepartmentService(DataContext db)
        {
            this.db = db;
        }

        public async Task<bool> Delete(int id)
        {
            var data = await db.Departments.FindAsync(id);
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

        public async Task<List<DepartmentDTO>?> GetAll(int collageId)
        {
            return await(
                 from a in db.Departments
                 where a.CollageId == collageId
                 select new DepartmentDTO
                 {
                     Id = a.Id,
                     Name = a.Name,
                     CollageId = a.CollageId
                 }
                    ).ToListAsync();
        }

        public async Task<DepartmentDTO?> GetById(int id)
        {
            return await(
               from a in db.Departments
               select new DepartmentDTO
               {
                   Id = a.Id,
                   Name = a.Name,
                   CollageId = a.CollageId
               }
                  ).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Insert(DepartmentDTO departmentDTO)
        {
            await db.Departments.AddAsync(new Department
            {
                Id = departmentDTO.Id,
                Name = departmentDTO.Name,
                CollageId = departmentDTO.CollageId
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

        public async Task<List<DepartmentDTO>> Search(string? Name)
        {
            return await(
           from a in db.Departments
           where a.Name.Contains(Name)
           select new DepartmentDTO
           {
               Id = a.Id,
               Name = a.Name,
               CollageId = a.CollageId
           }
              ).ToListAsync();
        }

        public async Task<bool> Update(DepartmentDTO departmentDTO, int Id)
        {
            if (departmentDTO == null || departmentDTO.Id != Id)
                return false;
            var data = await db.Departments.FindAsync(Id);
            data.Name = departmentDTO.Name;
            data.CollageId = departmentDTO.CollageId;
            //db.Entry(data).State = EntityState.Modified;
            db.Update(data);
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
