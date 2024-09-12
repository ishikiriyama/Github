using MessagePipe;
using Projects.Core.DomainEvents;
using Projects.Presentation.SceneEntity.GameLevel;
using System;
using VContainer;
using VContainer.Unity;

namespace Projects.GameController.Installer.GameScene.GameLevel
{
    /// <summary>
    /// ゲーム開始前の待機画面に入った時のイベントに対応する処理
    /// ユーザーステータスを初期化し、レベルを生成する
    /// </summary>
    public class GameLevelGeneratorOnGameReadyEvent : IInitializable, IDisposable
    {
        private readonly ISubscriber<OnGameReadyEvent> onGameReadyEventSubscriber;

        private GameLevelGenerator gameLevelGenerator;

        private IDisposable disposable;

        public void Dispose()
        {
            disposable.Dispose();
        }

        [Inject]
        public GameLevelGeneratorOnGameReadyEvent(
            ISubscriber<OnGameReadyEvent> onGameReadyEventSubscriber,
            GameLevelGenerator gameLevelGenerator)
        {
            this.onGameReadyEventSubscriber = onGameReadyEventSubscriber;
            this.gameLevelGenerator = gameLevelGenerator;
        }

        public void Initialize()
        {
            var bag = DisposableBag.CreateBuilder();

            onGameReadyEventSubscriber.Subscribe(OnGameReadyEvent).AddTo(bag);

            disposable = bag.Build();
        }

        private void OnGameReadyEvent(OnGameReadyEvent readyEvent)
        {
            if (readyEvent.IsFirstGame)
            {
                // 今は未定だがなんらかの初回ゲーム時の処理を行う
            }

            // レベルを生成
            gameLevelGenerator.Generate();
        }



    }
}