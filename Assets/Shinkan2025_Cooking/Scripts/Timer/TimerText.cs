using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

public class TimerText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    private CancellationToken _token;
    public async UniTask CountDownTimeText(float limitTime)
    {
        while (!_token.IsCancellationRequested)
        {
            await UniTask.Yield(cancellationToken: _token);
            if (limitTime <= 0f) break;
            limitTime -= Time.deltaTime;
            _timeText.text = $"TImer:{limitTime.ToString("N0")}";
        }
    }
}
