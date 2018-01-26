using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ShipModuleType
{
    Empty,
    Core,
    Engine,
    Cockpit,
}

public class ShipDesigner : MonoBehaviour {

    public int ModuleCount = 0;
    public GameObject Ship_Prefab;

    public Transform Core_Parent;

    private GameObject Ship_Generic_Prefab;
    private GameObject Ship_Core_Prefab;
    private GameObject Ship_Engine_Prefab;
    private GameObject Ship_Cockpit_Prefab;

    private void Start()
    {
        CreateShipModule(Ship_Core_Prefab, Core_Parent, ShipModuleType.Core);
    }

    public void CreateShipModule(GameObject Object_Prefab, Transform Parent, ShipModuleType ShipModuleType)
    {
        Object_Prefab = Instantiate(Ship_Prefab);
        Object_Prefab.transform.SetParent(Parent);
        Object_Prefab.transform.localScale = new Vector3(1, 1, 1);
        Object_Prefab.transform.position = Parent.position;
        Object_Prefab.gameObject.GetComponent<ShipModules>().ModuleType = ShipModuleType;
    }

}
