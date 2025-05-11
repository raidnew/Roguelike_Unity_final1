using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour, IDamager
{
    [SerializeField] private int _damageValue;
    public float Damage => _damageValue;
}
