using System.Collections.Generic;

public struct DialogueOptions
{
    public string answer;
    public LinkedList<Event> optionEvents;

    public DialogueOptions(string answer,  LinkedList<Event> optionEvents)
    {
        this.answer = answer;
        this.optionEvents = optionEvents;
    }
}