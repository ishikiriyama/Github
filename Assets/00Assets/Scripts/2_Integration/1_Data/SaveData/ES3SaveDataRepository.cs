using Projects.Core.Domain.User;

namespace Projects.Integration.Data.SaveData
{
    public class ES3SaveDataRepository : IUserStatsRepository
    {
        private const string SaveDataKey = "UserStats";

        private UserStats userStats;

        public ES3SaveDataRepository()
        {
            Load();
        }

        public void Save(UserStats userStats)
        {
            this.userStats = userStats;
            ES3.Save(SaveDataKey, userStats);
        }

        public UserStats Load()
        {
            userStats = ES3.Load(SaveDataKey, new UserStats(1, 0));
            return userStats;
        }
    }
}
