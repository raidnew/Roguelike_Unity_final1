using System.Collections;
using UnityEngine;

public class Deamon1 : MonoBehaviour
{
    [SerializeField] private Transform _tellerPoint;
    [SerializeField] private string[] _messages;
    [SerializeField] private Health _health;

    private static int _dieCount = 0;

    private void OnEnable()
    {
        _health.OnDied += OnDied;
    }

    private void OnDisable()
    {
        _health.OnDied -= OnDied;
    }

    private void Start()
    {
        StartCoroutine(Behaviour());
    }

    private void OnDied()
    {
        _dieCount++;
    }

    private IEnumerator Behaviour()
    {
        yield return new WaitForSeconds(3.0f);
        string message;
        if (_messages.Length == 0) message = "Argh-h-h!!!";
        else message = _dieCount >= _messages.Length ? _messages[_messages.Length - 1] : _messages[_dieCount];
        Bubble.Message(message, _tellerPoint);
    }
}