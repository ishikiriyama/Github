using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Projects.Presentation.UI.Component
{
    /// <summary>
    /// 数値データをTextMeshProで表示するためのビュークラス。
    /// </summary>
    public class TMProIntView : ValueView<int>
    {
        [SerializeField, Required] TextMeshProUGUI text;

        public override void SetValue(int newValue)
        {
            text.text = newValue.ToString();
        }

    }
}
