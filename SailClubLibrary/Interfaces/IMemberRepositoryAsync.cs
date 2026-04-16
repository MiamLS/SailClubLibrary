using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    public interface IMemberRepositoryAsync
    {
        Task<int> Count { get; }
        Task AddMemberAsync(Member member);
        Task RemoveMemberAsync(Member member);
        Task UpdateMemberAsync(Member member);
        Task <List<Member>> GetAllMembersAsync();
        Task PrintAllAsync();
        Task <Member?> SearchMemberAsync(string phoneNumber);
        Task <List<Member>> FilterMembersAsync(string filterCriteria);
    }
}
