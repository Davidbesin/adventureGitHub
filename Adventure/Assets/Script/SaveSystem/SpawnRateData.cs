using UnityEngine;

public class SpawnRateData
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int goldStaticLevel;
    public  int copperStaticLevel;
    public  int diamondStaticLevel;
    public  int ironStaticLevel;
    public  int manaGemStaticLevel;
    public  int rubiesStaticLevel;
    public  int silverStaticLevel;
    public  int stoneStaticLevel;
    public  int woodStaticLevel;
    public SpawnRateData ()
    {
        goldStaticLevel = GoldStat.staticlevel;
        copperStaticLevel = CopperStat.staticlevel;
        diamondStaticLevel = DiamondStat.staticlevel;
        ironStaticLevel = IronStat.staticlevel;
        manaGemStaticLevel = GemStat.staticlevel;
        rubiesStaticLevel = RubyStat.staticlevel;
        silverStaticLevel = SilverStat.staticlevel;
        stoneStaticLevel = StoneStat.staticlevel;
        woodStaticLevel = WoodStat.staticlevel;
    }
}


