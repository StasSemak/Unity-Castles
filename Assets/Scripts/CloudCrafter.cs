using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 3;
    public float cloudSpeedMult = 0.5f;

    private GameObject[] clouds;

    private void Awake()
    {
        clouds = new GameObject[numClouds];

        GameObject anchor = GameObject.Find("CloudAnchor");

        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);

            Vector3 cloudPos = Vector3.zero;
            cloudPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cloudPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            float scale = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scale);
            cloudPos.y = Mathf.Lerp(cloudPosMin.y, cloudPos.y, scale);
            cloudPos.z = 100 - 90 * scale;

            cloud.transform.position = cloudPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            cloud.transform.SetParent(anchor.transform);
            
            clouds[i] = cloud;
        }
    }

    void Update()
    {
        foreach (var cloud in clouds)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cloudPos = cloud.transform.position;
            cloudPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            if(cloudPos.x <= cloudPosMin.x)
            {
                cloudPos.x = cloudPosMax.x;
            }
            cloud.transform.position = cloudPos;
        }
    }
}
