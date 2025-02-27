using System.Collections;
using System.Collections.Generic;
using Shinkan2025_Cooking.Scripts.Timer;
using UnityEngine;
using UniRx;
using VContainer;

/// <summary>
/// �Q�[���i�s���Ǘ�����N���X�B�����ŃV�[���ڍs�ȂǌĂяo���B
/// </summary>
public class GameProgress : MonoBehaviour
{
    [SerializeField] StageTimer _stageTimer;
    [SerializeField] SceneSwitch _sceneSwitch;

    [Inject] private IGameEndIndicatable _gameEndIndicatable;

    /// <summary>
    /// �V�[���ڍs��ɍŏ��ɌĂяo���֐�
    /// �e�N���X�̏��������܂Ƃ߂čs��
    /// </summary>

    [ContextMenu("GameStart")]
    public void SetUpGameProgress()
    {
        Debug.Log("�Q�[���J�n");
        _stageTimer.StartTimer();

        //�Q�[���I���̒ʒm���w�ǂ��ăQ�[���I���������Ăяo��
        if (_gameEndIndicatable == null) return;

        _gameEndIndicatable.OnGameEnd
            .Subscribe(_ =>
            {
                ExitGame();

            }).AddTo(this);
    }

    /// <summary>
    /// �Q�[���I�����ɌĂяo���֐�
    /// </summary>
    public void ExitGame()
    {
        _sceneSwitch.SwitchScene();
    }
}
