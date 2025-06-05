using System;
using UnityEngine;

public class PropertyProvider : MonoBehaviour
{
    public Action<float> SetPercent;
    public Action<int,int> SetValue;
    public Action Finish;
}
