using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSwitcher : MonoBehaviour {

    public GameObject NextMapButton;
    public GameObject PreviousMapButton;

    public void Start()
    {
        //CheckMap(Map.Instance.Current);

        //NextMapButton.GetComponentInChildren<Button>().onClick.AddListener(() =>
        //{
        //    NextMap();
        //});
        //PreviousMapButton.GetComponentInChildren<Button>().onClick.AddListener(() =>
        //{
        //    PreviousMap();
        //});
    }

    public void NextMap()
    {
        float value = Map.Instance.Current + 1;
        Map.Instance.ChangeMap((MapType)value);

        CheckMap(value);
    }

    public void PreviousMap()
    {
        float value = Map.Instance.Current - 1;
        Map.Instance.ChangeMap((MapType)value);

        CheckMap(value);
    }

    void CheckMap(float current)
    {
        if (current == 0)
        {
            NextMapButton.SetActive(true);
            PreviousMapButton.SetActive(false);
        }
        else
        {
            NextMapButton.SetActive(true);
            PreviousMapButton.SetActive(true);
        }


        int max = Enum.GetValues(typeof(MapType)).Length - 1;

        if (current == max)
        {
            NextMapButton.SetActive(false);
            PreviousMapButton.SetActive(true);
        }
    }
}
