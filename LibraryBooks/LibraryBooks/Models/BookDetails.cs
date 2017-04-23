using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryBooks.Models
{
    public class BookDetails
    {
        [Display(Name = "Book Id")]
        public int id { get; set; }

        [Display(Name = "Book Name")]
        public string bookName { get; set; }
        [Display(Name = "Author Name")]
        public string authorName { get; set; }
        [Display(Name = "ISBN Code")]
        public string isbnCode { get; set; }
        [Display(Name = "Books Quantity")]
        public int quantityBooks { get; set; }
        [Display(Name = "Publish Date")]
        public Nullable<DateTime> publishDate { get; set; }
        [Display(Name = "Category")]
        public string bookCategory { get; set; }
        [Display(Name = "Quantity Issued")]
        public int quantityBooksIssued { get; set; }
        public int bookTransactionIdMain { get; set; }
        [Display(Name = "Book Id")]
        public int bookTransactionId { get; set; }
        [Display(Name = "Trans. Date")]
        public Nullable<DateTime> transactionDate { get; set; }
        [Display(Name = "Transc. Type")]
        public int transactionType { get; set; }
        [Display(Name = "Date(Issue\\Return")]
        public Nullable<DateTime> dateIssueReturn { get; set; }

    }
}