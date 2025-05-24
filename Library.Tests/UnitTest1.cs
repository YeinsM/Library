using FluentAssertions;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Repositories.Interfaces;
using Library.WebApi.Controllers;
using Library.WebAPI.Controllers;
using Library.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Library.Tests.Controllers;

public class BooksControllerTests
{
    private readonly Mock<IBookRepository> _bookRepoMock;
    private readonly Mock<IGenericRepository<Author>> _authorRepoMock;
    private readonly BooksController _controller;

    public BooksControllerTests()
    {
        _bookRepoMock = new Mock<IBookRepository>();
        _authorRepoMock = new Mock<IGenericRepository<Author>>();
        _controller = new BooksController(_bookRepoMock.Object, _authorRepoMock.Object);
    }

    [Fact]
    public async Task GetBooksBefore2000_ReturnsFilteredBooks()
    {
        // Arrange
        var books = new List<Book>
        {
            new() { BookId = 1, Title = "Old Book", YearPublished = 1990 },
            new() { BookId = 2, Title = "New Book", YearPublished = 2005 }
        };
        _bookRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

        // Act
        var result = await _controller.GetBooksBefore2000();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var ok = result as OkObjectResult;
        var response = ok?.Value as ;
        var filtered = ((IEnumerable<dynamic>)response!.Data!).ToList();

        filtered.Count.Should().Be(1);
        filtered[0].Title.Should().Be("Old Book");
    }

    [Fact]
    public async Task CreateBook_InvalidAuthor_ReturnsBadRequest()
    {
        // Arrange
        var book = new Book { Title = "Invalid", AuthorId = 999, PublicationYear = 2000 };
        _authorRepoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Author?)null);

        // Act
        var result = await _controller.Create(book);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var response = (result as BadRequestObjectResult)!.Value as ApiResponse<object>;
        response!.Code.Should().Be("AUTHOR_NOT_FOUND");
    }

    [Fact]
    public async Task CreateBook_ValidAuthor_ReturnsOk()
    {
        // Arrange
        var book = new Book { Title = "Valid", AuthorId = 1, PublicationYear = 2000 };
        _authorRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Author { AuthorId = 1, Name = "Author" });
        _bookRepoMock.Setup(r => r.AddAsync(book)).Returns(Task.CompletedTask);
        _bookRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Create(book);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var response = (result as OkObjectResult)!.Value as ApiResponse<Book>;
        response!.Success.Should().BeTrue();
        response.Data!.Title.Should().Be("Valid");
    }
}
