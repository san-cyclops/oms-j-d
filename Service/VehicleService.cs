using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Domain;
using Data;
using System.Data;
using Utility;
using System.Data.Entity;

namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class VehicleService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods.....

        /// <summary>
        /// Save Vehicle
        /// </summary>
        /// <param name="Vehicle"></param>
        public void AddVehicle(Vehicle Vehicle) 
        {
            context.Vehicles.Add(Vehicle);
            context.SaveChanges();
        }

        /// <summary>
        /// Retrive all active vehicle details by Vehicle Registration Number
        /// </summary>
        /// <param name="Vehicle"></param>
        /// <returns></returns>
        public Vehicle GetVehicleByRegistrationNo(string Vehicle)
        {
            return context.Vehicles.Where(v =>v.RegistrationNo == Vehicle && v.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active vehicle details
        /// </summary>
        /// <returns></returns>
        public List<Vehicle> GetAllVehicles() 
        {
            return context.Vehicles.Where(v => v.IsDelete.Equals(false)).ToList();
        }

        /// <summary>
        /// Update Vehicle
        /// </summary>
        /// <param name="existingVehicle"></param>
        public void UpdateVehicle(Vehicle existingVehicle)
        {
            existingVehicle.ModifiedUser = Common.LoggedUser;
            existingVehicle.ModifiedDate = Common.GetSystemDateWithTime();
            this.context.Entry(existingVehicle).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Retrive all active vehicle details by vehicle name
        /// </summary>
        /// <param name="vehicleName"></param>
        /// <returns></returns>
        public Vehicle GetVehicleByName(string vehicleName)
        {
            return context.Vehicles.Where(t => t.VehicleName == vehicleName && t.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Delete vehicle
        /// </summary>
        /// <param name="existingVehicle"></param>
        public void DeleteVehicle(Vehicle existingVehicle)
        {
            existingVehicle.IsDelete = true;
            this.context.Entry(existingVehicle).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public string[] GetAllRegistrationNumbers()
        {
            List<string> AutocompleteRegistrationNumbers = context.Vehicles.Where(v => v.IsDelete.Equals(false)).Select(v => v.RegistrationNo).ToList();
            return AutocompleteRegistrationNumbers.ToArray(); 
        }

        public DataTable GetAllVehiclesDataTable()
        {
            DataTable tblVehicle = new DataTable();
            var query = from v in context.Vehicles where v.IsDelete.Equals(false) select new { v.VehicleName, v.RegistrationNo, v.Model, v.VehicleType, v.FuelType };
            return tblVehicle = Common.LINQToDataTable(query);
        }


        public DataTable GetAllVehicleDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.Vehicles.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                {
                    query =
                        query.Where(
                            "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " +
                            reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                            reportConditionsDataStruct.ConditionFrom.Trim(),
                            reportConditionsDataStruct.ConditionTo.Trim());
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                {
                    query =
                        query.Where(
                            "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " +
                            reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                            DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                            DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                {
                    query =
                        query.Where(
                            "" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " +
                            reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1",
                            decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                            decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim()));
                }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", ");
            }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);

            var queryResult = (from lc in query
                               select new
                               {
                                   FieldString1 = lc.RegistrationNo,
                                   FieldString2 = lc.VehicleName,
                                   FieldString3 = lc.EngineNo,
                                   FieldString4 = lc.ChassesNo,
                                   FieldString5 = lc.Make,
                                   FieldString6 = lc.Model,
                                   FieldString7 = lc.VehicleType,
                                   FieldString8 = lc.FuelType,
                                   FieldString9 = lc.Weight,
                                   FieldString10 = lc.EngineCapacity,
                                   FieldString11 = lc.SeatingCapacity,
                                   FieldString12 = EntityFunctions.TruncateTime(lc.CreatedDate),
                                   FieldString13 = lc.Remark
                               }); //.ToArray();

            DataTable dtQueryResult = new DataTable();
            StringBuilder sbOrderByColumns = new StringBuilder();

            if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
            {
                foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                { sbOrderByColumns.Append("" + item.ReportField + " , "); }

                sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                dtQueryResult = queryResult.OrderBy(sbOrderByColumns.ToString())
                                           .ToDataTable();
            }
            else
            { dtQueryResult = queryResult.ToDataTable(); }

            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                {
                    if (!reportDataStruct.IsSelectionField)
                    { dtQueryResult.Columns.Remove(reportDataStruct.ReportField); }
                }
            }

            return dtQueryResult; //return queryResult.ToDataTable();
        }

        public DataTable GetAllVehicleDataTable()
        {
            var query = (from lc in context.Vehicles
                         select new
                         {
                             FieldString1 = lc.RegistrationNo,
                             FieldString2 = lc.VehicleName,
                             FieldString3 = lc.EngineNo,
                             FieldString4 = lc.ChassesNo,
                             FieldString5 = lc.Make,
                             FieldString6 = lc.Model,
                             FieldString7 = lc.VehicleType,
                             FieldString8 = lc.FuelType,
                             FieldString9 = lc.Weight,
                             FieldString10 = lc.EngineCapacity,
                             FieldString11 = lc.SeatingCapacity,
                             FieldString12 = EntityFunctions.TruncateTime(lc.CreatedDate),
                             FieldString13 = lc.Remark
                         }).ToArray();

            return query.AsEnumerable().ToDataTable();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.Vehicles
                                          .Where("IsDelete == @0 ", false);
            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
            //.Select(reportDataStruct.DbColumnName.Trim());


            switch (reportDataStruct.DbColumnName.Trim())
            {
                default:
                    qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
                    break;
            }

            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(DateTime)))
            {
                foreach (var item in qryResult)
                {
                    DateTime tdnew = new DateTime();
                    tdnew = Convert.ToDateTime(item.ToString());
                    selectionDataList.Add(tdnew.ToShortDateString());
                }
            }
            else
            {
                foreach (var item in qryResult)
                { selectionDataList.Add(item.ToString()); }
            }
            return selectionDataList;
        }
        #endregion
    }
}
