using UnityEngine;
using System.Collections;

public class VisualLifeController : MonoBehaviour
{
    public Transform lifesPanel;
    public GameObject lifeSpritePrefab;


    public void AddLifes(int count)
    {
        if (lifesPanel.childCount <= 0)
        {
            SpawnLifes(count);
        }
        else
        {
            DeleteRemoveAllLifes();
            SpawnLifes(count); 
        }
    }

    private void DeleteRemoveAllLifes()
    {
        foreach (Transform child in lifesPanel)
        {
            Destroy(child.gameObject);
        }
    }

    public void SpawnLifes(int count)
    {
        for (int i = 0; i < count; i++)
        {
          GameObject go = Instantiate(lifeSpritePrefab) ;
          go.transform.parent = lifesPanel;
           go.transform.localScale = Vector3.one;
        }
    }

    public void RemoveLife()
    {
        if (lifesPanel.childCount>0)
        {
          Destroy(lifesPanel.GetChild(0).gameObject);  
        }

    }
}
