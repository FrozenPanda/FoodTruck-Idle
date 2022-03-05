using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;

    public GameObject[] arrows;
    public Transform[] lookAt;
    private Transform currentLookAt;
    private Vector3 lookAtVector3;
    private int currentStage;

    public Transform arrowIndicator;

    private CharacterStackManager _characterStackManager;
    public void SetForCurrentStage(int _id , Transform lookAT = null)
    {
        currentLookAt = null;
        closeAllArrows();
        if (_id == 0)
        {
            arrows[0].SetActive(true);
            currentLookAt = arrows[_id].transform;
            lookAtVector3 = new Vector3(currentLookAt.position.x, transform.position.y, currentLookAt.position.z);
        }else if (_id == 1)
        {
            arrows[1].SetActive(true);
            currentLookAt = arrows[_id].transform;
            lookAtVector3 = new Vector3(currentLookAt.position.x, transform.position.y, currentLookAt.position.z);
        }else if (_id == 2)
        {
            arrows[2].SetActive(true);
            currentLookAt = arrows[_id].transform;
            lookAtVector3 = new Vector3(currentLookAt.position.x, transform.position.y, currentLookAt.position.z);
        }else if (_id == 3)
        {
            _characterStackManager = PlayerUpgradeController.instance.GetComponent<CharacterStackManager>();
            arrows[3].SetActive(true);
            currentLookAt = arrows[_id].transform;
            lookAtVector3 = new Vector3(currentLookAt.position.x, transform.position.y, currentLookAt.position.z);
        }else if (_id == 4)
        {
            arrows[4].SetActive(true);
            currentLookAt = arrows[_id].transform;
            lookAtVector3 = new Vector3(currentLookAt.position.x, transform.position.y, currentLookAt.position.z);
        }
        else
        {
            arrowIndicator.gameObject.SetActive(false);
        }
    }
    
    void Start()
    {
        SaveLoadSystem.Load();
        currentStage = SaveLoadSystem.instance.CurrentTutorialStage;
        SetForCurrentStage(currentStage);
    }

    private void NextStage()
    {
        currentStage++;
        SetForCurrentStage(currentStage);
        SaveLoadSystem.Load();
        SaveLoadSystem.instance.CurrentTutorialStage = currentStage;
        SaveLoadSystem.Save();
    }

    private void closeAllArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLookAt)
        {
            transform.LookAt(lookAtVector3);
        }

        if (Vector3.Distance(transform.position , lookAtVector3) < 2f)
        {
            NextStage();
        }

        if (currentStage == 3)
        {
            if (_characterStackManager.IsCharacterCarryAtMax())
            {
                arrowIndicator.gameObject.SetActive(true);
                arrows[3].SetActive(true);
            }
            else
            {
                arrowIndicator.gameObject.SetActive(false);
                arrows[3].SetActive(false);
            }
        }
    }
}
