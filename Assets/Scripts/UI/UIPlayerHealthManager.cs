using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthManager : MonoBehaviour
{
    private UIPlayerHealthManager() { }

    [SerializeField] private Image[] _healthImages;

    public void SetHealth(int health, int maxHealth)
    {
        int imgNumToShow = (health * _healthImages.Length) / maxHealth;

        for (int i = 0; i < _healthImages.Length; i++)
        {
            if (i < imgNumToShow)
            {
                _healthImages[i].enabled = true;
            }
            else
            {
                _healthImages[i].enabled = false;
            }
        }
    }
}
