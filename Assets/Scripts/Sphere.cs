using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    //
    //  Sphere Controls
    //
    public float sphereRotationX, sphereRotationY, sphereRotationZ;
    public int noOfCubes;
    public float seccondsBetweenCubeSpawns;

    //
    //  Cube Controls
    //
    public float minCubeSpeed, maxCubeSpeed;
    public float cubeMaxScaleMultiplyer;                //Sets upper range for cube scaling, base being 1
    public float minCubeLifeSecconds;
    public float maxCubeLifeSecconds;
    public bool cubeDecayFlash;                         //Wether or not a cube goes white before dying
    public float cubeDecayRate;

    List<GameObject> cubes = new List<GameObject>();    //Tracking cubes in scene

    System.DateTime nextTime;                           //Tracking time between cube spawns

    //
    //  Values for this sphere
    //
    float cubeScale;                                   
    Vector3 centerOfSphere;
    float radiusOfSpehere;

    public Object objectToSpawn;                         //Referebce to prefab to instanciate


    void Start()
    {
        
        centerOfSphere = gameObject.transform.position;         //Get center
        radiusOfSpehere = gameObject.transform.localScale.x/2;  //Get Radius
        cubeScale = 1.0f / gameObject.transform.localScale.x;  //Get Scale for cubes relative to sphere

        PopulateSphere();                               //Toggle on/off for instant population of sphere
    }


    private void Update()
    {
        gameObject.transform.Rotate((transform.rotation.x + sphereRotationX) * Time.deltaTime, (transform.rotation.y + sphereRotationY) * Time.deltaTime, (transform.rotation.z + sphereRotationZ) * Time.deltaTime);
        //if (cubes.Count < noOfCubes)
        if (gameObject.GetComponentsInChildren<Transform>().Length <= noOfCubes)
        {
            if (System.DateTime.Now >= nextTime)
            {
                nextTime = System.DateTime.Now.AddSeconds(seccondsBetweenCubeSpawns);
                cubes.Add(creatething());
                //Debug.Log(gameObject.GetComponentsInChildren<Transform>().Length);    //For reporting how many Cubes are on screen
            }
        }
    }


    private GameObject creatething()
    {
        Vector3 inside = Random.insideUnitSphere * radiusOfSpehere; //Get A random point within a sphere
        Vector3 position = new Vector3(centerOfSphere.x + inside.x, centerOfSphere.y + inside.y, centerOfSphere.z + inside.z);  //Scale the point to the scale of our sphere
        GameObject newCube = (GameObject)Instantiate(objectToSpawn, position, new Quaternion(), gameObject.transform);

        float scaleMultiplyer = Random.Range(1, cubeMaxScaleMultiplyer);
        newCube.transform.localScale = new Vector3(cubeScale * scaleMultiplyer, cubeScale * scaleMultiplyer, cubeScale * scaleMultiplyer);
        return newCube;
    }

    //  Populate sphere with initial amount of cubes
    private void PopulateSphere()
    {
        while(gameObject.GetComponentsInChildren<Transform>().Length < noOfCubes)
        {
            creatething();
        }
    }
}
