using UnityEngine;
using System.Collections.Generic;

class CompositionOrder : MonoBehaviour
{
    [SerializeField] private List<CompositeRoot> _order;

    private void Awake()
    {
        foreach (var compositionRoot in _order)
        {
            compositionRoot.Compose();
            compositionRoot.enabled = true;
        }
    }
}