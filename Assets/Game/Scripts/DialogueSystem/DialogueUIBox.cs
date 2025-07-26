using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class DialogueUIBox : MonoBehaviour, IDialogueNodeVisitor
{
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private TextMeshProUGUI lineText;
    [SerializeField] private Transform choicesBox;

    [SerializeField] private List<GameObject> _textBackgrounds;

    [SerializeField] private GameObject choiceGameobject;

    private DialogueNode currentNode;
    private DialogueNode nextNode;

    private bool listenToInput = false;

    private bool _isDialogueInProcess = false;

    public static Action onDialogueEnd;

    [Inject] private PlayerStateController _stateController;

    [SerializeField] private AudioSource _audioSource;
    private void OnEnable()
    {
        DialogueChoiceBox.onNextNodeRequest += UpdateNodeVisual;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && listenToInput)
        {
            if (nextNode == null) EndDialogue();
            else UpdateNodeVisual(nextNode);
            
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null || _isDialogueInProcess) return;
        _isDialogueInProcess = true;
        _stateController.ChangeState(PlayerStateType.Dialogue);
        foreach (GameObject background in _textBackgrounds) background.SetActive(true);
        foreach (Transform child in choicesBox) Destroy(child.gameObject);
        currentNode = dialogue.FirstNode;
        speakerName.text = currentNode.DialogueLine.Speaker.SpeakerName;
        listenToInput = false;
        StartCoroutine(TypeText(currentNode));
    }

    private IEnumerator TypeText(DialogueNode currentNode)
    {
        _audioSource.Play();
        foreach (char symbol in currentNode.DialogueLine.Text)
        {
            lineText.text += symbol;
            yield return new WaitForSeconds(0.05f);
        }
        _audioSource.Stop();
        currentNode.Accept(this);
    }

    private void UpdateNodeVisual(DialogueNode nextNode)
    {
        foreach (Transform child in choicesBox) Destroy(child.gameObject);
        currentNode = nextNode;
        if (currentNode == null) { EndDialogue(); return; }
        Cursor.lockState = CursorLockMode.Confined;
        speakerName.text = currentNode.DialogueLine.Speaker.SpeakerName;
        lineText.text = "";
        listenToInput = false;
        StartCoroutine(TypeText(currentNode));
    }

    private void EndDialogue()
    {
        _isDialogueInProcess = false;
        _stateController.ChangeState(PlayerStateType.Game);
        foreach (GameObject background in _textBackgrounds) background.SetActive(false);
        speakerName.text = "";
        lineText.text = "";
        foreach (Transform child in choicesBox) Destroy(child.gameObject);
        StartCoroutine(CallDialogueEndEvent());
        listenToInput = false;
    }

    private IEnumerator CallDialogueEndEvent()
    {
        yield return null;
        onDialogueEnd?.Invoke();
    }

    public void Visit(BasicDialogueNode node)
    {
        nextNode = node.NexnNode;
        listenToInput = true;
    }

    public void Visit(ChoiceDialogueNode node)
    {
        foreach(DialogueChoice choice in node.Choices)
        {
            GameObject choiceGO = Instantiate(choiceGameobject, choicesBox);
            choiceGO.GetComponent<DialogueChoiceBox>().Init(choice);
        }
    }

    private void OnDisable()
    {
        DialogueChoiceBox.onNextNodeRequest -= UpdateNodeVisual;
    }
}
