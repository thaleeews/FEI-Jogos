using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private TMP_Text textE;
    private bool isOnDialogue = false;
    private bool Dialoguing = false;

    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        textE.gameObject.SetActive(false);
        CloseDialogueBox();
        //ShowDialogue(testDialogue);
    }

    private void Update()
    {
        if (isOnDialogue && Input.GetKeyDown(KeyCode.E))
        {
            if (!Dialoguing)
            {
                Dialoguing = true;
                ShowDialogue(testDialogue);
            }
        }
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogueLine in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogueLine, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }

        CloseDialogueBox();
        Dialoguing = false;
    }

    public void OnDialogueRange(bool inRange) {
        isOnDialogue = inRange;
        textE.gameObject.SetActive(inRange);
    }

    private void CloseDialogueBox() {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
