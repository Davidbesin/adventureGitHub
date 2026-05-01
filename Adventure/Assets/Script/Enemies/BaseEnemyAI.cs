using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody), typeof(AiBrain))]
public class BaseEnemyAI : AIUnifiedTime, IEnemy, IHealth
{
    public float moveSpeed = 4f;
    [SerializeField] private int health;
    [SerializeField] private int baseHealth;
    private int MaxHealth => baseHealth * level;
    [SerializeField] private int level;
    public int Health {get {return health;} set {health =  Math.Clamp(value, 0, MaxHealth);} } 

    

    public float repathInterval = 0.5f;
    public bool isAttacking { get; set; }

    private GridTile currentGridTile;
    public Transform targetTransform;
    private Vector3 lastPosition;
    public float offset;

    public AiQueue aiQueue;
    [SerializeField] private Pathfinder pathfinder;

    private List<GridTile> currentPath;
    private int currentPathIndex;
    private Action aiAction;
    private Rigidbody ienemyRigidBody;

    private float repathTimer;

    void Start()
    {
        currentGridTile = GetNearestGridTile(transform.position);
        ienemyRigidBody = GetComponent<Rigidbody>();
        repathTimer = repathInterval;
        lastPosition = transform.position;
        Health = MaxHealth;
    }

    private void OnEnable()
    {
        aiAction = CalculatePathGate;
        aiQueue.Register(aiAction);
       
    }

    private void OnDisable()
    {
        aiQueue.Unregister(aiAction);
    }

    private void CalculatePathGate()
    {
        if (Vector3.Distance(targetTransform.position, lastPosition) > 0.1f || currentPath.Count == 0 || currentPath == null)
        {
            CalculatePath();
            lastPosition = transform.position;
        }   
    }
    

    void FixedUpdate()
    {
       // Debug.Log("done");
        FollowPathStep();
    }

    // Pathfinding
    
    private void CalculatePath()
    {
        if (targetTransform == null) return;

        GridTile startTile = GetNearestGridTile(transform.position);
        GridTile targetTile = GetNearestGridTile(targetTransform.position);

        if (startTile != null && targetTile != null)
        {
            currentPath = pathfinder.FindPath(startTile, targetTile);
            currentPathIndex = 0;
           // Debug.Log($"Path calculated with {currentPath.Count} tiles");
        }
    }

    // Movement step per frame
    private void FollowPathStep()
    {
        if (currentPath == null || currentPath.Count == 0) return;
        if (currentPathIndex >= currentPath.Count) return;

        GridTile tile = currentPath[currentPathIndex];
        Vector3 targetPos = tile.transform.position;

        // Apply offset along direction
        Vector3 direction = (targetPos - transform.position).normalized;
        Vector3 positionOffset = targetPos - direction * offset;

        if (Vector3.Distance(transform.position, positionOffset) > 0.01f)
        {
            Vector3 move = Vector3.MoveTowards(
                ienemyRigidBody.position,
                positionOffset,
                moveSpeed * Time.fixedDeltaTime
            );
            ienemyRigidBody.MovePosition(move);
           // Debug.Log("Moving");
        }
        else
        {
            // Reached this tile, move to next
          //  currentGridTile = tile;
            currentPathIndex++;
        }
    }

    GridTile GetNearestGridTile(Vector3 worldPos)
    {
        GridTile nearest = null;
        float minDist = Mathf.Infinity;

        foreach (GridTile tile in GridManager.AllTiles)
        {
            float dist = Vector3.Distance(tile.transform.position, worldPos);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = tile;
            }
        }

        return nearest;
    }

    // DEFAULT IENEMY
    public void MyDefault()
    {
        moveSpeed = 10;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        MyDefault();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void Die()
    {
        //Destroy(gameObject);
    }
} 