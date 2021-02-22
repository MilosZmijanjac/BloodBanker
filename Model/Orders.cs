using System;

namespace BloodBanker.Model
{
    public class Orders
    {
        public long OR_ID { get; set; }
        public string B_GRP { get; set; }
        public long RECIP_ID { get; set; }
        public long DONOR_ID { get; set; }
        public long MI_ID { get; set; }
        public DateTime REQ_DATE { get; set; }
        public DateTime DEL_DATE { get; set; }
        public int QUANTITY { get; set; }
    }
}