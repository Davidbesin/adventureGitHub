using UnityEngine;
using System.Collections.Generic;

public class AIBehaviourHouse : MonoBehaviour
{
    public enum Decision
    {
        JourneyToPlayer,
        AttackTower,
        AttackPlayer,
        Gather,
        WaitWithBoss,
        JourneyWithTheBoss 
    }

    private BaseEnemyAI baseEnemyAI;
    [SerializeField] private Transform player;
    private AIStateTracker tracker;
    private Transform theTargetTransform;

    // threshold for "enough followers"
    [SerializeField] private int requiredFollowers = 3;

    private void Awake()
    {
        baseEnemyAI = GetComponent<BaseEnemyAI>();
        tracker = GetComponent<AIStateTracker>();
    } 

    private void Update()
    {
        baseEnemyAI.targetTransform = theTargetTransform;
    }

    public void CurrentDecisionLogic(Decision currentDecision)
    {
        switch (currentDecision)
        {
            case Decision.JourneyToPlayer:
                JourneyToPlayer();
                break;
            case Decision.AttackTower:
                AttackBase();
                break;
            case Decision.AttackPlayer:
                AttackPlayer();
                break;
            case Decision.Gather:
                GatherBoss();
                break;
            case Decision.WaitWithBoss:
                BossGathering();
                break;   
            case Decision.JourneyWithTheBoss:
                JourneyWithBoss();
                break;
        }
    }

    void JourneyToPlayer()
    {
        theTargetTransform = player;
    }

    void AttackBase()
    {
        if (tracker.currentTower != null)
        theTargetTransform = tracker.currentTower;
        else { theTargetTransform = player;}
    } 

    void AttackPlayer()
    {
        theTargetTransform = player;
    }

    void GatherBoss()
    {
        // check how many roaming AIs are currently tracked by the leader
        List<RoamingAI> roamingList = tracker.GetRoamingList();

        if (roamingList.Count >= requiredFollowers)
        {
            // enough followers gathered, now move toward player
            theTargetTransform = player;
        }
        else
        {
            // not enough followers yet, do nothing
            return;
        }
    }

    void BossGathering()
    {
        if (tracker.currentLeader != null)
        theTargetTransform = tracker.currentLeader; 
        else 
        theTargetTransform = null;
    }

    void JourneyWithBoss()
    {
        if (tracker.currentLeader != null)
        {
            if (tracker.currentSituation == AIStateTracker.Tracker.InVicinityOfPlayer)
                theTargetTransform = player;
            else
                theTargetTransform = tracker.currentLeader;
        }
    }
}
