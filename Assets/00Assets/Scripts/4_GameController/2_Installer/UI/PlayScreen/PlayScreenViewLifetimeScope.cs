using MessagePipe;
using Projects.Core.Domain.User;
using Projects.Core.DomainEvents;
using Projects.Integration.ApplicationService.UserService;
using Projects.Presentation.UI.GameUI.PlayScreen;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Projects.GameController.Installer.UI.PlayScreen
{
    [RequireComponent(typeof(PlayScreenView))]
    public class PlayScreenViewLifetimeScope : LifetimeScope
    {
        [SerializeField, Required] private PlayScreenView playScreenView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(container =>
            {
                SubscribeRefreshButton(container);
                SubscribeUserStats(container);
            });
        }

        private void SubscribeRefreshButton(IObjectResolver container)
        {
            var publisher = container.Resolve<IPublisher<OnGameReadyEvent>>();
            playScreenView.RefreshButton.onClick.AddListener(() =>
            {
                publisher.Publish(new OnGameReadyEvent(false));
            });
        }

        private void SubscribeUserStats(IObjectResolver container)
        {
            var userStatsHandler = container.Resolve<IReactiveUserStatsHandler>();

            userStatsHandler.ReactiveStats.Subscribe(userStats =>
            {
                SetValuesOnUserStatsChanged(userStats);
            }).AddTo(this);

        }

        private void SetValuesOnUserStatsChanged(UserStats userStats)
        {
            playScreenView.LevelView.SetValue(userStats.Level);
            playScreenView.MoneyView.SetValue(userStats.Money);
        }

    }
}
