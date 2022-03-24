using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    private bool mouseHover;
    public ClickManager clickManager;
   
    

    [Header("INK Json")]
    [SerializeField] TextAsset inkJson;

    private void Awake()
    {
        visualCue.SetActive(false);
        mouseHover = false;
    }

    private void Start()
    {
        clickManager = FindObjectOfType<ClickManager>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseHover = true;
        visualCue.SetActive(true);
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseHover = false;
        visualCue.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(DisplayDialogue());
        
    }

    IEnumerator DisplayDialogue()
    {
        
        while (clickManager.playerWalking)
        {
            yield return new WaitForSeconds(0.1f);
        }

            if (!DialogueManager.GetInstance().dialogueIsPlaying)
            {if(clickManager.itemSuccess==false)
                DialogueManager.GetInstance().EnterDialogueMode(inkJson);
            }
        }


    }

    

