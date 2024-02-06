using AutoMapper;
using FormCraft.Business.Contracts.Exceptions;
using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Business.Services;
using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using FormCraft.Tests.Commons.Builders.Entities;
using FormCraft.Tests.Commons.Builders.Requests;
using Moq;
using Xunit;

namespace FormCraft.Business.Units.Tests.Forms;

public class UpdateFormTests
{
    [Fact]
    public async Task Update_FormNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var formRepositoryMock = new Mock<IFormRepository>();
        var mapperMock = new Mock<IMapper>();
        var formBusiness = new FormBusiness(formRepositoryMock.Object, mapperMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => formBusiness.Update(UpdateFormRequestBuilder.AnUpdateFormRequest.Build()));
    }

    [Theory]
    [InlineData(StatusEnum.Closed, StatusEnum.InProgress)]
    [InlineData(StatusEnum.Closed, StatusEnum.Validated)]
    [InlineData(StatusEnum.Validated, StatusEnum.Validated)]
    public async Task Update_InvalidStatus_ThrowsBadRequestException(StatusEnum formStatus, StatusEnum requestStatus)
    {
        // Arrange
        var form = new Form { StatusId = formStatus };
        var formRepositoryMock = new Mock<IFormRepository>();
        formRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>())).ReturnsAsync(form);
        var mapperMock = new Mock<IMapper>();
        var formBusiness = new FormBusiness(formRepositoryMock.Object, mapperMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => formBusiness.Update(UpdateFormRequestBuilder.AnUpdateFormRequest.WithStatusId(requestStatus).Build()));
    }

    [Fact]
    public async Task Update_ValidRequest_UpdatesFormAndReturnsResponse()
    {
        // Arrange
        var form = FormBuilder.AForm.WithLabel("Original Label")
                                    .WithStatusId(StatusEnum.InProgress)
                                    .WithFormTypeId(FormTypeEnum.Evaluation)
                                    .Build();
        var formRepositoryMock = new Mock<IFormRepository>();
        formRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>())).ReturnsAsync(form);
        var updatedForm = new Form() { Label = "Updated Label", FormTypeId = FormTypeEnum.Survey, StatusId = StatusEnum.Validated};
        var formResponse = new FormResponse() { Label = "Updated Label", FormTypeId = FormTypeEnum.Survey, StatusId = StatusEnum.Validated};
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<FormResponse>(It.IsAny<Form>())).Returns(formResponse);
        var formBusiness = new FormBusiness(formRepositoryMock.Object, mapperMock.Object);
        var request = UpdateFormRequestBuilder.AnUpdateFormRequest
            .WithId("1")
            .WithLabel("Updated Label")
            .WithStatusId(StatusEnum.Validated)
            .WithFormTypeId(FormTypeEnum.Survey)
            .Build();

        // Act
        var result = await formBusiness.Update(request);

        // Assert
        formRepositoryMock.Verify(repo => repo.Update(It.IsAny<Form>()), Times.Once);
        Assert.Equal(request.Label, result.Label);
        Assert.Equal(request.StatusId, result.StatusId);
        Assert.Equal(request.FormTypeId, result.FormTypeId);
    }
}
