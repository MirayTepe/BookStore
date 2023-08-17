using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperation.Commands.CreateAuthor
{
    public class CreateAuthorCommand{ 
        public CreateAuthorViewModel Model{get;set;}
        private readonly BookStoreDbContext _dbContext;

        private readonly IMapper _mapper;

        public CreateAuthorCommand(BookStoreDbContext dbContext,IMapper mapper){
            _dbContext=dbContext;
            _mapper=mapper;

        }

       public void Handle(){
            var author=_dbContext.Authors.SingleOrDefault(x => x.FirstName == Model.FirstName);
            if(author is not null)
                throw  new InvalidOperationException("Yazar zaten mevcut");
            author=_mapper.Map<Author>(Model);//new Book();
            // book.Title=Model.Title;
            // book.PublishDate=Model.PublishDate;
            // book.PageCount=Model.PageCount;
            // book.GenreId=Model.GenreId;
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();

       }
        public class CreateAuthorViewModel{
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }


       }
 


    }
    
    

}