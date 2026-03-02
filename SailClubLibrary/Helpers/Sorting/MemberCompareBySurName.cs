using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Helpers.Sorting
{
    public class MemberCompareBySurName : IComparer<Member>
    {
        public int Compare(Member? x, Member? y)
        {
            return x.SurName.CompareTo(y.SurName);
        }
    }
}
