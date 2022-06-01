using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
    float xRotation, yRotation, zRotation;      //Amount of rotation to add to transform
    float cubeLifeInSecconds;                   //How long a cube will last before dying
    System.DateTime startLife;                  //When a cube started its life
    float decayRate;
    float opacity;


    void Start()
    {
        float minSpeed = gameObject.GetComponentInParent<Sphere>().minCubeSpeed;
        float maxSpeed = gameObject.GetComponentInParent<Sphere>().maxCubeSpeed;

        decayRate = gameObject.GetComponentInParent<Sphere>().cubeDecayRate;

        //  Float speedScale = Random.Range(minSpeed, maxSpeed);              //For cubes to match direction of parent Sphere
        float speedScale = Random.Range(-maxSpeed, maxSpeed);               //For cubes to go in different directions

        //
        //  Rotational values to apply to object on update
        //
        xRotation = gameObject.GetComponentInParent<Sphere>().sphereRotationX * speedScale;
        yRotation = gameObject.GetComponentInParent<Sphere>().sphereRotationY * speedScale;
        zRotation = gameObject.GetComponentInParent<Sphere>().sphereRotationZ * speedScale;

        opacity = gameObject.GetComponent<Renderer>().material.color.a;     //Gettings base opacity


        //Random Color
        int colorChoice = Random.Range(0, 5);
        switch (colorChoice)
        {
            case 0:
                GetComponent<Renderer>().material.color = Color.red;
                break;
            case 1:
                GetComponent<Renderer>().material.color = Color.green;
                break;
            case 2:
                GetComponent<Renderer>().material.color = Color.cyan;
                break;
            case 3:
                GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case 4:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
            default:
                GetComponent<Renderer>().material.color = Color.green;
                break;
        }

        startLife = System.DateTime.Now;
        cubeLifeInSecconds = Random.Range(gameObject.GetComponentInParent<Sphere>().minCubeLifeSecconds, gameObject.GetComponentInParent<Sphere>().maxCubeLifeSecconds);

    }


    private void Update()
    {
        gameObject.transform.Rotate((transform.rotation.x + xRotation) * Time.deltaTime, (transform.rotation.y + yRotation) * Time.deltaTime, (transform.rotation.z + zRotation) * Time.deltaTime);

        if (System.DateTime.Now >= startLife.AddSeconds(cubeLifeInSecconds))
        {
            if (gameObject.GetComponentInParent<Sphere>().cubeDecayFlash)
                GetComponent<Renderer>().material.color = Color.white;  //Make cube go white before fading
            if (opacity <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                //  Calculate new opacity for object and apply it
                opacity -= (decayRate * Time.deltaTime);
                gameObject.GetComponent<Renderer>().material.color = new Color(gameObject.GetComponent<Renderer>().material.color.r, gameObject.GetComponent<Renderer>().material.color.g, gameObject.GetComponent<Renderer>().material.color.b, opacity);
            }
        }

    }
}
