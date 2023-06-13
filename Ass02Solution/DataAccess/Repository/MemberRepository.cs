using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void addNewMember(MemberObject member) => MemberDAO.Instance.addNewMember(member);

        public void deleteMember(int id) => MemberDAO.Instance.deleteMember(id);

        public MemberObject getMemberById(int id) => MemberDAO.Instance.getMemberById(id);

        public IEnumerable<MemberObject> getMembers() => MemberDAO.Instance.getListMember();

        public void updateMember(MemberObject member) => MemberDAO.Instance.updateMember(member);

        public MemberObject loginMember(String email,String password) => MemberDAO.Instance.loginMember(email, password);


        IEnumerable<MemberObject> IMemberRepository.getListAccount(string email) => MemberDAO.Instance.getListAccount(email);
    }
}
