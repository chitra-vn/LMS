using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryBooks.Models
{
    public class BookTransaction
    {
        public int bookTransactionId { get; set; }
        public Nullable<DateTime> transactionDate { get; set; }
        public int transactionType { get; set; }
        public Nullable<DateTime> dateIssueReturn { get; set; }
    }

}