using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryBooks.Models;
using LibraryBooks.Dao;

namespace LibraryBooks.Actions
{
    public class BookLibraryHomeAction
    {
        public static List<BookDetails> GetBookDetails()
        {
            List<BookDetails> lstBookDetails = new List<BookDetails>();
            try
            {
                lstBookDetails = BookLibraryHomeDao.GetBookDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstBookDetails;
        }


        public static List<BookDetails> GetBookTransactionDetails()
        {
            List<BookDetails> lstBookDetails = new List<BookDetails>();
            try
            {
                lstBookDetails = BookLibraryHomeDao.GetBookTransactionDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstBookDetails;
        }

        internal static string AddBookDetails(BookDetails bookobj)
        {
            string result = "";
            try
            {
                result = BookLibraryHomeDao.AddBookDetails(bookobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }

        public static List<BookDetails> EditBookDetails(int id)
        {
            List<BookDetails> lstBookDetails = new List<BookDetails>();

            try
            {
                lstBookDetails = BookLibraryHomeDao.EditBookDetails(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstBookDetails;
        }

        public static string UpdateBookDetails(int id,int qunatity)
        {
            string result = "";
            try
            {
                result = BookLibraryHomeDao.UpdateBookDetails(id, qunatity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }
        public static string DeleteBookDetails(int id)
        {
            string result = "";
            try
            {
                result = BookLibraryHomeDao.DeleteBookDetails(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }

        public static string IssueBookDetails(int id,int quantityIssue)
        {
            string result = "";
            try
            {
                result = BookLibraryHomeDao.IssueBookDetails(id, quantityIssue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }
        public static string UpdateBookTransactionDetails(int id, DateTime transactionDate, int transactionType, DateTime DateIssue)
        {
            string result = "";
            try
            {
                result = BookLibraryHomeDao.UpdateBookTransactionDetails(id, transactionDate, transactionType, DateIssue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }

        public static string ReturnBookDetails(int id, int transactionType,int IdMain)
        {
            string result = "";
            try
            {
                result = BookLibraryHomeDao.ReturnBookDetails(id, transactionType, IdMain);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }
    }
}