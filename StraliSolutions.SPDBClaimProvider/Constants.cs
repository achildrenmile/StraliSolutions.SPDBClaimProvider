using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StraliSolutions.SPDBClaimProvider
{
    class Constants
    {
        public const string DC = "DataConnection";
        public const string SPTRUSTEDIDENTITYTOKENISSUERNAME = "SPTrustedIdentityTokenIssuerName";
        public const string TABLE = "tblClaimProviderAccounts";
        public const string SELECTSTATEMENT = "select * from {0} where email like '%{1}%' or first_name like '%{1}%' or last_name like '%{1}%'";
        public const string PROVIDERINTERNALNAME = "ADFS_SPPDBCC";
        public const string PROIVDERDISPLAYNAME = "DB Claim Provider";
        public const string MAILCLAIMTYPE = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    }
}
