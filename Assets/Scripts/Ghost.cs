using UnityEngine;
using Pathfinding;

public class Ghost : MonoBehaviour
{

    public AIPath aiPath;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = aiPath.desiredVelocity.x >= 0.01f ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }
}
