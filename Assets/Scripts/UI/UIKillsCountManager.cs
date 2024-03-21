using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIKillsCountManager : MonoBehaviour
{
    [SerializeField] TMP_Text _killsCountText;

    private void Start()
    {
        _killsCountText.text = "0";
    }

    public void SetKillsCount(int kills)
    {
        _killsCountText.text = kills.ToString();
    }
}
