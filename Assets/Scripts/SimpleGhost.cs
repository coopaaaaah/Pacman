using UnityEngine;

public class SimpleGhost : MonoBehaviour
{

    public Transform target;
    
    // Update is called once per frame
    void Update()
    {
        transform.localScale = target.position.x switch
        {
            >= 0.01f => new Vector3(1, 1, 1),
            <= -0.01f => new Vector3(-1, 1, 1),
            _ => transform.localScale
        };
    }
}
