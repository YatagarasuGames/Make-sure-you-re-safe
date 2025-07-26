using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoiceBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI choiceText;
    private DialogueNode nextNode;

    public static Action<DialogueNode> onNextNodeRequest;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void Init(DialogueChoice choice)
    {
        choiceText.text = choice.ChoiceName;
        nextNode = choice.ChoiceNode;
    }

    private void OnClick()
    {
        onNextNodeRequest.Invoke(nextNode);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnClick);
    }
}
