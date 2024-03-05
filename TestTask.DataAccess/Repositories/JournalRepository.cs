using Microsoft.EntityFrameworkCore;
using TestTask.Application.Common.Interfaces;
using TestTask.Domain.Entities;

namespace TestTask.DataAccess.Repositories;

public class JournalRepository : IJournalRepository
{
    private readonly TestTaskDbContext _dbContext;

    public JournalRepository(TestTaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddLog(JournalLog journalLog)
    {
        _dbContext.JournalLogs.Add(journalLog);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<JournalLog> GetById(int id)
    {
        return await _dbContext.JournalLogs.Where(journal => journal.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<JournalLog>> GetRange(int skip, int take, DateTime? from, DateTime? to, string search)
    {
        
        return await _dbContext.JournalLogs
            .Where(log =>
                (from == null || log.TimeStamp >= from) &&
                (to == null || log.TimeStamp <= to) &&
                (string.IsNullOrWhiteSpace(search) || log.Message.Contains(search) || log.Request.Contains(search))
            )
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
}