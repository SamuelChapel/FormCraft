namespace FormCraft.Entities;

public enum QuestionTypeEnum { Open = 1, RadioButton = 2, Checkbox = 3, Dropdown = 4 }

public class QuestionType
{
    public QuestionTypeEnum Id { get; set; }
    public string Label { get; set; } = null!;
}
