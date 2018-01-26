using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometManager : MonoBehaviour {

    private static CometManager instance;
    public static CometManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CometManager>();
            }

            return CometManager.instance;
        }
    }

    public GameObject ParentCanvas;
    public GameObject CometPrefab;

    public float CometRatePerSecond = 1;

    public float Distance;
    public float Velocity;

    //public Reward Reward;

    // Use this for initialization
    void Start () {
        InvokeRepeating("CreateComet", 0, CometRatePerSecond);
    }

    public void CreateComet()
    {
        GameObject CometObject = Instantiate(CometPrefab);
        CometObject.transform.SetParent(ParentCanvas.transform);
        CometObject.transform.localScale = new Vector3(10, 10, 10);

        Comet comet = CometObject.GetComponent<Comet>();
        comet.InIComet(Distance, Velocity);
        //comet.Reward = Reward;
    }
}
