namespace BloodBanker.Model
{
    public class MyDonor
    {
        public decimal OR_ID { get; set; }
        public decimal DONOR_ID { get; set; }
        public string NAME { get; set; }
        public string B_GRP { get; set; }
        public string Display => OR_ID + ":" + NAME;
        public string DisplayLong => OR_ID + ":" + NAME + ":" + B_GRP;
    }
}