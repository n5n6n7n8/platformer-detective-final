using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Image))]
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
      public float Order;
   }
      
   [Range(1f, 2f)]
   public float People;
    Sprite Detective, Nothing, Wimbly;

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

   Detective = Resources.Load<Sprite>("UICharacters/Detective/rest");
   Wimbly = Resources.Load<Sprite>("UICharacters/Wimbly");
   Nothing = Resources.Load<Sprite>("UICharacters/Empty");
   
 }
  void Update()
  {
   
    SPEAK();
   if (Input.GetKeyDown(KeyCode.Space))
   {
      if (DialogueIndex == DialogueSegments.Length)
      {
         enabled = false;
         if (moveOn){
            if (hasAnim){
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
      if (!PlayingDialogue){
         StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex]));
                   
      }
      else
      {
         if (DialogueSegments[DialogueIndex].Skippable){
            Skip = true;
         }
      }
   }
  }
 void SPEAK()
    {
        // This is the main thing here. I'm just getting it to log an error if there's no Image component.
        Image voice;
        voice = gameObject.GetComponent<Image>();
            Debug.Log("I have no Image component! Fix meeeeeeeeeeeee");
      
        if(People == 2)
        {
            voice.sprite = Wimbly;
            Debug.Log("It works");
        }
        if(People == 1)
        {
            voice.sprite = Detective;
            Debug.Log("NarratorsUp!");
        }
        if(People == 0)
        {
            voice.sprite = Nothing;
            Debug.Log("Nope!");
        }
    }
   
  
  private IEnumerator PlayDialogue(DialogueSegment segment)
  {
   PlayingDialogue = true;
   BodyText.SetText(string.Empty);
   SubjectText.SetText(segment.SubjectText);
  People = segment.Order;
   
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


