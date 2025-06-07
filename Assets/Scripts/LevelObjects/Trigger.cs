using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class Trigger : MonoBehaviour, IAmUsable
{
    [SerializeField] private GameObject[] _triggerableObjects;

    public bool CanUse => true;

    public void Use()
    {
        foreach (GameObject triggeredObject in _triggerableObjects)
        {
            IAmTriggerable obj;
            if (triggeredObject.TryGetComponent<IAmTriggerable>(out obj))
                obj.Trigger();
        }
    }
}
