using System.Security;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Common.Interfaces;
using TestTask.Domain.Entities;
using TestTask.Domain.Exceptions;

namespace TestTask.DataAccess.Repositories;

public class TreeRepository : ITreeRepository
{
    private readonly TestTaskDbContext _dbContext;

    public TreeRepository(TestTaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TreeNode> GetNodeByName(string name)
    {
        return await _dbContext.TreeNodes.FirstOrDefaultAsync(tree => tree.Name == name);
    }

    public async Task CreateNode(TreeNode node, string treeName, int parentNodeId)
    {
        var parent = await _dbContext.TreeNodes.FirstOrDefaultAsync(tree => tree.Id == parentNodeId);

        if (parent is null)
        {
            throw new SecureException($"Node with ID = {parentNodeId} was not found");
        }

        if (parent.Name != treeName)
        {
            throw new SecureException("Requested node was found, but it doesn't belong your tree");
        }
        
        if (parent.Nodes.Exists(tree => tree.Name == node.Name))
        {
            throw new SecureException("Duplicate name");
        }
        
        parent.Nodes.Add(node);
        _dbContext.Update(parent);
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateTree(TreeNode node)
    {
        await _dbContext.TreeNodes.AddAsync(node);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteNode(string treeName, int nodeId)
    {
        var node = await _dbContext.TreeNodes.FirstOrDefaultAsync(tree => tree.Id == nodeId);

        if (node is null)
        {
            throw new SecureException($"Node with ID = {nodeId} was not found");
        }
        
        if (node.Name != treeName)
        {
            throw new SecureException("Requested node was found, but it doesn't belong your tree");
        }

        if (node.Nodes.Any())
        {
            throw new SecureException("You have to delete all children nodes first");
        }
        
        _dbContext.TreeNodes.Remove(node);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateNodeName(string treeName, int nodeId, string newNodeName)
    {
        var node = await _dbContext.TreeNodes.FirstOrDefaultAsync(tree => tree.Id == nodeId);
        var tree = await _dbContext.TreeNodes.FirstOrDefaultAsync(tree => tree.Name == treeName);
        
        if (node is null)
        {
            throw new SecureException($"Node with ID = {nodeId} was not found");
        }
        
        if (tree is null)
        {
            throw new SecureException($"Tree with name = {treeName} was not found");
        }

        if (tree.Nodes.FirstOrDefault(tree => tree.Id == nodeId) is null)
        {
            throw new SecureException("Requested node was found, but it doesn't belong your tree");
        }
        
        if (node.Name == treeName)
        {
            throw new SecureException("Couldn't rename root node");
        }

        node.Name = newNodeName;
        _dbContext.Update(node);
        await _dbContext.SaveChangesAsync();
    }
}