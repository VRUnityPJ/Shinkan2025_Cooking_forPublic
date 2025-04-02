using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Shinkan2025_Cooking.Scripts.Timer
{
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
                Debug.Log($"TImer:{limitTime.ToString("N0")}");
            }
        }
    }
}
