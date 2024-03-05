using TestTask.Domain.Entities;

namespace TestTask.Application.Common.Interfaces;

public interface IJournalRepository
{
    Task AddLog(JournalLog journalLog);
    Task<JournalLog> GetById(int id);
    Task<IEnumerable<JournalLog>> GetRange(int skip, int take, DateTime? from, DateTime? to, string search);
}