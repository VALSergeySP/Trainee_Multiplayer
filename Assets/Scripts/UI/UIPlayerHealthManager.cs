using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthManager : MonoBehaviour
{
    private UIPlayerHealthManager() { }

    [SerializeField] private Image[] _healthImages;
    [SerializeField] private Sprite _fullHealth;
    [SerializeField] private Sprite _emptyHealth;

    public void SetHealth(int health, int maxHealth)
    {
        int imgNumToShow = (health * _healthImages.Length) / maxHealth;

        for (int i = 0; i < _healthImages.Length; i++)
        {
            if (i < imgNumToShow)
            {
                _healthImages[i].sprite = _fullHealth;
            }
            else
            {
                _healthImages[i].sprite = _emptyHealth;
            }
        }
    }
}
