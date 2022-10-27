using UnityEngine;
using System.Collections.Generic;

class CompositionOrder : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _order;

    private void Awake()
    {
        foreach (var compositionRoot in _order)
        {
            compositionRoot.enabled = true;
        }
    }
}