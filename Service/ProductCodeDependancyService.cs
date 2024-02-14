using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Data;

namespace Service
{
    public class ProductCodeDependancyService
    {
        ERPDbContext context = new ERPDbContext();

        public ProductCodeDependancy GetProductCodeDependancyByForm(string formName) 
        {
            return context.ProductCodeDependancies.Where(pds => pds.FormName.Equals(formName)).FirstOrDefault();
        }


    }
}
