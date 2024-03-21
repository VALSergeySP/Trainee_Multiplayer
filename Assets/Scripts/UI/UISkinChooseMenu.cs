using TMPro;
using UnityEngine;

public class UISkinChooseMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _hostImages;
    [SerializeField] private TMP_Text[] _clientImages;

    private void Start()
    {
        for (int i = 0; i < Mathf.Min(_hostImages.Length, _clientImages.Length); i++)
        {
            _hostImages[i].enabled = false;
            _clientImages[i].enabled = false;
        }
    }

    public void SetCurrentVariant(int variant, int player)
    {
        
        if (player == 1)
        {
            for (int i = 0; i < _hostImages.Length; i++)
            {
                _hostImages[i].enabled = false;
            }

            _hostImages[variant].enabled = true;

        } else
        {
            for (int i = 0; i < _clientImages.Length; i++)
            {
                _clientImages[i].enabled = false;
            }

            _clientImages[variant].enabled = true;
        }
    }
}
