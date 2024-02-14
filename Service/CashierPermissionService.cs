using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using Utility;
using System.Data.Entity;
using EntityFramework.Extensions;
using MoreLinq;

namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class CashierPermissionService
    {
        ERPDbContext context = new ERPDbContext();

        public List<CashierPermission> GetAllCashierFunctionsForCashierPermission() 
        {
            List<CashierPermission> rtnList = new List<CashierPermission>();

            var qry = (from cf in context.CashierFunctions
                       select new
                       {
                           cf.FunctionName,
                           cf.FunctionDescription,
                           cf.Order,
                           cf.IsValue
                       }).ToArray();

            foreach (var temp in qry)
            {
                CashierPermission cashierPermission = new CashierPermission();

                cashierPermission.FunctionDescription = temp.FunctionDescription;
                cashierPermission.FunctionName = temp.FunctionName;
                cashierPermission.Order = temp.Order;
                cashierPermission.IsValue = temp.IsValue;

                rtnList.Add(cashierPermission);
            }
            return rtnList;
        }

        public List<CashierPermission> UpdateCashierPermission(List<CashierPermission> cashierPermissionListPrm, CashierPermission cashierPermissionPrm)
        {
            CashierPermission cashierPermission = new CashierPermission();
            cashierPermission = cashierPermissionListPrm.Where(cp => cp.Order.Equals(cashierPermissionPrm.Order)).FirstOrDefault();

            if (cashierPermission != null)
            {
                cashierPermissionListPrm.Remove(cashierPermission);
                cashierPermissionListPrm.Add(cashierPermissionPrm);
            }

            return cashierPermissionListPrm.OrderBy(cs => cs.Order).ToList();
        }

        public void UpdateCashierPermissionselect(int Edatatransfer, int datatransfer)
        {
           // context.CashierPermissions.Update(x => x.DataTransfer.Equals(Edatatransfer) , x => new CashierPermission { DataTransfer = datatransfer });
            var qry = (from d in context.CashierPermissions where d.DataTransfer.Equals(0) select d).Take(100).ToArray();

            foreach (var temp in qry)
            {
                context.CashierPermissions.Update(d => d.CashierPermissionID.Equals(temp.CashierPermissionID), d => new CashierPermission { DataTransfer = 1 });
            }
        }
        public void UpdateCashierPermission(int Edatatransfer, int datatransfer)
        {
            context.CashierPermissions.Update(x => x.DataTransfer.Equals(Edatatransfer) , x => new CashierPermission { DataTransfer = datatransfer });
            
        }

        public void UpdateCashierFunction(int Edatatransfer, int datatransfer)
        {
            context.CashierFunctions.Update(x => x.DataTransfer.Equals(Edatatransfer), x => new CashierFunction { DataTransfer = datatransfer });
        }

        public void SaveCashierPermoission(List<CashierPermission> cashierPermissionList, List<Location> locationList, CashierPermission cashierPermission, List<Location> unselectLocationList)   
        {
            var locationQry = locationList.ToArray();
            var cashierQry = cashierPermissionList.ToArray();
            var unselectLocationQry = unselectLocationList.ToArray();

            //int locationID = locationList.();

            foreach (var temp in unselectLocationQry)
            {
                context.CashierPermissions.Update(cp => cp.CashierID.Equals(cashierPermission.CashierID) && cp.LocationID.Equals(temp.LocationID), cp => new CashierPermission
                {
                    IsDelete = true,
                    Value = 0,
                    IsAccess = false,
                    IsActive = false,
                    DataTransfer = 0
                });
            }

            CashierPermission cashierPermissionL = new CashierPermission();

            cashierPermissionL = context.CashierPermissions.Where(cp => cp.CashierID.Equals(cashierPermission.CashierID)).FirstOrDefault();

            if (cashierPermissionL == null)
            {
                foreach (var tempLocation in locationQry)
                {
                    foreach (var temp in cashierQry)
                    {
                        CashierPermission cashierPermissionSave = new CashierPermission();

                        cashierPermissionSave.LocationID = tempLocation.LocationID;
                        cashierPermissionSave.CashierID = cashierPermission.CashierID;
                        cashierPermissionSave.EmployeeID = cashierPermission.EmployeeID;
                        cashierPermissionSave.Password = cashierPermission.Password;
                        cashierPermissionSave.JournalName = cashierPermission.JournalName;
                        cashierPermissionSave.EnCode = cashierPermission.EnCode;
                        cashierPermissionSave.Remarks = cashierPermission.Remarks;
                        cashierPermissionSave.Type = cashierPermission.Type;
                        cashierPermissionSave.TypeID = cashierPermission.TypeID;
                        cashierPermissionSave.FunctionName = temp.FunctionName;
                        cashierPermissionSave.FunctionDescription = temp.FunctionDescription;
                        cashierPermissionSave.Value = temp.Value;
                        cashierPermissionSave.IsAccess = temp.IsAccess;
                        cashierPermissionSave.Order = temp.Order;
                        cashierPermissionSave.IsActive = cashierPermission.IsActive;

                        context.CashierPermissions.Add(cashierPermissionSave);
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                foreach (var tempLocation in locationQry)
                {
                    foreach (var temp in cashierQry)
                    {
                        cashierPermissionL = new CashierPermission();
                        cashierPermissionL = context.CashierPermissions.Where(cp => cp.CashierID.Equals(cashierPermission.CashierID) && cp.LocationID.Equals(tempLocation.LocationID)).FirstOrDefault();

                        if (cashierPermissionL == null)
                        {
                            CashierPermission cashierPermissionSave = new CashierPermission();

                            cashierPermissionSave.LocationID = tempLocation.LocationID;
                            cashierPermissionSave.CashierID = cashierPermission.CashierID;
                            cashierPermissionSave.EmployeeID = cashierPermission.EmployeeID;
                            cashierPermissionSave.Password = cashierPermission.Password;
                            cashierPermissionSave.JournalName = cashierPermission.JournalName;
                            cashierPermissionSave.EnCode = cashierPermission.EnCode;
                            cashierPermissionSave.Remarks = cashierPermission.Remarks;
                            cashierPermissionSave.Type = cashierPermission.Type;
                            cashierPermissionSave.TypeID = cashierPermission.TypeID;
                            cashierPermissionSave.FunctionName = temp.FunctionName;
                            cashierPermissionSave.FunctionDescription = temp.FunctionDescription;
                            cashierPermissionSave.Value = temp.Value;
                            cashierPermissionSave.IsAccess = temp.IsAccess;
                            cashierPermissionSave.Order = temp.Order;
                            cashierPermissionSave.IsActive = cashierPermission.IsActive;

                            context.CashierPermissions.Add(cashierPermissionSave);
                            context.SaveChanges();
                        }
                        else
                        {
                            cashierPermissionL = new CashierPermission();
                            cashierPermissionL = context.CashierPermissions.Where(cp => cp.CashierID.Equals(cashierPermission.CashierID) && cp.LocationID.Equals(tempLocation.LocationID) && cp.Order.Equals(temp.Order)).FirstOrDefault();

                            if (cashierPermissionL == null)
                            {
                                CashierPermission cashierPermissionSave = new CashierPermission();

                                cashierPermissionSave.LocationID = tempLocation.LocationID;
                                cashierPermissionSave.CashierID = cashierPermission.CashierID;
                                cashierPermissionSave.EmployeeID = cashierPermission.EmployeeID;
                                cashierPermissionSave.Password = cashierPermission.Password;
                                cashierPermissionSave.JournalName = cashierPermission.JournalName;
                                cashierPermissionSave.EnCode = cashierPermission.EnCode;
                                cashierPermissionSave.Remarks = cashierPermission.Remarks;
                                cashierPermissionSave.Type = cashierPermission.Type;
                                cashierPermissionSave.TypeID = cashierPermission.TypeID;
                                cashierPermissionSave.FunctionName = temp.FunctionName;
                                cashierPermissionSave.FunctionDescription = temp.FunctionDescription;
                                cashierPermissionSave.Value = temp.Value;
                                cashierPermissionSave.IsAccess = temp.IsAccess;
                                cashierPermissionSave.Order = temp.Order;
                                cashierPermissionSave.IsActive = cashierPermission.IsActive;

                                context.CashierPermissions.Add(cashierPermissionSave);
                                context.SaveChanges();
                            }
                            else
                            {
                                context.CashierPermissions.Update(cp => cp.CashierID.Equals(cashierPermission.CashierID) && cp.LocationID.Equals(tempLocation.LocationID) && cp.Order.Equals(temp.Order), cp => new CashierPermission
                                {
                                    LocationID = tempLocation.LocationID,
                                    CashierID = cashierPermission.CashierID,
                                    EmployeeID = cashierPermission.EmployeeID,
                                    Password = cashierPermission.Password,
                                    JournalName = cashierPermission.JournalName,
                                    EnCode = cashierPermission.EnCode,
                                    Remarks = cashierPermission.Remarks,
                                    Type = cashierPermission.Type,
                                    TypeID = cashierPermission.TypeID,
                                    FunctionName = temp.FunctionName,
                                    FunctionDescription = temp.FunctionDescription,
                                    Value = temp.Value,
                                    IsAccess = temp.IsAccess,
                                    Order = temp.Order,
                                    IsActive = cashierPermission.IsActive,
                                    IsDelete = false,
                                    DataTransfer = 0
                                });
                            }
                        }
                    }
                }
            }
        }

        public void DeleteCashierPermoission(long cashierID) 
        {
            context.CashierPermissions.Where(cp => cp.CashierID.Equals(cashierID)).Delete();
            //context.CashierPermissions.Update(cp => cp.CashierID.Equals(cashierID), cp => new CashierPermission { IsDelete = true });
        }

        //public void UpdateCashierPermoission(CashierPermission cashierPermission) 
        //{
        //    cashierPermission.ModifiedUser = Common.LoggedUser;
        //    cashierPermission.ModifiedDate = Common.GetSystemDateWithTime();
        //    this.context.Entry(cashierPermission).State = EntityState.Modified;
        //    this.context.SaveChanges();
        //}

        public CashierPermission GetCashierPermissionByCashierID(long cashierID)
        {
            return context.CashierPermissions.Where(cp => cp.CashierID.Equals(cashierID) && cp.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<CashierPermission> GetCashierPermissionListByCashierID(long CashierID)
        {
            List<CashierPermission> rtnList = new List<CashierPermission>();

            int locID = 0;
            var locationID = (from cp in context.CashierPermissions where cp.CashierID == CashierID && cp.IsDelete==false select new { cp.LocationID }).Take(1);
            foreach (var temp in locationID) { locID = temp.LocationID; }

            var functionQry = (from cf in context.CashierFunctions
                               select new
                               {
                                   cf.FunctionName,
                                   cf.FunctionDescription,
                                   cf.Order,
                                   cf.IsValue
                               }).ToArray();

            foreach (var tmpFunction in functionQry)
            {
                CashierPermission cashierPermission = new CashierPermission();
                long order = tmpFunction.Order;

                cashierPermission = context.CashierPermissions.Where(cp => cp.CashierID.Equals(CashierID) && cp.LocationID.Equals(locID) && cp.Order.Equals(order)).FirstOrDefault();

                if (cashierPermission != null)
                {
                    cashierPermission.FunctionName = tmpFunction.FunctionName;
                    cashierPermission.FunctionDescription = tmpFunction.FunctionDescription;
                    cashierPermission.IsAccess = cashierPermission.IsAccess;
                    cashierPermission.Value = cashierPermission.Value;
                    cashierPermission.Order = tmpFunction.Order;
                    cashierPermission.IsValue = tmpFunction.IsValue;

                    rtnList.Add(cashierPermission);
                }
                else
                {
                    cashierPermission = new CashierPermission();
                    cashierPermission.FunctionName = tmpFunction.FunctionName;
                    cashierPermission.FunctionDescription = tmpFunction.FunctionDescription;
                    cashierPermission.IsAccess = false;
                    cashierPermission.Value = 0;
                    cashierPermission.Order = tmpFunction.Order;
                    cashierPermission.IsValue = tmpFunction.IsValue;

                    rtnList.Add(cashierPermission);
                }
            }
            return rtnList;
        }

        public List<Location> GetSelectedLocationsToCashierPermission(long cashierID)
        {
            List<Location> rtnList = new List<Location>();

            var qry = (from cp in context.CashierPermissions
                       where cp.CashierID == cashierID && cp.IsDelete == false
                       select new
                       {
                           cp.LocationID
                       }).ToArray().Distinct();

            foreach (var temp in qry)
            {
                Location location = new Location();
                LocationService locationService = new LocationService();
                location = locationService.GetLocationsByID(temp.LocationID);

                if (location != null) { rtnList.Add(location); }
            }
            return rtnList;
        }

        public DataTable GetAllCashierPermissionDataTable()
        {
            var dtret = from e in context.CashierPermissions
                         where e.DataTransfer.Equals(1) 
                         select new
                         {
                             e.CashierPermissionID ,
                             e.LocationID ,
                             e.CashierID ,
                             e.EmployeeID ,
                             e.Password ,
                             e.JournalName ,
                             e.EnCode ,
                             e.FunctionName ,
                             e.FunctionDescription ,
                             e.Order ,
                             e.Value ,
                             e.Type ,
                             e.TypeID ,
                             e.IsActive ,
                             e.IsAccess ,
                             e.Remarks ,
                             e.IsDelete,
                             e.GroupOfCompanyID,
                             e.CreatedUser,
                             e.CreatedDate,
                             e.ModifiedUser,
                             e.ModifiedDate,
                             e.DataTransfer,
                             e.IsValue
                         };
            return dtret.ToDataTable();
        }


        public DataTable GetAllCashierFunctionDataTable()
        {
            var dtret = from e in context.CashierFunctions
                        where e.DataTransfer.Equals(1)
                        select new
                        {
                            e.CashierFunctionID ,
                            e.FunctionName ,
                            e.FunctionDescription ,
                            e.Order,
                            e.TypeID ,
                            e.IsDelete,
                            e.GroupOfCompanyID,
                            e.CreatedUser,
                            e.CreatedDate,
                            e.ModifiedUser,
                            e.ModifiedDate,
                            e.DataTransfer,
                            e.IsValue
                        };
            return dtret.ToDataTable();
        }

        public bool ChechExistsPassword(string password) 
        {
            bool passwordExists = false;
            CashierPermission cashierPermission = new CashierPermission();
            cashierPermission = context.CashierPermissions.Where(cp => cp.Password.Equals(password)).FirstOrDefault();

            if (cashierPermission == null) { passwordExists = false; }
            else { passwordExists = true; }

            return passwordExists;
        }

        public List<CashierPermission> GetCashiersAccordingToLocation(int locationID)
        {
            List<CashierPermission> rtnList = new List<CashierPermission>();

            var cashiers = (from cp in context.CashierPermissions
                            join em in context.Employees on cp.EmployeeID equals em.EmployeeID
                            join ed in context.EmployeeDesignationTypes on cp.TypeID equals ed.EmployeeDesignationTypeID
                            where cp.LocationID == locationID
                            && cp.IsDelete == false
                            select new
                            {
                                em.EmployeeID,
                                em.EmployeeCode,
                                em.EmployeeName,
                                cp.Password,
                                ed.Designation
                            }).Distinct().ToArray();
            foreach (var tempCashiers in cashiers)
            {
                CashierPermission cashierPermissionAdd = new CashierPermission();

                cashierPermissionAdd.EmployeeCode = tempCashiers.EmployeeCode;
                cashierPermissionAdd.EmployeeName = tempCashiers.EmployeeName;
                cashierPermissionAdd.Password = tempCashiers.Password;
                cashierPermissionAdd.Designation = tempCashiers.Designation;

                rtnList.Add(cashierPermissionAdd);
            }

            return rtnList;
        }

    }
}
