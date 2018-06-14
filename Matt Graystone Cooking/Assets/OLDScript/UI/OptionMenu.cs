//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;
//using UnityEngine.UI;

//class OptionMenu : MonoBehaviour
//{
//    public GameObject OptionPanel;

//    public Button SaveButton;
//    public Button ExitButton;

//    public Toggle MuteSound;

//    public void Start()
//    {
//        SaveButton.onClick.AddListener(() => 
//        {
//            SoundManager.Instance.Play_Click();
//            //SaveLoad.Instance.SaveFile();
//        });
//        ExitButton.onClick.AddListener(() => 
//        {
//            SoundManager.Instance.Play_Click();
//            OptionPanel.SetActive(false);
//        });
//    }

//    public void Update()
//    {
//        if(MuteSound.isOn == true)
//        {
//            SoundManager.Instance.Muted = true;
//        }
//        else
//        {
//            SoundManager.Instance.Muted = false;
//        }
//    }
//}
