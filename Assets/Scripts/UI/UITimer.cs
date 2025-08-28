using System.Collections;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerTextField;
    [SerializeField] private TMP_Text _stateNameText;
    private float _timerValue;

    public void SetStateName(string newState)
    {
        _stateNameText.text = newState;
    }

    public void ResetTimer()
    {
        _timerTextField.text = "00:00";
    }

    public void StartTimerWithValue(float value)
    {
        _timerValue = value;
        _timerTextField.text = FloatToString(value);
        StopAllCoroutines();
        StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        while (_timerValue > 0)
        {
            yield return new WaitForSeconds(1f);
            _timerValue -= 1f;
            _timerTextField.text = FloatToString(_timerValue);
        }

        ResetTimer();
    }

    private string FloatToString(float value)
    {
        string res = "";

        int minutes = (int)Mathf.Floor(value / 60f);
        int seconds = (int)Mathf.Round(value % 60f);

        if(minutes < 10)
        {
            res += "0" + minutes + ":";
        } else
        {
            res += minutes + ":";
        }
        if(seconds < 10)
        {
            res += "0" + seconds;
        } else
        {
            res += seconds;
        }

        return res;
    }
}
