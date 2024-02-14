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
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlClient;
using MoreLinq;
using System.Collections;
using System.Data.Entity;

namespace Service
{
    public class InvScannerService
    {
        ERPDbContext context = new ERPDbContext();

        public List<InvScanner> GetAllScanners()
        {
             return context.InvScanner.Where(l => l.IsActive.Equals(true)).OrderBy(l => l.InvScannerID).ToList();
        }
    }
}
