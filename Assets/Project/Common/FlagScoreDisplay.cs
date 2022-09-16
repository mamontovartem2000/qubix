using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlagScoreDisplay : MonoBehaviour
{
    [SerializeField] private Image _scorePanel;
    [SerializeField] private TMP_Text _redScore;
    [SerializeField] private TMP_Text _blueScore;

    private void Awake()
    {
        FlagEvents.EnableFlagScoreDisplay += EnableScorePanel;
        FlagEvents.UpdateFlagScore += UpdateScore;
    }

    private void EnableScorePanel()
    {
        _scorePanel.gameObject.SetActive(true);
    }

    private void UpdateScore(int red, int blue)
    {
        _redScore.text = red.ToString();
        _blueScore.text = blue.ToString();
    }
    
    private void OnDestroy()
    {
        FlagEvents.EnableFlagScoreDisplay -= EnableScorePanel;
        FlagEvents.UpdateFlagScore -= UpdateScore;
    }
}
