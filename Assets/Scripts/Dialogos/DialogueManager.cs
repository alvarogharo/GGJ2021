using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    
    private void Awake(){
        superMovement = FindObjectOfType<CharacterMovement>();
    }
    private void Start(){
        typing = false;
        waiting = false;
        controlEnabled = false;
        phasesList = GetPhases();
        //Debug.Log(phasesList.Count);
        //StartDialogue(0, 0);
        /*var phList = new List<Phase>();
        var dialogueList = new List<Dialogue>();
        var sentenceList = new List<Sentence>();
        for( int i = 0; i < 4; i++){
            sentenceList.Add(new Sentence("holahola", 0, 1, ""));
        }
        dialogueList.Add(new Dialogue(sentenceList));
        phList.Add(new Phase(dialogueList));

        var json = JsonHelper.ToJson(phList);
        Debug.Log(json);*/
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
                    NextSentence();
            }
        }
    }

    private void EnableControl(){controlEnabled = true;}
    private void DisableControl(){controlEnabled = false;}
    

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
            DisableControl();
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
    public List<Sentence> sentences;
    public Dialogue(List<Sentence> items){
        this.sentences = items;
    }
}

[Serializable]
public class Phase{
    public List<Dialogue> dialogues;
    public Phase(List<Dialogue> items){
        this.dialogues = items;
    }
}

#endregion
