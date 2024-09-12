namespace Projects.Core.Domain.User
{
    public interface IUserStatsRepository
    {
        void Save(UserStats userStats);
        UserStats Load();
    }
}