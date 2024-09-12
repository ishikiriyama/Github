using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.Presentation.UI.Component
{
    /// <summary>
    /// 数値データをSliderで表示するためのビュークラス。
    /// </summary>
    public class SliderView : ValueView<float>
    {
        [SerializeField, Required] Slider slider;
        private GameObject fill;

        private void Awake()
        {
            fill = slider.fillRect.gameObject;
        }

        /// <summary>
        /// 外部から数値をセットするメソッド。数値がセットされると、自動的に表示が更新される。
        /// また数値が0の場合のときだけは、fillを非表示にする（Sliderの仕様上、0のときでもfillがちょっとだけ表示されるため）
        /// </summary>
        /// <param name="newValue"></param>
        public override void SetValue(float newValue)
        {
            slider.value = newValue;
            bool shouldFillBeActive = CheckFillBar(slider.value);
            fill.SetActive(shouldFillBeActive);
        }

        public bool CheckFillBar(float sliderValue)
        {
            return sliderValue != 0;
        }

    }
}
