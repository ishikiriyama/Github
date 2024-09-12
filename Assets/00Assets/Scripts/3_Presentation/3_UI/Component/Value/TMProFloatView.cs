using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Projects.Presentation.UI.Component
{
    /// <summary>
    /// 数値データをTextMeshProで表示するためのビュークラス。
    /// </summary>
    public class TMProFloatView : ValueView<float>
    {
        [SerializeField, Required] TextMeshProUGUI text;

        public override void SetValue(float newValue)
        {
            text.text = newValue.ToString();
        }

    }
}
