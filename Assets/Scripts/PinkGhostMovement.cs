using System.Linq;
using UnityEngine;

public class PinkGhostMovement : MonoBehaviour
{
    private Transform[] _waypoints;
    private readonly float _moveSpeed = 4;
    private int _currentWaypoint = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // will include Parent Enemy Path
        // seems expensive
        Transform[] transforms = GameObject.FindGameObjectWithTag("Enemy Path").GetComponentsInChildren<Transform>();
        _waypoints = transforms.Skip(1).ToArray();
        transform.position = _waypoints[_currentWaypoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentWaypoint <= _waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypoint].transform.position, _moveSpeed * Time.deltaTime);
            if (transform.position == _waypoints[_currentWaypoint].transform.position)
            {
                _currentWaypoint += 1;
            }

            if (_currentWaypoint == _waypoints.Length)
            {
                _currentWaypoint = 0;
            }
        }
    }

}
