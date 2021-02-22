using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using BloodBanker.Model;
using Dapper;

namespace BloodBanker.DAO
{
    public class MyDonorDAO
    {
        public static void pringels()
        {
            foreach (var i in LoadMyDonor(12345675)) Console.WriteLine(i.NAME);
        }

        public static List<MyDonor> LoadMyDonor(decimal id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<MyDonor>(
                    "SELECT U.NAME, O.B_GRP,O.OR_ID,O.DONOR_ID FROM USER U,ORDERS O WHERE DEL_DATE IS NULL AND O.DONOR_ID = (SELECT ID from ids where FID = U.SSN) " +
                    $"AND O.RECIP_ID = (SELECT ID from ids where FID = '{id.ToString()}')");
                return output.ToList();
            }
        }

        public static List<MyDonor> LoadMyDonorMI(decimal id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<MyDonor>(
                    "SELECT M.NAME,O.B_GRP, O.OR_ID,O.DONOR_ID FROM MED_INST M,ORDERS O WHERE DEL_DATE IS NULL AND  O.DONOR_ID = (SELECT ID from ids where FID = M.MI_ID) " +
                    $"AND O.RECIP_ID = (SELECT ID from ids where FID = '{id.ToString()}')");
                return output.ToList();
            }
        }

        public static List<MyDonor> LoadMyDonorInverseMI(decimal id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<MyDonor>(
                    "SELECT M.NAME,O.B_GRP, O.OR_ID,O.DONOR_ID FROM MED_INST M,ORDERS O WHERE DEL_DATE IS NULL AND  O.RECIP_ID = (SELECT ID from ids where FID = M.MI_ID) " +
                    $"AND O.DONOR_ID = (SELECT ID from ids where FID = '{id.ToString()}')");
                return output.ToList();
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}