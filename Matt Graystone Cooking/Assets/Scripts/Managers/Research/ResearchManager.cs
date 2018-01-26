using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResearchManager : MonoBehaviour {

    public string ResearchLocation;
    private JsonData ResearchJsonData;
    public Transform ResearchParent;
    public GameObject ResearchPrefab;

    private List<Research> Research = new List<Research>();
    public List<GameObject> ResearchItem = new List<GameObject>();

    public void Start ()
    {
        //ResearchJsonData = JsonMapper.ToObject(System.IO.File.ReadAllText(Application.dataPath + "/StreamingAssets/" + ResearchLocation + ".json"));

        for (int i = 0; i < ResearchJsonData.Count; i++)
        {
            ResearchEffect ResearchEffect = (ResearchEffect)Enum.Parse(typeof(ResearchEffect), (string)ResearchJsonData[i]["ResearchEffect"]);

            Research.Add(new Research(
                (string)ResearchJsonData[i]["Name"],
                (string)ResearchJsonData[i]["Decription"],
                ResearchEffect,
                float.Parse(ResearchJsonData[i]["Value"].ToString()),
                float.Parse(ResearchJsonData[i]["Coefficent"].ToString())
                ));

            GameObject researchPrefab = Instantiate(ResearchPrefab);
            researchPrefab.transform.SetParent(ResearchParent);
            researchPrefab.transform.localScale = new Vector3(1, 1, 1);
            researchPrefab.transform.position = ResearchParent.position;

            Research research = researchPrefab.GetComponent<ResearchData>().Research;
            research.Name = Research[i].Name;
            research.Decription = Research[i].Decription;
            research.ResearchEffect = Research[i].ResearchEffect;
            research.Value = Research[i].Value;
            research.Coefficent = Research[i].Coefficent;

            ResearchItem.Add(researchPrefab);
        }

        AddOnClickHandlers();
    }

    public void AddOnClickHandlers()
    {
        for(int i = 0; i < ResearchItem.Count; i++)
        {
            Research research = ResearchItem[i].GetComponent<ResearchData>().Research;

            switch(research.ResearchEffect)
            {
                default:
                    break;
                case ResearchEffect.AddCraftingSlot:
                    research.Button.onClick.AddListener(() => { AddCraftingSlot(); });
                    break;
                case ResearchEffect.ReduceGlobalCraftTime:
                    research.Button.onClick.AddListener(() => { ReduceGlobalCraftTime();
                    });
                    break;
                case ResearchEffect.DrillMultiplyer:
                    research.Button.onClick.AddListener(() => { DrillMultiplyer();
                    });
                    break;
                case ResearchEffect.ScavangerMultiplyer:
                    research.Button.onClick.AddListener(() => { ScavangerMultiplyer(); });
                    break;
                case ResearchEffect.UnlockWarp:
                    research.Button.onClick.AddListener(() => { UnlockWarp(); });
                    break;
                case ResearchEffect.UnlockNextMap:
                    research.Button.onClick.AddListener(() => {  UnlockNextMap(); });
                    break;
            }
        }
    }

    public void AddCraftingSlot()
    {
        if (CheckCost(ResearchEffect.AddCraftingSlot))
        {
            //CraftingManager.Instance.Add();
        }
    }
    public void ReduceGlobalCraftTime()
    {

    }
    public void DrillMultiplyer()
    {

    }
    public void ScavangerMultiplyer()
    {

    }
    public void UnlockWarp()
    {

    }
    public void UnlockNextMap()
    {

    }

    private bool CheckCost(ResearchEffect effect)
    {
        for(int i = 0; i < ResearchItem.Count; i ++)
        {
            Research research = ResearchItem[i].GetComponent<ResearchData>().Research;

            if(effect == research.ResearchEffect)
            {
                if (research.Value <= Game.Instance.TotalGold)
                {
                    Game.Instance.RemoveGold(research.Value);
                    return true;
                }
            }
        }

        return false;
    }
}
