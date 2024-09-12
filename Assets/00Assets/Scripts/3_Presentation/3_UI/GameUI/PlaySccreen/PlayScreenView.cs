using Projects.Presentation.UI.Component;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.Presentation.UI.GameUI.PlayScreen
{
    public class PlayScreenView : MonoBehaviour
    {
        [SerializeField, Required] ValueView<int> levelView;
        [SerializeField, Required] ValueView<int> moneyView;
        [SerializeField, Required] Button refreshButton;
        public ValueView<int> LevelView => levelView;
        public ValueView<int> MoneyView => moneyView;
        public Button RefreshButton => refreshButton;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

    }
}
