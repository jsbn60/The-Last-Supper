using System.Collections.Generic;

public struct DialogueOptions
{
    public string answer;
    public LinkedList<Pair<string,int>> optionEvents;
    public DialogueOptions(string answer, LinkedList<Pair<string,int>> optionEvents)
    {
        this.answer = answer;
        this.optionEvents = optionEvents;
    }
}