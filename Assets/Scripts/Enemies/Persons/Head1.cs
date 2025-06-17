using UnityEngine;

public class Head1 : MonoBehaviour, IAmTriggerable
{
    [SerializeField] private Transform _tellerPoint;
    [SerializeField] private string[] _messages;

    private static int _talkCount = 0;
    private bool _wasTalk = false;

    public bool CanUse => throw new System.NotImplementedException();

    private void Talk()
    {
        _talkCount++;
        _wasTalk = true;
        string message;
        if (_messages.Length == 0) message = "Argh-h-h!!!";
        else message = _talkCount >= _messages.Length ? _messages[_messages.Length - 1] : _messages[_talkCount];
        Bubble.Message(message, _tellerPoint);
    }

    public void Trigger()
    {
        if (!_wasTalk)
            Talk();
    }
}
