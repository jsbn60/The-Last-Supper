using System;

public abstract class Event
{
    public string type;
    
    public abstract void runEvent(Object[] args);

    protected Event(string type)
    {
        this.type = type;
    }
}