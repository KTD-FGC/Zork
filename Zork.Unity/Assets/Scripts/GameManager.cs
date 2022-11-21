using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Zork.Common;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UnityInputService InputService;
    
    [SerializeField]
    private UnityOutputService OutputService;

    [SerializeField]
    private TextMeshProUGUI Location;

    [SerializeField]
    private TextMeshProUGUI Score;

    [SerializeField]
    private TextMeshProUGUI Moves;

    private void Awake()
    {
        TextAsset gameJson = Resources.Load<TextAsset>("GameJson");
        _game = JsonConvert.DeserializeObject<Game>(gameJson.text);
        _game.Player.LocationChanged += Player_LocationChanged;
        _game.Player.MovesChanged += Player_MovesChanged;
        _game.Player.ScoreChanged += Player_ScoreChanged;
        _game.Run(InputService, OutputService);
    }

    public void Player_LocationChanged(object sender, Room location)
    {
        Location.text = location.Name;
    }

    public void Player_MovesChanged(object sender, int moves)
    {
        Moves.text = $"Moves: {moves}";
    }

    public void Player_ScoreChanged(object sender, int score)
    {
        Score.text = $"Score: {score}";
    }

    private void Start()
    {
        InputService.SetFocus();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InputService.ProcessInput();
            InputService.SetFocus();
        }

        if (!_game.IsRunning)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }

    private Game _game;
}
