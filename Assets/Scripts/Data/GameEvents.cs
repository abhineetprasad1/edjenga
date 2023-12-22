using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static System.Action<Dictionary<string, object>> OnStateChanged = delegate { };
    public static System.Action<Dictionary<string, object>> OnArrowClick = delegate { };
    public static System.Action<Dictionary<string, object>> OnJblockClick = delegate { };
    public static System.Action<Dictionary<string, object>> OnStackTest = delegate { };
    public static System.Action<Dictionary<string, object>> OnReset = delegate { };

}
