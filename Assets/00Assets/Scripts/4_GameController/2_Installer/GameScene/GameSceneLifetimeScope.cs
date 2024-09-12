using MessagePipe;
using Projects.Core.Domain.User;
using Projects.Core.DomainEvents;
using Projects.GameController.Installer.GameScene.GameLevel;
using Projects.Integration.ApplicationService.InputService;
using Projects.Presentation.Input;
using Projects.Presentation.SceneEntity.GameLevel;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Projects.GameController.Installer.GameScene
{
    public class GameSceneLifetimeScope : LifetimeScope
    {
        [SerializeField, Required] private GameLevelRepositoryAsset gameLevelRepositoryAsset;
        private class OnFirstGameReadyPublish : IPostStartable
        {
            private readonly IPublisher<OnGameReadyEvent> publisher;

            public OnFirstGameReadyPublish(IPublisher<OnGameReadyEvent> publisher)
            {
                this.publisher = publisher;
            }

            public void PostStart()
            {
                publisher.Publish(new OnGameReadyEvent(true));
            }
        }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterInputProvider(builder);
            RegisterGameLevelGenerator(builder);

            RegisterEntryPoints(builder);
        }

        private void RegisterInputProvider(IContainerBuilder builder)
        {
            builder.Register<UserInput>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterBuildCallback(container =>
            {
                IInputProvider userInput = container.Resolve<IInputProvider>();

                ISubscriber<OnGameStartEvent> onGameStartSubscriber = container.Resolve<ISubscriber<OnGameStartEvent>>();
                onGameStartSubscriber.Subscribe(_ => userInput.Enable()).AddTo(this);

                ISubscriber<OnLevelClearedEvent> onLevelClearedSubscriber = container.Resolve<ISubscriber<OnLevelClearedEvent>>();
                onLevelClearedSubscriber.Subscribe(_ => userInput.Disable()).AddTo(this);

                ISubscriber<OnLevelFailedEvent> onLevelFailedSubscriber = container.Resolve<ISubscriber<OnLevelFailedEvent>>();
                onLevelFailedSubscriber.Subscribe(_ => userInput.Disable()).AddTo(this);
            });
        }

        private void RegisterGameLevelGenerator(IContainerBuilder builder)
        {
            builder.RegisterComponentOnNewGameObject<GameLevelGenerator>(Lifetime.Scoped, "GameLevelGenerator");

            builder.RegisterBuildCallback(container =>
            {
                GameLevelGeneratorSetup(container);
            });
        }

        private void GameLevelGeneratorSetup(IObjectResolver container)
        {
            GameLevelGenerator gameLevelGenerator = container.Resolve<GameLevelGenerator>();
            // セーブデータからユーザーステータスを取得・更新し、それを元にレベルを生成する
            gameLevelGenerator.Construct(() =>
            {
                // セーブデータからユーザーステータスを取得し、レベルを生成する
                // セーブデータと現在のユーザーステータスの取得
                UserStats savedUserStats = container.Resolve<IUserStatsRepository>().Load();
                IUserStatsHandler runtimeUserStatsHandler = container.Resolve<IUserStatsHandler>();

                // ユーザーステータスの更新
                runtimeUserStatsHandler.UpdateLevel(savedUserStats.Level);
                runtimeUserStatsHandler.UpdateMoney(savedUserStats.Money);

                // レベルの生成
                UserStats runtimeUserStats = runtimeUserStatsHandler.GetStats();
                int userCurrentLevel = runtimeUserStats.Level - 1;
                int levelId = userCurrentLevel % gameLevelRepositoryAsset.GameLevelObjects.Length;
                GameLevelObject gameLevelObject = gameLevelRepositoryAsset.GameLevelObjects[levelId];
                return gameLevelObject;
            }, () =>
            {
                // レベル生成イベントの発行
                IPublisher<OnLevelGeneratedEvent> onLevelGeneratedPublisher = container.Resolve<IPublisher<OnLevelGeneratedEvent>>();
                IUserStatsHandler runtimeUserStatsHandler = container.Resolve<IUserStatsHandler>();
                onLevelGeneratedPublisher.Publish(new OnLevelGeneratedEvent(runtimeUserStatsHandler.GetStats().Level));
                return;
            });
        }

        private void RegisterEntryPoints(IContainerBuilder builder)
        {
            // ゲームが完成したらセーブ追加
            // builder.RegisterEntryPoint<OnSaveEventEntryPoint>();

            builder.RegisterEntryPoint<OnFirstGameReadyPublish>();

            builder.RegisterEntryPoint<GameLevelGeneratorOnGameReadyEvent>();
        }
    }
}
