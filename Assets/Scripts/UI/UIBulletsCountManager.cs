using TMPro;
using UnityEngine;

public class UIBulletsCountManager : MonoBehaviour
{
    [SerializeField] TMP_Text _bulletsCountText;
    private int _maxBullets;

    public void Init(int maxBullets)
    {
        _maxBullets = maxBullets;
        _bulletsCountText.text = $"{_maxBullets} / {_maxBullets}";
    }

    public void SetBulletsCount(int bullets)
    {
        _bulletsCountText.text = $"{bullets} / {_maxBullets}";
    }
}
