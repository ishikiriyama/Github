using MessagePipe;
using Projects.Integration.ApplicationService.UserService;
using Projects.Integration.Data.SaveData;
using Projects.Integration.Data.Settings;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Projects.GameController.Installer
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField, Required] GlobalSettingsAsset globalSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterGlobalSettings(builder);
            RegisterMessagePipe(builder);
            RegisterPlayerData(builder);
        }

        // GlobalSettingsAssetの登録
        private void RegisterGlobalSettings(IContainerBuilder builder)
        {
            builder.RegisterInstance(globalSettings).AsSelf().AsImplementedInterfaces();
        }

        // MessagePipeの登録
        private void RegisterMessagePipe(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();
        }

        private void RegisterPlayerData(IContainerBuilder builder)
        {
            // UserStats(ReadOnlyReactiveProperty)の登録
            builder.Register<ReactiveUserStatsHandler>(Lifetime.Singleton).AsImplementedInterfaces();

            // IUserStatsRepository(ES3SaveDataRepository)の登録
            builder.Register<ES3SaveDataRepository>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
