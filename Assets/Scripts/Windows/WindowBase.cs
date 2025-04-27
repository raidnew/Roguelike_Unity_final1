using UnityEngine;

public class WindowBase : MonoBehaviour, IWindow
{
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
}
