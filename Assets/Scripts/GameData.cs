using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Custom/GameData", order = 1)]
public class GameData : ScriptableObject
{

    
    // Add variables you want to save here
    public TrainData trainDataEntered;
    public GameObject trainObjectEntered;

    public int spawnEntranceNumber;
    public QuestData currentQuest;
    public int subTasksCompleted;

    public Vector3 position;


    public bool completedCorrectTrack;
    public bool completedCheckIn;
    public bool completedCorrectTrain;
    public bool completedWatchScreen;


    public bool NPCEnabled;
    public bool soundEnabled;
    // Example method to reset variables
    public void ResetData()
    {
        trainDataEntered = null;
        trainObjectEntered = null;
   
        position = Vector3.zero;
      
        subTasksCompleted = 0;
        spawnEntranceNumber = -1;
        completedCorrectTrack = false;
        completedCheckIn = false;
        completedCorrectTrain = false;
        completedWatchScreen = false;
        currentQuest.ResetQuest();
        

        NPCEnabled = false;
        soundEnabled = false;

    }
}
