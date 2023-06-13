using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> getMembers();
        MemberObject getMemberById(int id);
        void updateMember(MemberObject member);
        void deleteMember(int id);
        void addNewMember(MemberObject member);
        IEnumerable<MemberObject> getListAccount(String email);
        MemberObject loginMember(String email,String password);
    }
}
