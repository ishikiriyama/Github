using MessagePipe;
using Projects.Core.DomainEvents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Projects.Presentation.UI.GameUI.PlayScreen
{
    [RequireComponent(typeof(EventTrigger), typeof(Image))]
    public class GameStartTrigger : MonoBehaviour
    {
        private IPublisher<OnGameStartEvent> gameStartPublisher;

        void Awake()
        {
            var eventTrigger = GetComponent<EventTrigger>();
            var entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
            entry.callback.AddListener(_ => TriggerGameStart());
            eventTrigger.triggers.Add(entry);
        }

        public void Construct(IPublisher<OnGameStartEvent> gameStartPublisher)
        {
            this.gameStartPublisher = gameStartPublisher;
        }

        public void TriggerGameStart()
        {
            gameStartPublisher.Publish(new OnGameStartEvent());
            gameObject.SetActive(false);
        }
    }
}
