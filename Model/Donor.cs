using System;

namespace BloodBanker.Model
{
    public class Donor : User
    {
        public Donor()
        {
        }

        public Donor(User u, DateTime dob, int weight, DateTime ldt) : base(u)
        {
            DOB = dob;
            WEIGHT = weight;
            LAST_DONATION_DATE = ldt;
        }

        public DateTime DOB { get; set; }
        public int WEIGHT { get; set; }
        public DateTime LAST_DONATION_DATE { get; set; }
        public string Display => NAME + " " + WEIGHT;
    }
}