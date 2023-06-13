using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BusinessObject;

namespace DataAccess
{
    public class MemberDAO : BaseDAL
    {
        private static MemberDAO instance = null;
        private static readonly object instancelock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<MemberObject> getListMember()
        {
            IDataReader dataReader = null;
            String sql = "select * from [Member]";
            var memberList = new List<MemberObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(sql, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    memberList.Add(new MemberObject
                    {
                        MemberId = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        CompanyName = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        County = dataReader.GetString(4),
                        Password = dataReader.GetString(5)

                    });
                }
                return memberList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public MemberObject getMemberById(int id)
        {
            MemberObject member = null;
            IDataReader reader = null;
            String sql = "Select * from [Member] where MemberId = @MemberId";
            try
            {
                var param = dataProvider.CreateParameter("MemberId", 4, id, DbType.Int32);
                reader = dataProvider.GetDataReader(sql, CommandType.Text, out connection, param);
                if (reader.Read())
                {
                    member = new MemberObject
                    {
                        MemberId = reader.GetInt32(0),
                        Email = reader.GetString(1),
                        CompanyName = reader.GetString(2),
                        City = reader.GetString(3),
                        County = reader.GetString(4),
                        Password = reader.GetString(5)
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return member;
        }

        public IEnumerable<MemberObject> getListAccount(String email)
        {
            IDataReader dataReader = null;
            String sql = "select * from Member where Email = '"+email+"'";
            var memberList = new List<MemberObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(sql, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    memberList.Add(new MemberObject
                    {
                        MemberId = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        CompanyName = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        County = dataReader.GetString(4),
                        Password = dataReader.GetString(5)

                    });
                }
                return memberList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void addNewMember(MemberObject member)
        {
            try
            {
                MemberObject m = getMemberById(member.MemberId);
                if (m == null)
                {
                    String sql = "Insert [Member] values(@MemberId,@Email,@CompanyName,@City,@Country,@Password)";
                    var para = new List<SqlParameter>();
                    para.Add(dataProvider.CreateParameter("@MemberId", 4,member.MemberId, DbType.Int32));
                    para.Add(dataProvider.CreateParameter("@Email", 50, member.Email, DbType.String));
                    para.Add(dataProvider.CreateParameter("@CompanyName", 50, member.CompanyName, DbType.String));
                    para.Add(dataProvider.CreateParameter("@City", 50, member.City, DbType.String));
                    para.Add(dataProvider.CreateParameter("@Country", 50, member.County, DbType.String));
                    para.Add(dataProvider.CreateParameter("@Password", 50, member.Password, DbType.String));
                    dataProvider.Insert(sql, CommandType.Text, para.ToArray());
                }
                else
                {
                    throw new Exception("The product already exit.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

        }

        public void deleteMember(int id)
        {
            try
            {
                MemberObject member = getMemberById(id);
                String sql = "Delete from [Member] where MemberId = @Member";
                var param = dataProvider.CreateParameter("@Member", 4, id, DbType.Int32);
                dataProvider.Delete(sql, CommandType.Text, param);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void updateMember(MemberObject member)
        {
            try
            {
                String sql = "UPDATE [dbo].[Member] SET[MemberId] = '"+member.MemberId+"'," +
                    "[Email] = '"+member.Email+"',[CompanyName] = '"+member.CompanyName+"'," +
                    "[City] = '"+member.City+"',[Country] = '"+member.County+"'," +
                    "[Password] = '"+member.Password+"' WHERE[MemberId] = '"+member.MemberId+"'";
                var para = new List<SqlParameter>();
                dataProvider.Update(sql, CommandType.Text, para.ToArray());
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public MemberObject loginMember(String email,String password)
        {
            MemberObject member = null;
            IDataReader reader = null;
            String sql = "select * from Member where " +
                "Email = @Email " +
                " and[Password] = @Password";
            try
            {
                var para = new List<SqlParameter>();
                para.Add(dataProvider.CreateParameter("@Email", 50, email, DbType.String));
                para.Add(dataProvider.CreateParameter("@Password", 50, password, DbType.String));
                reader = dataProvider.GetDataReader(sql, CommandType.Text, out connection, para.ToArray());
                if (reader.Read())
                {
                    member = new MemberObject
                    {
                        MemberId = reader.GetInt32(0),
                        Email = reader.GetString(1),
                        CompanyName = reader.GetString(2),
                        City = reader.GetString(3),
                        County = reader.GetString(4),
                        Password = reader.GetString(5)
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return member;
        }
    }
}
