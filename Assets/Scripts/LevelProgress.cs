using System;

[Serializable]
public class LevelProgress
{
    public bool LevelCompleted = false;

    public int CollectItems = 0;

    public LevelProgress(bool levelCompleted = false, int collectItems = 0)
    {
        LevelCompleted = levelCompleted;
        CollectItems = collectItems;
    }
}