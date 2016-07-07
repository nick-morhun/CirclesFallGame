using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class GameController : MonoBehaviour
{
    private Game game;

    [SerializeField]
    private GameGui gameGui;

    [SerializeField]
    private InputManager inputManager;

    // Use this for initialization
    private void Start()
    {
        if (!gameGui)
        {
            Debug.LogError("GameController.Start(): gameGui is null");
            return;
        }

        if (!inputManager)
        {
            Debug.LogError("GameController.Start(): inputManager is null");
            return;
        }

        game = NewGame();
        gameGui.UpdateUI(game);
        inputManager.Touch += OnTouch;

        // TODO: create random circles
        var circleGO = GameObject.FindWithTag(Configuration.Instance.Target.Tag);
        var circle = circleGO.GetComponent<FallingObject>();
        circle.Initialize(circle.transform.localScale.x);
    }

    public void OnTouch(TouchEventArgs args)
    {
        RaycastHit2D hit = Physics2D.Raycast(args.PointerWorldPosition, Vector2.zero);

        if (hit.collider && hit.collider.tag == Configuration.Instance.Target.Tag)
        {
            var obj = hit.collider.GetComponent<FallingObject>();

            if (!obj)
            {

                return;
            }

            game.Score += obj.Score;

            int requiredScore = Configuration.Instance.PointsToNextLevel * (game.Level + 1);
            if (game.Score >= requiredScore)
            {
                
            }

            Object.Destroy(hit.collider.gameObject);
            gameGui.UpdateUI(game);
        }
    }

    private static Game NewGame()
    {
        return new Game();
    }
}
