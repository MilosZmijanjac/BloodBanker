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
    public class OrdersDAO
    {
        public static List<Orders> LoadOrders()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Orders>("select * from ORDERS", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<Orders> LoadOrders(long id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output =
                    cnn.Query<Orders>(
                        $"select * from ORDERS where (RECIP_ID='{id.ToString()}' or DONOR_ID='{id.ToString()}') and DEL_DATE not null",
                        new DynamicParameters());
                return output.ToList();
            }
        }

        public static Orders GetOrder(long id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Orders>($"select * from ORDERS where OR_ID={id.ToString()}",
                    new DynamicParameters());
                return output.ToList().First();
            }
        }

        public static void StoreOrders(Orders order)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into ORDERS (B_GRP,RECIP_ID,DONOR_ID,MI_ID,REQ_DATE,DEL_DATE,QUANTITY)" +
                            " values (@B_GRP,@RECIP_ID,@DONOR_ID,@MI_ID,@REQ_DATE,@DEL_DATE,@QUANTITY)", order);
            }
        }

        public static void UpdateOrders(decimal id, DateTime delDate)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(
                    $"UPDATE ORDERS SET DEL_DATE='{delDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}' WHERE OR_ID='{id.ToString()}'");
            }
        }

        public static Orders GetOrder(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Orders>($"select * from ORDERS where OR_ID='{id}'", new DynamicParameters());
                return output.ToList().First();
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}