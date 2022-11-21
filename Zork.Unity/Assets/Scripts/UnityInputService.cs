using System;
using TMPro;
using UnityEngine;
using UnityEngine.WSA;
using Zork.Common;

public class UnityInputService : MonoBehaviour, IInputService
{
    [SerializeField]
    private TMP_InputField InputField;
    
    public event EventHandler<string> InputReceived;

    public void ProcessInput()
    {
        if (!string.IsNullOrWhiteSpace(InputField.text))
        {
            InputReceived.Invoke(this, InputField.text.Trim());
        }

        InputField.text = string.Empty;
    }

    public void SetFocus()
    {
        InputField.Select();
        InputField.ActivateInputField();
    }
}
