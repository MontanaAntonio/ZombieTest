using UnityEngine;
using System.Collections;

public class ZigzagUnit : BaseUnit
{

    public float zigzagFrequency =3;
    public float zigzagStep;


    public override void StartFromSpawnPosition(Vector3 spawnPosition)
    {
        ResetTargetPosition();
        base.StartFromSpawnPosition(spawnPosition);
    }

    public override void Move()
    {
        if (state != UnitState.RUN)
            return;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, 0, targetPosition.z), step);
        
        if (targetPosition.x < 0)
        {
            if (transform.position.x <= targetPosition.x && transform.position.z <= targetPosition.z)
            {
                SelectAnotherWay();
            }
        }
        else
        {
            if (transform.position.x >= targetPosition.x && transform.position.z <= targetPosition.z)
            {
                SelectAnotherWay();
            }
        }

    


        if (GameManager.ins.finishLinePositionZ > transform.position.z)
        {
            Finished();
        }

    }



    public float InvertSidePosition()
    {
        //если бот находится на правой части и достиг своей
        // цели то он начинает идти на другую сторону 
        if (transform.position.x > 0)
        {
            //right
            return -GameManager.ins.screenBorders.x; 
        }
        else
        {
            //left
            return GameManager.ins.screenBorders.x;
        }

    }

    public void SelectAnotherWay()
    {
        targetPosition = new Vector3(InvertSidePosition(), 0, targetPosition.z- zigzagStep);
    }

    public void ResetTargetPosition()
    {
        zigzagStep = GameManager.ins.screenBorders.y/zigzagFrequency;
        targetPosition = new Vector3(RandomizeDirection(),0, GameManager.ins.screenBorders.y-zigzagStep);
    }

    public float RandomizeDirection()
    {
            return  Random.Range(0, 2) > 0 ? (GameManager.ins.screenBorders.x) : -GameManager.ins.screenBorders.x;
    }
}
