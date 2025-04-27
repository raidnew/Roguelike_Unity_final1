using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsHitArea : MonoBehaviour
{
    public Action OnClick;
    public Action OnOver;
    public Action OnOut;

    private bool _hover = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.Left) && MouseOnObject())
            OnClick?.Invoke();
        else
        {
            if (_hover && !MouseOnObject())
            {
                _hover = false;
                OnOut?.Invoke();
            }
            else if(!_hover && MouseOnObject())
            {
                _hover = true;
                OnOver?.Invoke();
            }
        }
    }

    private bool MouseOnObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
        if(hit) return hit.collider.gameObject == this.gameObject;
        return false;
    }
}
