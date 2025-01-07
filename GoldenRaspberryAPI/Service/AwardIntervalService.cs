using GoldenRaspberryAPI.Data;
using GoldenRaspberryAPI.Model;
using Microsoft.EntityFrameworkCore;
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
                var producersWithIntervals = _context.Movies
                    .Where(m => m.Winner)
                    .ToList()
                    .SelectMany(m => m.Producers
                        .Split(new[] { ",", "and" }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(producer => new { Producer = producer.Trim(), Movie = m }))
                    .GroupBy(pm => pm.Producer)
                    .Select(g => new
                    {
                        Producer = g.Key,
                        Intervals = g.Select(x => x.Movie.Year)
                            .OrderBy(year => year)
                            .Zip(g.Select(x => x.Movie.Year).OrderBy(year => year).Skip(1), (prev, next) => new 
                            {
                                Interval = next - prev,
                                PreviousWin = prev,
                                FollowingWin = next
                            }).ToList()
                    }).ToList();

                var intervals = producersWithIntervals
                    .SelectMany(p => p.Intervals, (p, i) => new ProducerAward
                    {
                        Producer = p.Producer,
                        Interval = i.Interval,
                        PreviousWin = i.PreviousWin,
                        FollowingWin = i.FollowingWin
                    })
                    .ToList();

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
                var allMovies = _context.Movies.ToList();
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
