using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EntityFramework.Extensions;
using Data;
using Domain;
using System.Data.Entity;

namespace Service
{
    public class CustomerFeedBackService
    {


        ERPDbContext context = new ERPDbContext();
        CustomerFeedBack customerFeedBack = new CustomerFeedBack();

        #region methods

        public bool Save(CustomerFeedBack customerFeedBack)
        {
            using (TransactionScope transaction = new TransactionScope())
            {

                if (customerFeedBack.CustomerFeedBackID.Equals(0))
                    context.CustomerFeedBacks.Add(customerFeedBack);
                else
                    context.Entry(customerFeedBack).State = EntityState.Modified;

                context.SaveChanges();

                transaction.Complete();
                return true;


            }




        }

        public CustomerFeedBack GetCustomerFeedBacksByDocNo(string DocNo)
        {
            return context.CustomerFeedBacks.Where(c => c.DocumentNo == DocNo && c.IsDelete.Equals(false)).FirstOrDefault();
        }
 
        public List<CustomerFeedBack> GetAllCustomerFeedBacks()
        {
            return context.CustomerFeedBacks.Where(c => c.IsDelete == false).ToList();
        }


        public CustomerFeedBack GetCustomerFeedBacksById(long CustomerFeedBackId)
        {

            return context.CustomerFeedBacks.Where(c => c.CustomerFeedBackID == CustomerFeedBackId && c.IsDelete.Equals(false)).FirstOrDefault();

        }

        public CustomerFeedBack getCustomerFeedBackByDocumentNo(int documentID, string documentNo, int locationID)
        {
            return context.CustomerFeedBacks.Where(ph => ph.DocumentNo.Equals(documentID) && ph.DocumentNo.Equals(documentNo) && ph.LocationID.Equals(locationID)).FirstOrDefault();
        }

        #region GetNewCode

        public string GetDocumentNo(string formName, int locationID, string locationCode, int documentId, bool isTemporytNo)
        {
            AutoGenerateInfo autoGenerateInfo;
            autoGenerateInfo = AutoGenerateInfoService.GetAutoGenerateInfoByForm(formName);
            string getNewCode = string.Empty;
            long tempCode = 0;

            string preFix = string.Empty;

            bool isLocationCode = autoGenerateInfo.IsLocationCode;
            int codeLength = autoGenerateInfo.CodeLength;
            preFix = autoGenerateInfo.Prefix.Trim();

            if (isTemporytNo)
            {

                if ((preFix.StartsWith("T")))
                    preFix = "X";
                else
                    preFix = "T";

            }

            if (isLocationCode)
                preFix = preFix + locationCode.Trim();


            if (isTemporytNo)
            {
                context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber { TempDocumentNo = d.TempDocumentNo + 1 });

                tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.TempDocumentNo);
            }
            else
            {
                context.DocumentNumbers.Update(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId), d => new DocumentNumber { DocumentNo = d.DocumentNo + 1 });

                tempCode = context.DocumentNumbers.Where(d => d.LocationID.Equals(locationID) && d.DocumentID.Equals(documentId)).Max(d => d.DocumentNo);
            }

            getNewCode = (tempCode).ToString();
            getNewCode = preFix + new StringBuilder().Insert(0, "0", (codeLength - (preFix.Length + getNewCode.Length))) + getNewCode;
            return getNewCode.ToUpper();
        }

        #endregion
        #endregion

    }
}
