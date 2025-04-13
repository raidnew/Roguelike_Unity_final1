using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserInterface : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _coinText;

    private void Awake()
    {
        Game.OnCountCoinsChanged += OnCountCoinChangeed;
    }

    private void OnCountCoinChangeed(int count)
    {
        _coinText.text = count.ToString();
    }

}
