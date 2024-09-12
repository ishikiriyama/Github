using MessagePipe;
using Projects.Core.DomainEvents;
using Projects.Presentation.UI.GameUI.FadeScreen;
using Projects.Presentation.UI.GameUI.GameOverScreen;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Projects.GameController.Installer.UI.GameOverScreen
{
    [RequireComponent(typeof(GameOverScreenView))]
    public class GameOverScreenViewLifetimeScope : LifetimeScope
    {
        [SerializeField, Required] private GameOverScreenView gameOverScreenView;

        private void Reset()
        {
            parentReference = ParentReference.Create<GameUILifetimeScope>();
            gameOverScreenView = GetComponent<GameOverScreenView>();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(container =>
            {
                SubscribeRetryButton(container);
            });
        }

        private void SubscribeRetryButton(IObjectResolver container)
        {
            var gameReadyPublisher = container.Resolve<IPublisher<OnGameReadyEvent>>();

            var fadeScreenView = container.Resolve<FadeScreenView>();

            gameOverScreenView.RetryButton.onClick.AddListener(() =>
            {
                fadeScreenView.StartFadeOut(0.5f, () =>
                {
                    gameReadyPublisher.Publish(new OnGameReadyEvent(false));
                });
            });

        }
    }
}
