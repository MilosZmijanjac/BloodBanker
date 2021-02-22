namespace BloodBanker.Model
{
    public class User
    {
        public User()
        {
        }

        public User(decimal sSN, decimal pH_NO, string nAME, string b_GRP,
            string eMAIL, string aDDRESS, string cITY, string uSERNAME,
            string pASSWORD, string tYPE_OF_USER, string dISEASE, long mI_ID)
        {
            SSN = sSN;
            PH_NO = pH_NO;
            NAME = nAME;
            B_GRP = b_GRP;
            EMAIL = eMAIL;
            ADDRESS = aDDRESS;
            CITY = cITY;
            USERNAME = uSERNAME;
            PASSWORD = pASSWORD;
            TYPE_OF_USER = tYPE_OF_USER;
            DISEASE = dISEASE;
            MI_ID = mI_ID;
        }


        public User(decimal sSN, decimal pH_NO, string nAME, string b_GRP,
            string eMAIL, string aDDRESS, string cITY, string uSERNAME,
            string pASSWORD, string tYPE_OF_USER)
        {
            SSN = sSN;
            PH_NO = pH_NO;
            NAME = nAME;
            B_GRP = b_GRP;
            EMAIL = eMAIL;
            ADDRESS = aDDRESS;
            CITY = cITY;
            USERNAME = uSERNAME;
            PASSWORD = pASSWORD;
            TYPE_OF_USER = tYPE_OF_USER;
            DISEASE = "";
            MI_ID = 0;
        }

        public User(User u)
        {
            SSN = u.SSN;
            PH_NO = u.PH_NO;
            NAME = u.NAME;
            B_GRP = u.B_GRP;
            EMAIL = u.EMAIL;
            ADDRESS = u.ADDRESS;
            CITY = u.CITY;
            USERNAME = u.USERNAME;
            PASSWORD = u.PASSWORD;
            TYPE_OF_USER = u.TYPE_OF_USER;
            DISEASE = u.DISEASE;
            MI_ID = u.MI_ID;
        }

        public decimal SSN { get; set; }
        public decimal PH_NO { get; set; }
        public string NAME { get; set; }
        public string B_GRP { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public string CITY { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string TYPE_OF_USER { get; set; }
        public string DISEASE { get; set; }
        public long MI_ID { get; set; }
    }
}