namespace SmartVisitServer.Web.Models.Users
{
    public class UserStatisticsViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int PassiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int BlockedUsers { get; set; }
        public int DeletedUsers { get; set; }

        public double ActiveUserPercentage { get; set; }
        public double PassiveUserPercentage { get; set; }
        public double InactiveUserPercentage { get; set; }
        public double BlockedUserPercentage { get; set; }
        public double DeletedUserPercentage { get; set; }
    }
}
