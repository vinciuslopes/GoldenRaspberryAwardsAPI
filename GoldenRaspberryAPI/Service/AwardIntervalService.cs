using GoldenRaspberryAPI.Data;
using GoldenRaspberryAPI.Model;
using System;
using System.Collections.Generic;
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

                var intervals = producers.SelectMany(p => p.Wins.Zip(p.Wins.Skip(1), (prev, next) => new ProducerAward
                {
                    Producer = p.Producer,
                    Interval = next.Year - prev.Year,
                    PreviousWin = prev.Year,
                    FollowingWin = next.Year
                })).ToList();

                int bigger = intervals.Max(i => i.Interval);
                int smaller = intervals.Min(i => i.Interval);

                AwardIntervalsResponse response = new AwardIntervalsResponse();
                response.Min = new List<ProducerAward>();
                response.Max = new List<ProducerAward>();

                response.Min.AddRange(intervals.OrderBy(i => i.Interval).Where(w => w.Interval == smaller));
                response.Max.AddRange(intervals.OrderByDescending(i => i.Interval).Where(w => w.Interval == bigger));

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro: {ex.Message}", ex);
            }
        }

        public List<Movie> GetMovies()
        {
            try
            {
                var movies = new List<Movie>();
                var allMovies = _context.Movies;
                foreach (var movie in allMovies)
                {
                    movies.Add(new Movie
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Producers = movie.Producers,
                        Studios = movie.Studios,
                        Winner = movie.Winner,
                        Year = movie.Year
                    });
                }

                return movies;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar todos os filmes. " + ex.Message);
            }
        }
    }
}
