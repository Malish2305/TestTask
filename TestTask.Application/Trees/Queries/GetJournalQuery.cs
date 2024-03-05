using MediatR;
using TestTask.Application.Common.Interfaces;
using TestTask.Domain.Entities;

namespace TestTask.Application.Trees.Queries;

public record GetJournalQuery(int Id) : IRequest<JournalLog>;

public class GetJournalQueryHandler : IRequestHandler<GetJournalQuery, JournalLog>
{
    private readonly IJournalRepository _journalRepository;

    public GetJournalQueryHandler(IJournalRepository journalRepository)
    {
        _journalRepository = journalRepository;
    }
    
    public async Task<JournalLog> Handle(GetJournalQuery request, CancellationToken cancellationToken)
    {
        return await _journalRepository.GetById(request.Id);
    }
}