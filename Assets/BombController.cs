using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour
{

    public Transform bombTransform;
    public bool pushed;
    public Collider[] hitColliders ;
    public int layerMask;
    public float explosionRadius = 2.5f;

    public void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("units");
    }

    public void GetBomb()
    {
        if (GameManager.ins.Bombs > 0)
        {
            pushed = true;
            SetActiveBombSprite(true);
        }
    }

    //Explosion Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bombTransform.position, explosionRadius);
    }

    public void Explosion()
    {
        hitColliders= Physics.OverlapSphere(bombTransform.position, explosionRadius, layerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            hitColliders[i].GetComponent<BaseUnit>().BombReaction();
            i++;
        }
        SoundManager.ins.Explosion();
        GameManager.ins.Bombs--;
        pushed = false;
        GameManager.ins.AnyChanged();
    }


    public void Update()
    {
        if (pushed)
        {

#if UNITY_EDITOR

            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    bombTransform.position = hit.point;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                bombTransform.GetChild(1).GetComponent<Animator>().Play("Explode");
                Explosion();
                SetActiveBombSprite(false);
            }
#else
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        bombTransform.position = hit.point;
                    }

                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    bombTransform.GetChild(1).GetComponent<Animator>().Play("Explode");
                    Explosion();
                    SetActiveBombSprite(false);
                }
            }
#endif
        }
    }


   
    public void SetActiveBombSprite(bool value)
    {
        bombTransform.GetChild(0).gameObject.SetActive(value);
    }
}
