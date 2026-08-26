using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.UI
{
    public class TMPAlpha : MonoBehaviour
    {
        [SerializeField]
        private float duration = 0.5f;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void FadeOut()
        {
            StartCoroutine(OnFade(1, 0));
        }

        private IEnumerator OnFade(float start, float end)
        {
            float percent = 0f;
            while (percent < 1f)
            {
                percent += Time.deltaTime / duration;

                Color color = _text.color;

                color.a = Mathf.Lerp(start, end, percent);

                _text.color = color;

                yield return null;
            }
        }

    }
}