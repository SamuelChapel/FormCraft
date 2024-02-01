using FormCraft.Entities;

namespace FormCraft.WebApp.Models;

public class CreateFormModel
{
    public string? Id { get; set; }
    public string Label { get; set; } = "Form Title";
    public string? CreatorId { get; set; }
    public FormTypeEnum FormType { get; set; }
    public StatusEnum? StatusId { get; set; }
}
