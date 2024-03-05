namespace TestTask.Domain.Entities;

public class TreeNode
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<TreeNode> Nodes { get; set; } = [];
}