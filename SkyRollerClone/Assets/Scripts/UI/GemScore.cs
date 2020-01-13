using UnityEngine;
using UnityEngine.UI;

namespace SkyRollerClone
{
    public class GemScore : MonoBehaviour
    {
        [SerializeField]
        private Text _gemScoreText;
        private void OnEnable()
        {
            GameManager.Instance.OnGemScoreChanged += GemScoreHandler;
        }

        private void OnDisable()
        {
            if (GameManager.IsInitialized)
            {
                GameManager.Instance.OnGemScoreChanged -= GemScoreHandler;
            }
        }

        private void GemScoreHandler(int v)
        {
            _gemScoreText.text = v.ToString();
        }
    }
}