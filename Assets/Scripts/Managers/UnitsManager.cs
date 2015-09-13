using UnityEngine;
using System.Collections;





public enum SpawnUnit
{
    UNIT_RED,
    UNIT_BLUE,
    UNIT_YELLOW,
}

public class UnitsManager : MonoBehaviour
{


    public BaseUnit[] poolUnitsRed;
    public BaseUnit[] poolUnitsBlue;
    public BaseUnit[] poolUnitsYellow;



    public void UnitSpawn(int count, Vector3 spawnPosition, SpawnUnit unitType )
    {

    }


    public Vector3 RandomizeSpawnPosition()
    {
        return new Vector3(Random.Range(-GameManager.ins.screenBorders.x, GameManager.ins.screenBorders.x), 0,GameManager.ins.screenBorders.y); 
    }


    public void SpawnHeap()
    {
        //foreach (BaseUnit unit in BaseUnit)
        //{
            
        //}
    }


    public void  SpawnUnitToStartPos(Vector3 spawnPosition, SpawnUnit spawnUnit)
    {

    }



}


