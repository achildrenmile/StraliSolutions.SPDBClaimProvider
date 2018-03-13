using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace StraliSolutions.SPDBClaimProvider
{

    public class DBHelper
    {
        private static PropertyBagHandler _propertyBagHandler;

        static DBHelper()
        {
            _propertyBagHandler = new PropertyBagHandler();
        }

        public static string propertySPTrustedIdentityTokenIssuerName
        {
            get
            {
                return _propertyBagHandler.getPropertyBagValue(Constants.SPTRUSTEDIDENTITYTOKENISSUERNAME);
            }
        }

        public static List<DBUser> Search(string pattern)
        {
            return getUsers(pattern);
        }

        public static DBUser FindExact(string pattern)
        {
            List<DBUser> users = getUsers(pattern);
            DBUser user = new DBUser();

            if (users.Count > 0)
                user = users[0];

            return user;            

        }

        private static List<DBUser> getUsers(string pattern)
        {
            List<DBUser> ret = new List<DBUser>();

            //Run with elevated privileges to get the context of the service account            

            string constr = _propertyBagHandler.getPropertyBagValue(Constants.DC);

            using (SqlConnection con = new SqlConnection(constr))
            {

                SqlCommand cmd = new SqlCommand(
                    string.Format(Constants.SELECTSTATEMENT, Constants.TABLE, pattern), con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ret.Add(new DBUser
                        {
                            ad_account_name = reader["ad_account_name"].ToString(),
                            email = reader["email"].ToString(),
                            first_name = reader["first_name"].ToString(),
                            last_name = reader["last_name"].ToString()
                        });
                    }


                }
            }

            return ret;
        }

    }
}
