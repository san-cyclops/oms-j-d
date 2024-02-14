using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

using System.Drawing;
using System.Text;
using System.Reflection;
using System.Linq;
using Domain;
using Utility;
using Service;
using System.Collections;

namespace Service
{
    public class ReportService
    {
        public bool ExecuteReportStoredProcedure(string productCodeFrom, string productCodeTo, string report, string reportType
            , bool isAmt, bool isQty
            , DataTable dtlocations, DataTable dtDep, DataTable dtCat, DataTable dtSubCat, DataTable dtSubCat2,DataTable dtSupplier, DataTable dtProductExtendedProperties
            , string displayType, DateTime dateFrom, DateTime dateTo)
        {
            var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ProductCodeFrom", Value= productCodeFrom},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ProductCodeTo", Value= productCodeTo},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CompanyId", Value=Common.LoggedCompanyID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=Common.LoggedLocationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@UserId", Value=Common.LoggedUserId},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Report", Value=report},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ReportType", Value=reportType},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Locations", Value=dtlocations},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Dep", Value=dtDep},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Cat", Value=dtCat},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@SubCat", Value=dtSubCat},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@SubCat2", Value=dtSubCat2},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Sup", Value=dtSupplier},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ExtendedProperties", Value=dtProductExtendedProperties},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Amt", Value=isAmt},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Qty", Value=isQty},
                        //new System.Data.SqlClient.SqlParameter { ParameterName ="@CVal", Value=isCVal},
                        //new System.Data.SqlClient.SqlParameter { ParameterName ="@SVal", Value=isSVal},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DisplayType", Value=displayType},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DateFrom", Value=dateFrom},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DateTo", Value=dateTo}
                    };

            if (CommonService.ExecuteStoredProcedure("spReportGen", parameter))
            { return true; }
            else
            { return false; }
        }

        public bool DeleteTempData()
        {
            if (CommonService.ExecuteSqlstatement("DELETE FROM InvTmpReportDetail WHERE UserId =" + Common.LoggedUserId.ToString().Trim() + ""))
            {
                return true;
            }
            else
            { return false; }
        }
        
    }
}
