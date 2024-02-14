using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.Extensions;
using MoreLinq;

using Domain;
using Data;
using System.Data;
using Utility;
using System.Data.Entity;
using System.Collections;


namespace Service
{
    /// <summary>
    /// Developed by - asanka
    /// </summary>

    public class LgsMaintenanceJobRequisitionService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        public string GetDocumentNo(string formName, int locationID, string locationCode, int documentId, bool isTemporytNo)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            long tempCode = 0;

            string preFix = string.Empty;

            bool isLocationCode = autoGenerateInfo.IsLocationCode;
            int codeLength = autoGenerateInfo.CodeLength;
            preFix = autoGenerateInfo.Prefix.Trim();

            if (isTemporytNo)
            {
                if ((preFix.StartsWith("T")))
                { preFix = "X"; }
                else
                { preFix = "T"; }
            }

            if (isLocationCode)
            { preFix = preFix + locationCode.Trim(); }

            if (isTemporytNo)
            {
                context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber { TempDocumentNo = d.TempDocumentNo + 1 });
                tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.TempDocumentNo);
            }
            else
            {
                context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber { DocumentNo = d.DocumentNo + 1 });
                tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.DocumentNo);
            }

            getNewCode = (tempCode).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode.ToUpper();
        }


        /// <summary>
        /// Get paused document details by document number
        /// </summary>
        /// <param name="pausedDocumentNo"></param>
        /// <returns></returns>
        public LgsMaintenanceJobRequisitionHeader GetPausedDocumentDetailsByDocumentNumber(string pausedDocumentNo)
        {
            return context.LgsMaintenanceJobRequisitionHeaders.Where(po => po.DocumentNo == pausedDocumentNo.Trim() && po.DocumentStatus.Equals(0)).FirstOrDefault();
        }

        /// <summary>
        /// Get LgsMaintenanceJobRequisitionHeader By DocumentNo 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public LgsMaintenanceJobRequisitionHeader GetLgsMaintenanceJobRequisitionHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsMaintenanceJobRequisitionHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        public LgsMaintenanceJobRequisitionHeader GetLgsMaintenanceJobRequisitionHeaderByDocumentNo(string documentNo)
        {
            return context.LgsMaintenanceJobRequisitionHeaders.Where(ph => ph.DocumentNo.Equals(documentNo)).FirstOrDefault();
        }
        

        /// <summary>
        /// Save Maintenance Job Requisition note
        /// </summary>
        /// <param name="lgsMaintenanceJobRequisitionHeader"></param>
        /// <param name="lgsMaterialRequestDetailsTemp"></param>
        /// <returns></returns>
        public bool SaveMaintenanceJobRequisitionNote(LgsMaintenanceJobRequisitionHeader lgsMaintenanceJobRequisitionHeader)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                if (lgsMaintenanceJobRequisitionHeader.LgsMaintenanceJobRequisitionHeaderID.Equals(0))
                {
                    context.LgsMaintenanceJobRequisitionHeaders.Add(lgsMaintenanceJobRequisitionHeader);
                    this.context.SaveChanges();
                    lgsMaintenanceJobRequisitionHeader.MaintenanceJobRequisitionHeaderID = lgsMaintenanceJobRequisitionHeader.LgsMaintenanceJobRequisitionHeaderID;
                    this.context.Entry(lgsMaintenanceJobRequisitionHeader).State = EntityState.Modified;
                    this.context.SaveChanges();
                }
                else
                {
                    this.context.Entry(lgsMaintenanceJobRequisitionHeader).State = EntityState.Modified;
                    this.context.SaveChanges();
                }
                                
                transactionScope.Complete();
                return true;
            }
        }

        /// <summary>
        /// Confirm Maintenance Job Requisition for Maintenance Job Assign Note
        /// Consider location
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <param name="documentStatus"></param>
        /// <returns></returns>
        public bool ConfirmMaintenanceJobRequisitionDocumentNo(int documentID, string documentNo, int locationID, int documentStatus)
        {
            if (context.LgsMaintenanceJobRequisitionHeaders.Where(jh => jh.DocumentID.Equals(documentID) && jh.DocumentNo.Equals(documentNo) && jh.LocationID.Equals(locationID) && jh.DocumentStatus.Equals(documentStatus) && jh.ReferenceDocumentDocumentID.Equals(0)).FirstOrDefault() != null)
            { return true; }
            else
            { return false; }
        }

        /// <summary>
        /// Confirm Maintenance Job Requisition for Maintenance Job Assign Note
        /// Do not Consider location
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="documentStatus"></param>
        /// <returns></returns>
        public bool ConfirmMaintenanceJobRequisitionDocumentNo(int documentID, string documentNo, int documentStatus)
        {
            return (from a in context.LgsMaintenanceJobRequisitionHeaders
                    where !(from b in context.LgsMaintenanceJobAssignHeaders
                            where b.DocumentStatus == 1
                            select b.ReferenceDocumentID).Contains(a.LgsMaintenanceJobRequisitionHeaderID) 
                            && a.DocumentStatus == documentStatus && a.DocumentID == documentID && a.DocumentNo == documentNo
                    select a.DocumentNo).Any();            
        }

        /// <summary>
        /// Get Saved Document Details for Maintenance Job Assign Note
        /// </summary>
        /// <param name="documentNo"></param>
        /// <returns></returns>
        public LgsMaintenanceJobRequisitionHeader GetSavedDocumentDetailsByDocumentNumber(string documentNo)
        {
            return context.LgsMaintenanceJobRequisitionHeaders.Where(jh => jh.DocumentNo == documentNo.Trim() && jh.DocumentStatus.Equals(1) && jh.ReferenceDocumentDocumentID.Equals(0)).FirstOrDefault();
        }

        public LgsMaintenanceJobRequisitionHeader GetMaintananceJobHeader(string documentNo) 
        {
            return context.LgsMaintenanceJobRequisitionHeaders.Where(jh => jh.DocumentNo == documentNo.Trim() && jh.DocumentStatus.Equals(1)).FirstOrDefault();
        }


        /// <summary>
        /// Get Saved Document number for Maintenance Job Assign Note
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public string  GetSavedDocumentNoByDocumentID(long documentID)
        {
            return context.LgsMaintenanceJobRequisitionHeaders.Where(jh => jh.LgsMaintenanceJobRequisitionHeaderID == documentID && jh.DocumentStatus.Equals(1) && jh.ReferenceDocumentDocumentID.Equals(0)).FirstOrDefault().DocumentNo.Trim();
        }

        /// <summary>
        /// Get LgsMaintenanceJobRequisitionHeader for Maintenance Job Assign Note
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public LgsMaintenanceJobRequisitionHeader GetLgsMaintenanceJobRequisitionHeaderByDocumentID(long documentID)
        {
            return context.LgsMaintenanceJobRequisitionHeaders.Where(jh => jh.LgsMaintenanceJobRequisitionHeaderID == documentID && jh.DocumentStatus.Equals(1) && jh.ReferenceDocumentDocumentID.Equals(0)).FirstOrDefault();
        }

        /// <summary>
        /// Get paused LgsMaintenanceJobRequisitionHeader By DocumentNo
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentNo"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public LgsMaintenanceJobRequisitionHeader GetPausedLgsMaintenanceJobRequisitionHeaderByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.LgsMaintenanceJobRequisitionHeaders.Where(ph => ph.DocumentID.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        /// <summary>
        ///  Get paused document numbers
        /// </summary>
        /// <returns></returns>
        public string[] GetPausedDocumentNumbers()
        {
            List<string> pausedDocumentList = new List<string>();
            pausedDocumentList = context.LgsMaintenanceJobRequisitionHeaders.Where(po => po.DocumentStatus.Equals(0)).Select(po => po.DocumentNo).ToList();
            string[] pausedDocumentCodes = pausedDocumentList.ToArray();
            return pausedDocumentCodes;
        }

        /// <summary>
        /// Get pending maintenance job list 
        /// </summary>
        /// <returns></returns>
        public string[] GetPendingMaintenanceJobList()
        {
            return (from a in context.LgsMaintenanceJobRequisitionHeaders
                    where !(from b in context.LgsMaintenanceJobAssignHeaders
                            where b.DocumentStatus == 1
                            select b.ReferenceDocumentID).Contains(a.LgsMaintenanceJobRequisitionHeaderID) && a.DocumentStatus == 1
                    select a.DocumentNo).ToArray();            
        }


        /// <summary>
        /// Get Maintenance Job Requisition details for report
        /// </summary>
        /// <returns></returns>
        public DataTable GetMaintenanceJobRequisitionTransactionDataTable(string documentNo, int documentStatus, int documentId)
        {
            var query = (
                        from ph in context.LgsMaintenanceJobRequisitionHeaders                                                                        
                        join lh in context.Locations on ph.LocationID equals lh.LocationID                        
                        where ph.DocumentNo.Equals(documentNo) && ph.DocumentStatus.Equals(documentStatus) && ph.DocumentID.Equals(documentId)
                        select
                            new
                            {
                                FieldString1 = ph.DocumentNo,
                                FieldString2 = DbFunctions.TruncateTime(ph.DocumentDate),
                                FieldString3 = DbFunctions.TruncateTime(ph.ExpectedDate),
                                FieldString4 = "",
                                FieldString5 = ph.ReferenceDocumentNo,
                                FieldString6 = "",
                                FieldString7 = "",
                                FieldString8 = "",
                                FieldString9 = "",
                                FieldString10 = "",
                                FieldString11 = "",
                                FieldString12 = lh.LocationCode + "  " + lh.LocationName,
                                FieldString13 = ph.JobDescription,
                                FieldString14 = "",
                                FieldString15 = "",
                                FieldString16 = "",
                                FieldString17 = "",
                                FieldString18 = "",
                                FieldString19 = "",
                                FieldString20 = "",
                                FieldString21 = "",
                                FieldString22 = "",
                                FieldString23 = "",
                                FieldString24 = "",
                                FieldString25 = "",
                                FieldString26 = "",
                                FieldString27 = "",
                                FieldString28 = "",
                            });

            return query.AsEnumerable().ToDataTable();

        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct, AutoGenerateInfo autoGenerateInfo)
        {
            // Create query 
            IQueryable qryResult = context.LgsMaintenanceJobRequisitionHeaders.Where("DocumentStatus == @0 AND DocumentId == @1", 1, autoGenerateInfo.DocumentID);

            switch (reportDataStruct.DbColumnName.Trim())
            {
                default:
                    qryResult = qryResult.Select(reportDataStruct.DbColumnName.Trim());
                    break;
            }

            // change this code to get rid of the foreach loop
            ArrayList selectionDataList = new ArrayList();
            if (reportDataStruct.ValueDataType.Equals(typeof(bool)))
            {
                foreach (var item in qryResult)
                {
                    if (item.Equals(true))
                    { selectionDataList.Add("Yes"); }
                    else
                    { selectionDataList.Add("No"); }
                }
            }
            else
            {
                foreach (var item in qryResult)
                {
                    if (!string.IsNullOrEmpty(item.ToString()))
                    { selectionDataList.Add(item.ToString()); }
                }
            }
            return selectionDataList;
        }

        public DataTable GetMaintenanceJobRequisitionDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, AutoGenerateInfo autoGenerateInfo)
        {

            var query = context.LgsMaintenanceJobRequisitionHeaders.Where("DocumentStatus= @0 AND DocumentId = @1", 1, autoGenerateInfo.DocumentID);

            // Insert Conditions to query
            foreach (Common.ReportConditionsDataStruct reportConditionsDataStruct in reportConditionsDataStructList)
            {
                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(string)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", reportConditionsDataStruct.ConditionFrom.Trim(), reportConditionsDataStruct.ConditionTo.Trim()); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(DateTime)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", DateTime.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), DateTime.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(decimal)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", decimal.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), decimal.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(long)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", long.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), long.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(bool)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " =  @0 OR " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " = @1", reportConditionsDataStruct.ConditionFrom.Trim().Equals("Yes") ? true : false, reportConditionsDataStruct.ConditionTo.Trim().Equals("Yes") ? true : false); }

                if (reportConditionsDataStruct.ReportDataStruct.ValueDataType.Equals(typeof(int)))
                { query = query.Where("" + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " >= @0 AND " + reportConditionsDataStruct.ReportDataStruct.DbColumnName.Trim() + " <= @1", int.Parse(reportConditionsDataStruct.ConditionFrom.Trim()), int.Parse(reportConditionsDataStruct.ConditionTo.Trim())); }
            }

            return (from h in query                               
                               select
                                   new
                                   {
                                       FieldString1 = h.DocumentNo,
                                       FieldString2 = DbFunctions.TruncateTime(h.DocumentDate),
                                       FieldString3 = DbFunctions.TruncateTime(h.ExpectedDate),
                                       FieldString4 = h.ReferenceDocumentNo,
                                       
                                   }).ToArray().ToDataTable();                                
        }

        public DataTable GetPendingMaintananceRequsitionDocuments() 
        {
            var sql = (from mjh in context.LgsMaintenanceJobRequisitionHeaders
                       join l in context.Locations on mjh.LocationID equals l.LocationID
                       where mjh.ExpiryDate >= DateTime.Now
                       && mjh.IsAssigned == false
                       select new
                       {
                           mjh.DocumentNo,
                           mjh.DocumentDate,
                           mjh.RequestedBy,
                           l.LocationName
                       }).ToArray();

            return sql.ToDataTable();
        }

        #endregion
    }
}
