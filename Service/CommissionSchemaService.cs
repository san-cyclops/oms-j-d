using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;
using Data;
using System.Data.Entity.Validation;

namespace Service
{
    public class CommissionSchemaService
    {
        ERPDbContext context = new ERPDbContext();

        public string[] GetAllCommissionSchemas()
        {
            List<string> CommissionSchemaList = context.CommissionSchemas.Where(c =>c.IsDelete.Equals(false)).Select(c => c.CommissionSchemaCode).ToList();
            return CommissionSchemaList.ToArray();

        }

        public string[] GetAllCommissionSchemaNames()
        {
            List<string> CommissionSchemaList = context.CommissionSchemas.Where(c => c.IsDelete.Equals(false)).Select(c => c.CommissionSchemaName).ToList();
            return CommissionSchemaList.ToArray();

        }

        public CommissionSchema GetCommissionSchemaByCode(string CommissionSchemaCode)
        {
            return context.CommissionSchemas.Where(c => c.CommissionSchemaCode.Equals(CommissionSchemaCode)).FirstOrDefault();
        }

        public CommissionSchema GetCommissionSchemaByID(int CommissionSchemaID)  
        {
            return context.CommissionSchemas.Where(c => c.CommissionSchemaID.Equals(CommissionSchemaID)).FirstOrDefault();
        }

       
    }
}
