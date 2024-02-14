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
using Utility;

namespace Service
{
    public class LoyaltyCardIssueService
    {
        ERPDbContext context = new ERPDbContext();
        LoyaltyCardIssueHeader loyaltyCardIssueHeader = new LoyaltyCardIssueHeader();
        LoyaltyCardIssueDetail loyaltyCardIssueDetail = new LoyaltyCardIssueDetail();


        public bool Save(LoyaltyCardIssueHeader loyaltyCardIssueHeader, List<LoyaltyCardGenerationDetail> loyaltyCardGenerationDetailList)
        {
            using (TransactionScope transaction = new TransactionScope())
            {

                if (loyaltyCardIssueHeader.LoyaltyCardIssueHeaderID.Equals(0))
                {                   
                    
                    context.LoyaltyCardIssueHeaders.Add(AddloyaltyCardIssueDetails(loyaltyCardIssueHeader, loyaltyCardGenerationDetailList));
                    context.SaveChanges();
                    loyaltyCardIssueHeader.CardIssueHeaderID = loyaltyCardIssueHeader.LoyaltyCardIssueHeaderID;
                    context.Entry(loyaltyCardIssueHeader).State = EntityState.Modified;
                    context.SaveChanges();

                    
                    foreach (LoyaltyCardIssueDetail loyaltyCardIssueDetail in loyaltyCardIssueHeader.LoyaltyCardIssueDetails)
                    {
                        loyaltyCardIssueDetail.CardIssueDetailID = loyaltyCardIssueDetail.LoyaltyCardIssueDetailID;
                    }
                    context.Entry(loyaltyCardIssueHeader).State = EntityState.Modified;
                    context.SaveChanges();

                    UpdateLoyaltyCardGenerationDetail(loyaltyCardGenerationDetailList);
                }
                else
                { context.Entry(loyaltyCardIssueHeader).State = EntityState.Modified; }

                context.SaveChanges();

                transaction.Complete();
                return true;
            }
        }

        private LoyaltyCardIssueHeader AddloyaltyCardIssueDetails(LoyaltyCardIssueHeader loyaltyCardIssueHeader, List<LoyaltyCardGenerationDetail> loyaltyCardGenerationDetailList)
        {
            loyaltyCardIssueHeader.LoyaltyCardIssueDetails = new List<LoyaltyCardIssueDetail>();
            foreach (LoyaltyCardGenerationDetail loyaltyCardGenerationDetail in loyaltyCardGenerationDetailList)
            {                
                LoyaltyCardIssueDetail loyaltyCardIssueDetail = new LoyaltyCardIssueDetail();
                loyaltyCardIssueDetail.IssueDate = loyaltyCardIssueHeader.IssueDate;
                loyaltyCardIssueDetail.IsDelete = false;
                loyaltyCardIssueDetail.LoyaltyCardIssueHeaderID = loyaltyCardIssueHeader.LoyaltyCardIssueHeaderID;
                loyaltyCardIssueDetail.LoyaltyCardGenerationDetailID = loyaltyCardGenerationDetail.LoyaltyCardGenerationDetailID;                

                loyaltyCardIssueHeader.LoyaltyCardIssueDetails.Add(loyaltyCardIssueDetail);
            }
            return loyaltyCardIssueHeader;
        }

        private void UpdateLoyaltyCardGenerationDetail(List<LoyaltyCardGenerationDetail> loyaltyCardGenerationDetailList)
        {
            foreach (LoyaltyCardGenerationDetail loyaltyCardGenerationDetail in loyaltyCardGenerationDetailList)
            {
                LoyaltyCardGenerationDetail loyaltyCardGenerationDetail2 = new LoyaltyCardGenerationDetail();
                loyaltyCardGenerationDetail2 = context.LoyaltyCardGenerationDetails.Where(cd => cd.LoyaltyCardGenerationDetailID == loyaltyCardGenerationDetail.LoyaltyCardGenerationDetailID && cd.IsDelete.Equals(false) && cd.IsActive.Equals(true)).FirstOrDefault();

                loyaltyCardGenerationDetail.IsIssued = true;
                context.Entry(loyaltyCardGenerationDetail2).CurrentValues.SetValues(loyaltyCardGenerationDetail);
                context.SaveChanges();                
            }
        }

        public void UpdateIssueHeader(LoyaltyCardIssueHeader loyaltyCardIssueHeader)
        {
            try
            {
                loyaltyCardIssueHeader.ModifiedUser = Common.LoggedUser;
                loyaltyCardIssueHeader.ModifiedDate = Common.GetSystemDateWithTime();
                this.context.Entry(loyaltyCardIssueHeader).State = EntityState.Modified;
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        public LoyaltyCardIssueHeader GetIssueHeaderByDocNo(string DocNo)
        {

            return context.LoyaltyCardIssueHeaders.Where(c => c.DocumentNo == DocNo && c.IsDelete.Equals(false)).FirstOrDefault();
        }

        public LoyaltyCardIssueHeader getIssueHeaderByDocNo(int documentID, string documentNo, int locationID)
        {
            return context.LoyaltyCardIssueHeaders.Where(ci => ci.DocumentNo.Equals(documentNo) && ci.ToLocationId.Equals(locationID)).FirstOrDefault();
        }

        public string[] GetAllDocNos()
        {
            List<string> LoyaltyCardIssueHeaderList = context.LoyaltyCardIssueHeaders.Where(c => c.IsDelete.Equals(false)).Select(c => c.DocumentNo).ToList();
            return LoyaltyCardIssueHeaderList.ToArray();
        }

        #region GetDocumentNo

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
    }
}
