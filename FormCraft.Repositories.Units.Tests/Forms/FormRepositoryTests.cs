using FormCraft.Repositories.Database.Contexts;
using FormCraft.Repositories.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FormCraft.Repositories.Units.Tests.Forms;

public class FormRepositoryTests
{
    [Fact]
    public async Task Search_ReturnsCorrectResults()
    {
        // Arrange
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("FORMCRAFT");

        var context = new ApplicationDbContext(builder.Options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Forms.AddRange(TestData.FormTestData());

        context.SaveChanges();

        var formRepository = new FormRepository(context);

        // Act
        var result = await formRepository.Search(["InProgress"], ["Survey"], "Form", null, null);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Contains(result, f => f.Label == "Form 1");
    }
}