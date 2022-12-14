using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zork.Common;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine.UI;
using UnityEditor;

public class UnityOutputService : MonoBehaviour, IOutputService
{
    [SerializeField]
    private TextMeshProUGUI TextLinePrefab;

    [SerializeField]
    private Image NewLinePrefab;

    [SerializeField]
    private Transform ContentTransform;

    public void Write(string message) => ParseAndWriteLine(message);

    public void Write(object obj) => ParseAndWriteLine(obj.ToString());

    public void WriteLine(string message) => ParseAndWriteLine(message);

    public void WriteLine(object obj) => ParseAndWriteLine(obj.ToString());

    private void ParseAndWriteLine(string message)
    {
        var textLine = Instantiate(TextLinePrefab, ContentTransform);
        textLine.text = message;
        _entries.Add(textLine.gameObject);
    }

    private List<GameObject> _entries = new List<GameObject>();
}
