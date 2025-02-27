using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;

    public void CountPointText(string currentPoint)
    {
        _scoreText.text = $"TotalScore:{currentPoint}";
    }
}
