using System.Collections;
using TMPro;
using UnityEngine;

public class Notice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private GameObject _messageObject;
    private static Notice __instance;
    private CanvasRenderer _canvasRenderer;

    public static void FastNotice(string message)
    {
        __instance.ShowNotice(message);
    }

    private void Awake()
    {
        __instance = this;
        HideMessageArea();
    }

    private void ShowNotice(string message)
    {
        StartCoroutine(MessageIsShowing(message));
    }

    private void HideMessageArea()
    {
        _messageObject.SetActive(false);
        _textField.text = "";
    }

    private void ShowMessage(string message)
    {
        _messageObject.SetActive(true);
        _textField.text = message;
    }

    IEnumerator MessageIsShowing(string message)
    {
        ShowMessage(message);
        yield return new WaitForSeconds(4);
        HideMessageArea();
        yield return false;
    }

    
}
