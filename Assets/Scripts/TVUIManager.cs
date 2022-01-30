using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TVUIManager : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image tvImage;
        [SerializeField] private Image newsBackground;
        [SerializeField] private Text newsText;

        public void toggleTVOverlay(bool fadeAway)
        {
            if (fadeAway)
            {
                background.GetComponent<ImageFade>().runFade(3, true);   
                tvImage.GetComponent<ImageFade>().runFade(2, true);   
                newsBackground.GetComponent<ImageFade>().runFade(2, true);
                newsText.GetComponent<TextFade>().runFade(2,true);
            }
            else
            {
                background.GetComponent<ImageFade>().runFade(2, false);   
                tvImage.GetComponent<ImageFade>().runFade(3, false);   
                newsBackground.GetComponent<ImageFade>().runFade(3, false);
                newsText.GetComponent<TextFade>().runFade(3,false);
            }
        }
    }
}