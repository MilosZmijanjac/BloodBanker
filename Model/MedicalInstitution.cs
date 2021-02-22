using System;

namespace BloodBanker.Model
{
    public class MedicalInstitution
    {
        public MedicalInstitution(long mI_ID, string nAME, decimal pHONE, string aDDRESS, string cITY,
            string wEBSITE, string eMAIL, string uSERNAME, string pASSWORD, string tYPE_OF_MI)
        {
            MI_ID = mI_ID;
            NAME = nAME ?? throw new ArgumentNullException(nameof(nAME));
            PHONE = pHONE;
            ADDRESS = aDDRESS ?? throw new ArgumentNullException(nameof(aDDRESS));
            CITY = cITY ?? throw new ArgumentNullException(nameof(cITY));
            WEBSITE = wEBSITE ?? throw new ArgumentNullException(nameof(wEBSITE));
            EMAIL = eMAIL ?? throw new ArgumentNullException(nameof(eMAIL));
            USERNAME = uSERNAME ?? throw new ArgumentNullException(nameof(uSERNAME));
            PASSWORD = pASSWORD ?? throw new ArgumentNullException(nameof(pASSWORD));
            TYPE_OF_MI = tYPE_OF_MI;
        }

        public MedicalInstitution(string nAME, decimal pHONE, string aDDRESS, string cITY,
            string wEBSITE, string eMAIL, string uSERNAME, string pASSWORD, string tYPE_OF_MI)
        {
            MI_ID = 0;
            NAME = nAME ?? throw new ArgumentNullException(nameof(nAME));
            PHONE = pHONE;
            ADDRESS = aDDRESS ?? throw new ArgumentNullException(nameof(aDDRESS));
            CITY = cITY ?? throw new ArgumentNullException(nameof(cITY));
            WEBSITE = wEBSITE ?? throw new ArgumentNullException(nameof(wEBSITE));
            EMAIL = eMAIL ?? throw new ArgumentNullException(nameof(eMAIL));
            USERNAME = uSERNAME ?? throw new ArgumentNullException(nameof(uSERNAME));
            PASSWORD = pASSWORD ?? throw new ArgumentNullException(nameof(pASSWORD));
            TYPE_OF_MI = tYPE_OF_MI;
        }

        public long MI_ID { get; set; }
        public string NAME { get; set; }
        public decimal PHONE { get; set; }
        public string ADDRESS { get; set; }
        public string CITY { get; set; }
        public string WEBSITE { get; set; }
        public string EMAIL { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string TYPE_OF_MI { get; set; }
    }
}