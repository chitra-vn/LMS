using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryBooks.Models;
using System.Data;
using System.Configuration;
using MySql;
using System.Data.SqlClient;

namespace LibraryBooks.Dao
{
    public class BookLibraryHomeDao
    {
        //internal static List<BookDetails> GetAllConferenceUsageTypes1()
        //{
        //    DataSet ds;
        //    List<BookDetails> lstBookDetails = new List<BookDetails>();
        //    BookDetails bookdetailsObj;

        //    try
        //    {
        //        ds = SQLUtilities.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
        //                     "SP_Get_AllOperationDesk");

        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in ds.Tables[0].Rows)
        //                {
        //                    operationDeskObj = new ManageOperationDesk();
        //                    operationDeskObj.OppdeskID = Convert.ToInt32(dr["OPR_DESK_ID"]);
        //                    operationDeskObj.OppDeskName = Convert.ToString(dr["OPR_DESK_NM"]);
        //                    operationDeskObj.OppDeskDist_List = Convert.ToString(dr["DISTR_LIST"]);
        //                    operationDeskObj.CreatedBy = Convert.ToString(dr["CREAT_BY_USER_ID"]);
        //                    operationDeskObj.CreatedTS = Convert.ToDateTime(dr["CREAT_TS"]);
        //                    operationDeskObj.UpdatedBy = Convert.ToString(dr["LST_UPDT_BY_USER_ID"]);
        //                    operationDeskObj.UpdatedTS = Convert.ToDateTime(dr["LST_UPDT_TS"]);
        //                    operationDeskObj.ActiveIn = Convert.ToBoolean(dr["ACT_IN"]);
        //                    lisOperationDesk.Add(operationDeskObj);
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return lisOperationDesk;
        //}

        internal static List<BookDetails> GetBookDetails()
        {
            DataSet ds;
            List<BookDetails> lstBookDetails = new List<BookDetails>();
            BookDetails bookdetailsObj;

            try
            {
                 ds = new DataSet();
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "sp_getbookdetails";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(ds);
                        }
                    }
                }

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            bookdetailsObj = new BookDetails();
                            bookdetailsObj.id = Convert.ToInt32(dr["Book_id"]);
                            bookdetailsObj.bookName = Convert.ToString(dr["Book_name"]);
                            bookdetailsObj.authorName = Convert.ToString(dr["Author_name"]);
                            bookdetailsObj.isbnCode = Convert.ToString(dr["ISBN_Code"]);
                            bookdetailsObj.quantityBooks = Convert.ToInt32(dr["Quantity_of_books"]);
                            DateTime publishDate = Convert.ToDateTime(dr["Publish_date"]);
                            bookdetailsObj.publishDate = publishDate.Date;
                            bookdetailsObj.bookCategory = Convert.ToString(dr["Book_Category"]);
                            bookdetailsObj.quantityBooksIssued = Convert.ToInt32(dr["quantity_of_books_issued"]);
                            lstBookDetails.Add(bookdetailsObj);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstBookDetails;
        }
        
            internal static List<BookDetails> GetBookTransactionDetails()
        {
            DataSet ds;
            List<BookDetails> lstBookDetails = new List<BookDetails>();
            BookDetails bookdetailsObj;

            try
            {
                ds = new DataSet();
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "sp_getbooktranscationdetails";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(ds);
                        }
                    }
                }

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            bookdetailsObj = new BookDetails();
                            bookdetailsObj.bookTransactionIdMain = Convert.ToInt32(dr["BookTransactionID"]);
                            bookdetailsObj.bookTransactionId = Convert.ToInt32(dr["Book_Id"]);
                            bookdetailsObj.transactionDate = Convert.ToDateTime(dr["Transaction_date"]).Date;
                            bookdetailsObj.transactionType = Convert.ToInt32(dr["Transaction_Type"]);
                            bookdetailsObj.dateIssueReturn = Convert.ToDateTime(dr["date_issue_return"]).Date;
                            lstBookDetails.Add(bookdetailsObj);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstBookDetails;
        }

        internal static string AddBookDetails(BookDetails BookObj)
        {
            string result = "";
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string query = "sp_insert";
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Book_name", BookObj.bookName);
                cmd.Parameters.AddWithValue("@Author_name", BookObj.authorName);
                cmd.Parameters.AddWithValue("@ISBN_Code", BookObj.isbnCode);
                cmd.Parameters.AddWithValue("@Quantity_of_books", BookObj.quantityBooks);
                cmd.Parameters.AddWithValue("@Publish_date", BookObj.publishDate);
                cmd.Parameters.AddWithValue("@Book_Category", BookObj.bookCategory);
                cmd.Parameters.AddWithValue("@quantity_of_books_issued", BookObj.quantityBooksIssued);
                cmd.ExecuteNonQuery();
                result = "Success";
                con.Close();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
           

        }

        internal static List<BookDetails> EditBookDetails(int id)
        {
            List<BookDetails> lstBookDetails = new List<BookDetails>();
            BookDetails bookdetailsObj;

            try
            {

                bookdetailsObj = new BookDetails();
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_edit_getdetails";
                cmd.Parameters.AddWithValue("@bookid", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   bookdetailsObj.bookName= Convert.ToString(reader["Book_name"]);
                    bookdetailsObj.authorName = Convert.ToString(reader["Author_name"]);
                    bookdetailsObj.isbnCode = Convert.ToString(reader["ISBN_Code"]);
                    bookdetailsObj.quantityBooks = Convert.ToInt32(reader["Quantity_of_books"]);
                    bookdetailsObj.publishDate = Convert.ToDateTime(reader["Publish_date"]);
                    bookdetailsObj.bookCategory = Convert.ToString(reader["Book_Category"]);
                    bookdetailsObj.quantityBooksIssued = Convert.ToInt32(reader["quantity_of_books_issued"]);

                    lstBookDetails.Add(bookdetailsObj);
                }
                con.Close();
                return lstBookDetails;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static string UpdateBookDetails(int id,int qunatity)
        {
            string result = "";
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string query = "sp_edit1";
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Book_id", id);
                cmd.Parameters.AddWithValue("@Quantity_of_books", qunatity);
                cmd.ExecuteNonQuery();
                result = "Success";
                con.Close();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        internal static string DeleteBookDetails(int id)
        {
            string result = "";
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string query = "sp_delete";
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Book_Id", id);
                cmd.ExecuteNonQuery();
                result = "Success";
                con.Close();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        internal static string IssueBookDetails(int id,int quantityIssue)
        {
            string result = "";
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string query = "sp_issue_inc_bookdetails";
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Book_id", id);

                cmd.Parameters.AddWithValue("@quantity_books_issued", quantityIssue);
                cmd.ExecuteNonQuery();
                result = "Success";
                con.Close();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        internal static string UpdateBookTransactionDetails(int id, DateTime transactionDate, int transactionType, DateTime DateIssue)
        {
            string result = "";
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string query = "sp_issue";
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Book_id", id);

                cmd.Parameters.AddWithValue("@Transaction_date", transactionDate);

                cmd.Parameters.AddWithValue("@Transaction_Type", transactionType);

                cmd.Parameters.AddWithValue("@date_issue_return", DateIssue);
                cmd.ExecuteNonQuery();
                result = "Success";
                con.Close();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        internal static string ReturnBookDetails(int id, int transactionType,int IdMain)
        {
            string result = "";
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string query = "sp_return_update_booktransaction";
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookid", id);
                cmd.Parameters.AddWithValue("@transactiontype", transactionType);
                cmd.Parameters.AddWithValue("@BookTransactionID", IdMain);
                cmd.ExecuteNonQuery();
                result = "Success";
                con.Close();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }

}