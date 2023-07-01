﻿using Clearance.Server.Data;
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
                    UserId = a.UserId,
                    UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                    OrderApplyDate = a.OrderApplyDate,
                    State = a.State,
                    OrderRecieveDate = a.OrderRecieveDate,
                    Done = a.Done
                }
                   ).ToListAsync();
        }

        public async Task<List<ClearanceDirectionsDTO>> GetAllByDirection(Guid Id)
        {
            var data = db.Users.Find(Id);
            if (data.Direction_Id is null)
            {
                return null;
            }
            int dirId = (int)data.Direction_Id;

            return await (
              from a in db.ClearanceDirections
              where a.DirectionId == dirId && a.State == null
              select new ClearanceDirectionsDTO
              {
                  Id = a.Id,
                  ClearanceId = a.ClearanceId,
                  DirectionId = a.DirectionId,
                  State = a.State,
                  StudentName = a.Clearance.FirstName + " " + a.Clearance.Father + " " + a.Clearance.LastName,
                  UnivNum = a.Clearance.UnivNum,
                  NationalNum = a.Clearance.NationalNum,
                  CollageName = a.Clearance.Collage.Name,
                  Department = a.Clearance.Department,
                  OrderApplyDate = a.Clearance.OrderApplyDate,
                  DirectionName = a.Direction.Name
              }
                 ).ToListAsync();
        }

        public async Task<List<ClearanceDirectionsDTO>> GetAllByDirectionState(Guid Id, bool state)
        {
            var data = db.Users.Find(Id);
            if (data.Direction_Id is null)
            {
                return null;
            }
            int dirId = (int)data.Direction_Id;

            return await (
              from a in db.ClearanceDirections
              where a.DirectionId == dirId && a.State == state
              select new ClearanceDirectionsDTO
              {
                  Id = a.Id,
                  ClearanceId = a.ClearanceId,
                  DirectionId = a.DirectionId,
                  State = a.State,
                  StudentName = a.Clearance.FirstName + " " + a.Clearance.Father + " " + a.Clearance.LastName,
                  UnivNum = a.Clearance.UnivNum,
                  NationalNum = a.Clearance.NationalNum,
                  CollageName = a.Clearance.Collage.Name,
                  Department = a.Clearance.Department,
                  OrderApplyDate = a.Clearance.OrderApplyDate,
                  DirectionName = a.Direction.Name
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
                  UserId = a.UserId,
                  UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                  OrderApplyDate = a.OrderApplyDate,
                  State = a.State,
                  OrderRecieveDate = a.OrderRecieveDate,
                  Done = a.Done
              }
                 ).ToListAsync();
        }

        public async Task<List<ClearanceDirectionsDTO>?> GetByclId(int id)
        {
            return await (
             from a in db.ClearanceDirections
             where a.ClearanceId == id
             select new ClearanceDirectionsDTO
             {
                 Id = a.Id,
                 ClearanceId = a.ClearanceId,
                 DirectionId = a.DirectionId,
                 State = a.State,
                 StudentName = a.Clearance.FirstName + " " + a.Clearance.Father + " " + a.Clearance.LastName,
                 UnivNum = a.Clearance.UnivNum,
                 NationalNum = a.Clearance.NationalNum,
                 CollageName = a.Clearance.Collage.Name,
                 Department = a.Clearance.Department,
                 OrderApplyDate = a.Clearance.OrderApplyDate,
                 DirectionName = a.Direction.Name
             }
                ).ToListAsync();
        }

        public async Task<ClearanceDirectionsDTO?> GetByDirectionId(int id)
        {
            return await (
             from a in db.ClearanceDirections
             select new ClearanceDirectionsDTO
             {
                 Id = a.Id,
                 ClearanceId = a.ClearanceId,
                 DirectionId = a.DirectionId,
                 State = a.State,
                 StudentName = a.Clearance.FirstName + " " + a.Clearance.Father + " " + a.Clearance.LastName,
                 UnivNum = a.Clearance.UnivNum,
                 NationalNum = a.Clearance.NationalNum,
                 CollageName = a.Clearance.Collage.Name,
                 Department = a.Clearance.Department,
                 OrderApplyDate = a.Clearance.OrderApplyDate,
                 DirectionName = a.Direction.Name
             }
                ).FirstOrDefaultAsync(x => x.Id == id);
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
                  UserId = a.UserId,
                  UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                  OrderApplyDate = a.OrderApplyDate,
                  State = a.State,
                  OrderRecieveDate = a.OrderRecieveDate,
                  Done = a.Done

              }
                 ).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Insert(ClearanceDTO clearanceDTO)
        {
            var data = new Data.Clearance
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
                OrderRecieveDate = clearanceDTO.OrderRecieveDate,
                Done = false
            };
            await db.Clearances.AddAsync(data);

            try
            {
                await db.SaveChangesAsync();
                // ارسال طلب براءة الذمة الى الجهات التابعة للمركز
                var directions = await db.CollageDirections
                    .Where(x => x.CollageId == clearanceDTO.CollageId)
                    .Select(x => x.DirectionId).ToArrayAsync();

                if (directions is not null)
                {
                    List<ClearanceDirection> lst = new();
                    foreach (var dirId in directions)
                    {
                        lst.Add(new() { ClearanceId = data.Id, DirectionId = dirId, State = null });
                    }
                    await db.AddRangeAsync(lst);
                    await db.SaveChangesAsync();
                }

                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<List<ClearanceDTO>> Search(string Name)
        {
            return await (
           from a in db.Clearances
           where a.FirstName.Contains(Name) ||
                 a.LastName.Contains(Name) ||
                 a.Father.Contains(Name) ||
                 a.Mother.Contains(Name) ||
                 a.UnivNum.Contains(Name) ||
                 a.NationalNum.Contains(Name) ||
                 a.Collage.Name.Contains(Name) ||
                 a.Department.Contains(Name) ||
                 a.Mobile.Contains(Name) ||
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
               UserId = a.UserId,
               UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
               OrderApplyDate = a.OrderApplyDate,
               State = a.State,
               OrderRecieveDate = a.OrderRecieveDate,
               Done = a.Done
           }
              ).ToListAsync();
        }

        public async Task<List<ClearanceDTO>> Search(Guid Id, string Name)
        {
            return await (
            from a in db.Clearances
            where (a.FirstName.Contains(Name) ||
                  a.LastName.Contains(Name) ||
                  a.Father.Contains(Name) ||
                  a.Mother.Contains(Name) ||
                  a.UnivNum.Contains(Name) ||
                  a.NationalNum.Contains(Name) ||
                  a.Collage.Name.Contains(Name) ||
                  a.Department.Contains(Name) ||
                  a.Mobile.Contains(Name) ||
                  a.State.Contains(Name)
                  )
                  && a.UserId == Id
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
                UserId = a.UserId,
                UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                OrderApplyDate = a.OrderApplyDate,
                State = a.State,
                OrderRecieveDate = a.OrderRecieveDate,
                Done = a.Done
            }
               ).ToListAsync();
        }

        public async Task<List<ClearanceDirectionsDTO>> SearchbyDirection(Guid Id, string Name)
        {
            var data = db.Users.Find(Id);
            if (data.Direction_Id is null)
            {
                return null;
            }

            int dirId = (int)data.Direction_Id;

            return await (
              from a in db.ClearanceDirections
              where a.DirectionId == dirId && a.State == null &&
              (a.Clearance.FirstName.Contains(Name) ||
                  a.Clearance.LastName.Contains(Name) ||
                  a.Clearance.Father.Contains(Name) ||
                  a.Clearance.Mother.Contains(Name) ||
                  a.Clearance.UnivNum.Contains(Name) ||
                  a.Clearance.NationalNum.Contains(Name) ||
                  a.Clearance.Collage.Name.Contains(Name) ||
                  a.Clearance.Department.Contains(Name) ||
                  a.Clearance.Mobile.Contains(Name) ||
                  a.Clearance.State.Contains(Name)
                  )
              select new ClearanceDirectionsDTO
              {
                  Id = a.Id,
                  ClearanceId = a.ClearanceId,
                  DirectionId = a.DirectionId,
                  State = a.State,
                  StudentName = a.Clearance.FirstName + " " + a.Clearance.Father + " " + a.Clearance.LastName,
                  UnivNum = a.Clearance.UnivNum,
                  NationalNum = a.Clearance.NationalNum,
                  CollageName = a.Clearance.Collage.Name,
                  Department = a.Clearance.Department,
                  OrderApplyDate = a.Clearance.OrderApplyDate,
                  DirectionName = a.Direction.Name
              }
                 ).ToListAsync();
        }

        public async Task<List<ClearanceDirectionsDTO>> SearchbyDirectionState(Guid Id, string Name, bool state)
        {
            var data = db.Users.Find(Id);
            if (data.Direction_Id is null)
            {
                return null;
            }

            int dirId = (int)data.Direction_Id;

            return await (
              from a in db.ClearanceDirections
              where a.DirectionId == dirId && a.State == state &&
              (a.Clearance.FirstName.Contains(Name) ||
                  a.Clearance.LastName.Contains(Name) ||
                  a.Clearance.Father.Contains(Name) ||
                  a.Clearance.Mother.Contains(Name) ||
                  a.Clearance.UnivNum.Contains(Name) ||
                  a.Clearance.NationalNum.Contains(Name) ||
                  a.Clearance.Collage.Name.Contains(Name) ||
                  a.Clearance.Department.Contains(Name) ||
                  a.Clearance.Mobile.Contains(Name) ||
                  a.Clearance.State.Contains(Name)
                  )
              select new ClearanceDirectionsDTO
              {
                  Id = a.Id,
                  ClearanceId = a.ClearanceId,
                  DirectionId = a.DirectionId,
                  State = a.State,
                  StudentName = a.Clearance.FirstName + " " + a.Clearance.Father + " " + a.Clearance.LastName,
                  UnivNum = a.Clearance.UnivNum,
                  NationalNum = a.Clearance.NationalNum,
                  CollageName = a.Clearance.Collage.Name,
                  Department = a.Clearance.Department,
                  OrderApplyDate = a.Clearance.OrderApplyDate,
                  DirectionName = a.Direction.Name
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
            data.State = clearanceDTO.State;
            data.OrderRecieveDate = clearanceDTO.OrderRecieveDate;
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

        public async Task<bool> UpdateDirection(ClearanceDirectionsDTO clearanceDirectionsDTO, int Id)
        {
            if (clearanceDirectionsDTO == null || clearanceDirectionsDTO.Id != Id)
                return false;
            var data = await db.ClearanceDirections.FindAsync(Id);
            data.State = clearanceDirectionsDTO.State;
            db.Entry(data).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();

                if (!db.ClearanceDirections
                    .Where(x => (x.State == false || x.State == null) 
                    && x.ClearanceId == data.ClearanceId).Any())
                {
                    var clData = db.Clearances.Find(data.ClearanceId);
                    clData.State = "بريء الذمة";
                    db.Entry(clData).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }

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
