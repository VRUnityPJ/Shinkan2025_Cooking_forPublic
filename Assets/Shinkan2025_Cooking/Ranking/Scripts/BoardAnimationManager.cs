using DG.Tweening;
using Ranking.Scripts;
using Ranking.Scripts.DataBase;
using Shinkan2025_Cooking.Scripts.Points;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shinkan2025_Cooking.Ranking.Scripts
{
    public class BoardAnimationManager : MonoBehaviour
    {
        [SerializeField] private GameObject NormalName;
        [SerializeField] private GameObject HardName;
        [SerializeField] private Button[] buttons;
        private TextMeshProUGUI[] normalNameTexts;
        private TextMeshProUGUI[] hardNameTexts;
        private void Start()
        {
            normalNameTexts = NormalName.GetComponentsInChildren<TextMeshProUGUI>();
            hardNameTexts = HardName.GetComponentsInChildren<TextMeshProUGUI>();
        }

        public void FadeOut()
        {
            foreach (var btn in buttons)
            {
                btn.interactable = false;
            }
            
            foreach (var txt in normalNameTexts)
            {
                txt.DOFade(0, 0.5f);
            }
            
            foreach (var txt in hardNameTexts)
            {
                txt.DOFade(0, 0.5f);
            }
        }

        public void FadeIn()
        {
            var seq = DOTween.Sequence().Pause();
            
            for (int i = 0; i < normalNameTexts.Length; i++)
            {
                seq.Append(normalNameTexts[i].DOFade(1, 0.4f));
                seq.Join(hardNameTexts[i].DOFade(1, 0.4f));
                seq.AppendInterval(0.2f); 
            }

            seq.AppendCallback(() =>
            {
                foreach (var btn in buttons)
                {
                    btn.interactable = true;
                }
            });
            seq.Restart();
        }

        public void UpdateNormalText(RankingData[] records)
        {
            if(records.Length != normalNameTexts.Length)
                Debug.LogError("取得したランキングの数がリーダーボードと一致していません");
            
            for (int i = 0; i < normalNameTexts.Length; i++)
            {
                Point point = records[i].GetData<Point>();
                PlayerName playerName = records[i].GetData<PlayerName>();

                normalNameTexts[i].text = point.IntValue.ToString() + "\n" + playerName.StringValue;
            }
        }
        public void UpdateHardText(RankingData[] records)
        {
            if(records.Length != normalNameTexts.Length)
                Debug.LogError("取得したランキングの数がリーダーボードと一致していません");
            
            for (int i = 0; i < hardNameTexts.Length; i++)
            {
                Point point = records[i].GetData<Point>();
                PlayerName playerName = records[i].GetData<PlayerName>();

                hardNameTexts[i].text = point.IntValue.ToString() + "\n" + playerName.StringValue;
            }
        }
    }
}