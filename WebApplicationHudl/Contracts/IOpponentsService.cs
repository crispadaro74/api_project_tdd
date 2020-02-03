using System.Collections.Generic;
using WebApplicationHudl.Model;

namespace WebApiHudl.Contracts
{
    public interface IOpponentsService
    {
        /// <summary>
        /// Get a list of schedule entries.
        /// </summary>
        /// <returns></returns>
        IEnumerable<OpponentsItem> GetOpponents();

        /// <summary>
        /// Update a single schedule entry.
        /// </summary>
        /// <param name="newItem"></param>
        void Update(OpponentsItem newItem);

        /// <summary>
        /// Create a new schedule entry.
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        OpponentsItem Add(OpponentsItem newItem);

        /// <summary>
        /// Get a single schedule entry by identifier.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        OpponentsItem GetById(int gameId);

        /// <summary>
        /// Remove a schedule entry.
        /// </summary>
        /// <param name="gameId"></param>
        void Remove(int gameId);
    }
}
