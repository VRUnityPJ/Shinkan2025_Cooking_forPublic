using System.Collections;
using System.Collections.Generic;
using Shinkan2025_Cooking.Scripts.Timer;
using UnityEngine;
using UniRx;
using VContainer;

/// <summary>
/// ゲーム進行を管理するクラス。ここでシーン移行など呼び出す。
/// </summary>
public class GameProgress : MonoBehaviour
{
    [SerializeField] StageTimer _stageTimer;
    [SerializeField] SceneSwitch _sceneSwitch;

    [Inject] private IGameEndIndicatable _gameEndIndicatable;

    /// <summary>
    /// シーン移行後に最初に呼び出す関数
    /// 各クラスの初期化をまとめて行う
    /// </summary>

    [ContextMenu("GameStart")]
    public void SetUpGameProgress()
    {
        Debug.Log("ゲーム開始");
        _stageTimer.StartTimer();

        //ゲーム終了の通知を購読してゲーム終了処理を呼び出す
        if (_gameEndIndicatable == null) return;

        _gameEndIndicatable.OnGameEnd
            .Subscribe(_ =>
            {
                ExitGame();

            }).AddTo(this);
    }

    /// <summary>
    /// ゲーム終了時に呼び出す関数
    /// </summary>
    public void ExitGame()
    {
        _sceneSwitch.SwitchScene();
    }
}
