using System;
using UnityEngine;

public interface ISight
{
    public Action<Vector3> OnFoundPlayer { get; set; }
    public Action<Vector3> OnWatchPlayer { get; set; }
    public Action<Vector3> OnLostPlayer { get; set; }
}
