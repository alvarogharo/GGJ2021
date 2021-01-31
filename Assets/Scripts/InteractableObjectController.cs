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
    public bool interactImmediatly = false;
    public bool hasOutline;
    protected Light2D outlineLight;
    protected AudioSource audioSrc;
    protected IntratableFasesController phaseController;
    // Start is called before the first frame update
    protected void Awake()
    {
        outlineLight = gameObject.GetComponentInChildren<Light2D>();
        audioSrc = gameObject.GetComponent<AudioSource>();
        phaseController = FindObjectOfType<IntratableFasesController>();
    }

    protected void Update()
    {
        if (isInteractable)
        {
            outlineLight.enabled = hasOutline;
        }
    }

    public virtual void Interact(){
        int index = FindIndexOfPhase(phaseActions, phaseController.currentFase);
        phaseActions[index].UseItem();
    }

    protected int FindIndexOfPhase(PhaseAction[] p, int phaseNumber){
        for(int i = 0; i < p.Length; i++){
            if(p[i].phaseIndex == phaseNumber){
                return i;
            }
        }

        return -1;
    }

    protected void OnMouseEnter()
    {
        hasOutline = true;
    }

    protected void OnMouseExit()
    {
        hasOutline = false;
    }

    protected void OnMouseDown()
    {
        if (isInteractable)
        {
            if(interactImmediatly){
                Interact();
                isInteractable = false;
                outlineLight.enabled = false;
            }else{
                FindObjectOfType<CharacterMovement>().SetObject(this);
            }
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
