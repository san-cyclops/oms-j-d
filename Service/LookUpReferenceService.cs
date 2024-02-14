using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain;
using System.Data;
using MoreLinq;
using EntityFramework.Extensions;

namespace Service
{
    public class LookUpReferenceService
    {
        ERPDbContext context = new ERPDbContext();

        #region Methods

        /// <summary>
        /// Search LookUpReference by value
        /// </summary>
        /// <param name="enumString"></param>
        /// <param name="lookupReferenceValue"></param>
        /// <returns></returns>
        public ReferenceType GetLookUpReferenceByValue(string enumString, string lookupReferenceValue)
        {
            return context.ReferenceTypes.Where(r => r.LookupType == enumString && r.LookupValue == lookupReferenceValue && r.IsDelete == false).FirstOrDefault();
        }
        
        /// <summary>
        /// Search LookUpReference by Key
        /// </summary>
        /// <param name="enumString"></param>
        /// <param name="lookupReferenceKey"></param>
        /// <returns></returns>
        public ReferenceType GetLookUpReferenceByKey(string enumString, int lookupReferenceKey)
        {
            return context.ReferenceTypes.Where(r => r.LookupType == enumString && r.LookupKey == lookupReferenceKey && r.IsDelete == false).FirstOrDefault();
        }

        /// <summary>
        /// Get LookUpReference Values
        /// </summary>
        /// <returns></returns>
        public string[] GetLookUpReferenceValues(string enumString)
        {
            List<string> lookUpReferenceValuesList = context.ReferenceTypes.Where(r => r.IsDelete.Equals(false) && r.LookupType == enumString).Select(r => r.LookupValue).ToList();
            string[] lookUpReferenceValues = lookUpReferenceValuesList.ToArray();
            return lookUpReferenceValues;
        }

        public void UpdateLookUpReference(int Edatatransfer, int datatransfer)
        {
            context.ReferenceTypes.Update(x => x.DataTransfer.Equals(Edatatransfer), x => new ReferenceType { DataTransfer = datatransfer });
        }

        /// <summary>
        /// Get LookUpReference Types
        /// </summary>
        /// <returns></returns>
        public int[] GetLookUpReferenceTypes(string enumString)
        {
            List<int> lookUpReferenceTypesList = context.ReferenceTypes.Where(r => r.IsDelete.Equals(false) && r.LookupType == enumString).Select(r => r.LookupKey).ToList();
            int[] lookUpReferenceTypes = lookUpReferenceTypesList.ToArray();
            return lookUpReferenceTypes;
        }

        public DataTable GetLookUpReferenceValuesDataTable()
        {
            var vare = from e in context.ReferenceTypes
                         where e.DataTransfer.Equals(1)
                         select new
                         {
                             e.ReferenceTypeID,
                             e.LookupType,
                             e.LookupKey,
                             e.LookupValue,
                             e.Remark,
                             e.IsDelete,
                             e.GroupOfCompanyID,
                             e.CreatedUser,
                             e.CreatedDate,
                             e.ModifiedUser,
                             e.ModifiedDate,
                             e.DataTransfer
                         };
            return vare.ToDataTable();
        }

        public List<ReferenceType> LoadLoyltyTypes(string lookUpType)   
        {
            return context.ReferenceTypes.Where(r => r.IsDelete.Equals(false) && r.LookupType == lookUpType).OrderBy(t => t.LookupKey).ToList();
        }
        
        #endregion
    }
}
