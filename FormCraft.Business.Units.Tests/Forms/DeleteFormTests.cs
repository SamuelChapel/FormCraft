using AutoMapper;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Business.Services;
using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using Moq;
using Xunit;

namespace FormCraft.Business.Units.Tests.Forms;

public class DeleteFormTests
{
    [Fact]
    public async Task Delete_FormNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var formRepositoryMock = new Mock<IFormRepository>();
        formRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>())).ReturnsAsync((Form)null!);
        var mapperMock = new Mock<IMapper>();
        var formBusiness = new FormBusiness(formRepositoryMock.Object, mapperMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => formBusiness.Delete(new DeleteFormRequest("1"), "creatorId", false));
    }

    [Fact]
    public async Task Delete_NotAuthorized_ThrowsBadRequestException()
    {
        // Arrange
        var form = new Form { CreatorId = "otherId", StatusId = StatusEnum.InProgress };
        var formRepositoryMock = new Mock<IFormRepository>();
        formRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>())).ReturnsAsync(form);
        var mapperMock = new Mock<IMapper>();
        var formBusiness = new FormBusiness(formRepositoryMock.Object, mapperMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => formBusiness.Delete(new DeleteFormRequest("1"), "creatorId", false));
    }

    [Fact]
    public async Task Delete_FormNotInProgress_ThrowsException()
    {
        // Arrange
        var form = new Form { CreatorId = "creatorId", StatusId = StatusEnum.Validated };
        var formRepositoryMock = new Mock<IFormRepository>();
        formRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>())).ReturnsAsync(form);
        var mapperMock = new Mock<IMapper>();
        var formBusiness = new FormBusiness(formRepositoryMock.Object, mapperMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => formBusiness.Delete(new DeleteFormRequest("1"), "creatorId", true));
    }

    [Fact]
    public async Task Delete_ValidRequest_DeletesForm()
    {
        // Arrange
        var form = new Form { CreatorId = "creatorId", StatusId = StatusEnum.InProgress };
        var formRepositoryMock = new Mock<IFormRepository>();
        formRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>())).ReturnsAsync(form);
        var mapperMock = new Mock<IMapper>();
        var formBusiness = new FormBusiness(formRepositoryMock.Object, mapperMock.Object);

        // Act
        await formBusiness.Delete(new DeleteFormRequest("1"), "creatorId", true);

        // Assert
        formRepositoryMock.Verify(repo => repo.Delete(form), Times.Once);
    }
}
