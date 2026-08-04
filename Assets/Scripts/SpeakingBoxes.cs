using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeakingBoxes : MonoBehaviour
{
   [System.Serializable]
   public class DialogueSegment
   {
      public string SubjectText;
      [TextArea]
      public string DialogueToPrint;

      public bool Skippable;
      [Range(.07f, 35f)]
      public float LettersPerSecond;

      [Range(1f, 6f)]
      public float Emotion;
      //this will be for the emotion display 
   }

   [SerializeField] private DialogueSegment[] DialogueSegments;
   [Space]
   [SerializeField] private TMP_Text SubjectText;
   //name of speaker
   [SerializeField] private TMP_Text BodyText;
   //what the words spoken are
   [SerializeField] public bool selfDestruct;
   //Deletes the narratornet
   [SerializeField] public bool moveOn;
   //moves from the remove panel to the add panel, also activates the hasAnim
   [SerializeField] public bool hasAnim;
   //Animation out of the dialogue
   [SerializeField] public bool changeScene;
   //changes scene after dialogue
   [SerializeField] private GameObject selfDestructing;
   //the thing that is destructed (narratornet, obeject in the scene)
   [SerializeField] private GameObject Add;
   //panel being moved to
   [SerializeField] private GameObject Remove;
   //panel being moved from
   [SerializeField] private Animator Person;
   //the animator for what our emotions draws from
   [Range(1f, 6f)]
   private float Emotion;
   private int DialogueIndex;
   private bool PlayingDialogue;
   private bool Skip;
   public string sceneName;
   
 private void Start()
 {
   StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex]));
   selfDestruct = selfDestructing;
   if (selfDestruct)
   {
      selfDestructing.SetActive(true);
   }

   
 }
  void Update()
  {
      Speak();
   if (Input.GetKeyDown(KeyCode.Space))
      {
         if (DialogueIndex == DialogueSegments.Length)
         {
            enabled = false;
            if (moveOn)
            {
               if (hasAnim)
               {
                  Debug.Log("Yep");
               }
               Add.SetActive(true);
               Remove.SetActive(false);
            }
            if (selfDestruct)
            {
               selfDestructing.SetActive(false);
            }
            if (changeScene)
            {
               SceneManager.LoadScene(sceneName);
            }
            return;
         }
         if (!PlayingDialogue)
         {
            StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex]));

         }
         else
         {
            if (DialogueSegments[DialogueIndex].Skippable)
            {
               Skip = true;
            }
         }
      }
  }

   public void Speak()
   {
      if (Emotion == 1)
      {
         Person.SetBool("idle", true);
         Person.SetBool("angry", false);
         Person.SetBool("thinks", false);
         Person.SetBool("confused", false);
         Person.SetBool("sad", false);
         Person.SetBool("happy", false);

      }
      else if (Emotion == 2)
      {
         Person.SetBool("angry", true);
         Person.SetBool("thinks", false);
         Person.SetBool("confused", false);
         Person.SetBool("sad", false);
         Person.SetBool("happy", false);
         Person.SetBool("idle", false);
      }
      else if (Emotion == 3)
      {
         Person.SetBool("sad", true);
         Person.SetBool("happy", false);
         Person.SetBool("idle", false);
         Person.SetBool("angry", false);
         Person.SetBool("thinks", false);
         Person.SetBool("confused", false);
      }
      else if (Emotion == 4)
      {
         //set thinking sprite true 
         Person.SetBool("thinks", true);
         //set all others false to avoid glitching
         Person.SetBool("confused", false);
         Person.SetBool("sad", false);
         Person.SetBool("happy", false);
         Person.SetBool("idle", false);
         Person.SetBool("angry", false);
      }
      else if (Emotion == 5)
      {
         Person.SetBool("happy", true);
         Person.SetBool("idle", false);
         Person.SetBool("angry", false);
         Person.SetBool("thinks", false);
         Person.SetBool("confused", false);
         Person.SetBool("sad", false);
      }
      else if (Emotion == 6)
      {
         Person.SetBool("confused", true);
         Person.SetBool("sad", false);
         Person.SetBool("happy", false);
         Person.SetBool("idle", false);
         Person.SetBool("angry", false);
         Person.SetBool("thinks", false);
      }




   }
  
  private IEnumerator PlayDialogue(DialogueSegment segment)
  {
   PlayingDialogue = true;
   BodyText.SetText(string.Empty);
   SubjectText.SetText(segment.SubjectText);
   Emotion = segment.Emotion;
   
    float delay = 1f / segment.LettersPerSecond;
   for (int i = 0; i < segment.DialogueToPrint.Length; i++)
   {
      if(Skip)
      {
         BodyText.SetText(segment.DialogueToPrint);
         Skip = false;
         break;
      }

      string chunkToAdd = string.Empty;
      chunkToAdd += segment.DialogueToPrint[i];
      if (segment.DialogueToPrint[i] == ' ' && i < segment.DialogueToPrint.Length - 1)
      {
         chunkToAdd = segment.DialogueToPrint.Substring(i, 2);
         i++;
      }
      BodyText.text += chunkToAdd;
      yield return new WaitForSeconds(delay);
   }     
   PlayingDialogue = false;
   DialogueIndex++;
  }
}


