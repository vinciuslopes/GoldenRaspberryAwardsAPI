using GoldenRaspberryAPI.Data;
using System;
using System.Linq;

namespace GoldenRaspberryAPI.Service
{
    public class AwardIntervalService
    {
        private readonly AppDbContext _context;

        public AwardIntervalService(AppDbContext context)
        {
            _context = context;
        }

        public object GetAwardIntervals()
        {
            try
            {
                var producers = _context.Movies
                .Where(p => p.Winner)
                .AsEnumerable()
                .GroupBy(g => g.Producers)
                .Select(s => new
                {
                    Producer = s.Key,
                    Wins = s.OrderBy(m => m.Year).ToList()
                })
                .Where(s => s.Wins.Count > 1)
                .ToList();

                var intervals = producers.SelectMany(p => p.Wins.Zip(p.Wins.Skip(1), (prev, next) => new
                {
                    Producer = p.Producer,
                    Interval = next.Year - prev.Year,
                    PreviousWin = prev.Year,
                    FollowingWin = next.Year
                })).ToList();

                int bigger = intervals.Max(i => i.Interval);
                int smaller = intervals.Min(i => i.Interval);

                return new
                {
                    min = intervals.OrderBy(i => i.Interval).Where(w => w.Interval == smaller),
                    max = intervals.OrderByDescending(i => i.Interval).Where(w => w.Interval == bigger)
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro: {ex.Message}", ex);
            }
        }
    }
}
