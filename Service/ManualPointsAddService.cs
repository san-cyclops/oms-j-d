using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MoreLinq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.Extensions;
using System.Reflection;
using Domain;
using Data;
using System.Data;
using Utility;
using System.Collections;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Configuration;

namespace Service
{
    public class ManualPointsAddService
    {
        public bool AddManualPoints(LoyaltyCustomer loyaltyCustomer, string recept, decimal amount, decimal points, int locationID, int unitNo, Employee employee, DateTime billDate, DateTime addedDate) 
        {
            var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CustomerID", Value=loyaltyCustomer.LoyaltyCustomerID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CustomerType", Value=loyaltyCustomer.LoyaltyType},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Receipt", Value=recept},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Amount", Value=amount},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Points", Value=points},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=locationID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@UnitNo", Value=unitNo},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CashierID", Value=employee.EmployeeID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DiscPer", Value=0},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DiscAmt", Value=0},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@PointsRate", Value=2},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@Zno", Value=0},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CardNo", Value=loyaltyCustomer.CustomerCode},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@CardType", Value=loyaltyCustomer.CardMasterID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ReDeemPoints", Value=0},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@ReDeemPointsRate", Value=0},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LoyaltyType", Value=loyaltyCustomer.LoyaltyType},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@IsGuidClaimed", Value=0},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DocumentDate", Value=billDate},
                    };
            if (CommonService.ExecuteStoredProcedure("spUpdateLoyaltyDetailsManual", parameter))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
