using Library.Domain.DTOs.Books;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories.Interfaces;
using Library.WebApi.Controllers;
using Library.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests;

public class BooksControllerTests
{
    private readonly Mock<IBookRepository> _bookRepoMock;
    private readonly BooksController _controller;

    public BooksControllerTests()
    {
        _bookRepoMock = new Mock<IBookRepository>();
        _controller = new BooksController(_bookRepoMock.Object);
    }

    //[Fact]
    public async Task GetBooksBefore2000()
    {
        // arrange
        var books = new List<Book>
        {
            new() { BookId = 1, Title = "Book A", YearPublished = 1995 },
            new() { BookId = 2, Title = "Book B", YearPublished = 1980 }
        };

        _bookRepoMock.Setup(x => x.GetBooksPublishedBefore2000Async())
                     .ReturnsAsync(books);

        // act
        var result = await _controller.GetBooksBefore2000();

        // assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<Book>>>(okResult.Value);
        Assert.True(response.Success);
        Assert.Equal(2, response.Data!.Count());
    }

    //[Fact]
    public async Task GetBooksBefore2000_WhenNoneFound()
    {
        _bookRepoMock.Setup(x => x.GetBooksPublishedBefore2000Async())
                     .ReturnsAsync([]);

        var result = await _controller.GetBooksBefore2000();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<Book>>>(okResult.Value);
        Assert.True(response.Success);
        Assert.Empty(response.Data!);
    }

    //[Fact]
    public async Task CreateBook_BadRequest_WhenModelIsInvalid()
    {
        var moqRepo = new Mock<IBookRepository>();

        var controller = new BooksController(moqRepo.Object);

        controller.ModelState.AddModelError("Title", "El campo titulo es obligatorio");

        var book = new CreateBookDto
        {
            Title = "",
            YearPublished = 2023,
            AuthorId = 1,
            Genre = "Fiction"
        };

        moqRepo.Setup(x => x.AuthorExistsAsync(It.IsAny<int>())).ReturnsAsync(true);

        var result = await controller.Create(book);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(badRequestResult.Value);
        Assert.False(response.Success);
        Assert.Contains("Datos incorrectos", response.Message);
    }

    //[Fact]
    public async Task GetBookById_ReturnsNotFound_WhenBookDoesNotExist()
    {
        var mockBookRepo = new Mock<IBookRepository>();

        mockBookRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))!.ReturnsAsync((Book?)null);

        var controller = new BooksController(mockBookRepo.Object);


        var result = await controller.GetBookById(404);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse<Book>>(notFound.Value);
        Assert.False(response.Success);
        Assert.Equal("NOT_FOUND", response.Code);
    }

    [Fact]
    public async Task GetPopularBooks_ReturnsBooksOrderedByLoanCount()
    {
        var mockRepo = new Mock<IBookRepository>();
        var controller = new BooksController(mockRepo.Object);

        var fakeData = new List<BookWithLoanCountDto>
         {
            new() { BookId = 1, Title = "Libro A", TotalLoans = 15 },
            new() { BookId = 2, Title = "Libro B", TotalLoans = 10 },
            new() { BookId = 3, Title = "Libro C", TotalLoans = 5 }
        };

        mockRepo.Setup(r => r.GetMostLoanedBooksLast6MonthsAsync())
                .ReturnsAsync(fakeData);

        var result = await controller.GetPopularBooks();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<IEnumerable<BookWithLoanCountDto>>>(okResult.Value);

        Assert.True(response.Success);
        Assert.Equal(3, response.Data!.Count());

        var topBook = response.Data.First();
        Assert.Equal("Libro A", topBook.Title);
        Assert.Equal(15, topBook.TotalLoans);
    }


}
