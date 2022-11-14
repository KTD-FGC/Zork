using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zork.Common;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UnityInputService Input;
    
    [SerializeField]
    private UnityOutputService Output;

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

        _game.Run(Input, Output);
    }

    private Game _game;
}
