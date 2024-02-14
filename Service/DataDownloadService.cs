using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data;
using System.Data;
using Utility;
using EntityFramework.Extensions;
using System.Transactions;
using System.Data.Common;
using System.Data.Entity;
using MoreLinq;
using System.Collections;

namespace Service
{
    public class DataDownloadService
    {
        public bool Save(int downloadModeID, int locationID)
        {
            using (TransactionScope transaction = new TransactionScope())
            {               

                var parameter = new DbParameter[] 
                    { 
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@DownloadModeID", Value=downloadModeID},
                        new System.Data.SqlClient.SqlParameter { ParameterName ="@LocationID", Value=locationID}                     
                    };

                if (CommonService.ExecuteStoredProcedure("spDataDownloadProcess", parameter))
                {
                    transaction.Complete();
                    return true;
                }
                else
                { return false; }

            }
        }
    }
}
