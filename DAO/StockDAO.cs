using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using BloodBanker.Model;
using Dapper;

namespace BloodBanker.DAO
{
    public class StockDAO
    {
        public static List<Stock> LoadStock()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Stock>("select * from STOCK ", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void StoreStock(Stock stock)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into STOCK values (@MI_IS,@B_GRP,@QUANTITY,@RATE)", stock);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}