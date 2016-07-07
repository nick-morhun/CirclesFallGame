using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GameGui : MonoBehaviour
{
    [SerializeField]
    private Text gameScore;

    [SerializeField]
    private Text gameLevel;

    public void UpdateUI(Game game)
    {
        if (game == null)
        {
            Debug.LogError("GameGui.Show(): game is null");
            return;
        }

        gameLevel.text = string.Format(Strings.LevelText,
            (game.Level + 1).ToString(CultureInfo.InvariantCulture));
        gameScore.text = string.Format(Strings.ScoreText,
            game.Score.ToString(CultureInfo.InvariantCulture));
    }

    private void Start()
    {
        if (!gameScore)
        {
            Debug.LogError("GameGui.Start(): gameScore is null");
            return;
        }

        if (!gameLevel)
        {
            Debug.LogError("GameGui.Start(): gameLevel is null");
            return;
        }
    }
}
