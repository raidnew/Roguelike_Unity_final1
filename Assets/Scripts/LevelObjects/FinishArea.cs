using System;
using UnityEngine;

public class FinishArea : MonoBehaviour, IAmUsable
{
    public static Action Complete;

    public bool CanUse => throw new System.NotImplementedException();

    public void Use()
    {
        Complete?.Invoke();
    }
}
