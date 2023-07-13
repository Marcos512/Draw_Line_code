using System;
using UnityEngine.SceneManagement;
using YG;

public static class YandexSavesManager
{
    public static LevelProgress[] GetLevelsProgress()
    {
        var levels = YandexGame.savesData.LevelsProgress;
        int count = SceneManager.sceneCountInBuildSettings;
        if (levels == null)
        {
            levels = CreateNewLevelsMass(count);
        }
        else if (levels.Length != count)
        {
            levels = ResizeMass(count, levels);
        }

        if (YandexGame.savesData.LevelsProgress != levels)
        {
            YandexGame.savesData.LevelsProgress = levels;
            YandexGame.SaveProgress();
        }
        return (LevelProgress[])levels.Clone();
    }

    public static void SaveLevelProgress(int level, int CollectItems) //сохраняем собронное кол-во итемок на уровне
    {
        var levelsProgress = GetLevelsProgress();
        if (!levelsProgress[level].LevelCompleted
            || levelsProgress[level].CollectItems < CollectItems)
        {
            YandexGame.savesData.LevelsProgress[level] = new LevelProgress(true, CollectItems);
        }
        YandexGame.SaveProgress();
    }

    private static LevelProgress[] CreateNewLevelsMass(int count)
    {
        var levelsMass = new LevelProgress[count];
        Array.Fill(levelsMass, new LevelProgress());
        return levelsMass;
    }

    private static LevelProgress[] ResizeMass(int count, LevelProgress[] oldMass)
    {
        var newMass = CreateNewLevelsMass(count);
        for (int i = 0; i < count && i < oldMass.Length; i++)
        {
            newMass[i] = oldMass[i];
        }
        return newMass;
    }

}

