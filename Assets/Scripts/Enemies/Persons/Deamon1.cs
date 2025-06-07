using System.Collections;
using UnityEngine;

public class Deamon1 : MonoBehaviour, IAmTriggerable
{
    [SerializeField] private Transform _tellerPoint;
    [SerializeField] private string[] _messages;
    [SerializeField] private Health _health;

    private static int _dieCount = 0;
    private bool _wasTalk = false;

    public bool CanUse => throw new System.NotImplementedException();

    private void OnEnable()
    {
        _health.OnDied += OnDied;
    }

    private void OnDisable()
    {
        _health.OnDied -= OnDied;
    }

    private void Talk()
    {
        _wasTalk = true;
        string message;
        if (_messages.Length == 0) message = "Argh-h-h!!!";
        else message = _dieCount >= _messages.Length ? _messages[_messages.Length - 1] : _messages[_dieCount];
        Bubble.Message(message, _tellerPoint);
    }

    private void OnDied()
    {
        _dieCount++;
    }

    public void Trigger()
    {
        if(!_wasTalk)
            Talk();
    }
}