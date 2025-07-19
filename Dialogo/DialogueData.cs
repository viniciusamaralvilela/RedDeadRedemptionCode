using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public struct Dialogue
{
    public string name;
    [TextArea(5, 10)]
    public string text;
}
[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObject/TalkTalkScript", order = 1)]
public class DialogueData : ScriptableObject
{
    public List<Dialogue> talkScript;
}
