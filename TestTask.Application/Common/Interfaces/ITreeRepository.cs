using TestTask.Domain.Entities;

namespace TestTask.Application.Common.Interfaces;

public interface ITreeRepository
{
    Task<TreeNode> GetNodeByName(string name);
    Task CreateNode(TreeNode node, string treeName, int parentNodeId);
    Task CreateTree(TreeNode node);
    Task DeleteNode(string treeName, int nodeId);
    Task UpdateNodeName(string treeName, int nodeId, string newNodeName);
}