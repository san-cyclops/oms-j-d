using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;

using Domain;
using Data;
using System.Data;
using Utility;

namespace Service
{
    /// <summary>
    /// TransferType Service
    /// Developed By - asanka    
    /// </summary>
    /// 

    public class LgsTransferTypeService
    {
        ERPDbContext context = new ERPDbContext();

        public LgsTransferType GetTransferTypesByName(string lgsTransferType)
        {
            return context.LgsTransferTypes.Where(d => d.TransferType == lgsTransferType && d.IsDelete.Equals(false)).FirstOrDefault();
        }

        public LgsTransferType GetTransferTypesByID(long lgsTransferTypeID)
        {
            return context.LgsTransferTypes.Where(d => d.LgsTransferTypeID == lgsTransferTypeID && d.IsDelete.Equals(false)).FirstOrDefault(); 
        }

        public List<LgsTransferType> GetAllTransferTypes()
        {
            return context.LgsTransferTypes.Where(l => l.IsDelete.Equals(false)).OrderBy(l => l.LgsTransferTypeID).ToList();
        }
    }
}
