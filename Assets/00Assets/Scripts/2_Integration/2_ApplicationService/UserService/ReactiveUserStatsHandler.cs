using Projects.Core.Domain.User;
using R3; // UniRxの後継ライブラリ
using System;

namespace Projects.Integration.ApplicationService.UserService
{
    public interface IReactiveUserStatsHandler : IUserStatsHandler
    {
        ReadOnlyReactiveProperty<UserStats> ReactiveStats { get; }
    }

    public class ReactiveUserStatsHandler : IReactiveUserStatsHandler, IDisposable
    {
        private readonly ReactiveProperty<UserStats> stats = new();

        public ReadOnlyReactiveProperty<UserStats> ReactiveStats => stats.ToReadOnlyReactiveProperty();

        public ReactiveUserStatsHandler()
        {
            stats.Value = new UserStats(1, 0);
        }

        public void UpdateLevel(int level)
        {
            stats.Value = new UserStats(level, stats.Value.Money);
        }

        public void UpdateMoney(int money)
        {
            stats.Value = new UserStats(stats.Value.Level, money);
        }

        public UserStats GetStats()
        {
            return stats.Value;
        }

        public void Dispose()
        {
            stats.Dispose();
        }
    }


}
