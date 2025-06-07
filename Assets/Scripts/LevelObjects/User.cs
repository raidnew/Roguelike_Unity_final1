using UnityEngine;

public class User : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IAmUsable usedObject;
        if (collision.gameObject.TryGetComponent<IAmUsable>(out usedObject))
            usedObject.Use();
    }
}
