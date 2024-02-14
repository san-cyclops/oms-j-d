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
    public class InvReturnTypeService
    {
        ERPDbContext context = new ERPDbContext();

        public InvReturnType GetReturnTypesByName(string InvReturnType) 
        {
            return context.InvReturnTypes.Where(d => d.ReturnType == InvReturnType && d.IsDelete.Equals(false)).FirstOrDefault();
        }

        public InvReturnType GetInvReturnTypesByID(long InvReturnTypeID)
        {
            return context.InvReturnTypes.Where(d => d.InvReturnTypeID == InvReturnTypeID && d.IsDelete.Equals(false)).FirstOrDefault();
        }

        public List<InvReturnType> GetAllInvReturnTypes(int documentID) 
        {
            return context.InvReturnTypes.Where(l => l.IsDelete.Equals(false) && l.RelatedFormID.Equals(documentID)).OrderBy(l => l.InvReturnTypeID).ToList();
        }
    }
}
