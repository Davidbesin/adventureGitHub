using UnityEngine;
using System.Collections.Generic;
using System;


public class Planet : MonoBehaviour
{
    [SerializeReference]public List<BaseResource> resourceTypes = new();
    public List <IWaveContributor> listContributors = new();
    public int EnemyWaveStrength{get; set;}
    
    
    public void JoinList(IWaveContributor waveContributor)
    {
        listContributors.Add(waveContributor);
        RecalculateWave();
    }

    public void GetOutOftheList(IWaveContributor waveContributor)
    {
        listContributors.Add(waveContributor);
        RecalculateWave();
    }
    void RecalculateWave()
    {
        foreach (IWaveContributor item in listContributors)
        {
            EnemyWaveStrength += item.ContributeToWave();
        }
        
    } 
    
    
    //Checks For whether player is within its range and assigns the SO related to the player Slot

    private void OnTriggerEnter(Collider other) 
    {
        Player.Instance.CollectPlanet(this);
    }


}  
