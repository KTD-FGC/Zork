using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zork.Common;

public class UnityInputService : MonoBehaviour, IInputService
{
    public event EventHandler<string> InputReceived;
}
