namespace FormCraft.Entities;

public enum StatusEnum { InProgress = 1, Validated = 2, Closed = 3 }

public class Status
{
    public StatusEnum Id { get; set; }
    public string Label { get; set; } = null!;
}
