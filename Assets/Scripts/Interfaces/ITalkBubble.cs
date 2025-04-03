using System;
using UnityEngine;

public interface ITalkBubble
{
    public Action<ITalkBubble> OnRemoveBubble { get; set; }
    public int CheckSumm { get; set; }
    public void ShowMessage(string message, Transform position, Camera camera, int time);
}

