function GetBookDetails() {
    debugger;
    var myPathName = window.location.pathname.split('/')[1];
    var myHostName = window.location.protocol + "//" + window.location.host + "/" + myPathName;
    var url = myHostName + "/GetBookDetails";
    var bodyContent = '<table id="bookDetails"  class="table table-striped table-bordered"><thead>' +
       '<tr><th>Book Name</th> <th>Author Name</th><th>ISBN Code</th><th>Quantity</th><th>Publish Date</th><th>Category</th><th>Quantity issued</th>' +
                           '<th>Action</th></tr></thead><tbody>';
    $.get(url, function (data) {
        $.each(JSON.parse(data), function (i, d) {
            bodyContent += '<tr><td>' + d.bookName + '</td>';
            bodyContent += '<td>' + d.authorName + '</td>'  ;
            bodyContent += '<td>' + d.isbnCode + '</td>';
            bodyContent += '<td>' + d.quantityBooks + '</td>';
            bodyContent += '<td>' + d.publishDate.split("T")[0] + '</td>';
            bodyContent += '<td>' + d.bookCategory + '</td>';
            bodyContent += '<td>' + d.quantityBooksIssued + '</td>';
            bodyContent += '<td><a href="#" onclick="IssueBookDetails(' + d.id + ',' + d.quantityBooks + ',' + d.quantityBooksIssued + ')">Issue</a>&nbsp;<a href="#" data-toggle="modal" data-target="#editBook" data-dismiss="modal" onclick="EditBookDetails(' + d.id + ')">Edit</a>&nbsp;<a href="#" onclick="DeleteBookDetails(' + d.id + ',' + d.quantityBooks + ',' + d.quantityBooksIssued + ')">Delete</a></td>';
            bodyContent += "</tr>";
        });
        bodyContent += "</tbody></table>";
        $("#bookDetailsDiv").html(bodyContent);
        $('#bookDetails').DataTable({
            "oLanguage": { "sSearch": "" },
            
        });
        $('#bookDetails').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.addClass('form-control  search-focus');
            search_input.css({ "border-radius": "4px", "height": "32px", "width": "150px", "padding-right": "0px", "padding-left": "25px" });
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.css({ "border-radius": "4px", "height": "26px" });
            length_sel.addClass('search-focus ');
            search_input.before('<div class="inner-addon left-addon"><i class="glyphicon glyphicon-search" style="margin-top:-2px;"></i>');
            search_input.append('</div>');


        });
    });
}

function GetBookTransactionDetails() {
    //debugger;
    var myPathName = window.location.pathname.split('/')[1];
    var myHostName = window.location.protocol + "//" + window.location.host + "/" + myPathName;
    var url = myHostName + "/GetBookTransactionDetails";
    var bodyContent = '<table id="bookTransactionDetails"  class="table table-striped table-bordered"><thead>' +
       '<tr><th>Book ID</th> <th>Transaction Date</th><th>Transaction Type</th><th>Date(Issue/Return)</th>' +
                           '</tr></thead><tbody>';
    $.get(url, function (data) {
        $.each(JSON.parse(data), function (i, d) {
            bodyContent += '<tr><td>' + d.bookTransactionId + '</td>';
            bodyContent += '<td>' + d.transactionDate.split("T")[0] + '</td>';
            if (d.transactionType == 1)
                bodyContent += '<td><a href="#" onclick="ReturnBookDetails(' + d.bookTransactionId + ',' + d.bookTransactionIdMain + ')">Return</a></td>';
            else
                bodyContent += '<td>Returned</td>'
            bodyContent += '<td>' + d.dateIssueReturn.split("T")[0] + '</td>';
            
            bodyContent += "</tr>";
        });
        bodyContent += "</tbody></table>";
        $("#bookTransactionDetails").html(bodyContent);
        $('#bookTransactionDetails').DataTable({
            "oLanguage": { "sSearch": "" },
            
        });
        $('#bookTransactionDetails').each(function () {
            var datatable = $(this);
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.addClass('form-control  search-focus');
            search_input.css({ "border-radius": "4px", "height": "32px", "width": "150px", "padding-right": "0px", "padding-left": "25px" });
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.css({ "border-radius": "4px", "height": "26px" });
            length_sel.addClass('search-focus ');
            search_input.before('<div class="inner-addon left-addon"><i class="glyphicon glyphicon-search" style="margin-top:-2px;"></i>');
            search_input.append('</div>');


        });
    });
}


function AddBookDetails()
{
    debugger
    var bookName = $("#bookName").val();
    var authorName = $("#authorName").val();
    var isbnCode = $("#isbnCode").val();
    var quantityBooks = $("#quantityBooks").val();
    var publishDate = $("#publishDate").val();
    var bookCategory = $("#bookCategory").val();
    var quantityBooksIssued = 0;
    $("#quantityBooksIssued").val(quantityBooksIssued);
    var myPathName = window.location.pathname.split('/')[1];
    var myHostName = window.location.protocol + "//" + window.location.host + "/" + myPathName;
    //var url = myHostName + "/BookLibraryHome/AddBookDetails";
    var url = myHostName + "/AddBookDetails";
    $.get(url,
     {
         bookName: bookName,
         authorName: authorName,
         isbnCode: isbnCode,
         quantityBooks: quantityBooks,
         publishDate: publishDate,
         bookCategory: bookCategory,
         quantityBooksIssued: quantityBooksIssued
     }, function (Message) {
         debugger;
         var Message = Message;
         if (Message == "Success") {
             $('.statusMessage').fadeIn(1000).fadeOut(100000).html("Book Added Successfully").css("color", "green");
             GetBookDetails();
         }
         else {
             $('.statusMessage').fadeIn(1000).fadeOut(100000).html("Error Occured").css("color", "red");
             return false;
                       
         }
     });
}


function EditBookDetails(Id)
{
    var myPathName = window.location.pathname.split('/')[1];
    var myHostName = window.location.protocol + "//" + window.location.host + "/" + myPathName;
    //var url = myHostName + "/BookLibraryHome/EditBookDetails";
    var url = myHostName + "/EditBookDetails";
    $.get(url, { id: Id }, function (data) {
        $.each(JSON.parse(data), function (i, d) {
            $("#editBookName").val(d.bookName);
            $("#editAuthorName").val(d.authorName);
            $("#editIsbn").val(d.isbnCode);
            $("#editQuantity").val(d.quantityBooks);
            var publishDate = d.publishDate.split("T");
            $("#editPublish").val(publishDate[0]);
            $("#editCategory").val(d.bookCategory);
            $("#editQuantityBooksIssued").val(d.quantityBooksIssued);
            if ($("#editQuantity").val() != "") {
                if ($("#btnUpdate").hasClass("disabled"))
                    $("#btnUpdate").removeClass("disabled")
            }
            
            $('#hdnBookId').val(Id);
            

        });

    });
}

function UpdateBookDetails(Id)
{
   
    var quantity = $("#editQuantity").val();
    var quantityIssue = $("#editQuantityBooksIssued").val();
    if (quantity-quantityIssue<0)
    {
        $(".editStatusMessage").html("Quantity should be greater than Quantity Issue").css("color", "red");
        return false;
    }
    else
    {
        $(".editStatusMessage").html("")
    }
    var myPathName = window.location.pathname.split('/')[1];
    var myHostName = window.location.protocol + "//" + window.location.host + "/" + myPathName;
    //var url = myHostName + "/BookLibraryHome/UpdateBookDetails";
    var url = myHostName + "/UpdateBookDetails";
    $.get(url,
       {
           id: Id,
           qunatity: quantity
       }, function (Message) {
           var Message = Message;
           if (Message == "Success") {
               $('.statusMessage').fadeIn(1000).fadeOut(100000).html("Book Details Updated Successfully ").css("color", "green");
               GetBookDetails();
               $('.modal').modal('hide');
               
           }
           else {
               $$('.statusMessage').fadeIn(1000).fadeOut(100000).html("Error Occured").css("color","red");
               return false;
               // window.location.replace(myHostName + "/Maintenance/ManageConferenceDetails?tab=0&msg=Updated");
           }

       });

}

function DeleteBookDetails(Id,quantity,quantityIssue)
{
    if (quantity-0!=0 || quantityIssue-0!=0) {
        $(".statusMessage").html("Quantity and Quantity Issue should be 0 to Delete").css("color", "red");
        return false;
    }
    else {
        $(".statusMessage").html("");
    }
    if (confirm("Are you sure want to Delete?")) {
        var myPathName = window.location.pathname.split('/')[1];
        var myHostName = window.location.protocol + "//" + window.location.host + "/" + myPathName;
        //var url = myHostName + "/BookLibraryHome/DeleteBookDetails";
        var url = myHostName + "/DeleteBookDetails";
        $.get(url,
           {
               id: Id,
           }, function (Message) {
               var Message = Message;
               if (Message == "Success") {
                   $('.statusMessage').fadeIn(1000).fadeOut(100000).html("Book Details Deleted Successfully ").css("color", "green");
                   GetBookDetails();

               }
               else {
                   $('.statusMessage').fadeIn(1000).fadeOut(100000).html("Error Occured").css("color", "red");
                   return false;
                   // window.location.replace(myHostName + "/Maintenance/ManageConferenceDetails?tab=0&msg=Updated");
               }

           });
    }
}

function IssueBookDetails(Id,quantity,quantityIssue)
{
    if (quantity - 0 == 0 || quantity - quantityIssue <= 0) {
        if (quantity - 0 == 0) {
            $(".statusMessage").html("Book Not Available").css("color", "red");
            return false;
        }
        if (quantity - quantityIssue <= 0) {
            $(".statusMessage").html("Book Stock Over!").css("color", "red");
            return false;
        }
    }
    else {
        $(".statusMessage").html("");
    }
    var myPathName = window.location.pathname.split('/')[1];
    var myHostName = window.location.protocol + "//" + window.location.host + "/" + myPathName;
    //var url = myHostName + "/BookLibraryHome/IssueBookDetails";
    var url = myHostName + "/IssueBookDetails";
    $.get(url,
       {
           id: Id,
           quantityIssue: quantityIssue,
       }, function (Message) {
           var Message = Message;
           if (Message == "Success") {
               $('.statusMessage').fadeIn(1000).fadeOut(100000).html("Book Issued Successfully ").css("color", "green");
              
               setTimeout(function(){ 
                   location.reload();
               }, 1000);
              
           }
           else {
               $('.statusMessage').fadeIn(1000).fadeOut(100000).html("Error Occured").css("color", "red");
               return false;
               // window.location.replace(myHostName + "/Maintenance/ManageConferenceDetails?tab=0&msg=Updated");
           }

       });

}

function ReturnBookDetails(Id,IdMain)
{
    var transactionType = 0;
    var myPathName = window.location.pathname.split('/')[1];
    var myHostName = window.location.protocol + "//" + window.location.host + "/" + myPathName;
    //var url = myHostName + "/BookLibraryHome/ReturnBookDetails";
    var url = myHostName + "/ReturnBookDetails";
    $.get(url,
       {
           id: Id,
           IdMain:IdMain,
           transactionType: transactionType,
       }, function (Message) {
           var Message = Message;
           if (Message == "Success") {
               $('.statusMessage').fadeIn(1000).fadeOut(100000).html("Book Returned Successfully ").css("color", "green");

               setTimeout(function () {
                   location.reload();
               }, 1000);

           }
           else {
               $('.statusMessage').fadeIn(1000).fadeOut(100000).html("Error Occured").css("color", "red");
               return false;
               // window.location.replace(myHostName + "/Maintenance/ManageConferenceDetails?tab=0&msg=Updated");
           }

       });
}