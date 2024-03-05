using MediatR;
using TestTask.Application.Common.Interfaces;
using TestTask.Domain.Entities;

namespace TestTask.Application.Trees.Queries;

public record GetTreeQuery(string Name) : IRequest<TreeNode>;

public class GetTreeQueryHandler : IRequestHandler<GetTreeQuery, TreeNode>
{
    private readonly ITreeRepository _treeRepository;

    public GetTreeQueryHandler(ITreeRepository treeRepository)
    {
        _treeRepository = treeRepository;
    }
    
    public async Task<TreeNode> Handle(GetTreeQuery request, CancellationToken cancellationToken)
    {
        var tree = await _treeRepository.GetNodeByName(request.Name);

        if (tree is not null)
        {
            return tree;
        }
        
        tree = new TreeNode
        {
            Name = request.Name
        };

        await _treeRepository.CreateTree(tree);

        return tree;
    }
}
