using MessagePipe;
using Projects.Core.DomainEvents;
using Projects.Presentation.UI.GameUI.PlayScreen;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Projects.GameController.Installer.UI.PlayScreen
{
    [RequireComponent(typeof(GameStartTrigger))]
    public class GameStartTriggerLifetimeScope : LifetimeScope
    {
        [SerializeField, Required] private GameStartTrigger gameStartTrigger;

        private void Reset()
        {
            gameStartTrigger = GetComponent<GameStartTrigger>();
        }


        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(container =>
            {
                var gameStartPublisher = container.Resolve<IPublisher<OnGameStartEvent>>();
                gameStartTrigger.Construct(gameStartPublisher);

                var gameReadySubscriber = container.Resolve<ISubscriber<OnGameReadyEvent>>();
                gameReadySubscriber.Subscribe(e =>
                {
                    gameStartTrigger.gameObject.SetActive(true);
                });

            });
        }
    }
}
