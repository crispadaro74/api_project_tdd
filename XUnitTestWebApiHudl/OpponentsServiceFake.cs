using System;
using System.Collections.Generic;
using System.Linq;
using WebApiHudl.Contracts;
using WebApplicationHudl.Model;

namespace XUnitTestWebApiHudl
{
    class OpponentsServiceFake : IOpponentsService
    {
        private readonly List<OpponentsItem> _schedule;

        public OpponentsServiceFake()
        {
            _schedule = new List<OpponentsItem>()
            {
                new OpponentsItem()
                {
                    GameId = 1234567,
                    SqlId = 1234567,
                    Date = DateTime.Now,
                    Opponent = "TestOpponent",
                    OpponentId = 123456,
                    IsHome = true,
                    GameType = 0,
                    Categories = new string[] { }
                },
                new OpponentsItem()
                {
                    GameId = 2233445,
                    SqlId = 2233445,
                    Date = DateTime.Now.AddDays(1),
                    Opponent = "TestOpponentX",
                    OpponentId = 654321,
                    IsHome = false,
                    GameType = 1,
                    Categories = new string[] { }
                },
                new OpponentsItem()
                {
                    GameId = 6677889,
                    SqlId = 6677889,
                    Date = DateTime.Now.AddDays(2),
                    Opponent = "TestOpponentY",
                    OpponentId = 123654,
                    IsHome = true,
                    GameType = 2,
                    Categories = new string[] { }
                }
            };
        }

        public OpponentsItem Add(OpponentsItem newItem)
        {
            Random rnd = new Random();
            var id = rnd.Next();
            newItem.GameId = id;
            newItem.SqlId = id;
            _schedule.Add(newItem);
            return newItem;
        }

        public OpponentsItem GetById(int gameId)
        {
            return _schedule.Where(a => a.GameId == gameId).FirstOrDefault();
        }

        public IEnumerable<OpponentsItem> GetOpponents()
        {
            return _schedule;
        }

        public void Remove(int gameId)
        {
            var existing = _schedule.First(a => a.GameId == gameId);
            _schedule.Remove(existing);
        }

        public void Update(OpponentsItem newItem)
        {
            var existing = _schedule.Where(a => a.GameId == newItem.GameId).FirstOrDefault();

            if (existing != null)
            {
                existing.Categories = newItem.Categories;
                existing.Date = newItem.Date;
                existing.GameType = newItem.GameType;
                existing.IsHome = newItem.IsHome;
                existing.Opponent = newItem.Opponent;
                existing.OpponentId = newItem.OpponentId;
            }
        }
    }
}
