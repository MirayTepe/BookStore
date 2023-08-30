using AutoMapper;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;
using static WebApi.Application.AuthorOperation.Commands.CreateAuthor.CreateAuthorCommand;
using static WebApi.Application.AuthorOperation.Commands.UpdateAuthor.UpdateAuthorCommand;
using static WebApi.Application.AuthorOperation.Queries.GetAuthorDetail.GetAuthorDetailQuery;
using static WebApi.Application.AuthorOperation.Queries.GetAuthors.GetAuthorsQuery;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Common
{
    public class MappingProfile:Profile{
        public MappingProfile(){
            CreateMap<CreateBookViewModel, Book>();
            CreateMap<Book,BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<Genre, GenreViewModel>();
			CreateMap<Genre, GenreDetailViewModel>();
			CreateMap<CreateGenreViewModel, Genre>();
			CreateMap<UpdateGenreViewModel, Genre>();
			
			CreateMap<Author, AuthorsViewModel>();
			CreateMap<Author, AuthorDetailViewModel>();
			CreateMap<CreateAuthorViewModel, Author>();
			CreateMap<UpdateAuthorViewModel, Author>();

            CreateMap<CreateUserModel,User>();



        }
    }
};