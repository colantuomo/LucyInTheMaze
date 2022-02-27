using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransitions : MonoBehaviour
{
    public RectTransform endPhaseText;
    public SentencesSO senteces;
    public float bouncingTextFXSpeed = 1f;
    public float fadeTransitionSpeed = 2.5f;
    private Image fadeImage;
    private Text endPhaseCurrentText;

    public static SceneTransitions instance;
    void Awake()
    {
        fadeImage = GetComponent<Image>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fadeImage.DOFade(1, 0);
        fadeImage.DOFade(0, .5f);
        endPhaseCurrentText = endPhaseText.GetComponent<Text>();
    }

    public void FadeSceneTransitionByIndex(int sceneIndex, bool win = true)
    {
        ShowEndPhaseText(sceneIndex, win);
    }

    public void FadeSceneTransitionByIndexNoText(int sceneIndex)
    {
        fadeImage.DOFade(1, 1).OnComplete(() => SceneManager.LoadScene(sceneIndex));
    }

    private void ShowEndPhaseText(int sceneIndex, bool win)
    {
        endPhaseCurrentText.text = GetRandomSentence(win);
        endPhaseText.DOLocalMoveY(-14f, bouncingTextFXSpeed)
        .SetEase(Ease.OutBounce);
        fadeImage.DOFade(1, fadeTransitionSpeed).OnComplete(() => SceneManager.LoadScene(sceneIndex));
    }

    private string GetRandomSentence(bool win)
    {
        if (!win)
        {
            return senteces.looseSentences[Random.Range(0, senteces.winSentences.Count)];
        }
        return senteces.winSentences[Random.Range(0, senteces.winSentences.Count)];
    }
}
