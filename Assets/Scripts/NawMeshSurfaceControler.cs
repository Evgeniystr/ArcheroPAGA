using UnityEngine;
using UnityEngine.AI;

public class NawMeshSurfaceControler : MonoBehaviour
{
    void Start()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
