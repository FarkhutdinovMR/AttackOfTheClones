using System.Collections;
using UnityEngine;

public class ColorSwithcer : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _mesh;
    [SerializeField] private Color _color;
    [SerializeField] private float _waitTime = 0.5f;

    private Coroutine _coroutine;
    private Color[] _colors;

    private void Awake()
    {
        _colors = new Color[_mesh.materials.Length];

        for (int i = 0; i < _mesh.materials.Length; i++)
            _colors[i] = _mesh.materials[i].color;
    }

    public void Init()
    {
        for (int i = 0; i < _mesh.sharedMaterials.Length; i++)
            _mesh.materials[i].color = _colors[i];
    }

    public void Switch()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(SwitchColor());
    }

    private IEnumerator SwitchColor()
    {
        for (int i = 0; i < _mesh.sharedMaterials.Length; i++)
            _mesh.materials[i].color = _color;

        yield return new WaitForSeconds(_waitTime);

        Init();
        _coroutine = null;
    }
}