﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Queries.GetBooks;

public class GetBooksQuery
{
    private readonly BookStoreDbContext dbContext;
    private readonly IMapper mapper;
    public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = dbContext.Books
            .Include(x => x.Genre)
            .Include(x => x.Author)
            .OrderBy(x => x.Id).ToList();

        List<BooksViewModel> vm = mapper.Map<List<BooksViewModel>>(bookList);

        //List<BooksViewModel> vm = new List<BooksViewModel>();

        //foreach (var book in bookList)
        //{
        //    vm.Add(new BooksViewModel()
        //    {
        //        Title = book.Title,
        //        Genre = ((GenreEnum)book.GenreId).ToString(),
        //        PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
        //        PageCount = book.PageCount,
        //    });
        //};

        return vm;
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
    }
}
