using UnityEngine;

public class AiBrain : MonoBehaviour
{
    private BaseEnemyAI baseEnemyAI;
    private AIStateTracker tracker;
    private AIBehaviourHouse behaviour; 
    private bool attack;

    public AIBehaviourHouse.Decision currentDecision;
    private bool playerOriented; // flag for orientation

    void Awake()
    {
        baseEnemyAI = GetComponent<BaseEnemyAI>();
        tracker = GetComponent<AIStateTracker>();
        behaviour = GetComponent<AIBehaviourHouse>();
    }

    void Update()
    {
        baseEnemyAI.isAttacking = attack;

        // keep playerOriented synced with tracker’s quota logic
        playerOriented = (tracker != null) ? (tracker.playerOriented) : false;
    }

    void Behaviour()
    {
        if (baseEnemyAI is RoamingAI)
        {
            if (playerOriented)
            {
                // Roaming AI distracted by towers, but player overrides
                if (tracker.currentSituation == AIStateTracker.Tracker.InVicinityOfPlayer)
                {
                    currentDecision = AIBehaviourHouse.Decision.AttackPlayer;
                    attack = true;
                }
                else if (tracker.currentSituation == AIStateTracker.Tracker.InVicinityOfTower)
                {
                    currentDecision = AIBehaviourHouse.Decision.AttackTower;
                    attack = true;
                }
                else
                {
                    currentDecision = AIBehaviourHouse.Decision.JourneyToPlayer;
                    attack = false;
                }
            }
            else
            {
                // Not player-oriented
                if (tracker.currentSituation == AIStateTracker.Tracker.InVicinityOfLeader)
                {
                    currentDecision = AIBehaviourHouse.Decision.WaitWithBoss;
                    attack = false;
                }
                else if (tracker.currentSituation == AIStateTracker.Tracker.InVicinityOfTower)
                {
                    currentDecision = AIBehaviourHouse.Decision.AttackTower;
                    attack = true;
                }
                else
                {
                    currentDecision = AIBehaviourHouse.Decision.JourneyToPlayer;
                    attack = false;
                }
            }
        }
        else if (baseEnemyAI is BossAI)
        {
            // Bosses move to towers unless player is near
            if (tracker.currentSituation == AIStateTracker.Tracker.InVicinityOfPlayer)
            {
                currentDecision = AIBehaviourHouse.Decision.AttackPlayer;
                attack = true;
            }
            else
            {
                currentDecision = AIBehaviourHouse.Decision.AttackTower;
                attack = true;
            }

            // --- Place for extra boss logic ---
        }
        else if (baseEnemyAI is LeaderAI)
        {
            // Leaders are always player-oriented
            currentDecision = AIBehaviourHouse.Decision.AttackPlayer;
            attack = true;
        }
        else if (baseEnemyAI is HiveAI)
        {
            // Hive AI always goes to player
            currentDecision = AIBehaviourHouse.Decision.AttackPlayer;
            attack = true;
        }
        else
        {
            // Fallback for other AI types
            currentDecision = AIBehaviourHouse.Decision.JourneyToPlayer;
            attack = false;
        }

        behaviour.CurrentDecisionLogic(currentDecision);
    }

    void OnTriggerEnter(Collider other)
    {
        Behaviour(); // re-evaluate decision when entering a trigger
    }

    void OnTriggerExit(Collider other)
    {
        Behaviour(); // re-evaluate decision when leaving a trigger
    }
}
