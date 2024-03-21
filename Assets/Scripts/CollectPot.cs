using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;


public class CollectPot : MonoBehaviour
{
    public Transform HerbSpawnPoint;

    public float radius = 2.7f;
    public List<string> tags; // herb tags
    public List<int> counts; //herb conuts

    public List<int> tempCounts;

    void Start()
    {
        tags.Add("HerbA");
        tags.Add("HerbB");
        tags.Add("HerbC");

        if (counts.Count != tags.Count)
        {
            counts = new List<int>(new int[tags.Count]);
            tempCounts = new List<int>(new int[tags.Count]);
        }
    }

    public List<int> GetHerbsCount()
    {
        List<int> temp = new List<int>(counts);
        ResetPot();
        return temp;
    }

    private void ResetPot()
    {
        // set list to 0
        for (int i = 0; i < counts.Count; i++)
        {
            counts[i] = 0;
            tempCounts[i] = 0;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                if (hitCollider.CompareTag(tags[i]))
                {
                    hitCollider.gameObject.transform.position = HerbSpawnPoint.position;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // set to 0
        for (int i = 0; i < counts.Count; i++)
        {
            tempCounts[i] = 0;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                if (hitCollider.CompareTag(tags[i]))
                {
                    tempCounts[i]++;
                    break; 
                }
            }
        }

        counts = tempCounts;
    }

}