using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour, IGround
{
    public Vector2 Speed
    {
        get { return Vector2.zero; }
    }
}
