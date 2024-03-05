namespace TestTask.Api.ResponseModels;

public class TreeNodeResponseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<TreeNodeResponseModel> Nodes { get; set; }
}