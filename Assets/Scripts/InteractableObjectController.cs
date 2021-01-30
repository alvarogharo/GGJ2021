using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class InteractableObjectController : MonoBehaviour
{
    [Tooltip ("Array de índices de objetos de fase en las que el objeto será útil ")]
    public PhaseAction[] phaseActions;
    public bool isInteractable;
    public bool hasOutline;
    private Light2D outlineLight;
    private AudioSource audioSrc;
    private IntratableFasesController phaseController;
    // Start is called before the first frame update
    void Awake()
    {
        outlineLight = gameObject.GetComponentInChildren<Light2D>();
        audioSrc = gameObject.GetComponent<AudioSource>();
        phaseController = FindObjectOfType<IntratableFasesController>();
    }

    private void Update()
    {
        if (isInteractable)
        {
            outlineLight.enabled = hasOutline;
        }
    }

    public void Interact(){
        int index = FindIndexOfPhase(phaseActions, phaseController.currentFase);
        phaseActions[index].UseItem();
    }

    private int FindIndexOfPhase(PhaseAction[] p, int phaseNumber){
        for(int i = 0; i < p.Length; i++){
            if(p[i].phaseIndex == phaseNumber){
                return i;
            }
        }

        return -1;
    }

    private void OnMouseEnter()
    {
        hasOutline = true;
    }

    private void OnMouseExit()
    {
        hasOutline = false;
    }

    private void OnMouseDown()
    {
        if (isInteractable)
        {
            FindObjectOfType<CharacterMovement>().SetObject(this);
            if(audioSrc.clip && !audioSrc.isPlaying){
                audioSrc.Play(0);
            }
        }
    }
}

[Serializable]
public class PhaseAction{
    public int phaseIndex;
    private int timesUsed = 0;
    private bool isRepeated = false;
    public DialogueAction[] singleActionDialogueIndex;
    public DialogueAction[] repeatedActionDialogueIndex;

    public void UseItem(){
        if(!isRepeated){
            DialogueAction da = singleActionDialogueIndex[timesUsed];
            GameObject.FindObjectOfType<DialogueManager>().StartDialogue(this.phaseIndex, da.dialogueIndex);
            if(da.dialogueEvent != null){
                da.dialogueEvent.Invoke();
            }
            this.timesUsed++;

            if(this.timesUsed == singleActionDialogueIndex.Length){
                this.isRepeated = true;
            }
        }else{
            DialogueAction da;
            if(repeatedActionDialogueIndex.Length > 0){
                int i = UnityEngine.Random.Range(0, repeatedActionDialogueIndex.Length);
                da = repeatedActionDialogueIndex[i];
                
            }else{
                da = singleActionDialogueIndex[singleActionDialogueIndex.Length-1];
            }

            GameObject.FindObjectOfType<DialogueManager>().StartDialogue(this.phaseIndex, da.dialogueIndex);
            if(da.dialogueEvent != null){
                da.dialogueEvent.Invoke();
            }
        }
    }
}

[Serializable]
public class DialogueAction{
    public int dialogueIndex;
    public UnityEvent dialogueEvent;
}
