using AutoMapper;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Business.Services;
using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using FormCraft.Tests.Commons.Builders.Responses;
using Moq;
using Xunit;

namespace FormCraft.Business.Units.Tests.Forms;

public class GetByIdFormTests
{
    [Fact]
    public async Task GetById_FormNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var formRepositoryMock = new Mock<IFormRepository>();
        formRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>())).ReturnsAsync((Form)null!);
        var mapperMock = new Mock<IMapper>();
        var formBusiness = new FormBusiness(formRepositoryMock.Object, mapperMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => formBusiness.GetById("1"));
    }

    [Fact]
    public async Task GetById_FormFound_ReturnsMappedResponse()
    {
        // Arrange
        var form = new Form();
        var formRepositoryMock = new Mock<IFormRepository>();
        formRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>())).ReturnsAsync(form);
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<FormWithQuestionsResponse>(form))
                  .Returns(FormWithQuestionsResponseBuilder.AFormWithQuestionsResponse.Build());
        var formBusiness = new FormBusiness(formRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await formBusiness.GetById("1");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<FormWithQuestionsResponse>(result);
    }
}