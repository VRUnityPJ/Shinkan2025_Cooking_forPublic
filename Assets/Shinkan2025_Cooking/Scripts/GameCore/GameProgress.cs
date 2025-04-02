using Shinkan2025_Cooking.Scripts.Timer;
using UniRx;
using UnityEngine;
using VContainer;

namespace Shinkan2025_Cooking.Scripts.GameCore
{
    /// <summary>
    /// ?Q?[???i?s???????????N???X?B???????V?[?????s?????????o???B
    /// </summary>
    public class GameProgress : MonoBehaviour
    {
        [SerializeField] StageTimer _stageTimer;
        [SerializeField] SceneSwitch _sceneSwitch;

        [Inject]
        private IGameProgressIndicatable _gameProgressIndicatable;

        void Start()
        {
            SetUpGameProgress();
        }
        
        /// <summary>
        /// ?V?[?????s???????????????o??????
        /// ?e?N???X???????????????????s??
        /// </summary>

        [ContextMenu("GameStart")]
        public void SetUpGameProgress()
        {
            _stageTimer.StartTimer();
            Debug.Log("GameStart");
            if (_gameProgressIndicatable == null)
            {
                Debug.Log("IGameEndIndicatableがnullです");
                return;
            }

            _gameProgressIndicatable.OnEndGame
                .Subscribe(_ =>
                {
                    ExitGame();

                }).AddTo(this);
        }

        /// <summary>
        /// ?Q?[???I???????????o??????
        /// </summary>
        public void ExitGame()
        {
            Debug.Log("タイトルSceneへ");
            _sceneSwitch.SwitchScene();
        }
    }
}
