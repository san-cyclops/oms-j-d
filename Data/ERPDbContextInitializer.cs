using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.IO;

namespace Data
{
    internal class ERPDbContextInitializer : DropCreateDatabaseIfModelChanges<ERPDbContext>
    {
        #region Methods
        protected override void Seed(ERPDbContext context)
        {
            AddGroupOfCompany(context);
            AddLookupReferenceTypes(context);
            //AddTax(context);


            AddCostCentres(context);
            AddUnitOfMeasures(context);
            AddCommissionSchemas(context);
            AddAutoGenerateInfo(context);
            AddProductCodeDependancy(context);



            ////General Ledger
            //AddLedgerAccounts(context);

            //
            //AddCompany(context);
            //AddLocation(context);

            
            //AddPaymentMethods(context);
            //AddSupplierGroups(context);

            ////Inventry
            //AddDepartments(context);
            //AddCategories(context);
            //AddSubCategories(context);
            //AddSubCategories2(context);
            //AddCustomerGroups(context);

            ////Logistic
            //AddLgsDepartments(context);
            //AddLgsCategories(context);
            //AddLgsSubCategories(context);
            //AddLgsSubCategories2(context);

            //AddAreas(context);
            //AddBrokers(context);
            //AddTerritorys(context);

            ////AddSuppliers(context);
            
           

            //AddGiftVoucherGroup(context);
            //AddGiftVoucherBook(context);
        }

        private static void AddLookupReferenceTypes(ERPDbContext context)
        {
            // Add Reference Type
            var referenceTypes = new List<ReferenceType>
                {
                    //Gender Type                    
                    new ReferenceType { LookupType = ((int)LookUpReference.GenderType).ToString(), LookupKey = 1, LookupValue = "Male", Remark =  (LookUpReference.GenderType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.GenderType).ToString(), LookupKey = 2, LookupValue = "Female", Remark =  (LookUpReference.GenderType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.GenderType).ToString(), LookupKey = 3, LookupValue = "Other", Remark =  (LookUpReference.GenderType).ToString()},
                    //Title Type
                    new ReferenceType { LookupType = ((int)LookUpReference.TitleType).ToString(), LookupKey = 1, LookupValue = "Mr.", Remark =  (LookUpReference.TitleType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.TitleType).ToString(), LookupKey = 2, LookupValue = "Mrs.", Remark =  (LookUpReference.TitleType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.TitleType).ToString(), LookupKey = 3, LookupValue = "Miss.", Remark =  (LookUpReference.TitleType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.TitleType).ToString(), LookupKey = 4, LookupValue = "Dr.", Remark =  (LookUpReference.TitleType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.TitleType).ToString(), LookupKey = 5, LookupValue = "Prof.", Remark =  (LookUpReference.TitleType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.TitleType).ToString(), LookupKey = 6, LookupValue = "Ven.", Remark =  (LookUpReference.TitleType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.TitleType).ToString(), LookupKey = 7, LookupValue = "Company", Remark =  (LookUpReference.TitleType).ToString()},
                    //Supplier Type
                    new ReferenceType { LookupType = ((int)LookUpReference.SupplierType).ToString(), LookupKey = 1, LookupValue = "Trade", Remark =  (LookUpReference.SupplierType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.SupplierType).ToString(), LookupKey = 2, LookupValue = "Non Trade", Remark =  (LookUpReference.SupplierType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.SupplierType).ToString(), LookupKey = 3, LookupValue = "Expense", Remark =  (LookUpReference.SupplierType).ToString()},
                    //Customer Type
                    new ReferenceType { LookupType = ((int)LookUpReference.CustomerType).ToString(), LookupKey = 1, LookupValue = "Trade", Remark =  (LookUpReference.CustomerType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.CustomerType).ToString(), LookupKey = 2, LookupValue = "Local", Remark =  (LookUpReference.CustomerType).ToString()},
                    //FiscalType
                    new ReferenceType { LookupType = ((int)LookUpReference.FiscalType).ToString(), LookupKey = 1, LookupValue = "January", Remark = (LookUpReference.FiscalType).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.FiscalType).ToString(), LookupKey = 2, LookupValue = "April", Remark = (LookUpReference.FiscalType).ToString()},
                    //Nationality
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 1, LookupValue = "Sri Lankan", Remark =  (LookUpReference.Nationality).ToString()},
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 2, LookupValue = "Indian", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 3, LookupValue = "Moldovans", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 4, LookupValue = "Nepalese", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 5, LookupValue = "Kazakhstani", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 6, LookupValue = "Bangladeshi", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 7, LookupValue = "Afghani", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 8, LookupValue = "Chinese", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 9, LookupValue = "Japanese", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 10, LookupValue = "Mongolian", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 11, LookupValue = "North Korean", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 12, LookupValue = "South Korean", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 13, LookupValue = "Taiwanese", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 14, LookupValue = "Cambodian", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 15, LookupValue = "Indonesian", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 16, LookupValue = "Malaysian", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 17, LookupValue = "Singaporean", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 18, LookupValue = "	Australian", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 19, LookupValue = "American", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 20, LookupValue = "African", Remark = (LookUpReference.Nationality).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Nationality).ToString(), LookupKey = 21, LookupValue = "Other", Remark = (LookUpReference.Nationality).ToString() },

                    //Religion
                    new ReferenceType { LookupType = ((int)LookUpReference.Religion).ToString(), LookupKey = 1, LookupValue = "Buddhism", Remark = (LookUpReference.Religion).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Religion).ToString(), LookupKey = 2, LookupValue = "Hinduism", Remark = (LookUpReference.Religion).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Religion).ToString(), LookupKey = 3, LookupValue = "Christianity", Remark = (LookUpReference.Religion).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Religion).ToString(), LookupKey = 4, LookupValue = "Islam", Remark = (LookUpReference.Religion).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Religion).ToString(), LookupKey = 5, LookupValue = "Other", Remark = (LookUpReference.Religion).ToString() },
                    //District
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 1, LookupValue = "Ampara", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 2, LookupValue = "Anuradhapura", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 3, LookupValue = "Badulla", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 4, LookupValue = "Batticaloa", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 5, LookupValue = "Colombo", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 6, LookupValue = "Galle", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 7, LookupValue = "Gampaha", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 8, LookupValue = "Hambantota", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 9, LookupValue = "Jaffna", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 10, LookupValue = "Kalutara", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 11, LookupValue = "Kandy", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 12, LookupValue = "Kegalle", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 13, LookupValue = "Kilinochchi", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 14, LookupValue = "Kurunegala", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 15, LookupValue = "Mannar", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 16, LookupValue = "Matale", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 17, LookupValue = "Matara", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 18, LookupValue = "Moneragala", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 19, LookupValue = "Mullaitivu", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 20, LookupValue = "Nuwara Eliya", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 21, LookupValue = "Polonnaruwa", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 22, LookupValue = "Puttalam", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 23, LookupValue = "Ratnapura", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 24, LookupValue = "Trincomalee", Remark = (LookUpReference.District).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.District).ToString(), LookupKey = 25, LookupValue = "Vavuniya", Remark = (LookUpReference.District).ToString() },
                    //CivilStatus
                    new ReferenceType { LookupType = ((int)LookUpReference.CivilStatus).ToString(), LookupKey = 1, LookupValue = "Unmarried", Remark = (LookUpReference.CivilStatus).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.CivilStatus).ToString(), LookupKey = 2, LookupValue = "Married", Remark = (LookUpReference.CivilStatus).ToString() },
                    //TvChannel
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 1, LookupValue = "Independent Television Network (ITN)", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 2, LookupValue = "Sri Lanka Rupavahini Corporation", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 3, LookupValue = "TNL", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 4, LookupValue = "MTV SPORTS", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 5, LookupValue = "ETV", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 6, LookupValue = "Swarnavahini", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 7, LookupValue = "Sirasa TV ", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 8, LookupValue = "Shakthi TV ", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 9, LookupValue = "Channel Eye", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 10, LookupValue = "ART Television", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 11, LookupValue = "TV Derana", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 12, LookupValue = "MAX TV", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 13, LookupValue = "Nethra TV", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 14, LookupValue = "Vasantham TV", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 15, LookupValue = "NTV", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 16, LookupValue = "Carlton Sports Network (CSN)", Remark = (LookUpReference.TvChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TvChannel).ToString(), LookupKey = 17, LookupValue = "The Buddhist TV", Remark = (LookUpReference.TvChannel).ToString() },
                    //NewsPaper
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 1, LookupValue = "Divaina", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 2, LookupValue = "Lankadeepa", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 3, LookupValue = "Dinamina", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 4, LookupValue = "Divaina", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 5, LookupValue = "Rivira", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 6, LookupValue = "Lakbima", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 7, LookupValue = "Silumina", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 8, LookupValue = "Mawbima", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 9, LookupValue = "Randiwa", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 10, LookupValue = "Ravaya", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 11, LookupValue = "Sunday Times", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 12, LookupValue = "Sunday Observer", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 13, LookupValue = "Daily News", Remark = (LookUpReference.NewsPaper).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.NewsPaper).ToString(), LookupKey = 14, LookupValue = "Daily Mirror", Remark = (LookUpReference.NewsPaper).ToString() },
                    //RadioChannel
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 1, LookupValue = "Shree FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 2, LookupValue = "City FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 3, LookupValue = "Swadeshiya Sevaya", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 4, LookupValue = "Velanda Sevaya", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 5, LookupValue = "Kothmale FM ", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 6, LookupValue = "Singha FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 7, LookupValue = "Sirasa FM ", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 8, LookupValue = "Siyatha FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 9, LookupValue = "V FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 10, LookupValue = "Y FM ", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 11, LookupValue = "Rangiri Sri Lanka", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 12, LookupValue = "Isira TNL", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 13, LookupValue = "Bauddaya Guwan Viduliya(The Buddhist)", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 14, LookupValue = "Kirula FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 15, LookupValue = "Rajarata Sewaya", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 16, LookupValue = "Rajarata Sewaya", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 17, LookupValue = "VIP Radio ", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 18, LookupValue = "Kandurata Sewaya", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 19, LookupValue = "Shaa FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 20, LookupValue = "Seth FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 21, LookupValue = "Kandurata FM ", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 22, LookupValue = "Lakviru FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 23, LookupValue = "FM Derana", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 24, LookupValue = "Hiru FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 25, LookupValue = "Lak FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 26, LookupValue = "Lakhanda", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 27, LookupValue = "Neth FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.RadioChannel).ToString(), LookupKey = 28, LookupValue = "Ran FM", Remark = (LookUpReference.RadioChannel).ToString() },
                    //Magazine
                    new ReferenceType { LookupType = ((int)LookUpReference.Magazine).ToString(), LookupKey = 1, LookupValue = "ADZ", Remark = (LookUpReference.Magazine).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Magazine).ToString(), LookupKey = 2, LookupValue = "Z", Remark = (LookUpReference.Magazine).ToString() },
                    //DeliverTo
                    new ReferenceType { LookupType = ((int)LookUpReference.DeliverTo).ToString(), LookupKey = 1, LookupValue = "Home Address", Remark = (LookUpReference.DeliverTo).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.DeliverTo).ToString(), LookupKey = 2, LookupValue = "Office Address", Remark = (LookUpReference.DeliverTo).ToString() },
                    //Relationship
                    new ReferenceType { LookupType = ((int)LookUpReference.Relationship).ToString(), LookupKey = 1, LookupValue = "Wife", Remark = (LookUpReference.Relationship).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Relationship).ToString(), LookupKey = 2, LookupValue = "Sister", Remark = (LookUpReference.Relationship).ToString() },
                    //Satatus
                    new ReferenceType { LookupType = ((int)LookUpReference.Status).ToString(), LookupKey = 1, LookupValue = "True", Remark = (LookUpReference.Status).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Status).ToString(), LookupKey = 2, LookupValue = "False", Remark = (LookUpReference.Status).ToString() },
                    //Transaction Definition
                    new ReferenceType { LookupType = ((int)LookUpReference.TransactionDefinition).ToString(), LookupKey = 1, LookupValue = "Transaction", Remark = (LookUpReference.TransactionDefinition).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TransactionDefinition).ToString(), LookupKey = 2, LookupValue = "Discount", Remark = (LookUpReference.TransactionDefinition).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TransactionDefinition).ToString(), LookupKey = 3, LookupValue = "Expense", Remark = (LookUpReference.TransactionDefinition).ToString() },
                    //Costing Methods
                    new ReferenceType { LookupType = ((int)LookUpReference.CostingMethod).ToString(), LookupKey = 1, LookupValue = "First In First Out (FIFO)", Remark = (LookUpReference.CostingMethod).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.CostingMethod).ToString(), LookupKey = 2, LookupValue = "Last In First Out (LIFO)", Remark = (LookUpReference.CostingMethod).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.CostingMethod).ToString(), LookupKey = 3, LookupValue = "Average Cost", Remark = (LookUpReference.CostingMethod).ToString() },
                    //Module Type
                    new ReferenceType { LookupType = ((int)LookUpReference.ModuleType).ToString(), LookupKey = 1, LookupValue = "Inventory", Remark = (LookUpReference.ModuleType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.ModuleType).ToString(), LookupKey = 2, LookupValue = "Logistic", Remark = (LookUpReference.ModuleType).ToString() },
                    

                    //Stock Adjustment Mode
                    new ReferenceType { LookupType = ((int)LookUpReference.StockAdjustmentMode).ToString(), LookupKey = 1, LookupValue = "Add Stock", Remark = (LookUpReference.StockAdjustmentMode).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.StockAdjustmentMode).ToString(), LookupKey = 2, LookupValue = "Reduce Stock", Remark = (LookUpReference.StockAdjustmentMode).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.StockAdjustmentMode).ToString(), LookupKey = 3, LookupValue = "Overwrite Stock", Remark = (LookUpReference.StockAdjustmentMode).ToString() },
                    
                    //EntryDrCr
                    new ReferenceType { LookupType = ((int)LookUpReference.EntryDrCr).ToString(), LookupKey = 1, LookupValue = "Dr", Remark = (LookUpReference.EntryDrCr).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.EntryDrCr).ToString(), LookupKey = 2, LookupValue = "Cr", Remark = (LookUpReference.EntryDrCr).ToString() },
                    //SupplierType - Logistic
                    new ReferenceType { LookupType = ((int)LookUpReference.LogisticSupplierType).ToString(), LookupKey = 1, LookupValue = "Expense", Remark =  (LookUpReference.LogisticSupplierType).ToString()},

                    //EditablePaymentMethods
                    new ReferenceType { LookupType = ((int)LookUpReference.EditablePaymentTerm).ToString(), LookupKey = 1, LookupValue = "Credit", Remark = (LookUpReference.EditablePaymentTerm).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.EditablePaymentTerm).ToString(), LookupKey = 2, LookupValue = "Cheque", Remark = (LookUpReference.EditablePaymentTerm).ToString() },

                    //card type
                    new ReferenceType { LookupType = ((int)LookUpReference.LCardType).ToString(), LookupKey = 1, LookupValue = "Discount", Remark = (LookUpReference.LCardType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.LCardType).ToString(), LookupKey = 2, LookupValue = "Arapaima", Remark = (LookUpReference.LCardType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.LCardType).ToString(), LookupKey = 3, LookupValue = "Arrowana", Remark = (LookUpReference.LCardType).ToString() },

                    //Data Download Mode
                    new ReferenceType { LookupType = ((int)LookUpReference.DataDownloadMode).ToString(), LookupKey = 1, LookupValue = "Add Stock", Remark = (LookUpReference.DataDownloadMode).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.DataDownloadMode).ToString(), LookupKey = 2, LookupValue = "Reduce Stock", Remark = (LookUpReference.DataDownloadMode).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.DataDownloadMode).ToString(), LookupKey = 3, LookupValue = "Overwrite Stock", Remark = (LookUpReference.DataDownloadMode).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.DataDownloadMode).ToString(), LookupKey = 4, LookupValue = "Opening Stock", Remark = (LookUpReference.DataDownloadMode).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.DataDownloadMode).ToString(), LookupKey = 5, LookupValue = "Transfer Stock", Remark = (LookUpReference.DataDownloadMode).ToString() },

                    //Tag Type
                    //nolimit
                    new ReferenceType { LookupType = ((int)LookUpReference.TagType).ToString(), LookupKey = 1, LookupValue = "2X1 Sticker", Remark = (LookUpReference.TagType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TagType).ToString(), LookupKey = 2, LookupValue = "3Sticker", Remark = (LookUpReference.TagType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TagType).ToString(), LookupKey = 3, LookupValue = "GlitzTAG A4 new", Remark = (LookUpReference.TagType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TagType).ToString(), LookupKey = 4, LookupValue = "NolimitTAG A4 new", Remark = (LookUpReference.TagType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TagType).ToString(), LookupKey = 5, LookupValue = "Price Removable TAG", Remark = (LookUpReference.TagType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TagType).ToString(), LookupKey = 6, LookupValue = "Zebra 4 Cross TAG", Remark = (LookUpReference.TagType).ToString() },

                    new ReferenceType { LookupType = ((int)LookUpReference.TagType).ToString(), LookupKey = 7, LookupValue = "Special Offer TAG", Remark = (LookUpReference.TagType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TagType).ToString(), LookupKey = 8, LookupValue = "Discount TAG", Remark = (LookUpReference.TagType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TagType).ToString(), LookupKey = 9, LookupValue = "Watch TAG", Remark = (LookUpReference.TagType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.TagType).ToString(), LookupKey = 10, LookupValue = "Old Price New Price TAG", Remark = (LookUpReference.TagType).ToString() },

                    new ReferenceType { LookupType = ((int)LookUpReference.TagType).ToString(), LookupKey = 11, LookupValue = "Zebra 110 2X1 Sticker", Remark = (LookUpReference.TagType).ToString() },
                    
                    //Loylty Type
                    new ReferenceType { LookupType = ((int)LookUpReference.LoyaltyType).ToString(), LookupKey = 2, LookupValue = "Arapaima", Remark = (LookUpReference.LoyaltyType).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.LoyaltyType).ToString(), LookupKey = 3, LookupValue = "Arrowana", Remark = (LookUpReference.LoyaltyType).ToString() },

                    //Race
                    new ReferenceType { LookupType = ((int)LookUpReference.Race).ToString(), LookupKey = 1, LookupValue = "Sinhalese", Remark = (LookUpReference.Race).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Race).ToString(), LookupKey = 2, LookupValue = "Tamils", Remark = (LookUpReference.Race).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Race).ToString(), LookupKey = 3, LookupValue = "Moors", Remark = (LookUpReference.Race).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Race).ToString(), LookupKey = 4, LookupValue = "Sri Lankan Malays", Remark = (LookUpReference.Race).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Race).ToString(), LookupKey = 5, LookupValue = "Burghers", Remark = (LookUpReference.Race).ToString() },
                    new ReferenceType { LookupType = ((int)LookUpReference.Race).ToString(), LookupKey = 6, LookupValue = "Other", Remark = (LookUpReference.Race).ToString() },
                    
                };

            referenceTypes.ForEach(r => context.ReferenceTypes.Add(r));
            context.SaveChanges();
        }

        private static void AddTax(ERPDbContext context)
        {
            // Add Tax
            var taxes = new List<Tax>
                {
                    new Tax { TaxCode = "VAT", TaxName = "VAT", EffectivePercentage = (decimal)12.00 , TaxPercentage =(decimal)12.00 },
                    new Tax { TaxCode = "NBT", TaxName = "NBT", EffectivePercentage = (decimal)2.00, TaxPercentage =(decimal)2.00},
                    new Tax { TaxCode = "NBT1", TaxName = "NBT1", EffectivePercentage = (decimal)2.04, TaxPercentage =(decimal)2.04},
                };

            taxes.ForEach(u => context.Taxes.Add(u));
            context.SaveChanges();
        }

        //private static void AddTransactionRights(ERPDbContext context)
        //{
        //    // Add Tax
        //    var transactionRights = new List<TransactionRights>
        //        {
                    
        //            new TransactionRights { DocumentID = , TransactionCode = "VAT", TransactionName = "", IsAccess = true, IsPause = true, IsSave = true, IsModify = true, IsDelete = false},
        //            new TransactionRights { DocumentID = , TransactionCode = "VAT", TransactionName = "", IsAccess = true, IsPause = true, IsSave = true, IsModify = true, IsDelete = false},
        //            new TransactionRights { DocumentID = , TransactionCode = "VAT", TransactionName = "", IsAccess = true, IsPause = true, IsSave = true, IsModify = true, IsDelete = false},
        //            new TransactionRights { DocumentID = , TransactionCode = "VAT", TransactionName = "", IsAccess = true, IsPause = true, IsSave = true, IsModify = true, IsDelete = false},
        //            new TransactionRights { DocumentID = , TransactionCode = "VAT", TransactionName = "", IsAccess = true, IsPause = true, IsSave = true, IsModify = true, IsDelete = false},
        //            new TransactionRights { DocumentID = , TransactionCode = "VAT", TransactionName = "", IsAccess = true, IsPause = true, IsSave = true, IsModify = true, IsDelete = false},
                   
        //        };

        //    transactionRights.ForEach(u => context.Taxes.Add(u));
        //    context.SaveChanges();
        //}

        private static void AddUnitOfMeasures(ERPDbContext context)
        {
            // Add Unit of Measure
            var unitOfMeasures = new List<UnitOfMeasure>
                {
                    new UnitOfMeasure { UnitOfMeasureCode = "001", UnitOfMeasureName = "Nos", Remark = "No of Units"},
                };
            
            unitOfMeasures.ForEach(u => context.UnitOfMeasures.Add(u));
            context.SaveChanges();
        }

        // Add Payment Methods
        private static void AddPaymentMethods(ERPDbContext context)
        {
            var paymentMethods = new List<PaymentMethod>
                {
                    new PaymentMethod { PaymentMethodCode = "001", PaymentMethodName = "Cash", CommissionRate = 0, IsDelete = false, GroupOfCompanyID = 0},
                    new PaymentMethod { PaymentMethodCode = "002", PaymentMethodName = "Credit", CommissionRate = 0, IsDelete = false, GroupOfCompanyID = 0},
                    new PaymentMethod { PaymentMethodCode = "003", PaymentMethodName = "Cheque", CommissionRate = 0, IsDelete = false, GroupOfCompanyID = 0},
                };

            paymentMethods.ForEach(u => context.PaymentMethods.Add(u));
            context.SaveChanges();
        }

        private static void AddSupplierGroups(ERPDbContext context)
        {
            // Add Supplier Groups
            var supplierGroups = new List<SupplierGroup>
                {
                    new SupplierGroup { SupplierGroupCode = "001", SupplierGroupName = "Group 01"},
                };

            supplierGroups.ForEach(g => context.SupplierGroups.Add(g));
            context.SaveChanges();
        }

        private void AddCommissionSchemas(ERPDbContext context)
        {
            var commissionSchemas = new List<CommissionSchema>
            {
                new CommissionSchema {CommissionSchemaCode="001",CommissionSchemaName="Schema Name 001"},
            };
            commissionSchemas.ForEach(c => context.CommissionSchemas.Add(c));
            context.SaveChanges();
        }

        private void AddCostCentres(ERPDbContext context)
        {
            var costCentres = new List<CostCentre>
            {
                new CostCentre {CostCentreCode="001",CostCentreName="Cost Centre 001"},
            };
            costCentres.ForEach(c => context.CostCentres.Add(c));
            context.SaveChanges();
        }

        private static void AddAutoGenerateInfo(ERPDbContext context)
        {
            // Add Sub AutoGenerateInfo
            var autoGenerateInfo = new List<AutoGenerateInfo>
                {
                    #region common references FrmGroupPrivileges

                    new AutoGenerateInfo { ModuleType=1, DocumentID=1, FormId = 1, FormName = "FrmCompany" , FormText = "Company" , Prefix = "", CodeLength = 2, Suffix = 2, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=2, FormId = 2, FormName = "FrmLocation" , FormText = "Location" , Prefix = "", CodeLength = 2, Suffix = 2, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=3, FormId = 3, FormName = "FrmCostCentre" , FormText = "Cost Centers" , Prefix = "", CodeLength = 3, Suffix = 3, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },

                    new AutoGenerateInfo { ModuleType=1, DocumentID=4, FormId = 4, FormName = "FrmArea" , FormText = "Area" , Prefix = "A", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=5, FormId = 5, FormName = "FrmTerritory" , FormText = "Territory" , Prefix = "T", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=6, FormId = 6, FormName = "FrmUnitOfMeasure" , FormText = "Unit of Measure" , Prefix = "", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=7, FormId = 7, FormName = "FrmTypes" , FormText = "Return Types" , Prefix = "", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=8, FormId = 8, FormName = "FrmTypes" , FormText = "Transfer Types" , Prefix = "", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=9, FormId = 9, FormName = "FrmProductExtendedProperty" , FormText = "Product Extended Property" , Prefix = "", CodeLength = 3, Suffix = 3, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=10, FormId = 10, FormName = "FrmProductExtendedValue" , FormText = "Product Extended Value" , Prefix = "", CodeLength = 5, Suffix = 5, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false }, 
                    new AutoGenerateInfo { ModuleType=1, DocumentID=11, FormId = 11, FormName = "FrmColour" , FormText = "Colour" , Prefix = "", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=12, FormId = 12, FormName = "FrmSize" , FormText = "Size" , Prefix = "S", CodeLength = 3, Suffix = 2, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false }, 
                    new AutoGenerateInfo { ModuleType=1, DocumentID=13, FormId = 13, FormName = "FrmCustomerGroup" , FormText = "Customer Group" , Prefix = "", CodeLength = 3, Suffix = 3, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=14, FormId = 14, FormName = "FrmSupplierGroup" , FormText = "Supplier Group" , Prefix = "", CodeLength = 3, Suffix = 3, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=15, FormId = 15, FormName = "FrmPaymentMethod" , FormText = "Payment Method" , Prefix = "", CodeLength = 3, Suffix = 3, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=16, FormId = 16, FormName = "FrmVehicle" , FormText = "Vehicle" , Prefix = "V", CodeLength = 3, Suffix = 2, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=17, FormId = 17, FormName = "FrmDriver" , FormText = "Driver" , Prefix = "", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=18, FormId = 18, FormName = "FrmHelper" , FormText = "Helper" , Prefix = "", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=19, FormId = 19, FormName = "FrmUserPrivileges" , FormText = "User Privileges" , Prefix = "UP", CodeLength = 5, Suffix = 3, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=20, FormId = 20, FormName = "FrmLoginUser" , FormText = "Login User" , Prefix = "", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=21, FormId = 21, FormName = "FrmEmployee" , FormText = "Employee" , Prefix = "E", CodeLength = 6, Suffix = 5, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=22, FormId = 22, FormName = "FrmGroupPrivileges" , FormText = "Group Privileges" , Prefix = "GP", CodeLength = 5, Suffix = 3, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=23, FormId = 23, FormName = "FrmDesignationType" , FormText = "Employee Designation Type" , Prefix = "D", CodeLength = 3, Suffix = 2, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=24, FormId = 24, FormName = "FrmManualPasswordChange" , FormText = "Change Password" , Prefix = "", CodeLength = 0, Suffix =  0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },

                    //common transactions
                    //new AutoGenerateInfo { ModuleType=1, DocumentID=30, FormId = 30, FormName = "" , FormText = "Supplier Payment" , Prefix = "SAO", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    //new AutoGenerateInfo { ModuleType=1, DocumentID=31, FormId = 31, FormName = "" , FormText = "Customer Receipt" , Prefix = "SAI", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=501, FormId = 501, FormName = "FrmSampleOut" , FormText = "Sample Out" , Prefix = "SAO", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=502, FormId = 502, FormName = "FrmSampleIn" , FormText = "Sample In" , Prefix = "SAI", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=503, FormId = 503, FormName = "FrmOpeningStock" , FormText = "Opening Stock" , Prefix = "OS", CodeLength = 15, Suffix = 13, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=504, FormId = 504, FormName = "FrmDataDownload" , FormText = "Data Download" , Prefix = "DD", CodeLength = 15, Suffix = 13, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },

                    new AutoGenerateInfo { ModuleType=1, DocumentID=18001, FormId = 18001, FormName = "ShowCostPrice" , FormText = "Show Cost Price" , Prefix = "", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=1, DocumentID=18002, FormId = 18002, FormName = "ShowSellingPrice" , FormText = "Show Selling Price" , Prefix = "", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion

                    #region inventory & sales referencs
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1001, FormId = 1001, FormName = "FrmSupplier" , FormText = "Supplier" , Prefix = "S", CodeLength = 6, Suffix = 5, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1002, FormId = 1002, FormName = "FrmCustomer" , FormText = "Customer" , Prefix = "C", CodeLength = 6, Suffix = 5, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1003, FormId = 1003, FormName = "FrmDepartment" , FormText = "Department" , Prefix = "", CodeLength = 2, Suffix = 2, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1004, FormId = 1004, FormName = "FrmCategory" , FormText = "Category" , Prefix = "", CodeLength = 2, Suffix = 2, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1005, FormId = 1005, FormName = "FrmSubCategory" , FormText = "Sub Category" , Prefix = "", CodeLength = 4, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1006, FormId = 1006, FormName = "FrmSubCategory2" , FormText = "Sub Category 2" , Prefix = "", CodeLength = 5, Suffix = 5, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1007, FormId = 1007, FormName = "FrmProduct" , FormText = "Product" , Prefix = "", CodeLength = 12, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1008, FormId = 1008, FormName = "FrmSalesPerson" , FormText = "Sales Person" , Prefix = "SP", CodeLength = 5, Suffix = 3, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1009, FormId = 1009, FormName = "FrmPromotionMaster" , FormText = "Promotion Master" , Prefix = "PM", CodeLength = 15, Suffix = 13, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                   
                    #endregion

                    #region inventory & sales transactions
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1501, FormId = 1501, FormName = "FrmPurchaseOrder" , FormText = "Purchase Order" , Prefix = "PO", CodeLength = 15, Suffix = 13, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1502, FormId = 1502, FormName = "FrmGoodsReceivedNote" , FormText = "Goods Received Note" , Prefix = "GRN", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1503, FormId = 1503, FormName = "FrmPurchaseReturnNote" , FormText = "Purchase Return Note" , Prefix = "PRN", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1504, FormId = 1504, FormName = "FrmTransferOfGoodsNote" , FormText = "Transfer of Goods Note" , Prefix = "TOG", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1505, FormId = 1505, FormName = "FrmStockAdjustment" , FormText = "Stock Adjustment" , Prefix = "SA", CodeLength = 15, Suffix = 13, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1506, FormId = 1506, FormName = "FrmQuotation" , FormText = "Quotation" , Prefix = "QT", CodeLength = 15, Suffix = 13, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1507, FormId = 1507, FormName = "FrmSalesOrder" , FormText = "Sales Order" , Prefix = "SO", CodeLength = 15, Suffix = 13, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1508, FormId = 1508, FormName = "FrmInvoice" , FormText = "Invoice" , Prefix = "I", CodeLength = 15, Suffix = 14, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1509, FormId = 1509, FormName = "FrmSalesReturnNote" , FormText = "Sales Return Note" , Prefix = "SRN", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=true },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1510, FormId = 1510, FormName = "FrmDispatchNote" , FormText = "Dispatch Note" , Prefix = "DPN", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1511, FormId = 1511, FormName = "FrmBarcode" , FormText = "Barcode" , Prefix = "B", CodeLength = 15, Suffix = 14, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1512, FormId = 1512, FormName = "FrmProductPriceChange" , FormText = "Product Price Change" , Prefix = "PPC", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=1513, FormId = 1513, FormName = "FrmProductPriceChangeDamage" , FormText = "Product Price Change Damage" , Prefix = "PPD", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    #endregion
                    
                    #region non trading references
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2001, FormId = 2001, FormName = "FrmLogisticSupplier" , FormText = "Logistic Supplier" , Prefix = "", CodeLength = 5, Suffix = 5, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2002, FormId = 2002, FormName = "FrmLogisticDepartment" , FormText = "Logistic Department" , Prefix = "", CodeLength = 2, Suffix = 2, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2003, FormId = 2003, FormName = "FrmLogisticCategory" , FormText = "Logistic Category" , Prefix = "", CodeLength = 4, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2004, FormId = 2004, FormName = "FrmLogisticSubCategory" , FormText = "Logistic Sub Category" , Prefix = "", CodeLength = 4, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2005, FormId = 2005, FormName = "FrmLogisticSubCategory2" , FormText = "Logistic Sub Category 2" , Prefix = "", CodeLength = 4, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2006, FormId = 2006, FormName = "FrmLogisticProduct" , FormText = "Logistic Product" , Prefix = "", CodeLength = 14, Suffix = 14, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion

                    #region non trading transactions
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2501, FormId = 2501, FormName = "FrmLogisticQuotation" , FormText = "Logistic Quotation" , Prefix = "LQT", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2502, FormId = 2502, FormName = "FrmLogisticPurchaseOrder" , FormText = "Logistic Purchase Order" , Prefix = "LPO", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=true, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2503, FormId = 2503, FormName = "FrmLogisticGoodsReceivedNote" , FormText = "Logistic Goods Received Note" , Prefix = "LGR", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2504, FormId = 2504, FormName = "FrmLogisticPurchaseReturnNote" , FormText = "Logistic Purchase Return Note" , Prefix = "LPR", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2505, FormId = 2505, FormName = "FrmLogisticTransferOfGoodsNote" , FormText = "Logistic Transfer of Goods Note" , Prefix = "LTR", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2506, FormId = 2506, FormName = "FrmLogisticStockAdjustment" , FormText = "Logistic Stock Adjustment" , Prefix = "LSA", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2507, FormId = 2507, FormName = "FrmMaterialRequestNote" , FormText = "Material Request Note" , Prefix = "MRN", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2508, FormId = 2508, FormName = "FrmMaterialAllocationNote" , FormText = "Material Allocation Note" , Prefix = "MAN", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2509, FormId = 2509, FormName = "FrmMaterialConsumptionNote" , FormText = "Material Consumption Note" , Prefix = "MCN", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2510, FormId = 2510, FormName = "FrmMaintenanceJobRequisitionNote" , FormText = "Maintenance Job Requisition Note" , Prefix = "MJR", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2511, FormId = 2511, FormName = "FrmMaintenanceJobAssignNote" , FormText = "Maintenance Job Assign Note" , Prefix = "MJA", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2512, FormId = 2512, FormName = "FrmServiceOut" , FormText = "Service Out" , Prefix = "SA", CodeLength = 15, Suffix = 13, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2513, FormId = 2513, FormName = "FrmServiceIn" , FormText = "Service In" , Prefix = "SI", CodeLength = 15, Suffix = 13, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=2514, FormId = 2514, FormName = "FrmLogisticTransactionSearch" , FormText = "Logistic Transaction Panel" , Prefix = "", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion

                    #region accounts reference
                    new AutoGenerateInfo { ModuleType=5, DocumentID=4001, FormId = 4001, FormName = "FrmPettyCashMasterCreation" , FormText = "Petty Cash Master Creation" , Prefix = "P", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=5, DocumentID=4002, FormId = 4002, FormName = "FrmLinkedAccount" , FormText = "Linked Account" , Prefix = "", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion

                    #region accounts transactions
                    new AutoGenerateInfo { ModuleType=5, DocumentID=4501, FormId = 4501, FormName = "FrmPettyCashReimbursement" , FormText = "Petty Cash Reimbursement" , Prefix = "PCR", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=5, DocumentID=4502, FormId = 4502, FormName = "FrmPettyCashIOU" , FormText = "Petty Cash IOU" , Prefix = "IOU", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=5, DocumentID=4503, FormId = 4503, FormName = "FrmPettyCashBillEntry" , FormText = "Petty Cash Bill Entry" , Prefix = "PBE", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=5, DocumentID=4504, FormId = 4504, FormName = "FrmPettyCashPayment" , FormText = "Petty Cash Payment" , Prefix = "PCP", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=5, DocumentID=4505, FormId = 4505, FormName = "FrmPettyCashPaymentUpdate" , FormText = "Petty Cash Payment Process" , Prefix = "PPU", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion
                   
                    #region giftvoucher references
                    new AutoGenerateInfo { ModuleType=6, DocumentID=5001, FormId = 5001, FormName = "FrmGiftVoucherGroup" , FormText = "Gift Voucher Group" , Prefix = "G", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=6, DocumentID=5002, FormId = 5002, FormName = "FrmGiftVoucherBookCodeGeneration" , FormText = "Gift Voucher Book Code Generation" , Prefix = "B", CodeLength = 8, Suffix = 7, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=6, DocumentID=5003, FormId = 5003, FormName = "FrmGiftVoucherMaster" , FormText = "Gift Voucher Master" , Prefix = "GVM", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion

                    #region giftvoucher transactions
                    new AutoGenerateInfo { ModuleType=6, DocumentID=5501, FormId = 5501, FormName = "FrmGiftVoucherPurchaseOrder" , FormText = "Gift Voucher Purchase Order" , Prefix = "GPO", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=6, DocumentID=5502, FormId = 5502, FormName = "FrmGiftVoucherGoodsReceivedNote" , FormText = "Gift Voucher Goods Received Note" , Prefix = "GVR", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=6, DocumentID=5503, FormId = 5503, FormName = "FrmGiftVoucherTransfer" , FormText = "Gift Voucher Transfer" , Prefix = "GVT", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion

                    #region crm references
                    new AutoGenerateInfo { ModuleType=4, DocumentID=3001, FormId = 3001, FormName = "FrmLoyaltyCustomer" , FormText = "Loyalty Customer" , Prefix = "LC", CodeLength = 5, Suffix = 3, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=3002, FormId = 3002, FormName = "FrmCardMaster" , FormText = "Card Types" , Prefix = "CM", CodeLength = 5, Suffix = 3, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=3003, FormId = 3003, FormName = "FrmCardNoGeneration" , FormText = "Card No Generation" , Prefix = "CN", CodeLength = 5, Suffix = 3, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=3004, FormId = 3004, FormName = "FrmManualPointsAdd" , FormText = "Manual Points Add" , Prefix = "", CodeLength = 0, Suffix = 0, AutoGenerete = false, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion 

                    #region crm transactions
                    new AutoGenerateInfo { ModuleType=4, DocumentID=3501, FormId = 3501, FormName = "FrmCardIssue" , FormText = "Card Issue" , Prefix = "CIS", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=3502, FormId = 3502, FormName = "FrmLostAndRenew" , FormText = "Lost and Renew" , Prefix = "LAR", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=3503, FormId = 3503, FormName = "FrmCustomerFeedBack" , FormText = "Customer FeedBack" , Prefix = "CFB", CodeLength = 15, Suffix = 12, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion 

                    #region Common reports
                    //new AutoGenerateInfo { ModuleType=1, DocumentID=7001, FormId = 7001, FormName = "RptOpeningBalanceRegister" , FormText = "Opening Balance Register" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion 

                    #region inventry and sales (sales rep)
                    //new AutoGenerateInfo { ModuleType=2, DocumentID=8001, FormId = 8001, FormName = "RptProductWiseSales" , FormText = "Product Wise Sales" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=8002, FormId = 8002, FormName = "RptSupplierWiseSales" , FormText = "Supplier Wise Sales" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=8003, FormId = 8003, FormName = "RptSalesRegister" , FormText = "Sales Register" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=8004, FormId = 8004, FormName = "RptSummarySalesBook" , FormText = "Summary Sales Book" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=8005, FormId = 8005, FormName = "RptLocationSales" , FormText = "Location Sales" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=8006, FormId = 8006, FormName = "RptExtendedPropertySalesRegister" , FormText = "Sales Register (Extended Properties)" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },

                    new AutoGenerateInfo { ModuleType=2, DocumentID=17001, FormId = 17001, FormName = "RptUserWiseDiscount" , FormText = "User Wise Discount" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17002, FormId = 17002, FormName = "RptUserMediaTerminal" , FormText = "User Media Terminal" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17003, FormId = 17003, FormName = "RptUserWiseSales" , FormText = "User Wise Sales" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17004, FormId = 17004, FormName = "RptDetailMediaSales" , FormText = "Detail Media Sales" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17005, FormId = 17005, FormName = "RptMediaSalesSummary" , FormText = "Media Sales Summary" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17006, FormId = 17006, FormName = "RptSalesDiscount" , FormText = "Sales Discount" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17007, FormId = 17007, FormName = "RptCreditCardCollection" , FormText = "Credit Card Collection" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },   
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17008, FormId = 17008, FormName = "FrmPosSalesSummary" , FormText = "POS Sales Summary" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17009, FormId = 17009, FormName = "FrmCurrentSales" , FormText = "Current Sales" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17010, FormId = 17010, FormName = "RptStaffCreditSales" , FormText = "Staff Credit Sales" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17015, FormId = 17015, FormName = "FrmBasketAnalysisReport" , FormText = "Basket Analysis Report" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion 

                    #region inventory and sales (purchasing rep)
                    new AutoGenerateInfo { ModuleType=2, DocumentID=9001, FormId = 9001, FormName = "RptPurchaseRegister" , FormText = "Purchase Register" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=9002, FormId = 9002, FormName = "RptPendingPurchaseOrders" , FormText = "Pending Purchase Orders" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=9003, FormId = 9003, FormName = "RptSupplierWisePerformanceAnalysis" , FormText = "Supplier Wise Performance Analysis" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },

                    #endregion 

                    #region inventory and sales (stock rep)
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10001, FormId = 10001, FormName = "RptExcessStockAdjustment" , FormText = "Add Stock Adjustment" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10002, FormId = 10002, FormName = "RptShortageStockAdjustment" , FormText = "Reduce Stock Adjustment" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10003, FormId = 10003, FormName = "RptStockAdjustmentPercentage" , FormText = "Stock Adjustment" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10004, FormId = 10004, FormName = "RptBatchWiseStockAnalysis" , FormText = "Batch Wise Stock Analysis" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10005, FormId = 10005, FormName = "RptBatchWiseStockDetails" , FormText = "Batch Wise Stock Details" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10006, FormId = 10006, FormName = "RptBatchWiseStockBalance" , FormText = "Batch Wise Stock Balance" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10007, FormId = 10007, FormName = "RptProductWiseBatchWiseStockBalance" , FormText = "Product Wise Batch Wise Stock Balance" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10008, FormId = 10008, FormName = "RptAgingOfStockBatchWise" , FormText = "Aging Of Stock Batch Wise" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    //new AutoGenerateInfo { ModuleType=2, DocumentID=10009, FormId = 10009, FormName = "RptShortagePercentageStockAdjustment" , FormText = "Shortage Percentage Stock Adjustment" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=2, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10010, FormId = 10010, FormName = "InvRptOpeningBalanceRegister" , FormText = "Inventory Opening Balance Register" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10011, FormId = 10011, FormName = "InvRptStockBalance" , FormText = "Inventory Stock Balance" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10012, FormId = 10012, FormName = "InvRptBatchStockBalance" , FormText = "Inventory Batch Stock Balance" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10013, FormId = 10013, FormName = "RptExtendedPropertyStockBalance" , FormText = "Stock Balances (Extended Properties)" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10014, FormId = 10014, FormName = "InvRptTransferRegister" , FormText = "Product Transfer Register" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10015, FormId = 10015, FormName = "FrmGivenDateStock" , FormText = "Given Date Stock" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10016, FormId = 10016, FormName = "FrmGivenDateStock" , FormText = "Stock Movement Details" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10501, FormId = 10501, FormName = "RptBatchMaster", FormText = "Batch Master" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17014, FormId = 17014, FormName = "FrmBinCard", FormText = "Stock Movement Details" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=17015, FormId = 17015, FormName = "FrmBasketAnalysisReport", FormText = "Basket Analysis Report" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },

                    #endregion 

                    // you can use 10009

                    #region inventory and sales (other rep)
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10502, FormId = 10502, FormName = "RptPriceChange", FormText = "Price Change Report" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10503, FormId = 10503, FormName = "RptProductPriceChangeDetail", FormText = "Price Change Detail Report" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=10504, FormId = 10504, FormName = "RptProductPriceChangeDamageDetail", FormText = "Price Change Damage Detail Report" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion 

                    #region crm ref reports
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13001, FormId = 13001, FormName = "RptCustomerAddress" , FormText = "RptCustomerAddress" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=true, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13002, FormId = 13002, FormName = "RptBranchWiseCustomerDetails" , FormText = "Branch Wise Customer Details" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13003, FormId = 13003, FormName = "RptInActiveCustomerDetails" , FormText = "InActive Customer Details" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion 
                    
                    #region crm trans reports
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13004, FormId = 13004, FormName = "RptCustomerHistory" , FormText = "Customer History" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13005, FormId = 13005, FormName = "RptCustomerBehavior" , FormText = "Customer Behavior" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13006, FormId = 13006, FormName = "RptCustomervisitdetails" , FormText = "Customer visit details" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13007, FormId = 13007, FormName = "RptNumberOfVisitsCustomerWise" , FormText = "Number Of Visits Customer Wise" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13008, FormId = 13008, FormName = "RptCashierWiseLoyaltySummary" , FormText = "Cashier Wise Loyalty Summary" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },

                    new AutoGenerateInfo { ModuleType=4, DocumentID=13009, FormId = 13009, FormName = "RptLocationWiseSummary" , FormText = "Location Wise Summary" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },                  

                    new AutoGenerateInfo { ModuleType=4, DocumentID=13010, FormId = 13010, FormName = "RptCustomerStatement" , FormText = "CustomerStatement" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13011, FormId = 13011, FormName = "RptBestcustomerdetails" , FormText = "Best customer details" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13012, FormId = 13012, FormName = "RptLocationWiseLoyaltyAnalysis" , FormText = "Location Wise Loyalty Analysis" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13013, FormId = 13013, FormName = "RptMembershipUpgradesAnalysis" , FormText = "Membership Upgrades Analysis" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13014, FormId = 13014, FormName = "RptLostAndRenewalCardAnalysis" , FormText = "Lost And Renewal Card Analysis" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13015, FormId = 13015, FormName = "RptFreeCardIssueDetails" , FormText = "Free Card Issue Details" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13016, FormId = 13016, FormName = "RptLocationWiseLoyaltySummary" , FormText = "Location Wise Loyalty Summary" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13017, FormId = 13017, FormName = "RptMonthVisitCustomerWise" , FormText = "Month Visit Customer Wise" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13018, FormId = 13018, FormName = "RptMemberShipUpgradeProposalReport" , FormText = "Member Ship Upgrade Proposal Report" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=4, DocumentID=13019, FormId = 13019, FormName = "RptCardInventory" , FormText = "Card Inventory" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion 

                    #region Pos Module
                    new AutoGenerateInfo { ModuleType=7, DocumentID=6001, FormId = 6001, FormName = "FrmCashier" , FormText = "Cashier" , Prefix = "", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=7, DocumentID=6002, FormId = 6002, FormName = "FrmCashierGroup" , FormText = "Cashier Group" , Prefix = "", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion 

                    #region Gift Voucher Module
                    new AutoGenerateInfo { ModuleType=6, DocumentID=16001, FormId = 16001, FormName = "RptGiftVoucherRegister" , FormText = "Gift Voucher Register" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion 

                    //new AutoGenerateInfo { ModuleType=1, DocumentID=1, FormId = 1, FormName = "FrmDocumentViewer" , FormText = "Document Viewer" , Prefix = "", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    //new AutoGenerateInfo { ModuleType=1, DocumentID=1, FormId = 1, FormName = "FrmPaymentMethodLimit" , FormText = "Payment Method Limit" , Prefix = "", CodeLength = 5, Suffix = 4, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=1, PoIsMandatory=false, IsDispatchRecall=false },
                    
                    #region Non Trading (purchasing rep) Purchase 11001:12000
                    //new AutoGenerateInfo { ModuleType=2, DocumentID=11001, FormId = 11001, FormName = "RptPurchaseRegister" , FormText = "Purchase Register" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    //new AutoGenerateInfo { ModuleType=2, DocumentID=11002, FormId = 11002, FormName = "RptPurchaseReturnRegister" , FormText = "Purchase Return Register" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion 

                    #region Non trading (stock rep) Stock 12001:12500
                    new AutoGenerateInfo { ModuleType=3, DocumentID=12010, FormId = 12010, FormName = "LgsRptOpeningBalanceRegister" , FormText = "Logistic Opening Balance Register" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=12011, FormId = 12011, FormName = "LgsRptStockBalance" , FormText = "Logistic Stock Balance" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=12012, FormId = 12012, FormName = "LgsRptTransferRegister" , FormText = "Logistic Product Transfer Register" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=3, DocumentID=17016, FormId = 17016, FormName = "FrmLogistciBinCard", FormText = "Stock Movement Details" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    
                    
                    #endregion

                    #region Accounts Reports

                    new AutoGenerateInfo { ModuleType=5, DocumentID=14001, FormId = 14001, FormName = "RptReceiptsRegister" , FormText = "Receipts Register" , Prefix = "RPT", CodeLength = 0, Suffix = 0, AutoGenerete = true, AutoClear = true, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=true, ReportPrefix="", ReportType=2, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion

                    #region Special User Rights
                    new AutoGenerateInfo { ModuleType=2, DocumentID=19001, FormId = 19001, FormName = "ChangeSellingPrice" , FormText = "Change Selling Price" , Prefix = "", CodeLength = 0, Suffix = 0, AutoGenerete = false, AutoClear = false, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=0, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=19002, FormId = 19002, FormName = "ShowCostPrice" , FormText = "Show Cost Price" , Prefix = "", CodeLength = 0, Suffix = 0, AutoGenerete = false, AutoClear = false, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=0, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=19003, FormId = 19003, FormName = "ShowSellingPrice" , FormText = "Show Selling Price" , Prefix = "", CodeLength = 0, Suffix = 0, AutoGenerete = false, AutoClear = false, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=0, PoIsMandatory=false, IsDispatchRecall=false },
                    new AutoGenerateInfo { ModuleType=2, DocumentID=19004, FormId = 19004, FormName = "ShowQty" , FormText = "Show Qty" , Prefix = "", CodeLength = 0, Suffix = 0, AutoGenerete = false, AutoClear = false, IsDepend = false, IsDependCode = false, IsSupplierProduct=false, IsOverWriteQty=false, IsLocationCode=false, ReportPrefix="", ReportType=0, PoIsMandatory=false, IsDispatchRecall=false },
                    #endregion
      
                };

            autoGenerateInfo.ForEach(g => context.AutoGenerateInfos.Add(g));
            context.SaveChanges();
        }

        private static void AddDepartments(ERPDbContext context)
        {
            // Add Departments
            var departments = new List<InvDepartment>
                {
                    new InvDepartment { DepartmentCode = "D01", DepartmentName = "DEPARTMENT NAME D01", Remark = "D01 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                    new InvDepartment { DepartmentCode = "D02", DepartmentName = "DEPARTMENT NAME D02", Remark = "D02 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                    new InvDepartment { DepartmentCode = "D03", DepartmentName = "DEPARTMENT NAME D03", Remark = "D03 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                };

            departments.ForEach(g => context.InvDepartments.Add(g));
            context.SaveChanges();
        }

        private static void AddLgsDepartments(ERPDbContext context)
        {
            // Add Logistic Departments
            var departments = new List<LgsDepartment>
                {
                    new LgsDepartment { DepartmentCode = "D01", DepartmentName = "DEPARTMENT NAME D01", Remark = "D01 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                    new LgsDepartment { DepartmentCode = "D02", DepartmentName = "DEPARTMENT NAME D02", Remark = "D02 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                    new LgsDepartment { DepartmentCode = "D03", DepartmentName = "DEPARTMENT NAME D03", Remark = "D03 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                };

            departments.ForEach(g => context.LgsDepartments.Add(g));
            context.SaveChanges();
        }

        private static void AddCategories(ERPDbContext context)
        {
            // Add Categories
            var categories = new List<InvCategory>
                {
                    new InvCategory { InvDepartmentID = 1, CategoryCode = "D01C01", CategoryName = "CATEGORY NAME D01C01", Remark = "D01C01 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                    new InvCategory { InvDepartmentID = 1, CategoryCode = "D01C02", CategoryName = "CATEGORY NAME D01C02", Remark = "D01C02 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                    new InvCategory { InvDepartmentID = 3, CategoryCode = "D03C01", CategoryName = "CATEGORY NAME D03C01", Remark = "D03C01 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                };

            categories.ForEach(g => context.InvCategories.Add(g));
            context.SaveChanges();
        }

        private static void AddLgsCategories(ERPDbContext context)
        {
            // Add Logistic Categories
            var categories = new List<LgsCategory>
                {
                    new LgsCategory { LgsDepartmentID = 1, CategoryCode = "D01C01", CategoryName = "CATEGORY NAME D01C01", Remark = "D01C01 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                    new LgsCategory { LgsDepartmentID = 1, CategoryCode = "D01C02", CategoryName = "CATEGORY NAME D01C02", Remark = "D01C02 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                    new LgsCategory { LgsDepartmentID = 3, CategoryCode = "D03C01", CategoryName = "CATEGORY NAME D03C01", Remark = "D03C01 Remark", IsDelete = false, GroupOfCompanyID = 0 },
                };

            categories.ForEach(g => context.LgsCategories.Add(g));
            context.SaveChanges();
        }

        private static void AddSubCategories(ERPDbContext context)
        {
            // Add Sub Categories
            var subCategories = new List<InvSubCategory>
                {
                    new InvSubCategory { InvCategoryID = 1, SubCategoryCode = "SC01", SubCategoryName = "SC01N", Remark = "SC01R", IsDelete = false, GroupOfCompanyID = 0 },
                    new InvSubCategory { InvCategoryID = 2, SubCategoryCode = "SC02", SubCategoryName = "SC02N", Remark = "SC02R", IsDelete = false, GroupOfCompanyID = 0 },
                    new InvSubCategory { InvCategoryID = 1, SubCategoryCode = "SC03", SubCategoryName = "SC03N", Remark = "SC03R", IsDelete = false, GroupOfCompanyID = 0 },
                };

            subCategories.ForEach(g => context.InvSubCategories.Add(g));
            context.SaveChanges();
        }

        private static void AddLgsSubCategories(ERPDbContext context)
        {
            // Add Logistic Sub Categories
            var subCategories = new List<LgsSubCategory>
                {
                    new LgsSubCategory { LgsCategoryID = 1, SubCategoryCode = "SC01", SubCategoryName = "SC01N", Remark = "SC01R", IsDelete = false, GroupOfCompanyID = 0 },
                    new LgsSubCategory { LgsCategoryID = 2, SubCategoryCode = "SC02", SubCategoryName = "SC02N", Remark = "SC02R", IsDelete = false, GroupOfCompanyID = 0 },
                    new LgsSubCategory { LgsCategoryID = 1, SubCategoryCode = "SC03", SubCategoryName = "SC03N", Remark = "SC03R", IsDelete = false, GroupOfCompanyID = 0 },
                };

            subCategories.ForEach(g => context.LgsSubCategories.Add(g));
            context.SaveChanges();
        }

        private static void AddSubCategories2(ERPDbContext context)
        {
            // Add Sub Categories2
            var subCategories2 = new List<InvSubCategory2>
                {
                    new InvSubCategory2 { InvSubCategoryID = 1, SubCategory2Code = "SC201", SubCategory2Name = "SC201N", Remark = "SC201R", IsDelete = false, GroupOfCompanyID = 0 },
                    new InvSubCategory2 { InvSubCategoryID = 3, SubCategory2Code = "SC202", SubCategory2Name = "SC202N", Remark = "SC202R", IsDelete = false, GroupOfCompanyID = 0 },
                    new InvSubCategory2 { InvSubCategoryID = 2, SubCategory2Code = "SC203", SubCategory2Name = "SC203N", Remark = "SC203R", IsDelete = false, GroupOfCompanyID = 0 },
                };

            subCategories2.ForEach(g => context.InvSubCategories2.Add(g));
            context.SaveChanges();
        }

        private static void AddLgsSubCategories2(ERPDbContext context)
        {
            // Add Logistic Sub Categories2
            var subCategories2 = new List<LgsSubCategory2>
                {
                    new LgsSubCategory2 { LgsSubCategoryID = 1, SubCategory2Code = "SC201", SubCategory2Name = "SC201N", Remark = "SC201R", IsDelete = false, GroupOfCompanyID = 0 },
                    new LgsSubCategory2 { LgsSubCategoryID = 3, SubCategory2Code = "SC202", SubCategory2Name = "SC202N", Remark = "SC202R", IsDelete = false, GroupOfCompanyID = 0 },
                    new LgsSubCategory2 { LgsSubCategoryID = 2, SubCategory2Code = "SC203", SubCategory2Name = "SC203N", Remark = "SC203R", IsDelete = false, GroupOfCompanyID = 0 },
                };

            subCategories2.ForEach(g => context.LgsSubCategories2.Add(g));
            context.SaveChanges();
        }
        
        private static void AddSuppliers(ERPDbContext context)
        {
            // Add Suppliers
            var suppliers = new List<Supplier>
                {
                    new Supplier { SupplierGroupID = 1, SupplierCode = "S01", SupplierTitle = 1, SupplierName = "S1Name", SupplierType = 1, BillingTelephone = "0451287419", LedgerID = 1, OtherLedgerID = 1},
                    new Supplier { SupplierGroupID = 1, SupplierCode = "S02", SupplierTitle = 2, SupplierName = "S2Name", SupplierType = 1, BillingTelephone = "0451274419", LedgerID = 1, OtherLedgerID = 1},                                        
                };

            suppliers.ForEach(g => context.Suppliers.Add(g));
            context.SaveChanges();
        }

        private static void AddCustomerGroups(ERPDbContext context)
        {
            // Add Customer Groups
            var customerGroups = new List<CustomerGroup>
                {
                    new CustomerGroup { CustomerGroupCode = "001", CustomerGroupName = "Group 01"},
                    new CustomerGroup { CustomerGroupCode = "002", CustomerGroupName = "Group 02"},
                    new CustomerGroup { CustomerGroupCode = "003", CustomerGroupName = "Group 03"},
                };

            customerGroups.ForEach(g => context.CustomerGroups.Add(g));
            context.SaveChanges();
        }

        private static void AddAreas(ERPDbContext context)
        {
            var areas = new List<Area>
                {
                    new Area { AreaCode = "001", AreaName = "Area 01" ,Remark = "D03R", IsDelete = false, GroupOfCompanyID = 0 },
                    new Area { AreaCode = "002", AreaName = "Area 02" ,Remark = "D03R", IsDelete = false, GroupOfCompanyID = 0 },
                    new Area { AreaCode = "003", AreaName = "Area 03" ,Remark = "D03R", IsDelete = false, GroupOfCompanyID = 0 },
                }; 

            areas.ForEach(a => context.Areas.Add(a));
            context.SaveChanges();
        }

        private static void AddTerritorys(ERPDbContext context)
        {
            var territorys = new List<Territory>
                {
                    new Territory { TerritoryCode = "001", TerritoryName = "Territory 01",  IsDelete = false, GroupOfCompanyID = 0 ,AreaID = 1},
                    new Territory { TerritoryCode = "002", TerritoryName = "Territory 02",  IsDelete = false, GroupOfCompanyID = 0 ,AreaID = 1},
                    new Territory { TerritoryCode = "003", TerritoryName = "Territory 03",  IsDelete = false, GroupOfCompanyID = 0 ,AreaID = 1},
                };
            territorys.ForEach(t => context.Territories.Add(t));
            context.SaveChanges();
        }

        private static void AddBrokers(ERPDbContext context)
        {
            var brokers = new List<Broker>
                {
                    new Broker { BrokerCode = "001", BrokerName = "Broker 01", IsDelete = false, GroupOfCompanyID = 0 },
                    new Broker { BrokerCode = "002", BrokerName = "Broker 02",  IsDelete = false, GroupOfCompanyID = 0 },
                    new Broker { BrokerCode = "003", BrokerName = "Broker 03",  IsDelete = false, GroupOfCompanyID = 0 },
                };

            brokers.ForEach(a => context.Brokers.Add(a));
            context.SaveChanges();
        }

        private static void AddProductCodeDependancy(ERPDbContext context)
        {
            var productCodeDependanciey = new List<ProductCodeDependancy> 
                {
                    new ProductCodeDependancy { FormName = "FrmProduct", DependOnDepartment = true, DependOnCategory = true, DependOnSubCategory = true, DependOnSubCategory2 = false  },
                    new ProductCodeDependancy { FormName = "FrmLogisticProduct", DependOnDepartment = true, DependOnCategory = true, DependOnSubCategory = true, DependOnSubCategory2 = false  },
                };

            productCodeDependanciey.ForEach(a => context.ProductCodeDependancies.Add(a));
            context.SaveChanges();
        }

        private static void AddGroupOfCompany(ERPDbContext context)
        {
            var groupOfCompanies = new List<GroupOfCompany>
            {
                new GroupOfCompany {GroupOfCompanyCode="1", GroupOfCompanyName="NOLIMIT", IsInventory = true, IsGeneralLedger = true, IsManufacture =  false, IsLogistic = true, IsHirePurchase = false, IsHrManagement = false, IsActive = true, IsDelete = false, CreatedUser = "ADMIN", ModifiedUser = "ADMIN", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now},

            };
            groupOfCompanies.ForEach(g => context.GroupOfCompanies.Add(g));
            context.SaveChanges();
        }

        private static void AddCompany(ERPDbContext context)
        {
            var companies = new List<Company>
            {
                //new Company {GroupOfCompanyID= 1,CostCentreID=1, CompanyCode= "C0001", CompanyName ="SARSA SOFT SOLUTIONS",OtherBusinessName1="Name 01",OtherBusinessName2 = "Name 02", OtherBusinessName3 = "Name 03",},
            };
            companies.ForEach(g => context.Companies.Add(g));
            context.SaveChanges();
        }

        private static void AddLocation(ERPDbContext context)
        {
            var locations = new List<Location>
            {
               // new Location {GroupOfCompanyCode="GC01",GroupOfCompanyName="Cynex Soft"},
            };
            locations.ForEach(g => context.Locations.Add(g));

//            SET IDENTITY_INSERT [dbo].[Location] ON
//INSERT [dbo].[Location] ([LocationID], [CompanyID], [LocationCode], [LocationName], [Address1], [Address2], [Address3], [Telephone], [Mobile], [FaxNo], [Email], [ContactPersonName], [OtherBusinessName], [LocationPrefixCode], [TypeOfBusiness], [CostingMethod], [CostCentreID], [IsVat], [IsStockLocation], [IsDelete], [GroupOfCompanyID], [CreatedUser], [CreatedDate], [ModifiedUser], [ModifiedDate], [DataTransfer]) VALUES (1, 1, N'L0001', N'NOLIMIT', N'vv', N'vv', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'Last In First Out (LIFO)', 1, 1, 1, 0, 1, N'Cynex', CAST(0x0000A20400FA9674 AS DateTime), N'Cynex', CAST(0x0000A20400FA9674 AS DateTime), 0)
//INSERT [dbo].[Location] ([LocationID], [CompanyID], [LocationCode], [LocationName], [Address1], [Address2], [Address3], [Telephone], [Mobile], [FaxNo], [Email], [ContactPersonName], [OtherBusinessName], [LocationPrefixCode], [TypeOfBusiness], [CostingMethod], [CostCentreID], [IsVat], [IsStockLocation], [IsDelete], [GroupOfCompanyID], [CreatedUser], [CreatedDate], [ModifiedUser], [ModifiedDate], [DataTransfer]) VALUES (2, 1, N'L0002', N'GLITZ', N'vv', N'vv', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'Last In First Out (LIFO)', 1, 0, 0, 0, 1, N'Cynex', CAST(0x0000A20400FAA018 AS DateTime), N'Cynex', CAST(0x0000A20400FAA018 AS DateTime), 0)
//INSERT [dbo].[Location] ([LocationID], [CompanyID], [LocationCode], [LocationName], [Address1], [Address2], [Address3], [Telephone], [Mobile], [FaxNo], [Email], [ContactPersonName], [OtherBusinessName], [LocationPrefixCode], [TypeOfBusiness], [CostingMethod], [CostCentreID], [IsVat], [IsStockLocation], [IsDelete], [GroupOfCompanyID], [CreatedUser], [CreatedDate], [ModifiedUser], [ModifiedDate], [DataTransfer]) VALUES (3, 1, N'L0003', N'PALLU', N'vv', N'vv', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'Last In First Out (LIFO)', 1, 0, 0, 0, 1, N'Cynex', CAST(0x0000A20400FAA7A2 AS DateTime), N'Cynex', CAST(0x0000A20400FAA7A2 AS DateTime), 0)
//INSERT [dbo].[Location] ([LocationID], [CompanyID], [LocationCode], [LocationName], [Address1], [Address2], [Address3], [Telephone], [Mobile], [FaxNo], [Email], [ContactPersonName], [OtherBusinessName], [LocationPrefixCode], [TypeOfBusiness], [CostingMethod], [CostCentreID], [IsVat], [IsStockLocation], [IsDelete], [GroupOfCompanyID], [CreatedUser], [CreatedDate], [ModifiedUser], [ModifiedDate], [DataTransfer]) VALUES (4, 1, N'L0004', N'NUGEGODA', N'vv', N'vv', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'Last In First Out (LIFO)', 1, 0, 0, 0, 1, N'Cynex', CAST(0x0000A20400FABF6B AS DateTime), N'Cynex', CAST(0x0000A20400FABF6B AS DateTime), 0)
//INSERT [dbo].[Location] ([LocationID], [CompanyID], [LocationCode], [LocationName], [Address1], [Address2], [Address3], [Telephone], [Mobile], [FaxNo], [Email], [ContactPersonName], [OtherBusinessName], [LocationPrefixCode], [TypeOfBusiness], [CostingMethod], [CostCentreID], [IsVat], [IsStockLocation], [IsDelete], [GroupOfCompanyID], [CreatedUser], [CreatedDate], [ModifiedUser], [ModifiedDate], [DataTransfer]) VALUES (5, 1, N'L0005', N'BORELLA', N'vv', N'vv', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'Last In First Out (LIFO)', 1, 0, 0, 0, 1, N'Cynex', CAST(0x0000A20400FAC606 AS DateTime), N'Cynex', CAST(0x0000A20400FAC606 AS DateTime), 0)
//SET IDENTITY_INSERT [dbo].[Location] OFF
            context.SaveChanges();
        }

        private static void AddGiftVoucherGroup(ERPDbContext context)
        {
            // Add Gift Voucher Group
            var invGiftVoucherMasterGroups = new List<InvGiftVoucherGroup>
                {
                    new InvGiftVoucherGroup { GiftVoucherGroupCode = "001", GiftVoucherGroupName = "GROUP 01", Remark = "GROUP 01"},
                    new InvGiftVoucherGroup { GiftVoucherGroupCode = "002", GiftVoucherGroupName = "GROUP 02", Remark = "GROUP 02"},
                    new InvGiftVoucherGroup { GiftVoucherGroupCode = "003", GiftVoucherGroupName = "GROUP 03", Remark = "GROUP 03"},
                };

            invGiftVoucherMasterGroups.ForEach(u => context.InvGiftVoucherGroups.Add(u));
            context.SaveChanges();
        }

        private static void AddGiftVoucherBook(ERPDbContext context)
        {
            // Add Gift Voucher Book
            var invGiftVoucherMasterBooks = new List<InvGiftVoucherBookCode>
                {
                    new InvGiftVoucherBookCode { BookCode = "001", BookName = "VOUCHER 500", InvGiftVoucherGroupID = 1, GiftVoucherValue = 500, PageCount = 10, StartingNo = 1, CurrentSerialNo = 1, SerialLength = 8, BookPrefix = "1V"},
                    new InvGiftVoucherBookCode { BookCode = "002", BookName = "VOUCHER 500", InvGiftVoucherGroupID = 3, GiftVoucherValue = 500, PageCount = 15, StartingNo = 6, CurrentSerialNo = 1, SerialLength = 8, BookPrefix = "1B"},
                    new InvGiftVoucherBookCode { BookCode = "003", BookName = "VOUCHER 1000", InvGiftVoucherGroupID = 1, GiftVoucherValue = 1000, PageCount = 25, StartingNo = 1, CurrentSerialNo = 1, SerialLength = 8, BookPrefix = "1C"},
                };

            invGiftVoucherMasterBooks.ForEach(u => context.InvGiftVoucherBookCodes.Add(u));
            context.SaveChanges();
        }

        #region General Ledger
        private void AddLedgerAccounts(ERPDbContext context)
        {

            var accLedgerAccounts = new List<AccLedgerAccount>
                {
                    new AccLedgerAccount { LedgerType = "1", TypeLevel = 0, TypeNode = "0", LedgerCode = "1", LedgerName = "Asset", AccountStatus = 0},
                    new AccLedgerAccount { LedgerType = "2", TypeLevel = 0, TypeNode = "0", LedgerCode = "2", LedgerName = "Liability", AccountStatus = 0},
                    new AccLedgerAccount { LedgerType = "3", TypeLevel = 0, TypeNode = "0", LedgerCode = "3", LedgerName = "Equity", AccountStatus = 0},
                    new AccLedgerAccount { LedgerType = "4", TypeLevel = 0, TypeNode = "0", LedgerCode = "4", LedgerName = "Income", AccountStatus = 0},
                    new AccLedgerAccount { LedgerType = "5", TypeLevel = 0, TypeNode = "0", LedgerCode = "5", LedgerName = "Expense", AccountStatus = 0},
                    new AccLedgerAccount { LedgerType = "1", TypeLevel = 1, TypeNode = "1", LedgerCode = "101", LedgerName = "Current Asset", AccountStatus = 0},
                    new AccLedgerAccount { LedgerType = "1", TypeLevel = 2, TypeNode = "2", LedgerCode = "10101", LedgerName = "Accounts Receivable", AccountStatus = 0},
                    new AccLedgerAccount { LedgerType = "5", TypeLevel = 1, TypeNode = "1", LedgerCode = "50101", LedgerName = "Cost of Goods", AccountStatus = 0},
                    new AccLedgerAccount { LedgerType = "1", TypeLevel = 2, TypeNode = "2", LedgerCode = "10102", LedgerName = "Petty Cash", AccountStatus = 2},
                    new AccLedgerAccount { LedgerType = "1", TypeLevel = 3, TypeNode = "3", LedgerCode = "1010201", LedgerName = "Petty Cash Book", AccountStatus = 2},
                    new AccLedgerAccount { LedgerType = "1", TypeLevel = 2, TypeNode = "2", LedgerCode = "10103", LedgerName = "Bank Control", AccountStatus = 1},
                    new AccLedgerAccount { LedgerType = "1", TypeLevel = 3, TypeNode = "3", LedgerCode = "1010301", LedgerName = "COMMERCIAL BANK - A/C - 0012002120", AccountStatus = 1},
                };

            accLedgerAccounts.ForEach(l => context.AccLedgerAccounts.Add(l));
            context.SaveChanges();

            //var baseDir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin", string.Empty) + "\\Migrations";

            //context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir + "\\DataClear.sql"));
            //context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir + "\\Table1Inserts.sql"));
            //context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir + "\\Table2Inserts.sql"));

            ////or

            //var sqlFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.sql").OrderBy(x => x);
            //foreach (string file in sqlFiles)
            //{
            //    context.Database.ExecuteSqlCommand(File.ReadAllText(file));
            //}

            ////string fileName = "AutoGeneratgeInfo.sql";
            //string path = Path.Combine(Environment.CurrentDirectory, @"Data\SqlScript\" + " AutoGeneratgeInfo.sql ");
            //context.Database.ExecuteSqlCommand(File.ReadAllText(path));
        }
        #endregion

//        SELECT * FROM
//    (SELECT ROW_NUMBER() 
//        OVER (ORDER BY Salary) AS Row, 
//        EmployeeId, EmployeeName, Salary 
//    FROM Employees) AS EMP
//WHERE Row = 4

        #endregion
    }
}
