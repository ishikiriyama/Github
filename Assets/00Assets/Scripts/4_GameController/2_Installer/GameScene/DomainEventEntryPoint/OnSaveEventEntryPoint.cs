using MessagePipe;
using Projects.Core.Domain.User;
using Projects.Core.DomainEvents;
using System;
using VContainer;
using VContainer.Unity;

namespace Projects.GameController.Installer.GameScene.DomainEventEntryPoint
{
    /// <summary>
    /// ゲームをセーブするイベント
    /// セーブを行う
    /// </summary>
    public class OnSaveEventEntryPoint : IInitializable, IDisposable
    {
        private readonly ISubscriber<OnSaveEvent> subscriber;

        private readonly IUserStatsHandler userStatsHandler;
        private readonly IUserStatsRepository userStatsRepository;

        private IDisposable disposable;

        [Inject]
        public OnSaveEventEntryPoint(
            ISubscriber<OnSaveEvent> subscriber,
            IUserStatsHandler userStatsHandler,
            IUserStatsRepository userStatsRepository)
        {
            this.subscriber = subscriber;
            this.userStatsHandler = userStatsHandler;
            this.userStatsRepository = userStatsRepository;
        }

        public void Initialize()
        {
            var bag = DisposableBag.CreateBuilder();

            subscriber.Subscribe(OnSave).AddTo(bag);

            disposable = bag.Build();
        }

        private void OnSave(OnSaveEvent onSaveEvent)
        {
            // ユーザーステータスを更新
            int currentLevel = userStatsHandler.GetStats().Level + 1;
            userStatsHandler.UpdateLevel(currentLevel);

            // セーブ
            userStatsRepository.Save(userStatsHandler.GetStats());
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}