using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : Singleton<QuestLog>
{
    protected QuestLog() { }

    [SerializeField] List<GameObject> quests;

    public void SetQuestCheck(string name) {
        foreach (var questItem in quests)
        {
            if (questItem.name == name) {
                questItem.SetActive(true);
                break;
            }
        }
    }
}
