using UnityEngine;
using System.Collections.Generic;
using System;

public class MiningRateManager : MonoBehaviour
{    
    public static MiningRateManager Instance; 
    
    //Authority
    public CopperStat copperStat;
    public DiamondStat diamondStat;
    public GoldStat goldStat;
    public IronStat ironStat;
    public GemStat manaGemStat;
    public RubyStat rubiesStat;
    public SilverStat silverStat;
    public StoneStat stoneStat;
    public WoodStat woodStat;
    

    public List<CopperStat> copperList = new();
    public List<DiamondStat> diamondList = new();
    public List<GoldStat> goldList = new();
    public List<IronStat> ironList = new();
    public List<GemStat> gemList = new();
    public List<RubyStat> rubyList = new();
    public List<SilverStat> silverList = new();
    public List<StoneStat> stoneList = new();
    public List<WoodStat> woodList = new();
    
    void Awake()
    {
        Instance = this;
        Debug.Log("yes");  
    }

    private void Start()
    {
        SyncTransactions();
    }

    public void SyncTransactions()
    {
        foreach(var stat in goldList)
        {
            stat.toSpend = goldStat.toSpend;
        }
        foreach(var stat in copperList)
        {
            stat.toSpend = copperStat.toSpend;
        }
        foreach(var stat in diamondList)
        {
            stat.toSpend = diamondStat.toSpend;
        }
        foreach(var stat in ironList)
        {
            stat.toSpend = ironStat.toSpend;
        }
        foreach(var stat in gemList)
        {
            stat.toSpend = manaGemStat.toSpend;
        }
        foreach(var stat in rubyList)
        {
            stat.toSpend = rubiesStat.toSpend;
        }
        foreach(var stat in silverList)
        {
            stat.toSpend = silverStat.toSpend;
        }
        foreach(var stat in woodList)
        {
            stat.toSpend = woodStat.toSpend;
        }
        foreach(var stat in stoneList)
        {
            stat.toSpend = stoneStat.toSpend;
        }
    }

    public void SyncAllLevels()
    {
        SyncCopperLevel();
        SyncDiamondLevel();
        SyncGoldLevel();
        SyncIronLevel();
        SyncGemLevel();
        SyncRubyLevel();
        SyncSilverLevel();
        SyncStoneLevel();
        SyncWoodLevel();
    }

    public void SyncCopperLevel() 
    {
        int highestLevel = 0;
        for (int i = 0; i < copperList.Count; i++)
        {
            if (copperList[i].level > highestLevel)
                highestLevel = copperList[i].level;
        }
        for (int i = 0; i < copperList.Count; i++)
            copperList[i].level = highestLevel;

        CopperStat.staticlevel = highestLevel;
    }

    public void SyncDiamondLevel()
    {
        int highestLevel = 0;
        for (int i = 0; i < diamondList.Count; i++)
        {
            if (diamondList[i].level > highestLevel)
                highestLevel = diamondList[i].level;
        }
        for (int i = 0; i < diamondList.Count; i++)
            diamondList[i].level = highestLevel;

        DiamondStat.staticlevel = highestLevel;
    }

    public void SyncGoldLevel()
    {
        int highestLevel = 0;
        for (int i = 0; i < goldList.Count; i++)
        {
            if (goldList[i].level > highestLevel)
                highestLevel = goldList[i].level;
        }
        for (int i = 0; i < goldList.Count; i++)
            goldList[i].level = highestLevel;

        GoldStat.staticlevel = highestLevel;
    }

    public void SyncIronLevel()
    {
        int highestLevel = 0;
        for (int i = 0; i < ironList.Count; i++)
        {
            if (ironList[i].level > highestLevel)
                highestLevel = ironList[i].level;
        }
        for (int i = 0; i < ironList.Count; i++)
            ironList[i].level = highestLevel;

        IronStat.staticlevel = highestLevel;
    }

    public void SyncGemLevel()
    {
        int highestLevel = 0;
        for (int i = 0; i < gemList.Count; i++)
        {
            if (gemList[i].level > highestLevel)
                highestLevel = gemList[i].level;
        }
        for (int i = 0; i < gemList.Count; i++)
            gemList[i].level = highestLevel;

        GemStat.staticlevel = highestLevel;
    }

    public void SyncRubyLevel()
    {
        int highestLevel = 0;
        for (int i = 0; i < rubyList.Count; i++)
        {
            if (rubyList[i].level > highestLevel)
                highestLevel = rubyList[i].level;
        }
        for (int i = 0; i < rubyList.Count; i++)
            rubyList[i].level = highestLevel;

        RubyStat.staticlevel = highestLevel;
    }

    public void SyncSilverLevel()
    {
        int highestLevel = 0;
        for (int i = 0; i < silverList.Count; i++)
        {
            if (silverList[i].level > highestLevel)
                highestLevel = silverList[i].level;
        }
        for (int i = 0; i < silverList.Count; i++)
            silverList[i].level = highestLevel;

        SilverStat.staticlevel = highestLevel;
    }

    public void SyncStoneLevel()
    {
        int highestLevel = 0;
        for (int i = 0; i < stoneList.Count; i++)
        {
            if (stoneList[i].level > highestLevel)
                highestLevel = stoneList[i].level;
        }
        for (int i = 0; i < stoneList.Count; i++)
            stoneList[i].level = highestLevel;

        StoneStat.staticlevel = highestLevel;
    }

    public void SyncWoodLevel()
    {
        int highestLevel = 0;
        for (int i = 0; i < woodList.Count; i++)
        {
            if (woodList[i].level > highestLevel)
                highestLevel = woodList[i].level;
        }
        for (int i = 0; i < woodList.Count; i++)
            woodList[i].level = highestLevel;

        WoodStat.staticlevel = highestLevel;
    }
}
