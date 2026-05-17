using TMPro;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text timerDisplayText;

    private void OnEnable()
    {
        WaveEvents.OnCountdownChanged += RefreshTimerUI;
    }

    private void OnDisable()
    {
        WaveEvents.OnCountdownChanged -= RefreshTimerUI;
    }

    private void RefreshTimerUI(float timeLeft)
    {
        timerDisplayText.text = Mathf.CeilToInt(timeLeft).ToString();
    }
}