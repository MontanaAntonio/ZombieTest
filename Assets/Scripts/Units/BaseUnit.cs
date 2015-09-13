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
    public float speed;
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

    public virtual void SpawnAndMove(Vector3 spawnPosition)
    {
        state = UnitState.RUN;
        SetActiveObject(true);
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
        float step = speed * Time.deltaTime;
       transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, 0, targetPosition.z), step);
        
    }

    public virtual void PushOnUnit()
    {
        Debug.Log("Pushed: "+ gameObject.name);
        particles.Play();

        Dead();
    }



    public virtual void Dead()
    {
        state = UnitState.DEAD;
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


    public void Disable()
    {
        ChangeState(UnitState.NONE);
    }


    public void ChangeState(UnitState unitState)
    {
        state = unitState;
        GameManager.ins.AnyChanged();
    }
}
