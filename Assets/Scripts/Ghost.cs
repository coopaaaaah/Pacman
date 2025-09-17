using System;
using UnityEngine;
using Pathfinding;

public class Ghost : MonoBehaviour
{
    public Transform enemyGFX;
    public Transform target;
    public float speed = 800f;
    public float nextWaypointDistance = 3f;

    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndOfPath = false;
    private Seeker _seeker;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (target && _seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_path == null)
        {
            return;
        }

        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }
        
        Vector2 direction = ((Vector2) _path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * (speed * Time.deltaTime);
        _rb.AddForce(force);
        
        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            _currentWaypoint++;
        }

        transform.localScale = _rb.linearVelocity.x switch
        {
            >= 0.01f => new Vector3(1, 1, 1),
            <= -0.01f => new Vector3(-1, 1, 1),
            _ => transform.localScale
        };
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }
}
