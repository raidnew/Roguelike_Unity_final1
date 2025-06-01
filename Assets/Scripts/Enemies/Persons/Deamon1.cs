using System.Collections;
using UnityEngine;

public class Deamon1 : MonoBehaviour
{
    [SerializeField] private Transform _tellerPoint;

    private void Start()
    {
        StartCoroutine(Behaviour());
    }

    private IEnumerator Behaviour()
    {
        yield return new WaitForSeconds(3.0f);
        Bubble.Message("Hello", _tellerPoint);
    }
}