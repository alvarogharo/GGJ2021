using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private TextMeshProUGUI superDialogueText;
    [SerializeField] private TextMeshProUGUI handDialogueText;

    //Conversations
    private List<Phase> phasesList;
    private Dialogue currentDialogue;
    private int currentSentenceIndex;

    //Control bools
    private bool typing;
    private bool waiting;
    private bool controlEnabled;

    //References
    private CharacterMovement superMovement;
    private CameraMovement cameraMovement;
    private BlackBarsController blackBarsController;

    //Transforms
    private Transform superTransform;
    private Transform handTransform;

    
    private void Awake(){
        superMovement = FindObjectOfType<CharacterMovement>();
        cameraMovement = FindObjectOfType<CameraMovement>();
        blackBarsController = FindObjectOfType<BlackBarsController>();
        superTransform = superDialogueText.transform.parent.parent.parent.parent;
        handTransform = handDialogueText.transform.parent.parent.parent.parent;
    }
    private void Start(){
        typing = false;
        waiting = false;
        controlEnabled = false;
        superDialogueText.text = "";
        handDialogueText.text = "";
        phasesList = GetPhases();
    }

    private void Update(){
        if(Input.GetMouseButtonDown(0) && !waiting && controlEnabled){
            if (typing){
                StopAllCoroutines();
                typing = false;
                if(currentDialogue.sentences[currentSentenceIndex].transmitter == 0){
                    superDialogueText.text = currentDialogue.sentences[currentSentenceIndex].text;
                }else{
                    handDialogueText.text = currentDialogue.sentences[currentSentenceIndex].text;
                }
            }else{
                if(currentDialogue != null)
                    if(!currentDialogue.sentences[currentSentenceIndex].eventAfterText.Equals("")){
                        FindObjectOfType<EventManager>().TriggerEvent(currentDialogue.sentences[currentSentenceIndex].eventAfterText);
                    }
                    NextSentence();
            }
        }

        //Super text at left
        if(superTransform.position.x > Camera.main.transform.position.x){
            superDialogueText.transform.parent.parent.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperRight;
            Vector3 pos = superDialogueText.transform.parent.parent.parent.GetComponent<RectTransform>().localPosition;
            pos.x = -2.7f;
            superDialogueText.transform.parent.parent.parent.GetComponent<RectTransform>().localPosition = pos;
        }
        //Super text at right
        if(superTransform.position.x < Camera.main.transform.position.x){
            superDialogueText.transform.parent.parent.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
            Vector3 pos = superDialogueText.transform.parent.parent.parent.GetComponent<RectTransform>().localPosition;
            pos.x = 2.7f;
            superDialogueText.transform.parent.parent.parent.GetComponent<RectTransform>().localPosition = pos;
        }
        //Hand text at left
        if(handTransform.position.x > Camera.main.transform.position.x){
            handDialogueText.transform.parent.parent.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperRight;
            Vector3 pos = handDialogueText.transform.parent.parent.parent.GetComponent<RectTransform>().localPosition;
            pos.x = -2.5f;
            handDialogueText.transform.parent.parent.parent.GetComponent<RectTransform>().localPosition = pos;
        }
        //Hand text at right
        if(handTransform.position.x < Camera.main.transform.position.x){
            handDialogueText.transform.parent.parent.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
            Vector3 pos = handDialogueText.transform.parent.parent.parent.GetComponent<RectTransform>().localPosition;
            pos.x = 3.25f;
            handDialogueText.transform.parent.parent.parent.GetComponent<RectTransform>().localPosition = pos;
        }
    }

    public void EnableControl(){controlEnabled = true;}
    public void DisableControl(){controlEnabled = false;}
    

#region ReadJSON
    public static List<Phase> GetPhases(){
        string json = ReadFromJson(Constants.DIALOGUE_PATH);
        var phases = new List<Phase>();
        if(json != ""){
            phases = JsonHelper.FromJson<Phase>(json);
        }
        return phases;
    }

    private static string ReadFromJson(string path){
         if(File.Exists(path)){
             using(StreamReader reader = new StreamReader(path))
             {
                 string json = reader.ReadToEnd();
                 return json;
             }
         }
         return "";
     }
#endregion

#region Typing

    public void StartDialogue(int phase, int dialogue){
        //Desactivamos el control de movimiento del personaje
        superMovement.DisableControl(true);
        cameraMovement.DisableControl();
        blackBarsController.ShowBlackBars();
        EnableControl();
        currentDialogue = phasesList[phase].dialogues[dialogue];
        currentSentenceIndex = -1;

        NextSentence();
    }

    private void NextSentence(){
        currentSentenceIndex++;

        handDialogueText.text = "";
        superDialogueText.text = "";

        if(currentSentenceIndex < currentDialogue.sentences.Count){
            if(currentDialogue.sentences[currentSentenceIndex].transmitter == 0){
                StartCoroutine(TypeSuperDialogue());
            }else{
                StartCoroutine(TypeHandDialogue());
            }
        }else{
            //Fin de dialogo
            currentDialogue = null;
            DisableControl();
            blackBarsController.HideBlackBars();
            superMovement.EnableControl();
            cameraMovement.EnableControl();
        }
    }

    private IEnumerator TypeSuperDialogue(){
        typing = true;
        waiting = true;
        yield return new WaitForSeconds(currentDialogue.sentences[currentSentenceIndex].delayBefore);
        waiting = false;

        foreach(char letter in currentDialogue.sentences[currentSentenceIndex].text.ToCharArray())
        {
            superDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        typing = false;
    }

    private IEnumerator TypeHandDialogue(){
        typing = true;
        waiting = true;
        yield return new WaitForSeconds(currentDialogue.sentences[currentSentenceIndex].delayBefore);
        waiting = false;

        foreach(char letter in currentDialogue.sentences[currentSentenceIndex].text.ToCharArray())
        {
            handDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        typing = false;
    }

    public bool IsInDialogue(){return currentDialogue != null;}
}
#endregion

#region TextObjects
[Serializable]
public class Sentence{
    public string text;
    public int transmitter; //0 para super, 1 para hand
    public float delayBefore;
    public string eventAfterText;

    public Sentence(string txt, int t, float d, string e){
        this.text = txt;
        this.transmitter = t;
        this.delayBefore = d;
        this.eventAfterText = e;
    }
}

[Serializable]
public class Dialogue{
    public int idDialogue;
    public List<Sentence> sentences;
    public Dialogue(List<Sentence> items){
        this.sentences = items;
    }
}

[Serializable]
public class Phase{
    public int idPhase;
    public List<Dialogue> dialogues;
    public Phase(List<Dialogue> items){
        this.dialogues = items;
    }
}

#endregion
