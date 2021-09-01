using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TMP_Text timerText = null;
    public GameObject particleGameObject;
    public Transform currentParticleSpawn;
    public Button testParticleButton;
    public bool particleInJourney;
    public List<LevelController> levelControllers;
    private ParticleBehaviour currentParticle;
    private float timeAtStart = 0f;
    private float timeAtEnd = 0f;
    private int currentLevel;
    private float currentTimeScale = 1f;
    public Slider timeScaleSlider;

    [Header("Message Panel")]
    public GameObject panelGameObject;
    public TMP_Text panelText;
    public EasyTween panelEasyTween;
    public Color successColor;
    public Color failureColor;

    private void Awake()
    {
        currentLevel = 0;
        testParticleButton.onClick.AddListener(TestParticle);
    }

    private void SetLevel()
    {
        int priority = levelControllers[currentLevel].GetPriority();
        levelControllers[currentLevel].EndLevel();
        currentLevel++;
        if (currentLevel < levelControllers.Count)
        {
            LevelController level = levelControllers[currentLevel];
            currentParticleSpawn = level.launchPoint;
            level.StartLevel(priority);
        }
    }

    public void TestParticle()
    {
        currentParticle = Instantiate(particleGameObject, currentParticleSpawn.position, currentParticleSpawn.localRotation).GetComponent<ParticleBehaviour>();
        currentParticle.OnJourneyEnd += OnParticleJourneyEnd;
        currentParticle.LaunchParticle();
        timeAtStart = Time.time;
        particleInJourney = true;
        testParticleButton.interactable = false;
    }

    private void Update()
    {
        if (particleInJourney)
        {
            float timeTaken = (float)Math.Round((Time.time - timeAtStart) * 100f) / 100f;
            timerText.text = timeTaken.ToString();
            if (timeTaken > 1.5f)
            {
                particleInJourney = false;
                OnParticleJourneyEnd(false, "Particle too slow");
            }
        }

        if (timeScaleSlider.value != currentTimeScale)
        {
            currentTimeScale = timeScaleSlider.value;
            SetTimeScale(currentTimeScale);
        }
    }

    private void OnParticleJourneyEnd(bool success, string message)
    {
        particleInJourney = false;
        timeAtEnd = Time.time - timeAtStart;
        testParticleButton.interactable = true;
        Destroy(currentParticle.gameObject);

        if (success)
        {
            if (timeAtEnd > 1)
            {
                panelText.text = "particle too slow";
                panelText.color = failureColor;
            }
            else
            {
                panelText.text = message;
                panelText.color = successColor;
                SetLevel();
            }
        }
        else
        {
            panelText.text = message;
            panelText.color = failureColor;
        }

        panelGameObject.SetActive(true);
        panelEasyTween.OpenCloseObjectAnimation();
    }

    public void SetTimeScale(float value)
    {

        Time.timeScale = value;
        Debug.Log("Setting Time Scale" + Time.timeScale);
    }
}
