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

    [SerializeField]
    private Text time;

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
        time.text = string.Format(Strings.TimeText,
            (int)(game.TotalSeconds / 3600), (int)(game.TotalSeconds % 3600) / 60,
            (int)(game.TotalSeconds % 60));
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

        if (!time)
        {
            Debug.LogError("GameGui.Start(): time is null");
            return;
        }
    }
}
