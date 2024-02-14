using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;
using Data;
using System.Data;

namespace Service
{
    public class BaseFormService
    {
        ERPDbContext context = new ERPDbContext();

        public List<AutoGenerateInfo> GetAutoGenerateInfo() 
        {
            return context.AutoGenerateInfos.ToList();
        }
    }
}
