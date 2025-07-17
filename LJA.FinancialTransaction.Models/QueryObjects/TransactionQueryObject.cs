using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJA.FinancialTransaction.Models.QueryObjects
{
    public class TransactionQueryObject
    {
        public int? Id { get; set; } = null;

        public decimal? Amount { get; set; }

        public DateTime? Date { get; set; } = null;

        public string? Description { get; set; } = null;

        public int? CategoryId { get; set; } = null;

        public int? SourceId { get; set; } = null;
    }
}
