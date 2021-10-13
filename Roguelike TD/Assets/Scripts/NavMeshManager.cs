using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface enemySurface;

    private void Start()
    {
        UpdateNavMeshSurface();
    }

    public void UpdateNavMeshSurface()
    {
        enemySurface.BuildNavMesh();
    }
}
