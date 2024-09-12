using MessagePipe;
using Projects.Core.DomainEvents;
using Projects.Presentation.UI.GameUI.FadeScreen;
using R3;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Projects.GameController.Installer.UI
{
    public class GameUILifetimeScope : LifetimeScope
    {
        [SerializeField, Required] private PlayMakerFSM fsmGameObject;
        [SerializeField, Required] private FadeScreenView fadeScreenView;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<DomainEventToFsmEvent>(Lifetime.Scoped)
                .WithParameter(fsmGameObject);

            builder.RegisterInstance(fadeScreenView);

            builder.RegisterBuildCallback(container =>
            {
                SubscribeToDomainEvents(container);
            });
        }

        private void SubscribeToDomainEvents(IObjectResolver container)
        {
            var domainEventToFsmEvent = container.Resolve<DomainEventToFsmEvent>();
            container.Resolve<ISubscriber<OnGameReadyEvent>>().Subscribe(e =>
            {
                domainEventToFsmEvent.SendEvent(nameof(OnGameReadyEvent));
            }).AddTo(this);

            container.Resolve<ISubscriber<OnGameStartEvent>>().Subscribe(e =>
            {
                domainEventToFsmEvent.SendEvent(nameof(OnGameStartEvent));
            }).AddTo(this);

            container.Resolve<ISubscriber<OnLevelClearedEvent>>().Subscribe(e =>
            {
                domainEventToFsmEvent.SendEvent(nameof(OnLevelClearedEvent));
            }).AddTo(this);

            container.Resolve<ISubscriber<OnLevelFailedEvent>>().Subscribe(e =>
            {
                domainEventToFsmEvent.SendEvent(nameof(OnLevelFailedEvent));
            }).AddTo(this);
        }

    }

    public class DomainEventToFsmEvent
    {
        /* 
         - Fsm Event names
         FSM_OnGameReady
         FSM_OnGameStart
         FSM_OnLevelClear
         FSM_OnLevelFailed
         */

        private readonly PlayMakerFSM fsm;
        private readonly static Dictionary<string, string> eventMap = new()

        {
            {"OnGameReadyEvent", "FSM_OnGameReady"},
            {"OnGameStartEvent", "FSM_OnGameStart"},
            {"OnLevelClearedEvent", "FSM_OnLevelClear"},
            {"OnLevelFailedEvent", "FSM_OnLevelFailed"}
        };

        public DomainEventToFsmEvent(PlayMakerFSM fsm)
        {
            this.fsm = fsm;
        }

        public void SendEvent(string domainEventName)
        {
            if (eventMap.ContainsKey(domainEventName))
            {
                fsm.Fsm.Event(eventMap[domainEventName]);
            }
        }
    }

}
