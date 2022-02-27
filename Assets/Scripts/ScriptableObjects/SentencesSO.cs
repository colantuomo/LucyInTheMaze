using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SentencesSO", menuName = "ScriptableObjects/SentencesSO")]
public class SentencesSO : ScriptableObject
{
    public List<string> winSentences;
    public List<string> looseSentences;
}
