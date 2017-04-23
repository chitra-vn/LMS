using LibraryBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryBooks.Actions;
using LibraryBooks.Dao;
using Newtonsoft.Json;

namespace LibraryBooks.Controllers
{
    public class BookLibraryHomeController : Controller
    {
        // GET: BookLibraryHome
       
        public ActionResult Index()
        {
            //GetConferenceUsageType();
            return View();

        }
        public string GetBookDetails()
        {
            List<BookDetails> lstBookDetails = new List<BookDetails>();
            try
            {
                lstBookDetails = BookLibraryHomeAction.GetBookDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(lstBookDetails);
        }
        public string GetBookTransactionDetails()
        {
            List<BookDetails> lstBookDetails = new List<BookDetails>();
            try
            {
                lstBookDetails = BookLibraryHomeAction.GetBookTransactionDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(lstBookDetails);
        }
        public string AddBookDetails(string bookName,string authorName,string isbnCode,string quantityBooks,string publishDate,string bookCategory,string quantityBooksIssued)
        {
            String Message = "";
            try
            {
                BookDetails bookobj = new BookDetails();
                bookobj.bookName = bookName;
                bookobj.authorName = authorName;
                bookobj.isbnCode = isbnCode;
                bookobj.quantityBooks =Convert.ToInt32(quantityBooks);
              // DateTime publishDate1= publishDate.ToShortDateString();
                bookobj.publishDate = Convert.ToDateTime(publishDate);
                bookobj.bookCategory = bookCategory;
                bookobj.quantityBooksIssued =Convert.ToInt32(quantityBooksIssued);
                Message = BookLibraryHomeAction.AddBookDetails(bookobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Message;
        }

        public string EditBookDetails(int id)
        {
            List<BookDetails> lstBookDetails = new List<BookDetails>();
            try
            {
                lstBookDetails = BookLibraryHomeAction.EditBookDetails(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(lstBookDetails);
        }

        public string UpdateBookDetails(int id,int qunatity)
        {
            String Message = "";
            try
            {
                Message = BookLibraryHomeAction.UpdateBookDetails(id, qunatity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Message;
        }
        public string DeleteBookDetails(int id)
        {
            String Message = "";
            try
            {
                Message = BookLibraryHomeAction.DeleteBookDetails(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Message;
        }

        public string IssueBookDetails(int id,string quantityIssue)
        {
            String Message = "";
            String Message1 = "";
            try
            {
                int quantityIssue1 = Convert.ToInt32(quantityIssue);
                quantityIssue1 += 1;
                DateTime transactionDate = DateTime.Now;
                int transactionType = 1;
                DateTime DateIssue = DateTime.Now;
                
                Message = BookLibraryHomeAction.IssueBookDetails(id,quantityIssue1);
                Message1= BookLibraryHomeAction.UpdateBookTransactionDetails(id, transactionDate, transactionType, DateIssue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Message;
        }

        public string ReturnBookDetails(string id, string transactionType,string IdMain)
        {
            String Message = "";
            try
            {
                int id1 = Convert.ToInt32(id);
                int transactionType1 = 0;
                int IdMain1 = Convert.ToInt32(IdMain);

                Message = BookLibraryHomeAction.ReturnBookDetails(id1, transactionType1, IdMain1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Message;
        }

        public ActionResult Dummy()
        {
            return View();
        }
    }
}