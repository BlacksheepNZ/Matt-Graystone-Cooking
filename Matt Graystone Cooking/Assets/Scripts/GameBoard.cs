using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour {

    public Map Map;

    //public Clouds CloudPrefab;

    public GameObject Sun;

    private static GameBoard instance;
    public static GameBoard Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameBoard>();
            }

            return GameBoard.instance;
        }
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(AutoTick());
    }
	
    void CreateClouds()
    {
        //CloudPrefab = (Clouds)Instantiate(CloudPrefab);
        //CloudPrefab.transform.SetParent(GameObject.Find("Clouds").transform);
    }

	// Update is called once per frame
	void Update ()
    {
        Sun.transform.position = Vector3.Slerp(Sun.transform.position, new Vector3(1000, 0, 0), Time.time / (24*60*60));
    }

    private IEnumerator AutoTick()
    {
        while (true)
        {
            CreateClouds();
            yield return new WaitForSeconds(Random.Range(1, 10));
        }
    }
}
