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
using System.Data.Common;
using System.Transactions;

namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class CashierGroupService
    {
        ERPDbContext context = new ERPDbContext();

        public List<EmployeeDesignationType> GetAllEmployeeDesignationTypes()  
        {
            return context.EmployeeDesignationTypes.OrderBy(dt => dt.EmployeeDesignationTypeID).ToList();
        }

        public List<CashierGroup> GetInitialFunctions()  
        {
            List<CashierGroup> rtnList = new List<CashierGroup>();

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
                CashierGroup cashierGroup = new CashierGroup();

                cashierGroup.FunctionDescription = temp.FunctionDescription;
                cashierGroup.FunctionName = temp.FunctionName;
                cashierGroup.Order = temp.Order;
                cashierGroup.IsValue = temp.IsValue;

                rtnList.Add(cashierGroup);
            }
            return rtnList;
        }

        public List<CashierGroup> UpdateCashierGroup(List<CashierGroup> cashierGroupListPrm, CashierGroup cashierGroupPrm) 
        {
            CashierGroup cashierGroup = new CashierGroup();
            cashierGroup = cashierGroupListPrm.Where(cp => cp.Order.Equals(cashierGroupPrm.Order) && cp.EmployeeDesignationTypeID.Equals(cashierGroupPrm.EmployeeDesignationTypeID)).FirstOrDefault();

            if (cashierGroup != null)
            {
                cashierGroupListPrm.Remove(cashierGroup);
                cashierGroupListPrm.Add(cashierGroupPrm);
            }
            else
            {
                cashierGroupListPrm.Add(cashierGroupPrm);
            }

            return cashierGroupListPrm.OrderBy(cs => cs.Order).ToList();
        }


        public void SaveCashierGroup(List<CashierGroup> cashierGroupListPrm, int employeeDesignationTypeID, bool updeAllCashiers, bool locationUpdate, long order, decimal value, bool isAccess, List<Location> locationList)
        {
            var cashierGroupQry = cashierGroupListPrm.ToArray();
            var locationQry = locationList.ToArray();
            CashierGroup cashierGroup = new CashierGroup();

            foreach (var temp in cashierGroupQry)
            {
                cashierGroup = context.CashierGroups.Where(cg => cg.EmployeeDesignationTypeID.Equals(temp.EmployeeDesignationTypeID) && cg.Order.Equals(temp.Order)).FirstOrDefault();

                if (cashierGroup == null)
                {
                    CashierGroup cashierGroupSave = new CashierGroup();
                    cashierGroupSave.EmployeeDesignationTypeID = temp.EmployeeDesignationTypeID;
                    cashierGroupSave.FunctionName = temp.FunctionName;
                    cashierGroupSave.FunctionDescription = temp.FunctionDescription;
                    cashierGroupSave.Order = temp.Order;
                    cashierGroupSave.Value = temp.Value;
                    cashierGroupSave.Type = temp.Type;
                    cashierGroupSave.IsAccess = temp.IsAccess;
                    cashierGroupSave.IsValue = temp.IsValue;

                    context.CashierGroups.Add(cashierGroupSave);
                    context.SaveChanges();
                }
                else
                {
                    context.CashierGroups.Update(cg => cg.EmployeeDesignationTypeID.Equals(temp.EmployeeDesignationTypeID) && cg.Order.Equals(temp.Order), cp => new CashierGroup
                    {
                        FunctionName = temp.FunctionName,
                        FunctionDescription = temp.FunctionDescription,
                        Value = temp.Value,
                        Type = temp.Type,
                        IsAccess = temp.IsAccess,
                        IsValue = temp.IsValue,
                    });
                }
            }

            if (updeAllCashiers)
            {
                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@EmployeeDesignationTypeID", Value=employeeDesignationTypeID },
                    };

                CommonService.ExecuteStoredProcedure("spCashierGroupUpdate", parameter);

            }

            if (locationUpdate)
            {
                foreach (var temp in locationQry)
                {
                    context.CashierPermissions.Update(cp => cp.TypeID.Equals(employeeDesignationTypeID) && cp.Order.Equals(order) && cp.LocationID.Equals(temp.LocationID), cp => new CashierPermission
                    {
                        IsAccess = isAccess,
                        Value = value,
                    });

                }
            }
        }


        public List<CashierPermission> GetGroupFunctionsByDesignationID(int designationID) 
        {
            List<CashierPermission> rtnList = new List<CashierPermission>();

            var qry = (from cf in context.CashierGroups
                       where cf.EmployeeDesignationTypeID == designationID
                       select new
                       {
                           cf.FunctionName,
                           cf.FunctionDescription,
                           cf.Order,
                           cf.IsValue,
                           cf.IsAccess,
                           cf.Value,
                       }).ToArray();

            foreach (var temp in qry)
            {
                CashierPermission cashierPermission = new CashierPermission();

                cashierPermission.FunctionDescription = temp.FunctionDescription;
                cashierPermission.FunctionName = temp.FunctionName;
                cashierPermission.Order = temp.Order;
                cashierPermission.IsValue = temp.IsValue;
                cashierPermission.IsAccess = temp.IsAccess;
                cashierPermission.Value=temp.Value;

                rtnList.Add(cashierPermission);
            }
            return rtnList;
        }


        public List<CashierGroup> GetGroupFunctionsByDesignationIDForCashierGroup(int designationID) 
        {
            List<CashierGroup> rtnList = new List<CashierGroup>();
            CashierGroup cashierGroup = new CashierGroup();
            CashierGroup cashierGroupFind = new CashierGroup();

            var functionQry = (from cf in context.CashierFunctions
                               where cf.IsDelete == false
                               select new
                               {
                                   cf.FunctionName,
                                   cf.FunctionDescription,
                                   cf.Order,
                                   cf.IsValue,
                               }).ToArray();

            foreach (var temp in functionQry)
            {
                cashierGroupFind = context.CashierGroups.Where(cg => cg.Order.Equals(temp.Order) && cg.EmployeeDesignationTypeID.Equals(designationID)).FirstOrDefault();

                if (cashierGroupFind != null)
                {
                    cashierGroup = new CashierGroup();
                    cashierGroup.FunctionDescription = cashierGroupFind.FunctionDescription;
                    cashierGroup.FunctionName = cashierGroupFind.FunctionName;
                    cashierGroup.Order = cashierGroupFind.Order;
                    cashierGroup.IsValue = cashierGroupFind.IsValue;
                    cashierGroup.IsAccess = cashierGroupFind.IsAccess;
                    cashierGroup.Value = cashierGroupFind.Value;

                    rtnList.Add(cashierGroup);
                }
                else
                {
                    cashierGroup = new CashierGroup();
                    cashierGroup.FunctionDescription = temp.FunctionDescription;
                    cashierGroup.FunctionName = temp.FunctionName;
                    cashierGroup.Order = temp.Order;
                    cashierGroup.IsValue = temp.IsValue;
                    cashierGroup.IsAccess = false;
                    cashierGroup.Value = 0;

                    rtnList.Add(cashierGroup);
                }
            }
            return rtnList;
        }


    }
}
