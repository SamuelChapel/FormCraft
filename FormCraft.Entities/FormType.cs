namespace FormCraft.Entities;

public enum FormTypeEnum { Survey = 1, Comment = 2, Evaluation = 3 }

public class FormType
{
    public FormTypeEnum Id { get; set; }
    public string Label { get; set; } = null!;
}
