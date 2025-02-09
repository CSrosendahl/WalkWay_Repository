using UnityEngine;
using System.Collections;
using TMPro;

public class TrainMover : MonoBehaviour
{
    // This script is attached to every train prefab. It handles the movement of the train, and the text and line images on the train

    public Transform boardingDestination; // The destination of the train when it is boarding
    public Transform exitDestination; // The destination of the train when it has been boarded
    private Transform currentDestination; // The current destination of the train
    public TrainData trainData; // TrainData asset
    
  

    public float maxSpeed = 1.0f; // The maximum speed of the train
    public float acceleration = 0.5f; // How quickly the train accelerates
    public float quickDecelerationRate = 2.0f; // The rate at which the train decelerates quickly within the deceleration distance
    public float decelerationDistance = 5.0f; // The distance from the destination at which the train starts to decelerate
    public float stopDistance = 0.1f; // Distance at which the train considers it has "arrived"
    public float waitTime = 10.0f; // Time to wait at destination before moving again


    public float currentSpeed = 0f; // Current speed of the train
    public bool isMoving = false; // Is the train moving?

    public bool questObjective; // Filler trains will not be a quest objective, and will drive straight through the station


    
    public TMP_Text[] stationText; // TextObjects
    public GameObject[] lineImage; // Gameobjects that hold renderer for lineImages

    public string overrideStationName; // Override station names in case the train is not part of the quest to avoid confusion
    public Material overridelineMaterial; // Override lineMaterial in case the train is not part of the quest to avoid confusion

    public int trainDataID;



    void Start()
    {
 
        for (int i = 0; i < stationText.Length; i++)
        {
           
            if (questObjective)
            {
                stationText[i].text = trainData.trainName; 
            }
            else
            {
                stationText[i].text = overrideStationName;
            }
        }
        for (int i = 0; i < lineImage.Length; i++)
        {
            if(questObjective)
            {
                lineImage[i].GetComponent<MeshRenderer>().material = trainData.trainLineMaterial;
            }
            else
            {
                lineImage[i].GetComponent<MeshRenderer>().material = overridelineMaterial;
            }
           
        }
       
        // Set the first destination as the current one and start moving
        currentDestination = boardingDestination;
      
        isMoving = true;
        trainDataID = trainData.trainID;
    }

    void Update()
    {
        if (isMoving )
        {
            MoveTrain();

        }
      
    }
   
    void MoveTrain()
    {
        // Calculate the distance to the current destination
        float distanceToDestination = Vector3.Distance(transform.position, currentDestination.position);

        // Check if the train is within the deceleration distance and above 1f speed
        if (distanceToDestination < decelerationDistance && currentSpeed > 1f)
        {
            // Decelerate quickly to the speed of 1f
            currentSpeed = Mathf.Max(currentSpeed - quickDecelerationRate * Time.deltaTime, 1f);
            // Train slowing down, arrival
        }
        else if (currentSpeed < maxSpeed && distanceToDestination >= decelerationDistance)
        {
            // Accelerate the train until it reaches max speed
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
        }

        // Move the train towards the current destination
        transform.position = Vector3.MoveTowards(transform.position, currentDestination.position, currentSpeed * Time.deltaTime);

        // Play train move sound here

        // If the train is close enough to the current destination, stop and start waiting
        if (distanceToDestination <= stopDistance && currentSpeed <= 1f && questObjective)
        {
 
            
            isMoving = false; // Stop the train
           
            StartCoroutine(WaitAtDestination());

        }else if(!questObjective)
        {
            MoveToExitDestination(); // The filler trains move directly through the station without stopping
                                    // currently not in use since we do not have any filler trains being spawned.
        }
    }

    IEnumerator WaitAtDestination()
    {
       
        // Set the new destination and reset the speed for acceleration

        if(currentDestination == exitDestination)
        {
            TrainManager.instance.hasSpawned = false; // Allow the train spawner to spawn a new train (currently not being used, should be removed)
            Destroy(gameObject);
        } else
        {
            yield return new WaitForSeconds(waitTime);
            currentDestination = (currentDestination == boardingDestination) ? exitDestination : boardingDestination;
            currentSpeed = 0f; // Reset speed to 0 to start acceleration from a full stop
            isMoving = true; // Allow the train to start moving again
            // Play start sound 
        }

       
    }
    public void MoveToExitDestination()
    {
        currentDestination = exitDestination; // Set the exit destination as the current destination
        currentSpeed = maxSpeed; // Set the speed to maximum for acceleration
        isMoving = true; // Allow the train to start moving again
    }


    //IEnumerator StartMoving()
    //{
    //    // Wait before starting to move
    //    yield return new WaitForSeconds(waitTime);
    //    isMoving = true;
    //}
}
