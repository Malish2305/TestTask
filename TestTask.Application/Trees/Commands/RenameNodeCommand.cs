using MediatR;
using TestTask.Application.Common.Interfaces;

namespace TestTask.Application.Trees.Commands;

public record RenameNodeCommand(string TreeName, int NodeId, string NewNodeName) : IRequest;

public class RenameNodeCommandHandler : IRequestHandler<RenameNodeCommand>
{
    private readonly ITreeRepository _treeRepository;

    public RenameNodeCommandHandler(ITreeRepository treeRepository)
    {
        _treeRepository = treeRepository;
    }
    
    public async Task Handle(RenameNodeCommand request, CancellationToken cancellationToken)
    {
        await _treeRepository.UpdateNodeName(request.TreeName, request.NodeId, request.NewNodeName);
    }
}