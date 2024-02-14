using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Data;
using Utility;

namespace Service
{
    public class HelperService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods
        public DataTable GetAllHelperDataTable(List<Common.ReportConditionsDataStruct> reportConditionsDataStructList, List<Common.ReportDataStruct> reportDataStructList, List<Common.ReportDataStruct> reportOrderByDataStructList)
        {
            StringBuilder querrySelect = new StringBuilder();
            var query = context.Helpers.Where("IsDelete=false");

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
                                   FieldString1 = lc.HelperCode,
                                   FieldString2 = lc.HelperName,
                                   FieldString3 = lc.Address1 + lc.Address2+ lc.Address3,
                                   FieldString4 = lc.Telephone,
                                   FieldString5 = lc.ReferenceNo,
                                   FieldString6 = EntityFunctions.TruncateTime(lc.CreatedDate),
                                  // FieldString7 = "",
                                  // FieldString8 = "",
                                  // FieldString9 = "",
                                  // FieldString10 = "",
                                  // FieldString11 = "",
                                   //FieldString12 = "",
                                   //FieldString13 = "",
                                  // FieldString14 = ""
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

        public DataTable GetAllHelperDataTable()
        {
            var query = (from lc in context.Helpers
                         select new
                         {
                             FieldString1 = lc.HelperCode,
                             FieldString2 = lc.HelperName,
                             FieldString3 = lc.Address,
                             FieldString4 = lc.Telephone,
                             FieldString5 = lc.ReferenceNo,
                             FieldString6 = EntityFunctions.TruncateTime(lc.CreatedDate),
                             FieldString7 = "",
                             FieldString8 = "",
                             FieldString9 = "",
                             FieldString10 = "",
                             FieldString11 = "",
                             FieldString12 = "",
                             FieldString13 = "",
                             FieldString14 = ""
                         }).ToArray();

            return query.AsEnumerable().ToDataTable();
        }

        public ArrayList GetSelectionData(Common.ReportDataStruct reportDataStruct)
        {
            IQueryable qryResult = context.Helpers
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
