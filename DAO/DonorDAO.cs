using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using BloodBanker.Model;
using Dapper;

namespace BloodBanker.DAO
{
    internal class DonorDAO
    {
        public static List<Donor> LoadDonor()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Donor>("select * from USER inner join DONOR ON USER.SSN=DONOR.SSN ",
                    new DynamicParameters());
                return output.ToList();
            }
        }

        public static Donor GetDonor(decimal ssn)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output =
                    cnn.Query<Donor>(
                        $"select * from USER inner join DONOR ON USER.SSN=DONOR.SSN where DONOR.SSN='{ssn.ToString()}'",
                        new DynamicParameters());
                return output.ToList().First();
            }
        }

        public static void StoreDonor(Donor donor)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(
                    "insert into USER values (@SSN,@PH_NO,@NAME,@B_GRP,@EMAIL,@ADDRESS,@CITY,@USERNAME,@PASSWORD,@TYPE_OF_USER,@DISEASE,@MI_ID)",
                    donor);
                cnn.Execute("insert into DONOR values (@SSN,@DOB,@WEIGHT,@LAST_DONATION_DATE)", donor);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}