using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = System.Object;

[Serializable]
public class DialoguePoint : Event
{
    public string shownText;
    public List<DialogueOptions> dialogueOptions;
    
    // Arg1 = TextObject to display textdasd
    // Arg2 = DialogueButtons
    public override void runEvent(Object[] args)
    {
        ((Text) args[0]).text = shownText;
        // showtext
        // Link optionEvents to Buttons

        Button[] buttons = new Button[4];

        for (int i = 1; i <= 4; i++)
        {
           buttons[i - 1] = ((DialogeButton) args[i]).GetComponent<Button>();
           //Debug.Log(((DialogeButton)args[i]).gameObject.name);
        }

        for (int i = 0; i < dialogueOptions.Count; i++)
        {
            buttons[i].GetComponent<DialogeButton>().setResponseText(dialogueOptions[i].answer);
            var i1 = i;
            buttons[i].onClick.AddListener(() =>
            {
                foreach (Event optionEvent in dialogueOptions[i1].optionEvents.Reverse())
                {
                    SceneManager.Instance.dayEvents.AddFirst(optionEvent);
                    SceneManager.Instance.setupNextEvent();
                }
            });
        }
    }

    public DialoguePoint(string type, string shownText, List<DialogueOptions> dialogueOptions) : base(type)
    {
        this.shownText = shownText;
        this.dialogueOptions = dialogueOptions;
    }
}