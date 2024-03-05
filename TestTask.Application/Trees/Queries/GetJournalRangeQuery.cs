using MediatR;
using TestTask.Application.Common.Interfaces;
using TestTask.Domain.Entities;

namespace TestTask.Application.Trees.Queries;

public record GetJournalRangeQuery(int Skip, int Take, DateTime? from, DateTime? to, string search) : IRequest<IEnumerable<JournalLog>>;

public class GetJournalRangeQueryHandler : IRequestHandler<GetJournalRangeQuery, IEnumerable<JournalLog>>
{
    private readonly IJournalRepository _journalRepository;

    public GetJournalRangeQueryHandler(IJournalRepository journalRepository)
    {
        _journalRepository = journalRepository;
    }

    public async Task<IEnumerable<JournalLog>> Handle(GetJournalRangeQuery request, CancellationToken cancellationToken)
    {
        return await _journalRepository.GetRange(request.Skip, request.Take, request.from, request.to, request.search);
    }
}