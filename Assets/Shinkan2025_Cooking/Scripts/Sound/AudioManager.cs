using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using UniRx;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [Inject] private IGameProgressIndicatable _progressIndicatable;

    public void Start()
    {
        _progressIndicatable.OnStartGame
            .Subscribe(_ => _audioSource.Play()).AddTo(this);
    }


}
