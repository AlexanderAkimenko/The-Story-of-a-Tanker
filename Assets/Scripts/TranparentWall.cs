using UnityEngine;

public class TranparentWall : MonoBehaviour
{
    [SerializeField] private Material _transparentMaterial;

    private Renderer _renderer;
    private Material _originMaterial;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        _originMaterial = _renderer != null ? _renderer.material : null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other. GetComponent<WallTrigger>() != null)
        {
            _renderer.material = _transparentMaterial;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<WallTrigger>() != null)
        {
            _renderer.material = _originMaterial;
        }
    }
}
