using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand{
        public UpdateBookViewModel Model {get; set;}
        private readonly IBookStoreDbContext _dbContext;
        public int BookId {get; set;}
        public UpdateBookCommand(IBookStoreDbContext dbContext){
            _dbContext=dbContext;

        }

         public void Handle(){
              var book=_dbContext.Books.SingleOrDefault(x =>x.Id==BookId);
            if(book is null)
                throw new InvalidOperationException("Kitap bulunamadı.");
            
            book.GenreId=Model.GenreId!=default? Model.GenreId:book.GenreId;
            book.PageCount=Model.PageCount!=default?Model.PageCount:book.PageCount;
            book.PublishDate=Model.PublishDate!=default?Model.PublishDate:book.PublishDate;
            book.Title=Model.Title!=default?Model.Title:book.Title;


            _dbContext.SaveChanges();
        }

    }
    public class UpdateBookViewModel{
         public string Title{get; set;} 
         public int GenreId{get; set;}
         public int PageCount{get; set;}
         public DateTime PublishDate{get; set;}


    }
    
    
}