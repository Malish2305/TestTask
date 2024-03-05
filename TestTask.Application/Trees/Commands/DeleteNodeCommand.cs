using MediatR;
using TestTask.Application.Common.Interfaces;

namespace TestTask.Application.Trees.Commands;

public record DeleteNodeCommand(string TreeName, int NodeId) : IRequest;

public class DeleteNodeCommandHandler : IRequestHandler<DeleteNodeCommand>
{
    private readonly ITreeRepository _treeRepository;

    public DeleteNodeCommandHandler(ITreeRepository treeRepository)
    {
        _treeRepository = treeRepository;
    }
    
    public async Task Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
    {
        await _treeRepository.DeleteNode(request.TreeName, request.NodeId);
    }
}
