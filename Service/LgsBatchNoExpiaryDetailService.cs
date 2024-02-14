using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using Domain;
using Data;
using System.Data;
using Utility;
using System.Data.Entity;

namespace Service
{
    public class LgsBatchNoExpiaryDetailService
    {
        ERPDbContext context = new ERPDbContext();
        InvProductMaster invProductMaster = new InvProductMaster();

        public LgsProductBatchNoExpiaryDetail GetBatchNoExpiaryDetailByBarcode(long barcode)
        {
            return context.LgsProductBatchNoExpiaryDetails.Where(bd => bd.BarCode.Equals(barcode) && bd.IsDelete == false).FirstOrDefault();
        }
    }
}
