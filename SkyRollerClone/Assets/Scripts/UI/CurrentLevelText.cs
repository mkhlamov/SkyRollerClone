using UnityEngine;
using UnityEngine.UI;

namespace SkyRollerClone.UI
{
    public class CurrentLevelText : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        private void OnEnable()
        {
            if (_text == null)
            {
                _text = GetComponent<Text>();
            }
            UpdateText(GameManager.Instance.GetCurrentLevel());
            GameManager.Instance.OnLevelUpdated += UpdateText;
        }

        private void OnDisable()
        {
            if (GameManager.IsInitialized)
            {
                GameManager.Instance.OnLevelUpdated -= UpdateText;
            }
        }

        private void UpdateText(int v)
        {
            _text.text = "Level " + v.ToString();
        }
    }
}