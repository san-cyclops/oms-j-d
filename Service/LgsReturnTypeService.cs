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
    /// Developed by Nuwan
    /// </summary>
    public class LgsReturnTypeService
    {
        ERPDbContext context = new ERPDbContext();

        public LgsReturnType GetReturnTypesByName(string lgsReturnType)
        {
            return context.LgsReturnTypes.Where(d => d.ReturnType == lgsReturnType && d.IsDelete.Equals(false)).FirstOrDefault();
        }

        public LgsReturnType GetLgsReturnTypesByID(long lgsReturnTypeID)
        {
            return context.LgsReturnTypes.Where(d => d.LgsReturnTypeID == lgsReturnTypeID && d.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<LgsReturnType> GetAllLgsReturnTypes(int documentID) 
        {
            return context.LgsReturnTypes.Where(l => l.IsDelete.Equals(false) && l.RelatedFormID.Equals(documentID)).OrderBy(l => l.LgsReturnTypeID).ToList();
        }
    }
}
