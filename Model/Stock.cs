using System;

namespace BloodBanker.Model
{
    public class Stock
    {
        public Stock(long mI_ID, string b_GRP, int qUANTITY, int rATE)
        {
            MI_ID = mI_ID;
            B_GRP = b_GRP ?? throw new ArgumentNullException(nameof(b_GRP));
            QUANTITY = qUANTITY;
            RATE = rATE;
        }

        public long MI_ID { get; set; }
        public string B_GRP { get; set; }
        public int QUANTITY { get; set; }
        public int RATE { get; set; }
    }
}