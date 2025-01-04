using UnityEngine;

public class MeshSmooth : MonoBehaviour
{
    [SerializeField]
    private MeshFilter _meshRenderer;

    private void Awake()
    {
        MeshSmoother();
    }

    private void OnValidate()
    {
        MeshSmoother();
    }

        private void MeshSmoother()
    {
        _meshRenderer = GetComponent<MeshFilter>();
        BashUtils.SmoothNormals(_meshRenderer.mesh);
    }
}
