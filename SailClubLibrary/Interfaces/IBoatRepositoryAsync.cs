using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    public interface IBoatRepositoryAsync
    {
        #region Properties
        Task<int> Count { get; }
        #endregion

        #region Methods
        Task<List<Boat>> GetAllBoatsAsync();
        Task AddBoatAsync(Boat boat);
        Task RemoveBoatAsync(string sailNumber);
        Task UpdateBoatAsync(Boat boat);
        Task<Boat?> SearchBoatAsync(string sailNumber);
        Task<List<Boat>> FilterBoatsAsync(string filterCriteria);
        #endregion
    }
}
