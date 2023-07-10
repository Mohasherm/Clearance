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
                    DepartmentId = a.DepartmentId,
                    Department = db.Departments.FirstOrDefault(x => x.Id == a.DepartmentId).Name,//a.Department.Name,
                    Mobile = a.Mobile,
                    AppointmentDate = a.AppointmentDate,
                    UserId = a.UserId,
                    UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                    OrderApplyDate = a.OrderApplyDate,
                    State = a.State,
                    OrderRecieveDate = a.OrderRecieveDate,
                    Done = a.Done,
                    Year = a.Year
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
                  Department = db.Departments.FirstOrDefault(x => x.Id == a.Clearance.DepartmentId).Name,//a.Clearance.Department.Name,
                  OrderApplyDate = a.Clearance.OrderApplyDate,
                  DirectionName = a.Direction.Name,
                  DoneDate = a.DoneDate,
                  UserName = a.AppUser == null ? "" : a.AppUser.UserName
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
                  Department = db.Departments.FirstOrDefault(x => x.Id == a.Clearance.DepartmentId).Name,//a.Clearance.Department.Name,
                  OrderApplyDate = a.Clearance.OrderApplyDate,
                  DirectionName = a.Direction.Name,
                  DoneDate = a.DoneDate,
                  UserName = a.AppUser == null ? "" : a.AppUser.UserName
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
                  DepartmentId = a.DepartmentId,
                  Department = db.Departments.FirstOrDefault(x => x.Id == a.DepartmentId).Name,//a.Department.Name,
                  Mobile = a.Mobile,
                  AppointmentDate = a.AppointmentDate,
                  UserId = a.UserId,
                  UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                  OrderApplyDate = a.OrderApplyDate,
                  State = a.State,
                  OrderRecieveDate = a.OrderRecieveDate,
                  Done = a.Done,
                  Year = a.Year

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
                 Department = db.Departments.FirstOrDefault(x => x.Id == a.Clearance.DepartmentId).Name,//a.Clearance.Department.Name,
                 OrderApplyDate = a.Clearance.OrderApplyDate,
                 DirectionName = a.Direction.Name,
                 DoneDate = a.DoneDate,
                 UserName = a.AppUser == null ? "" : a.AppUser.FirstName + " "+ a.AppUser.LastName
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
                 Department = db.Departments.FirstOrDefault(x => x.Id == a.Clearance.DepartmentId).Name,//a.Clearance.Department.Name,
                 OrderApplyDate = a.Clearance.OrderApplyDate,
                 DirectionName = a.Direction.Name,
                 DoneDate = a.DoneDate,
                 UserName = a.AppUser == null ? "" : a.AppUser.UserName
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
                  DepartmentId = a.DepartmentId,
                  Department = db.Departments.FirstOrDefault(x => x.Id == a.DepartmentId).Name,//a.Department.Name,
                  Mobile = a.Mobile,
                  AppointmentDate = a.AppointmentDate,
                  UserId = a.UserId,
                  UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                  OrderApplyDate = a.OrderApplyDate,
                  State = a.State,
                  OrderRecieveDate = a.OrderRecieveDate,
                  Done = a.Done,
                  Year = a.Year,
                  DateValidation = a.DateValidation

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
                DepartmentId = (int)clearanceDTO.DepartmentId,
                Mobile = clearanceDTO.Mobile,
                AppointmentDate = clearanceDTO.AppointmentDate,
                UserId = clearanceDTO.UserId,
                OrderApplyDate = clearanceDTO.OrderApplyDate,
                State = clearanceDTO.State,
                OrderRecieveDate = clearanceDTO.OrderRecieveDate,
                Done = false,
                Year = clearanceDTO.Year

            };
            await db.Clearances.AddAsync(data);

            try
            {
                await db.SaveChangesAsync();
                // ارسال طلب براءة الذمة الى الجهات التابعة للمركز
                var directions = await db.DepartmentDirection
                    .Where(x => x.DepartmentId == clearanceDTO.DepartmentId)
                    .Select(x => x.DirectionId).ToArrayAsync();

                if (directions is not null)
                {
                    List<ClearanceDirection> lst = new();
                    foreach (var dirId in directions)
                    {
                        lst.Add(new() { 
                            ClearanceId = data.Id, 
                            DirectionId = dirId, 
                            State = null,
                            UserId = null
                        });
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

        public async Task<bool> RenewOrder(ClearanceDTO clearanceDTO, int Id)
        {
            if (clearanceDTO == null || clearanceDTO.Id != Id)
                return false;
            var data = await db.Clearances.FindAsync(Id);
           
            data.AppointmentDate = clearanceDTO.AppointmentDate;
            data.OrderApplyDate = clearanceDTO.OrderApplyDate;
            data.OrderRecieveDate = clearanceDTO.OrderRecieveDate;
            data.State = clearanceDTO.State;
            data.Done = clearanceDTO.Done;

            db.Entry(data).State = EntityState.Modified;

            var clDirec = await db.ClearanceDirections.Where(x => x.ClearanceId == Id).ToListAsync();

            clDirec.ForEach(x => x.State = null);

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
                  db.Departments.FirstOrDefault(x => x.Id == a.DepartmentId).Name.Contains(Name) || //a.Department.Name.Contains(Name) ||
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
               DepartmentId = a.DepartmentId,
               Department = db.Departments.FirstOrDefault(x => x.Id == a.DepartmentId).Name,//a.Department.Name,
               Mobile = a.Mobile,
               AppointmentDate = a.AppointmentDate,
               UserId = a.UserId,
               UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
               OrderApplyDate = a.OrderApplyDate,
               State = a.State,
               OrderRecieveDate = a.OrderRecieveDate,
               Done = a.Done,
               Year = a.Year
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
                   db.Departments.FirstOrDefault(x => x.Id == a.DepartmentId).Name.Contains(Name) || //a.Department.Name.Contains(Name) ||
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
                DepartmentId = a.DepartmentId,
                Department = db.Departments.FirstOrDefault(x => x.Id == a.DepartmentId).Name,//a.Department.Name,
                Mobile = a.Mobile,
                AppointmentDate = a.AppointmentDate,
                UserId = a.UserId,
                UserName = a.AppUser.FirstName + " " + a.AppUser.LastName,
                OrderApplyDate = a.OrderApplyDate,
                State = a.State,
                OrderRecieveDate = a.OrderRecieveDate,
                Done = a.Done,
                Year = a.Year
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
                  db.Departments.FirstOrDefault(x => x.Id == a.Clearance.DepartmentId).Name.Contains(Name) ||// a.Clearance.Department.Name.Contains(Name) ||
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
                  Department = db.Departments.FirstOrDefault(x => x.Id == a.Clearance.DepartmentId).Name,//a.Clearance.Department.Name,
                  OrderApplyDate = a.Clearance.OrderApplyDate,
                  DirectionName = a.Direction.Name,
                  DoneDate = a.DoneDate,
                  UserName = a.AppUser == null ? "" : a.AppUser.UserName
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
                   db.Departments.FirstOrDefault(x => x.Id == a.Clearance.DepartmentId).Name.Contains(Name) || //a.Clearance.Department.Name.Contains(Name) ||
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
                  Department = db.Departments.FirstOrDefault(x => x.Id == a.Clearance.DepartmentId).Name,//a.Clearance.Department.Name,
                  OrderApplyDate = a.Clearance.OrderApplyDate,
                  DirectionName = a.Direction.Name,
                  DoneDate = a.DoneDate,
                  UserName = a.AppUser == null ? "" : a.AppUser.UserName
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
            data.DepartmentId = (int)clearanceDTO.DepartmentId;
            data.Mobile = clearanceDTO.Mobile;
            data.State = clearanceDTO.State;
            data.OrderRecieveDate = clearanceDTO.OrderRecieveDate;
            data.Done = clearanceDTO.Done;
            data.Year = clearanceDTO.Year;
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

        public async Task<bool> UpdateDirection(ClearanceDirectionsDTO clearanceDirectionsDTO, int Id , Guid userId)
        {
            if (clearanceDirectionsDTO == null || clearanceDirectionsDTO.Id != Id)
                return false;
            var data = await db.ClearanceDirections.FindAsync(Id);
            data.State = clearanceDirectionsDTO.State;
            data.DoneDate = DateTime.Now;
            data.UserId = userId;
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
                    clData.DateValidation = DateTime.Now;
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
