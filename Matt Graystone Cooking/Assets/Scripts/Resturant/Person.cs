using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Person
{
    public GameObject personObject;
    public float TimeToDestination;
    public bool At_Table = false;
    private bool AtStartPosition = true;
    public bool HasBeenFed = false;

    private GameObject spawnObject;
    private Chair targetObject;

    public int ConsomeCount = 1;

    public bool Finished = false;

    public void Feed()
    {
        HasBeenFed = true;
    }

    public void SetTargets(GameObject a, Chair b)
    {
        spawnObject = a;
        targetObject = b;
    }

    public IEnumerator Update()
    {
        while (true)
        {
            if (At_Table == false && AtStartPosition == true && Finished == false)
            {
                float distance = Vector3.Distance(personObject.transform.position, targetObject.transform.position);
                if (distance < 1)
                {
                    At_Table = true;
                    AtStartPosition = false;
                }
                else
                {
                    MoveToLocation(personObject, spawnObject, targetObject.gameObject, TimeToDestination / 2);
                }
            }

            if (At_Table == true && AtStartPosition == false)
            {
                float distance = Vector3.Distance(personObject.transform.position, spawnObject.transform.position);

                if (HasBeenFed == true)
                {
                    if (distance < 1)
                    {
                        At_Table = false;
                        targetObject.Vacant();
                        Finished = true;
                        AtStartPosition = true;
                    }
                    else
                    {
                        MoveToLocation(personObject, targetObject.gameObject, spawnObject, TimeToDestination / 2);
                    }
                }
            }

            yield return null;
        }
    }

    void MoveToLocation(GameObject vessel, GameObject home, GameObject target, float TimeToDestination)
    {
        vessel.transform.rotation = Quaternion.LookRotation(target.transform.position - home.transform.position);
        float str = Mathf.Min(10.0f * Time.deltaTime, 1);
        vessel.transform.rotation = Quaternion.Lerp(vessel.transform.rotation, target.transform.rotation, str);

        float distance = Vector3.Distance(home.transform.position, target.transform.position);
        float speed = distance / TimeToDestination;

        vessel.transform.position = Vector3.MoveTowards(vessel.transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
