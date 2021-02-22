using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using BloodBanker.Model;
using Dapper;

namespace BloodBanker.DAO
{
    public class MedicalInstitutionDAO
    {
        public static List<MedicalInstitution> LoadMedInst()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<MedicalInstitution>("select * from MED_INST", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<MedicalInstitution> LoadMedInst(string type)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<MedicalInstitution>($"select * from MED_INST where TYPE_OF_MI='{type}'",
                    new DynamicParameters());
                return output.ToList();
            }
        }

        public static MedicalInstitution LoginMedInst(string username, string password)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<MedicalInstitution>(
                    $"select * from MED_INST where USERNAME='{username}' AND PASSWORD='{password}'",
                    new DynamicParameters());
                return output.Count() == 1 ? output.ToList()[0] : null;
            }
        }

        public static MedicalInstitution GetMedicalInstitution(decimal id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<MedicalInstitution>($"select * from MED_INST where MI_ID='{id.ToString()}'",
                    new DynamicParameters());
                return output.Count() == 1 ? output.ToList()[0] : null;
            }
        }

        public static void UpdateMedInst(MedicalInstitution medInst, string column, string value)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"update MED_INST set '{column}'='{value}' where MI_ID='{medInst.MI_ID.ToString()}'");
            }
        }

        public static bool IsUsernameAvailable(string username)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<MedicalInstitution>($"select * from MED_INST where USERNAME='{username}'",
                    new DynamicParameters());
                return output.Count() == 0 ? true : false;
            }
        }

        public static long GetLastId()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<MaxID>("select max(MI_ID) from MED_INST ", new DynamicParameters());
                return output.ToList().First().MI_ID;
            }
        }

        public static void StoreMedInst(MedicalInstitution med)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(
                    "insert into MED_INST (NAME,PHONE,ADDRESS,CITY,WEBSITE,EMAIL,USERNAME,PASSWORD,TYPE_OF_MI)" +
                    " values (@NAME,@PHONE,@ADDRESS,@CITY,@WEBSITE,@EMAIL,@USERNAME,@PASSWORD,@TYPE_OF_MI)", med);
            }
        }

        public static long GetAppID(MedicalInstitution medInst)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<IDS>($"select ID from IDS where FID='{medInst.MI_ID.ToString()}'",
                    new DynamicParameters());
                return output.ToList().First().ID;
            }
        }


        public static List<MedicalInstitution> LoadBB_Hos(MedicalInstitution medInst)
        {
            var id = GetAppID(medInst).ToString();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<MedicalInstitution>(
                    $"select * from MED_INST where MI_ID in (select RECIP_ID from ORDERS where DONOR_ID='{id}')",
                    new DynamicParameters());
                return output.ToList();
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}