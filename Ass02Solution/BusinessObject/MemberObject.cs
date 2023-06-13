using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class MemberObject
    {
        private int _memberId;

        public int MemberId
        {
            get { return _memberId; }
            set { _memberId = value; }
        }

        private String _email;

        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }


        private String _CompanyName;

        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        private String _city;

        public String City
        {
            get { return _city; }
            set { _city = value; }
        }
        private String _country;

        public String County
        {
            get { return _country; }
            set { _country = value; }
        }

        private String _password;

        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public MemberObject()
        {

        }
        public MemberObject(int memberId,String email, String companyName, String city, String country,String password)
        {
            MemberId = memberId;
            Email = email;
            CompanyName = companyName;
            City = city;
            County = country;
            Password = password;
        }



    }
}
