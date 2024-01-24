namespace FormCraft.Entities.Common;

public interface IDated
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
