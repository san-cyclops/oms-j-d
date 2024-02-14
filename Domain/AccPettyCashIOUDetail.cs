using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccPettyCashIOUDetail  : BaseEntity
    {
        private AccPettyCashIOUDetail[] _accPettyCashIOUDetail;


        public AccPettyCashIOUDetail() { }
        public AccPettyCashIOUDetail(AccPettyCashIOUDetail[] pArray)
        {
            _accPettyCashIOUDetail = new AccPettyCashIOUDetail[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _accPettyCashIOUDetail[i] = pArray[i];
            }
        }

        public long AccPettyCashIOUDetailID { get; set; }

        [DefaultValue(0)]
        public long PettyCashIOUDetailID { get; set; } 

        public long AccPettyCashIOUHeaderID { get; set; }

        [DefaultValue(0)]
        public int CompanyID { get; set; }

        [DefaultValue(0)]
        public int LocationID { get; set; }

        [DefaultValue(0)]
        public int DocumentID { get; set; }

        [Required]
        public long LedgerID { get; set; }
        
        [Required()]
        [MaxLength(25)]
        [NotMapped]
        public string LedgerCode { get; set; }

        [Required()]
        [MaxLength(100)]
        [NotMapped]
        public string LedgerName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [DefaultValue(0)]
        public int DocumentStatus { get; set; }

        [DefaultValue(0)]
        public bool IsUpLoad { get; set; }

        public virtual AccPettyCashIOUHeader AccPettyCashIOUHeader { get; set; }

        public AccPettyCashIOUDetail GetEnumerator()
        {
            return new AccPettyCashIOUDetail(_accPettyCashIOUDetail);
        }
    }
}
