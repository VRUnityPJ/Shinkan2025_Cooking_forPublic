using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using TMPro;

public class ComboFoodText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _comboText;

    private void Start()
    {
        _comboText.DOFade(0f, 0);
    }

    public void ShowComboText(string recipeName)
    {
        _comboText.text = $"Get!! : {recipeName}";

        TextAnimation();
    }

    [ContextMenu("TestAnimation")]
    private void TextAnimation()
    {
        _comboText.DOFade(1f,1f);
        _comboText.rectTransform.localScale = Vector3.one * 0.2f;
        _comboText.rectTransform.DOScale(1f, 0.6f)
            .SetEase(Ease.OutBounce)
            .OnComplete(()=> _comboText.rectTransform.DOScale(0f, 0.6f).SetDelay(4f));
    }
}
