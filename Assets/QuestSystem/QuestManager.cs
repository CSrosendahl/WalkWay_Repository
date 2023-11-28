using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public List<QuestData> questTemplates; // List of quest templates with different train IDs
    public QuestData currentQuest; // The player's current quest
   // public Text questInfoText; // Reference to the Text UI element
    public TextMeshPro questText;
    public MeshRenderer questInfoOnPhone;
    public TextMeshPro questCompletedOrFail;
    public Material[] linjeMaterial; // Optimize thiiiiiiis!

    public AudioClip questFailedSound;
    public AudioClip questCompleteSound;
    public AudioSource audioSource;

    private void Start()
    {
        //AcceptQuest();
  


    }
    public void AcceptQuest()
    {
        // Assign a random quest to the player at the start of the game
        currentQuest = GetRandomQuest();
        DisplayQuestInfo(currentQuest);
       
    }

    public void CompleteQuest(QuestData quest)
    {
        // Complete quest, and end the game
        // Fade out/Blackscreen/Sound/Prompt....
        // DisplayQuestInfo(currentQuest);

        // Wait 2 second, fade to black into new scene.
      
        GameManager.instance.CompleteQuestArea();
        audioSource.clip = questCompleteSound;
        audioSource.Play();
        questCompletedOrFail.text = "Tillykke!\r\nDu tog det rigtige tog";
        Debug.Log("COMPLETED QUUUUUEST");
        questText.text = "";
      
        currentQuest = null;
        

    }

    public void WrongQuestObjective()
    {
        // Play sound, wrong train.
        Debug.Log("Wrong train");
        //audioSource.clip = questFailedSound;
        //audioSource.Play();
        GameManager.instance.CompleteQuestArea();
        questCompletedOrFail.text = "Desv�rre!\r\nDu tog det forkerte tog";
        questText.text = "";
        currentQuest = null;

    }

    private QuestData GetRandomQuest()
    {
        // Randomly select a quest template from the list
        int randomIndex = Random.Range(0, questTemplates.Count);
        return questTemplates[randomIndex];
    }

    private void DisplayQuestInfo(QuestData quest)
    {
        // Update the UI to display the quest information, including the train ID
        questText.text = quest.questDescription + "\n";
        if(currentQuest != null)
        {
            questInfoOnPhone.enabled = true;
        }else
        {
            
            questInfoOnPhone.enabled = false;
            
        }

        // OPTIMIZE THIS ! Get the data from TrainData instead of the quest
        if(currentQuest.trainID == 0)
        {
            questInfoOnPhone.material = linjeMaterial[0];
        }
        else if(currentQuest.trainID == 1)
        {
            questInfoOnPhone.material = linjeMaterial[1];
        }
        else if (currentQuest.trainID == 2)
        {
            questInfoOnPhone.material = linjeMaterial[2];
        }
        else if (currentQuest.trainID == 3)
        {
            questInfoOnPhone.material = linjeMaterial[3];
        }
        else if (currentQuest.trainID == 4)
        {
            questInfoOnPhone.material = linjeMaterial[4];
        }
        else if (currentQuest.trainID == 5)
        {
            questInfoOnPhone.material = linjeMaterial[5];
        }
     

    }


   

}


