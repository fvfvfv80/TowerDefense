using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.UI
{
    public enum MESSAGE { MONEY = 0, BUILD }
    public class SystemTextViewer : MonoBehaviour
    {
        private TextMeshProUGUI textSystem;
        private TMPAlpha tmpAlpha;

        private void Awake()
        {
            textSystem = GetComponent<TextMeshProUGUI>();
            tmpAlpha = GetComponent<TMPAlpha>();
        }

        //로컬라이제이션?
        public void PrintText(MESSAGE type)
        {
            switch (type)
            {
                case MESSAGE.MONEY:
                    textSystem.text = "System : Not enough money...";
                    break;
                case MESSAGE.BUILD:
                    textSystem.text = "System : Invalid build tower...";
                    break;
            }

            tmpAlpha.FadeOut();
        }

    }
}