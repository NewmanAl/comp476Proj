using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestText : MonoBehaviour
{
    public static QuestText instance;
    private Text text;
    private int numResources;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        instance.text = instance.GetComponent<Text>();
        updateResourceQuest();
    }

    public void incrementResource() {
        print("dafuq");
        instance.numResources++;
        if(instance.numResources < 3) {
            updateResourceQuest();
        } else {
            updateBridgeQuest();
        }
    }

    private void updateBridgeQuest() {
        SetQuestText("Set TNT on bridge by holding 'T' and save the world!");
    }

    private void updateResourceQuest() {
        SetQuestText("Find all three resources required to create TNT\n" + instance.numResources + "/3");
    }

    private void SetQuestText(string _text)
    {
        instance.text.text = _text;
    }

}
