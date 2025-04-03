using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Shinkan2025_Cooking.Scripts.Timer
{
    public class TimerText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        private CancellationToken _token = new CancellationToken();
        
        private void Start()
        {
            _token = this.GetCancellationTokenOnDestroy(); 
        }

        
        public async UniTask CountDownTimeText(float limitTime)
        {
            while (!_token.IsCancellationRequested)
            {
                await UniTask.Yield(cancellationToken: _token);
                if (limitTime <= 0f) break;
                limitTime -= Time.deltaTime;
                Debug.Log("limitTime:" +limitTime);
                _timeText.text = $"Timer:{limitTime:N0}";
                Debug.Log($"Timer:{limitTime:N0}");
            }
        }
    }
}
