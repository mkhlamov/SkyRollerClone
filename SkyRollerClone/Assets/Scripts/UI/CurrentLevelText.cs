using UnityEngine;
using UnityEngine.UI;

namespace SkyRollerClone
{
    public class CurrentLevelText : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }
        private void OnEnable()
        {
            _text.text = "Level " + GameManager.Instance.GetCurrentLevel().ToString();
            GameManager.Instance.OnLevelUpdated += UpdateText;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnLevelUpdated -= UpdateText;
        }

        private void UpdateText(int v)
        {
            _text.text = "Level " + v.ToString();
        }
    }
}