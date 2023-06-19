using Clearance.Server.Data;
using Clearance.Server.Repo.IRepo;
using Clearance.Shared;
using Microsoft.EntityFrameworkCore;

namespace Clearance.Server.Repo
{
    public class ClearanceService : IClearanceService
    {
        private readonly DataContext db;

        public ClearanceService(DataContext db)
        {
            this.db = db;
        }
        public async Task<bool> Delete(int id)
        {
            var data = await db.Clearances.FindAsync(id);
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

        public async Task<List<ClearanceDTO>> GetAll()
        {
            return await (
                from a in db.Clearances
                select new ClearanceDTO
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Father = a.Father,
                    Mother = a.Mother,
                    UnivNum = a.UnivNum,
                    NationalNum = a.NationalNum,
                    CollageId = a.CollageId,
                    CollageName = a.Collage.Name,
                    Department = a.Department,
                    Mobile = a.Mobile,
                    AppointmentDate = a.AppointmentDate,
                    UserId = (Guid)a.UserId,
                    UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                    OrderApplyDate = a.OrderApplyDate,
                    State = a.State,
                    OrderRecieveDate = a.OrderRecieveDate
                }
                   ).ToListAsync();
        }

        public async Task<List<ClearanceDTO>> GetAllByState(string State)
        {
            return await (
               from a in db.Clearances
               where a.State == State
               select new ClearanceDTO
               {
                   Id = a.Id,
                   FirstName = a.FirstName,
                   LastName = a.LastName,
                   Father = a.Father,
                   Mother = a.Mother,
                   UnivNum = a.UnivNum,
                   NationalNum = a.NationalNum,
                   CollageId = a.CollageId,
                   CollageName = a.Collage.Name,
                   Department = a.Department,
                   Mobile = a.Mobile,
                   AppointmentDate = a.AppointmentDate,
                   UserId = (Guid)a.UserId,
                   UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                   OrderApplyDate = a.OrderApplyDate,
                   State = a.State,
                   OrderRecieveDate = a.OrderRecieveDate
               }
                  ).ToListAsync();
        }

        public async Task<List<ClearanceDTO>> GetAllByUserId(Guid Id)
        {
            return await (
              from a in db.Clearances
              where a.UserId == Id
              select new ClearanceDTO
              {
                  Id = a.Id,
                  FirstName = a.FirstName,
                  LastName = a.LastName,
                  Father = a.Father,
                  Mother = a.Mother,
                  UnivNum = a.UnivNum,
                  NationalNum = a.NationalNum,
                  CollageId = a.CollageId,
                  CollageName = a.Collage.Name,
                  Department = a.Department,
                  Mobile = a.Mobile,
                  AppointmentDate = a.AppointmentDate,
                  UserId = (Guid)a.UserId,
                  UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                  OrderApplyDate = a.OrderApplyDate,
                  State = a.State,
                  OrderRecieveDate = a.OrderRecieveDate
              }
                 ).ToListAsync();
        }

        public async Task<ClearanceDTO?> GetById(int id)
        {
            return await (
              from a in db.Clearances
              select new ClearanceDTO
              {
                  Id = a.Id,
                  FirstName = a.FirstName,
                  LastName = a.LastName,
                  Father = a.Father,
                  Mother = a.Mother,
                  UnivNum = a.UnivNum,
                  NationalNum = a.NationalNum,
                  CollageId = a.CollageId,
                  CollageName = a.Collage.Name,
                  Department = a.Department,
                  Mobile = a.Mobile,
                  AppointmentDate = a.AppointmentDate,
                  UserId = (Guid)a.UserId,
                  UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                  OrderApplyDate = a.OrderApplyDate,
                  State = a.State,
                  OrderRecieveDate = a.OrderRecieveDate
              }
                 ).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Insert(ClearanceDTO clearanceDTO)
        {
            await db.Clearances.AddAsync(new Data.Clearance
            {
                Id = clearanceDTO.Id,
                FirstName = clearanceDTO.FirstName,
                LastName = clearanceDTO.LastName,
                Father = clearanceDTO.Father,
                Mother = clearanceDTO.Mother,
                UnivNum = clearanceDTO.UnivNum,
                NationalNum = clearanceDTO.NationalNum,
                CollageId = clearanceDTO.CollageId,
                Department = clearanceDTO.Department,
                Mobile = clearanceDTO.Mobile,
                AppointmentDate = clearanceDTO.AppointmentDate,
                UserId = clearanceDTO.UserId,
                OrderApplyDate = clearanceDTO.OrderApplyDate,
                State = clearanceDTO.State,
                OrderRecieveDate = clearanceDTO.OrderRecieveDate
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

        public async Task<List<ClearanceDTO>> Search(string? Name)
        {
            return await (
           from a in db.Clearances
           where a.FirstName.Contains(Name) ||
                 a.LastName.Contains(Name) ||
                 a.Father.Contains(Name) ||
                 a.Mother.Contains(Name) ||
                 a.UnivNum.ToString().Contains(Name) ||
                 a.NationalNum.ToString().Contains(Name) ||
                 a.Collage.Name.Contains(Name) ||
                 a.Department.Contains(Name) ||
                 a.Mobile.ToString().Contains(Name) ||
                 a.State.Contains(Name) 
           select new ClearanceDTO
           {
               Id = a.Id,
               FirstName = a.FirstName,
               LastName = a.LastName,
               Father = a.Father,
               Mother = a.Mother,
               UnivNum = a.UnivNum,
               NationalNum = a.NationalNum,
               CollageId = a.CollageId,
               CollageName = a.Collage.Name,
               Department = a.Department,
               Mobile = a.Mobile,
               AppointmentDate = a.AppointmentDate,
               UserId = (Guid)a.UserId,
               UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
               OrderApplyDate = a.OrderApplyDate,
               State = a.State,
               OrderRecieveDate = a.OrderRecieveDate
           }
              ).ToListAsync();
        }

        public async Task<List<ClearanceDTO>> Search(Guid Id, string? Name)
        {
            return await(
            from a in db.Clearances
            where (a.FirstName.Contains(Name) ||
                  a.LastName.Contains(Name) ||
                  a.Father.Contains(Name) ||
                  a.Mother.Contains(Name) ||
                  a.UnivNum.ToString().Contains(Name) ||
                  a.NationalNum.ToString().Contains(Name) ||
                  a.Collage.Name.Contains(Name) ||
                  a.Department.Contains(Name) ||
                  a.Mobile.ToString().Contains(Name) ||
                  a.State.Contains(Name)) && a.UserId == Id
            select new ClearanceDTO
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Father = a.Father,
                Mother = a.Mother,
                UnivNum = a.UnivNum,
                NationalNum = a.NationalNum,
                CollageId = a.CollageId,
                CollageName = a.Collage.Name,
                Department = a.Department,
                Mobile = a.Mobile,
                AppointmentDate = a.AppointmentDate,
                UserId = (Guid)a.UserId,
                UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                OrderApplyDate = a.OrderApplyDate,
                State = a.State,
                OrderRecieveDate = a.OrderRecieveDate
            }
               ).ToListAsync();
        }

        public async Task<bool> Update(ClearanceDTO clearanceDTO, int Id)
        {
            if (clearanceDTO == null || clearanceDTO.Id != Id)
                return false;
            var data = await db.Clearances.FindAsync(Id);
            data.FirstName = clearanceDTO.FirstName;
            data.LastName = clearanceDTO.LastName;
            data.Father = clearanceDTO.Father;
            data.Mother = clearanceDTO.Mother;
            data.UnivNum = clearanceDTO.UnivNum;
            data.NationalNum = clearanceDTO.NationalNum;
            data.Department = clearanceDTO.Department;
            data.Mobile = clearanceDTO.Mobile;
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
