using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RedScreenUIViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerHPModule playerHP;

    private Image _imageScreen;

    private void Awake()
    {
        _imageScreen = GetComponent<Image>();
        playerHP.OnTakeDamage += OnAnimation;
    }

    public void OnAnimation()
    {
        StopCoroutine(nameof(Process));
        StartCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        Color color = _imageScreen.color;
        color.a = 0.4f;

        _imageScreen.color = color;

        while (color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            _imageScreen.color = color;

            yield return null;
        }
    }
}
