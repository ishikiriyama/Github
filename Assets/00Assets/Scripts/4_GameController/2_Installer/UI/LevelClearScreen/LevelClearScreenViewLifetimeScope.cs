using MessagePipe;
using Projects.Core.DomainEvents;
using Projects.Presentation.UI.GameUI.FadeScreen;
using Projects.Presentation.UI.GameUI.LevelClearScreen;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Projects.GameController.Installer.UI.LevelClearScreen
{
    [RequireComponent(typeof(LevelClearScreenView))]
    public class LevelClearScreenViewLifetimeScope : LifetimeScope
    {
        [SerializeField, Required] private LevelClearScreenView levelClearScreenView;

        private void Reset()
        {
            parentReference = ParentReference.Create<GameUILifetimeScope>();
            levelClearScreenView = GetComponent<LevelClearScreenView>();
        }


        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(container =>
            {
                SubscribeNextLevelButton(container);
            });
        }

        private void SubscribeNextLevelButton(IObjectResolver container)
        {
            // ステージをクリアした際の次へボタンを押した際の処理
            // 遷移とセーブを行う

            var savePublisher = container.Resolve<IPublisher<OnSaveEvent>>();
            var gameReadyPublisher = container.Resolve<IPublisher<OnGameReadyEvent>>();

            var fadeScreenView = container.Resolve<FadeScreenView>();

            levelClearScreenView.NextLevelButton.onClick.AddListener(() =>
            {
                fadeScreenView.StartFadeOut(0.5f, () =>
                {
                    savePublisher.Publish(new OnSaveEvent());
                    gameReadyPublisher.Publish(new OnGameReadyEvent(false));
                });
            });

        }
    }
}
