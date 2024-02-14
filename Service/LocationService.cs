using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MoreLinq;
using Domain;
using Data;
using Utility;
using System.Data.Entity;
using EntityFramework.Extensions;
using System.Data.Common;
using System.Transactions;

namespace Service
{
    /// <summary>
    /// Developed by Nuwan
    /// </summary>
    public class LocationService
    {
        ERPDbContext context = new ERPDbContext();

        #region GetNewCode....

        /// <summary>
        /// Generate new Location code
        /// </summary>
        /// <param name="preFix"></param>
        /// <param name="suffix"></param>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        public string GetNewCode(string preFix, int suffix, int codeLength)
        {
            string GetNewCode = string.Empty;
            GetNewCode = context.Locations.Max(l => l.LocationCode);
            if (GetNewCode != null)
            {
                GetNewCode = GetNewCode.Trim().Substring(preFix.Length, codeLength - preFix.Length);
            }
            else
            {
                GetNewCode = "0";
            }
            GetNewCode = (int.Parse(GetNewCode) + 1).ToString();
            GetNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + GetNewCode.Length))) + GetNewCode;
            return GetNewCode;
        }

        #endregion

        #region Methods.....

        /// <summary>
        /// Save Location details
        /// </summary>
        /// <param name="Location"></param>
        public void AddLocation(Location Location)
        {
            context.Locations.Add(Location);
            context.SaveChanges();
        }

        public void AddSubDetails(string newLocationCode, int locationID)    
        {
            LocationService locationService = new LocationService();
            Location location = new Location();
            location = locationService.GetLocationsByCode(newLocationCode);

            if (location != null)
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@NewLocationID", Value=location.LocationID },
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=locationID },
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CompanyID", Value=location.CompanyID },
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CostCentreID", Value=location.CostCentreID },
                    };

                    if (CommonService.ExecuteStoredProcedure("spLocationSubDetailsSave", parameter))
                    {
                        transaction.Complete();
                    }
                }
            }
        }

        public List<Location> GetLocationsIpList()
        {
            return context.Locations.Where(l => l.IsDelete.Equals(false) && l.IsActive.Equals(true) && l.IsHeadOffice.Equals(false)).OrderBy(l => l.LocationID).ToList();
        }
        public void SaveCostCentres(List<LocationAssignedCostCentre> locationAssignedCostCentreList)   
        {
            var costCentreList = locationAssignedCostCentreList;

            foreach (var temp in costCentreList)
            {
                LocationAssignedCostCentre locationAssignedCostCentre = new LocationAssignedCostCentre();
                //locationAssignedCostCentre = locationAssignedCostCentreList.Where(cs => cs.CostCentreID.Equals(temp.CostCentreID) && cs.LocationID.Equals(temp.LocationID)).FirstOrDefault();
                locationAssignedCostCentre = context.LocationAssignedCostCentres.Where(cs => cs.CostCentreID.Equals(temp.CostCentreID) && cs.LocationID.Equals(temp.LocationID)).FirstOrDefault();

                if (locationAssignedCostCentre == null)
                {
                    LocationAssignedCostCentre locationAssignedCostCentreSave = new LocationAssignedCostCentre();

                    locationAssignedCostCentreSave.LocationID = temp.LocationID;
                    locationAssignedCostCentreSave.CostCentreID = temp.CostCentreID;
                    locationAssignedCostCentreSave.Select = temp.Select;

                    context.LocationAssignedCostCentres.Add(locationAssignedCostCentreSave);
                    context.SaveChanges();
                }
                else
                {
                    context.LocationAssignedCostCentres.Update(cs => cs.LocationID.Equals(temp.LocationID) && cs.CostCentreID.Equals(temp.CostCentreID), cp => new LocationAssignedCostCentre
                    {
                        LocationID=temp.LocationID,
                        CostCentreID=temp.CostCentreID,
                        Select=temp.Select,
                    });
                }
            }
        }

        //public void AddDocumentNumbers(string newLocationCode, int locationID)
        //{
        //    LocationService locationService = new LocationService();
        //    Location location = new Location();
        //    location = locationService.GetLocationsByCode(newLocationCode);

        //    if (location != null)
        //    {
        //        using (TransactionScope transaction = new TransactionScope())
        //        {
        //            var parameter = new DbParameter[] 
        //            { 
        //                new System.Data.SqlClient.SqlParameter { ParameterName ="@NewLocationID", Value=locationID },
        //                new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=location.LocationID },
        //                new System.Data.SqlClient.SqlParameter { ParameterName ="@CompanyID", Value=location.CompanyID },
        //                new System.Data.SqlClient.SqlParameter { ParameterName ="@CostCentreID", Value=location.CostCentreID },
        //            };

        //            if (CommonService.ExecuteStoredProcedure("spLocationSubDetailsSave", parameter))
        //            {
        //                transaction.Complete();
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Update location details
        /// </summary>
        /// <param name="existingLocation"></param>
        public void UpdateLocation(Location existingLocation)
        {
            existingLocation.ModifiedUser = Common.LoggedUser;
            existingLocation.ModifiedDate = Common.GetSystemDateWithTime();
            this.context.Entry(existingLocation).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Retrive all active location details by location code
        /// </summary>
        /// <param name="LocationCode"></param>
        /// <returns></returns>
        public Location GetLocationsByCode(string LocationCode)
        {
            return context.Locations.Where(l => l.LocationCode.Equals(LocationCode) && l.IsDelete.Equals(false)).FirstOrDefault();
        }

        public Location GetHeadOfficeLocationsByCode(string LocationCode)
        {
            return context.Locations.Where(l => l.LocationCode.Equals(LocationCode) && l.IsDelete.Equals(false) && l.IsHeadOffice.Equals(true)).FirstOrDefault();
        }

        public Location GetLocationsByID(int LocationID)
        {
            return context.Locations.Where(l => l.LocationID.Equals(LocationID) && l.IsDelete.Equals(false)).FirstOrDefault();
        }

        public Location GetHeadOfficeLocationsByID(int LocationID)
        {
            return context.Locations.Where(l => l.LocationID.Equals(LocationID) && l.IsDelete.Equals(false) && l.IsHeadOffice.Equals(true)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active location details by location name
        /// </summary>
        /// <param name="LocationName"></param>
        /// <returns></returns>
        public Location GetLocationsByName(string LocationName)
        {
            return context.Locations.Where(l => l.LocationName.Equals(LocationName) && l.IsDelete.Equals(false)).FirstOrDefault();
        }

        public Location GetHeadOfficeLocationsByName(string LocationName)
        {
            return context.Locations.Where(l => l.LocationName.Equals(LocationName) && l.IsDelete.Equals(false) && l.IsHeadOffice.Equals(true)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active location details by location name
        /// </summary>
        /// <param name="LocationName"></param>
        /// <returns></returns>
        public Company GetCompanyByID(int companyID)
        {
            return context.Companies.Where(l => l.CompanyID.Equals(companyID) && l.IsDelete.Equals(false)).FirstOrDefault();
        }

        /// <summary>
        /// Retrive all active location details
        /// </summary>
        /// <returns></returns>
        /// 

        public List<Location> GetAllLocations()
        {
            return context.Locations.Where(l => l.IsDelete.Equals(false)).OrderBy(l=> l.LocationID).ToList();
        }

        public List<Location> GetAllLocationsInventory() 
        {
            return context.Locations.Where(l => l.IsDelete.Equals(false) && l.IsStockLocation.Equals(true)).OrderBy(l => l.LocationID).ToList();
        }

        public List<Location> GetAllInventoryLocations()
        {
            return context.Locations.Where(l => l.IsDelete.Equals(false) && l.IsStockLocation.Equals(true)).OrderBy(l => l.LocationID).ToList();
        }

        public string[] GetAllInventoryLocationNames()
        {
            var locationNames = context.Locations.Where(l => l.IsDelete.Equals(false) && l.IsStockLocation.Equals(true)).OrderBy(l => l.LocationName).Select(l=> l.LocationName).ToList();
            return locationNames.ToArray();
        }

        public List<Location> GetHeadOfficeLocation()
        {
            return context.Locations.Where(l => l.IsDelete.Equals(false) && l.IsHeadOffice.Equals(true)).OrderBy(l => l.LocationID).ToList();
        }

        /// <summary>
        /// Retrive active locations except logged location
        /// </summary>
        /// <returns></returns>
        /// 

        public List<Location> GetLocationsExceptingCurrentLocation(int locationID)
        {
            return context.Locations.Where(l => l.IsDelete.Equals(false) && l.LocationID != locationID).OrderBy(l => l.LocationID).ToList();
        }

        public List<Location> GetLocationsExceptingCurrentLocationInventory(int locationID) 
        {
            return context.Locations.Where(l => l.IsDelete.Equals(false) && l.LocationID != locationID && l.IsStockLocation.Equals(true)).OrderBy(l => l.LocationID).ToList();
        }

        /// <summary>
        /// Retrive active HO locations
        /// </summary>
        /// <returns></returns>
        /// 
        public List<Location> GetHeadOfficeLocationsExceptingCurrentHOLocation(int locationID)
        {
            return context.Locations.Where(l => l.IsDelete.Equals(false) && l.LocationID != locationID && l.IsHeadOffice.Equals(true)).OrderBy(l => l.LocationID).ToList();
        }

        public void UpdateLocationForDataTransfer(int datatransfer)
        {
            context.Locations.Update(ps => ps.DataTransfer.Equals(1), ps => new Location { DataTransfer = datatransfer });
        }

        public void UpdateLocationForDTSelect(int datatransfer)
        {
            context.Locations.Update(ps => ps.DataTransfer.Equals(0), ps => new Location { DataTransfer = datatransfer });
        }

        public void UpdateDTLocationForDataTransfer(int datatransfer)
        {
            context.Locations.Update(ps => ps.DataTransfer.Equals(0), ps => new Location { DataTransfer = datatransfer });
        }

        /// <summary>
        /// Delete location details
        /// </summary>
        /// <param name="existingLocation"></param>
        public void DeleteLocation(Location existingLocation) 
        {
            existingLocation.IsDelete = true;
            this.context.Entry(existingLocation).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public string[] GetAllLocationCodes()
        {
            List<string> LocationCodeList = context.Locations.Where(l => l.IsDelete.Equals(false)).Select(l => l.LocationCode).ToList();
            return LocationCodeList.ToArray();
        }

        public string[] GetAllLocationNames()
        {
            List<string> LocationNameList = context.Locations.Where(l => l.IsDelete.Equals(false)).OrderBy(l=> l.LocationName).Select(l => l.LocationName).ToList();
            return LocationNameList.ToArray();
        }


        public DataTable GetAllLocationsOutLetDataTble()
        {
            DataTable tblLocations = new DataTable();
            var query = from d in context.Locations
                        join c in context.Companies on d.CompanyID equals c.CompanyID
                        join cs in context.CostCentres on c.CostCentreID equals cs.CostCentreID
                        where d.IsHeadOffice.Equals(true)
                        select new
                        {
                            d.LocationID,
                            d.CompanyID,
                            d.LocationCode,
                            d.LocationName,
                            d.Address1,
                            d.Address2,
                            d.Address3,
                            d.Telephone,
                            d.Mobile,
                            d.FaxNo,
                            d.Email,
                            d.ContactPersonName,
                            d.OtherBusinessName,
                            d.LocationPrefixCode,
                            d.TypeOfBusiness,
                            d.CostingMethod,
                            d.CostCentreID,
                            d.IsVat,
                            d.IsStockLocation,
                            d.IsDelete,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            DataTransfer = d.DataTransfer,
                            d.IsHeadOffice,
                            d.UploadPath,
                            d.DownloadPath,
                            d.LocalUploadPath,
                            d.BackupPath,
                            d.LocationIP
                        };


            return tblLocations = query.ToDataTable();
        }
        public DataTable GetAllLocationsDataTble()
        {
            DataTable tblLocations=new DataTable();
            var query = from d in context.Locations 
                        join c in context.Companies on d.CompanyID equals c.CompanyID
                        join cs in context.CostCentres on c.CostCentreID equals cs.CostCentreID
                        select new 
                        { 
                          d.LocationCode, 
                          d.LocationName,
                          c.CompanyName,
                          d.Mobile,
                          d.FaxNo,
                          d.Email, 
                          d.Address1, 
                          d.Address2, 
                          d.Address3, 
                          d.Telephone,
                          d.CostingMethod, 
                          cs.CostCentreName,
                        };


            return tblLocations = query.ToDataTable();
        }

        public DataTable GetAllLocationsHODataTble()
        {
                DataTable tblLocations = new DataTable();
            var query = from d in context.Locations
                        where d.IsHeadOffice.Equals(true) && d.IsActive.Equals(true)
                        select new
                        {
                            d.LocationID,
                            d.CompanyID,
                            d.LocationCode,
                            d.LocationName,
                            d.Address1,
                            d.Address2,
                            d.Address3,
                            d.Telephone,
                            d.Mobile,
                            d.FaxNo,
                            d.Email,
                            d.ContactPersonName,
                            d.OtherBusinessName,
                            d.LocationPrefixCode,
                            d.TypeOfBusiness,
                            d.CostingMethod,
                            d.CostCentreID,
                            d.IsVat,
                            d.IsStockLocation,
                            d.IsDelete,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            DataTransfer = d.DataTransfer,
                            d.IsHeadOffice,
                            d.UploadPath,
                            d.DownloadPath,
                            d.LocalUploadPath,
                            d.BackupPath,
                            d.LocationIP
                        };


            return tblLocations = query.ToDataTable();
        }

        public DataTable GetAllLocationsOLDataTble()
        {
            DataTable tblLocations = new DataTable();
            var query = from d in context.Locations
                        where d.IsHeadOffice.Equals(false) && d.IsActive.Equals(true)
                        select new
                        {
                            d.LocationID,
                            d.CompanyID,
                            d.LocationCode,
                            d.LocationName,
                            d.Address1,
                            d.Address2,
                            d.Address3,
                            d.Telephone,
                            d.Mobile,
                            d.FaxNo,
                            d.Email,
                            d.ContactPersonName,
                            d.OtherBusinessName,
                            d.LocationPrefixCode,
                            d.TypeOfBusiness,
                            d.CostingMethod,
                            d.CostCentreID,
                            d.IsVat,
                            d.IsStockLocation,
                            d.IsDelete,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            DataTransfer = d.DataTransfer,
                            d.IsHeadOffice,
                            d.UploadPath,
                            d.DownloadPath,
                            d.LocalUploadPath,
                            d.BackupPath,
                            d.LocationIP
                        };


            return tblLocations = query.ToDataTable();
        }

        public DataTable GetOutLetDataTble()
        {
            DataTable tblLocations = new DataTable();
            var query = from d in context.Locations
                        where d.IsHeadOffice.Equals(false) && d.IsActive.Equals(true)
                        select new
                        {
                            d.LocationID,
                            d.CompanyID,
                            d.LocationCode,
                            d.LocationName,
                            d.Address1,
                            d.Address2,
                            d.Address3,
                            d.Telephone,
                            d.Mobile,
                            d.FaxNo,
                            d.Email,
                            d.ContactPersonName,
                            d.OtherBusinessName,
                            d.LocationPrefixCode,
                            d.TypeOfBusiness,
                            d.CostingMethod,
                            d.CostCentreID,
                            d.IsVat,
                            d.IsStockLocation,
                            d.IsDelete,
                            d.GroupOfCompanyID,
                            d.CreatedUser,
                            d.CreatedDate,
                            d.ModifiedUser,
                            d.ModifiedDate,
                            DataTransfer = d.DataTransfer,
                            d.IsHeadOffice,
                            d.UploadPath,
                            d.DownloadPath,
                            d.LocalUploadPath,
                            d.BackupPath,
                            d.LocationIP
                        };


            return tblLocations = query.ToDataTable();
        }

        public DataTable GetAllDTLocationsDataTble()
        {
            DataTable tblLocations = new DataTable();
            var query = from d in context.Locations
                        where d.DataTransfer.Equals(1)
                        select new
                            {
                                d.LocationID,
                                d.CompanyID,
                                d.LocationCode,
                                d.LocationName,
                                d.Address1,
                                d.Address2,
                                d.Address3,
                                d.Telephone,
                                d.Mobile,
                                d.FaxNo,
                                d.Email,
                                d.ContactPersonName,
                                d.OtherBusinessName,
                                d.LocationPrefixCode,
                                d.TypeOfBusiness,
                                d.CostingMethod,
                                d.CostCentreID,
                                d.IsVat,
                                d.IsStockLocation,
                                d.IsDelete,
                                d.GroupOfCompanyID,
                                d.CreatedUser,
                                d.CreatedDate,
                                d.ModifiedUser,
                                d.ModifiedDate,
                                DataTransfer = d.DataTransfer,
                                d.IsHeadOffice,
                                d.UploadPath,
                                d.DownloadPath,
                                d.LocalUploadPath,
                                d.BackupPath,
                                d.LocationIP
                            };
 
            return tblLocations = query.ToDataTable();
            //context.Configuration.LazyLoadingEnabled = false;
            //return context.Locations.SqlQuery("select LocationID ,CompanyID ,LocationCode ,LocationName ,Address1 ,Address2 ,Address3 ,Telephone ,Mobile ,FaxNo ,Email ,ContactPersonName ,OtherBusinessName ,LocationPrefixCode ,TypeOfBusiness ,CostingMethod ,CostCentreID ,IsVat ,IsStockLocation ,IsDelete ,GroupOfCompanyID ,CreatedUser ,CreatedDate ,ModifiedUser ,ModifiedDate ,DataTransfer from Location").ToDataTable();
        }

        public DataTable GetAllLocationDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportGroupDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.Locations.Where("IsDelete=false");

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof (int)))
                {
                    if (reportConditionsDataStruct.ReportDataStruct.IsJoinField == true)
                    {
                        switch (reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim())
                        {
                            case "CompanyID":
                                query = (from qr in query
                                         join jt in context.Companies.Where(
                                             "" +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " >= " + "@0 AND " +
                                             reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                             " <= @1",
                                             (reportConditionsDataStruct.ConditionFrom.Trim()),
                                             (reportConditionsDataStruct.ConditionTo.Trim())
                                             ) on qr.CompanyID equals jt.CompanyID
                                         select qr
                                        );

                                break;
                            default:
                                query =
                                    query.Where(
                                        "" + reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() +
                                        " >= @0 AND " +
                                        reportConditionsDataStruct.ReportDataStruct.DbJoinColumnName.Trim() + " <= @1",
                                        long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()),
                                        long.Parse(reportConditionsDataStruct.ConditionTo.Trim()));

                                break;
                        }
                    }
                    else
                    { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= " + "@0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
                }
            }

            // Set fields to be selected
            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                querrySelect.Append(reportDataStruct.DbColumnName.Trim() + " AS " + reportDataStruct.ReportField.Trim() + ", ");
            }

            // Remove last two charators (", ")
            querrySelect.Remove(querrySelect.Length - 2, 2);
            //DataTable n = query.ToDataTable();

            var queryResult = (from lc in query
                      join cm in context.Companies on lc.CompanyID equals cm.CompanyID
                      join cs in context.CostCentres on lc.CostCentreID equals cs.CostCentreID
                      select new
                      {
                          FieldString1 = lc.LocationCode,
                          FieldString2 = lc.LocationName,
                          FieldString3 = cm.CompanyName,
                          FieldString4 = (lc.Address1 + ", " + lc.Address2 + ", " + lc.Address3),
                          FieldString5 = lc.Telephone,
                          FieldString6 = lc.ContactPersonName,
                          FieldString7 = lc.OtherBusinessName,
                          FieldString8 = EntityFunctions.TruncateTime(lc.CreatedDate),
                          FieldString9 = lc.CostingMethod,
                          FieldString10 = lc.TypeOfBusiness,
                          FieldString11 = cs.CostCentreName,
                          FieldString12 = lc.LocationPrefixCode
                      }); //.ToArray();
            //DataTable n1 = queryResult.ToDataTable();

            DataTable dtQueryResult = new DataTable();
            StringBuilder sbOrderByColumns = new StringBuilder();

            if (reportOrderByDataStructList.Any(g => g.IsResultOrderBy))
            {
                foreach (var item in reportOrderByDataStructList.Where(g => g.IsResultOrderBy.Equals(true)))
                {sbOrderByColumns.Append("" + item.ReportField + " , ");}

                sbOrderByColumns.Remove(sbOrderByColumns.Length - 2, 2); // remove last ','

                dtQueryResult = queryResult.OrderBy(sbOrderByColumns.ToString())
                                           .ToDataTable();
            }
            else
            {dtQueryResult = queryResult.ToDataTable();}

            foreach (Common.ReportDataStruct reportDataStruct in reportDataStructList)
            {
                if (dtQueryResult.Columns.Contains(reportDataStruct.ReportField))
                {
                    if (!reportDataStruct.IsSelectionField)
                    {dtQueryResult.Columns.Remove(reportDataStruct.ReportField);}
                }
            }
            
            return dtQueryResult; //queryResult.ToDataTable();
        }

        public DataTable GetAllLocationDataTable()
        {
            //DataTable tblLocation = new DataTable();

            var query = (from lc in context.Locations
                      join cm in context.Companies on lc.CompanyID equals cm.CompanyID
                      join cs in context.CostCentres on lc.CostCentreID equals cs.CostCentreID
                      select new
                      {
                          FieldString1 = lc.LocationCode,
                          FieldString2 = lc.LocationName,
                          FieldString3 = cm.CompanyName,
                          FieldString4 = (lc.Address1 + ", " + lc.Address2 + ", " + lc.Address3),
                          FieldString5 = lc.Telephone,
                          FieldString6 = lc.ContactPersonName,
                          FieldString7 = lc.OtherBusinessName,
                          FieldString8 = EntityFunctions.TruncateTime(lc.CreatedDate),
                          FieldString9 = lc.CostingMethod,
                          FieldString10 = lc.TypeOfBusiness,
                          FieldString11 = cs.CostCentreName
                      }).ToArray();

            //var data2 = (from c in sq
            //             select new
            //             {
            //                 FieldString1 = c.FieldString1.ToString(),
            //                 FieldString2 = c.FieldString2.ToString(),
            //                 FieldString3 = c.FieldString3.ToString(),
            //                 FieldString4 = c.FieldString4.ToString(),
            //                 FieldString5 = c.FieldString5.ToString(),
            //                 FieldString6 = c.FieldString6.ToString(),
            //                 FieldString7 = c.FieldString7.ToString(),
            //                 FieldString8 = c.FieldString8.ToString(),
            //                 FieldString9 = c.FieldString9.ToString(),
            //                 FieldString10 = c.FieldString10.ToString(),
            //                 FieldString11 = c.FieldString11.ToString(),
            //                 FieldString12 = c.FieldString12.ToString(),
            //                 FieldString13 = c.FieldString13.ToString(),
            //                 FieldString14 = c.FieldString14.ToString()

            //             });


            //return tblLocation = Common.LINQToDataTable(data2);
            return query.AsEnumerable().ToDataTable();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.Locations
                                             .Where("IsDelete == @0 ", false)
                                             .OrderBy(reportDataStruct.DbColumnName);
                            //.OrderBy("" + reportDataStruct.DbColumnName.Trim() + "")
                            //.Select(reportDataStruct.DbColumnName.Trim());


            switch (reportDataStruct.DbColumnName.Trim())
            {
                case "CompanyID":
                    qryResult = qryResult.Join(context.Companies, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.CompanyName)")
                                .GroupBy("new(CompanyName)", "new(CompanyName)")
                                .OrderBy("Key.CompanyName")
                                .Select("Key.CompanyName");
                    break;
                case "CostCentreID":
                    qryResult = qryResult.Join(context.CostCentres, reportDataStruct.DbColumnName.Trim(), reportDataStruct.DbColumnName.Trim(), "new(inner.CostCentreName)")
                               .GroupBy("new(CostCentreName)", "new(CostCentreName)")
                               .OrderBy("Key.CostCentreName")
                               .Select("Key.CostCentreName");
                    break;
                case "LocationCode":
                    qryResult = qryResult
                               .OrderBy("LocationCode")
                               .Select("LocationCode");
                    break;
                default:
                    qryResult = qryResult.OrderBy(reportDataStruct.DbColumnName)
                                         .Select(reportDataStruct.DbColumnName.Trim());
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

        public bool IsExistsLocation(string locationCode)
        {
            
            if( GetLocationsByCode(locationCode)!=null)
                return true;
            else
               return false;

           

        }

        public bool IsExistsLocation(int locationID)
        {

            if (GetLocationsByID(locationID) != null)
                return true;
            else
                return false;

        }


        public List<Location> GetLocationsByUserId(int userMasterId, string userPassword)
        {
            Location location;

            List<Location> transactionList = new List<Location>();


            var locationDetails = (from up in context.UserMaster
                                   join tg in context.UserPrivilegesLocations on up.UserMasterID equals tg.UserMasterID
                                   join l in context.Locations on tg.LocationID equals l.LocationID
                                   where up.UserMasterID.Equals(userMasterId) && up.Password.Equals(userPassword) && tg.IsSelect == true
                                   select new
                                   {
                                       l.LocationID,
                                       l.LocationName


                                   }).ToArray();

            foreach (var tempPrivileges in locationDetails)
            {

                location = new Location();


                location.LocationName = tempPrivileges.LocationName;
                location.LocationID = tempPrivileges.LocationID;



                transactionList.Add(location);

            }


            return transactionList.OrderBy(pd => pd.LocationID).ToList();
        }

        public List<CostCentre> GetLoationAssignedCostCentres(Location location)
        {
            CostCentreService costCentreService = new CostCentreService();
            List<CostCentre> rtnList = new List<CostCentre>();

            rtnList = costCentreService.GetAllCostCentres();

            var qry = (from cs in context.LocationAssignedCostCentres
                       where cs.LocationID == location.LocationID &&
                       cs.IsDelete == false
                       select new
                       {
                           cs.CostCentreID,
                           cs.LocationID,
                           cs.Select
                       }).ToArray();
            foreach (var temp in qry)
            {
                CostCentre costCentre = new CostCentre();

                costCentre = costCentreService.GetCostCentresByID(temp.CostCentreID);
                rtnList.Remove(costCentre);

                costCentre.Select = temp.Select;
                rtnList.Add(costCentre);
            }
            return rtnList.OrderBy(cs => cs.CostCentreID).ToList();
        }


        public int[] GetInventoryLocationsIdRangeByLocationNames(string nameFrom, string nameTo)
        {
            return context.Locations.Where(l => l.IsDelete.Equals(false) && l.IsStockLocation.Equals(true) && l.LocationName.ToLower().CompareTo(nameFrom.ToLower()) >= 0 && l.LocationName.ToLower().CompareTo(nameTo.ToLower()) <= 0)
                                    .Select(l=> l.LocationID).ToArray();
        }

        #endregion
    }
}
