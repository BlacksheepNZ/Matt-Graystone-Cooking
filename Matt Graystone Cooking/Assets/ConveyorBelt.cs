using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConveyorBelt : MonoBehaviour
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;

    public GameObject Parent;
    public List<GameObject> ItemToAdd;

	// Use this for initialization
	void Start () {
        ItemToAdd = new List<GameObject>();

        //SaveLoad.Instance.item_[0];
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void AddItem(GameObject item)
    {
        GameObject i = Instantiate(item);
        i.transform.SetParent(Parent.transform);
        i.transform.localScale = new Vector3(1, 1, 1);
        i.transform.position = Parent.transform.position;

        ItemToAdd.Add(i);
    }
        
}
