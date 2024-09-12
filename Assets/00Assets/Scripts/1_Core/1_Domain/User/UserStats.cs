using System;

namespace Projects.Core.Domain.User
{
    [Serializable]
    public struct UserStats
    {
        public int Level { get; private set; }
        public int Money { get; private set; }

        public UserStats(int level, int money)
        {
            Level = level;
            Money = money;
        }
    }

    public interface IUserStatsHandler
    {
        void UpdateLevel(int level);
        void UpdateMoney(int money);
        UserStats GetStats();
    }
}
