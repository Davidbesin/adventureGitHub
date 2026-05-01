using UnityEngine;
using System;
using System.Collections.Generic;

public class AIStateTracker : MonoBehaviour
{
    public bool JoinLeader { get; private set; }

    public enum Tracker
    {
        InVicinityOfTower,
        InVicinityOfPlayer,
        InVicinityOfLeader,
    }

    private HashSet<Tracker> trackingSet = new(); // avoids duplicates
    public Tracker currentSituation { get; set; }

    private int quota;
    public bool playerOriented {get; private set;}
    

    private BaseEnemyAI aiReference;   // generic AI reference
    private LeaderAI leaderReference;  // specific leader reference if applicable

    public Transform currentTower;
    public Transform currentLeader;

    private List<RoamingAI> roamingAITracker = new(); // stays here if this tracker owns it

    private void Awake()
    {
        aiReference = GetComponent<BaseEnemyAI>();
        leaderReference = aiReference as LeaderAI; // null if not a leader
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider == null) return;

        LocateAndAttackPlayer(collider);
        LocateAndAttackTower(collider);
        RoamingAiFindBoss(collider);
        LeaderGatherRoamingAI(collider);
    }

    private void Update()
    {
        playerOriented = (quota == 0);
        currentSituation = TrackerPriority();
    }

    void OnTriggerExit(Collider other)
    {
        if (other == null) return;

        BaseDefensiveTower tower = other.GetComponent<BaseDefensiveTower>();
        Player player = other.GetComponent<Player>();
        LeaderAI leader = other.GetComponent<LeaderAI>();
        RoamingAI roaming = other.GetComponent<RoamingAI>();

        if (tower != null) trackingSet.Remove(Tracker.InVicinityOfTower);
        if (player != null) trackingSet.Remove(Tracker.InVicinityOfPlayer);
        if (leader != null)
        {
            trackingSet.Remove(Tracker.InVicinityOfLeader);
            JoinLeader = false;
            currentLeader = null;
        }
        if (roaming != null && leaderReference != null)
        {
            roamingAITracker.Remove(roaming);
        }
    }

    Tracker TrackerPriority()
    {
        if (trackingSet.Contains(Tracker.InVicinityOfPlayer))
            return Tracker.InVicinityOfPlayer;

        if (trackingSet.Contains(Tracker.InVicinityOfLeader) &&
            trackingSet.Contains(Tracker.InVicinityOfTower) &&
            playerOriented)
            return Tracker.InVicinityOfLeader;

        if (trackingSet.Contains(Tracker.InVicinityOfTower))
            return Tracker.InVicinityOfTower;

        if (trackingSet.Contains(Tracker.InVicinityOfLeader))
            return Tracker.InVicinityOfLeader;

        return currentSituation; // fallback to last known situation
    }

    public List<RoamingAI> GetRoamingList()
    {
        return new List<RoamingAI>(roamingAITracker);
    }


    void LocateAndAttackTower(Collider other)
    {
        BaseDefensiveTower tower = other.GetComponent<BaseDefensiveTower>();
        if (tower == null) return;

        trackingSet.Add(Tracker.InVicinityOfTower);
        quota = Mathf.Max(0, quota - 1); // clamp quota
        currentTower = tower.transform;
    }

    void RoamingAiFindBoss(Collider other)
    {
        LeaderAI leader = other.GetComponent<LeaderAI>();
        if (leader == null) return;

        trackingSet.Add(Tracker.InVicinityOfLeader);
        JoinLeader = true;
        currentLeader = leader.transform;
    }

    void LeaderGatherRoamingAI(Collider other)
    {
        RoamingAI roaming = other.GetComponent<RoamingAI>();
        if (leaderReference == null) return; // only leaders gather
        if (roaming != null && !roamingAITracker.Contains(roaming))
        {
            roamingAITracker.Add(roaming);
        }
    }

    void LocateAndAttackPlayer(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;

        trackingSet.Add(Tracker.InVicinityOfPlayer);
    }
}
