using UnityEngine;
using System.Collections;

public enum UnitState
{
    NONE,
    RUN,
    DEAD,
}

public class BaseUnit : MonoBehaviour
{
    public float size = 1; //size for accurate spawn
    public int scoreForKill= 10;
    public float speed=3;
    public UnitState state = UnitState.DEAD;
    private ParticleSystem particles;
    private Collider collider;
    public SkinnedMeshRenderer mesh;
    public Vector3 targetPosition;


    public void Awake()
    {
        if(particles==null)
        particles = gameObject.GetComponent<ParticleSystem>();

        if (collider == null)
            collider = gameObject.GetComponent<Collider>();
    }

    public void OnEnable()
    {
        UnitsManager.UnitStateEvent += ChangeState;
    }

    public void OnDestroy()
    {
        UnitsManager.UnitStateEvent -= ChangeState;
    }


    public void SetActiveObject( bool enabled)
    {
        mesh.enabled = enabled;
        collider.enabled = enabled;
    }

 

    public virtual void LookOnTarget()
    {
        transform.LookAt(targetPosition);
    }

    public virtual void Move()
    {
        if(state!=UnitState.RUN)
        return;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0, targetPosition.z), step);

      
        if (GameManager.ins.finishLinePositionZ > transform.position.z)
        {
            Finished();
        }

        //продолжать бег дальше не пропадая на финишной 
        //else if (targetPosition.z >= transform.position.z)
        //{
      
        //}
        
    }

    public virtual void BombReaction()
    {
        ChangeState(UnitState.DEAD);
    }

    public virtual void DeadEffects()
    {
        SoundManager.ins.DeadUnit();
        particles.Play();
    }

    public virtual void Finished()
    {
        GameManager.ins.MinusLife();
        ChangeState(UnitState.NONE);
    }

    public virtual void PushOnUnit()
    {
        ChangeState(UnitState.DEAD);
    }

    public virtual void Dead()
    {
        DeadEffects();
        GameManager.ins.SetScore(scoreForKill); 
        SetActiveObject(false);
    }

    public virtual void ContactWithBorder()
    {
        GameManager.ins.MinusLife();
    }

    private void Update()
    {
        if (state == UnitState.RUN)
        {
            Move();
            LookOnTarget();
        }
    }



    public void ChangeState(UnitState unitState)
    {
        state = unitState;
        GameManager.ins.AnyChanged();

        switch (unitState)
        {
            case UnitState.DEAD:
                Dead();
                break;
            case UnitState.RUN:
                SetActiveObject(true);
                break;
            case UnitState.NONE:
                SetActiveObject(false);
                break;

        }
    }

    public virtual void StartFromSpawnPosition(Vector3 spawnPosition)
    {
        ChangeState(UnitState.RUN);
        transform.position = spawnPosition;
    }
}
