using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class SummoningDecision : MonoBehaviour
{
    private BundlePackage turrents;
    private Rigidbody rb;

    private bool placed = false;
    private bool Summoning => SummonManager.Instance.summoning;

    [SerializeField] float checkRadius = 1.5f;
    [SerializeField] Vector3 offset;

    public UnityEvent eventRed;
    public UnityEvent eventGreen;

    private bool debris;
    private bool lastDebrisState;

    private Coroutine routine;

    public List<Collider> detectedObjects = new();

    void Awake()
    {
        turrents = GetComponent<BundlePackage>();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        placed = false;
        routine = StartCoroutine(CheckRoutine());
        rb.position = SummonManager.Instance.platform.position + offset;

        eventRed.AddListener(SummonManager.Instance.CannotEndSummoning);
        eventGreen.AddListener(SummonManager.Instance.CanEndSummoning);
    }

    void OnDisable()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }

    IEnumerator CheckRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.33f);

        while (!placed)
        {
            if (Summoning)
            {
                CheckSphere();
                SummonMethod();
            }
            else
            {
                TowerIsPlaced(); // lock once summoning ends
            }

            yield return wait;
        }
    }

    void CheckSphere()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, checkRadius);
        detectedObjects.Clear();
        debris = false;

        foreach (var hit in hits)
        {
            if (IsIgnored(hit)) continue;
            if (hit.transform.IsChildOf(transform)) continue;

            detectedObjects.Add(hit);
            debris = true;
            break;
        }
    }

    bool IsIgnored(Collider other)
    {
        return other.TryGetComponent<HexTile>(out _) ||
               other.TryGetComponent<Player>(out _) ||
               other.TryGetComponent<Detector>(out _) ||
               other.TryGetComponent<MineSpawner>(out _);
    }

    void SummonMethod()
    {
        if (placed) return; // safety guard

        rb.position = SummonManager.Instance.platform.position + offset;
        SetState(red: debris, green: !debris);

        if (debris != lastDebrisState)
        {
            if (debris) eventRed?.Invoke();
            else eventGreen?.Invoke();

            lastDebrisState = debris;
        }
    }

    public void TowerIsPlaced()
    {
        SetState(normal: true);
        placed = true;

        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }

    public void ToggleTower(bool plaBool)
    {
        placed = plaBool;
    }

    void SetState(bool red = false, bool green = false, bool normal = false)
    {
        if (turrents.red.activeSelf != red) turrents.red.SetActive(red);
        if (turrents.green.activeSelf != green) turrents.green.SetActive(green);
        if (turrents.normal.activeSelf != normal) turrents.normal.SetActive(normal);
    }
}
