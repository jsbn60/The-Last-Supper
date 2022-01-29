using System;

public abstract class Event
{
    public int id;
    
    public string type;
    
    public abstract void runEvent(Object[] args);

    protected Event(string type, int id)
    {
        this.type = type;
        this.id = id;
    }
}