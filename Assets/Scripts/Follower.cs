using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public GameObject[] cars;
    public GameObject ambulance;
    public PathCreator creator;
    public bool isEnter = false;
    public float speed = 5f;
    float distanceTravelled;
    [SerializeField]
    public float limitDistance = 0.027f;
    [SerializeField]
    public float maxDistance = 20f;
    public LayerMask layerMask;
    public bool isStop = false;
    public bool isRed = false;
    public bool isStopForEvery = false;

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, cars.Length);
        foreach(GameObject car in cars)
		{
            car.SetActive(false);
		}
        cars[index].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        Vector3 origin = new Vector3(transform.position.x , transform.position.y + 0.5f, transform.position.z );
        if (Physics.Raycast(origin, transform.TransformDirection(Vector3.forward), out hit,maxDistance,layerMask))
        {
            Debug.DrawRay(origin, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (Vector3.Distance(origin, hit.point) < 4)
			{
				isStop = true;
			}
			else
			{
				isStop = false;
			}
        }
        if (isRed || isStopForEvery)
		{
			if (isEnter )
			{
                speed = 0;
            }
			else
			{
                if (isStop)
                {
                    speed = 0;
                }
				else
				{
                    speed = 5;
				}
            }
		}
		else
		{
            speed = 5;
        }
        distanceTravelled+=speed*Time.deltaTime;
        transform.position = creator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = creator.path.GetRotationAtDistance(distanceTravelled);
    }
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("TriggerR"))
		{
            isEnter = false;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("TriggerR"))
        {
            isEnter = true;
        }
        if (other.CompareTag("Car"))
        {
            Debug.Log("collide");

        }
    }
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Car"))
		{
            Debug.Log("collide");
		}
	}

}