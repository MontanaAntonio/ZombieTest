using UnityEngine;
using System.Collections;





public enum SpawnUnit
{
    UNIT_YELLOW = 0,
    UNIT_RED =1,
    UNIT_BLUE =2,
 

}

public class UnitsManager : MonoBehaviour
{

    
    public UnitsArrayAndCounter poolUnitsRed;
    public UnitsArrayAndCounter poolUnitsBlue;
    public UnitsArrayAndCounter poolUnitsYellow;
  
    public float spawnDelay =60;
    public float lastSpawnTime;
    public static event System.Action<UnitState> UnitStateEvent;
    public float bordersWithOffset;
    public float bordersOffset =1;

    public void Awake()
    {
        InstantiateUnitsOfType(poolUnitsRed, 10);
        InstantiateUnitsOfType(poolUnitsBlue, 10);
        InstantiateUnitsOfType(poolUnitsYellow, 10);


    }

    public void Start()
    {
        bordersWithOffset = GameManager.ins.screenBorders.x-bordersOffset;
    }

    public Vector3 RandomizeSpawnPosition()
    {
        return new Vector3(Random.Range(-bordersWithOffset, bordersWithOffset), 0,GameManager.ins.screenBorders.y); 
    }

    public void InstantiateUnitsOfType(UnitsArrayAndCounter units, int unitsCount)
    {
        units.arraySpawnUnits = new BaseUnit[10];
        for (int i = 0; i < unitsCount; i++)
        {
         GameObject unit = Instantiate(units.prefab);
            units.arraySpawnUnits[i] = unit.GetComponent<BaseUnit>();
            units.arraySpawnUnits[i].SetActiveObject(false);
        }
    }



    public void  SpawnUnitToStartPos(Vector3 spawnPosition, UnitsArrayAndCounter arrayWithSpawnUnit)
    {
        BaseUnit[] units = arrayWithSpawnUnit.arraySpawnUnits;

        if (arrayWithSpawnUnit.counter < units.Length)
        {
            units[arrayWithSpawnUnit.counter].StartFromSpawnPosition(spawnPosition);
            arrayWithSpawnUnit.counter++;
        }
        else
        {
            arrayWithSpawnUnit.counter = 0;
            SpawnUnitToStartPos(spawnPosition, arrayWithSpawnUnit);
        }
        
    }

    public UnitsArrayAndCounter SelectArrayWithSpawnUnits(SpawnUnit spawnUnit)
    {
        switch (spawnUnit)
        {
            case SpawnUnit.UNIT_BLUE:
                return poolUnitsBlue;
            case SpawnUnit.UNIT_RED:
                return poolUnitsRed;
            case SpawnUnit.UNIT_YELLOW:
                return poolUnitsYellow;
        }
        return poolUnitsRed;
    }

    public void DisableAllunits()
    {
        UnitStateEvent(UnitState.NONE);
    }

    public void SpawnUnits()
    {
        if (Time.time  > lastSpawnTime+ spawnDelay)
        {
            SpawnUnitToStartPos(RandomizeSpawnPosition(), SelectArrayWithSpawnUnits(RandomizeUnitType()));
            lastSpawnTime = Time.time;
        }
      
    }


    public SpawnUnit RandomizeUnitType()
    {
        return (SpawnUnit)Random.Range(0, 4);
    }
}

[System.Serializable]
public class UnitsArrayAndCounter
{
    public GameObject prefab;
    public BaseUnit[] arraySpawnUnits;
    public int counter;
}

