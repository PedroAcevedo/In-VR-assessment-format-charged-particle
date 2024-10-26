using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SurveyInteractionController : MonoBehaviour
{
    public enum Condition { menu, model, interactive };

    public List<QuestionRelation> questions;
    public string questionDescription;
    public bool isLastCondition;
    public Condition condition;

    public GameObject instructions;
    public GameObject answerAll;
    public GameObject doSurvey;
    public GameObject endExperiment;
    public GameObject previousButton;
    public GameObject finalUI;
    public GameObject QuestionMenu;
    public UnityEvent changePlayerPosition;
    public List<bool> responses;


    public List<GameObject> buttonList;
    public List<Transform> answerPositionList;
    public List<Button> MenuButtons;
    public TMPro.TextMeshProUGUI questionLabel;
    public TMPro.TextMeshProUGUI relationLabel;
    public TMPro.TextMeshProUGUI currentQuestion;

    public GameObject particleModels;
    public GameObject[] answersIndicator;
    public GameObject simulationBox;
    public SimulationController simulation;

    private string selectedOption = "";
    private int questionCounter = 0;
    private List<GameObject> modelList;
    private List<List<Vector3>> particleSettings;
    private List<List<int>> interestPointValue;

    // Start is called before the first frame update
    void Start()
    {
        responses = new List<bool>();

        switch (condition)
        {
            case Condition.menu:
                for (int i = 0; i < buttonList.Count; i++)
                {
                    SetButtonEvent(i);
                }

                UpdateCurrentQuestionLabel();
                SetCurrentQuestion();
                ButtonState();

                QuestionMenu.SetActive(false);
                break;
            case Condition.model:
                modelList = new List<GameObject>();

                UpdateCurrentQuestionLabel();
                SetCurrentQuestion3D();
                ButtonState();

                QuestionMenu.SetActive(false);
                break;
            case Condition.interactive:
                particleSettings = new List<List<Vector3>>();
                interestPointValue = new List<List<int>>();
                    
                UpdateCurrentQuestionLabel();
                simulation.particleSetting = LoadParticleSettings();
                SetCurrentQuestionInteractive();
                ButtonState();
                break;
        }


    }

    public void OnOptionSelected(int position)
    {
        ResetCurrentSelection();

        selectedOption = questions[questionCounter].images[position];
        questions[questionCounter].selectedAnswer = selectedOption;
        buttonList[position].GetComponent<Outline>().enabled = true;

        if (selectedOption != "" && !QuestionMenu.activeSelf)
        {
            QuestionMenu.SetActive(true);
        }
    }

    public void OnOption3DSelected(int option)
    {
        for (int i = 0; i < answersIndicator.Length; i++)
        {
            answersIndicator[i].SetActive(i == option);
        }
        
        if(option != -1)
        {
            questions[questionCounter].selectedAnswer = option + "";

            if (!QuestionMenu.activeSelf)
            {
                QuestionMenu.SetActive(true);
            }
        }
            
    }

    public void ChangeQuestion(int direction)
    {
        ResetCurrentSelection();

        questionCounter += direction;
        QuestionMenu.SetActive(false);

        if (questionCounter == questions.Count)
        {
            instructions.SetActive(false);
            answerAll.SetActive(true);
            questionCounter -= 1;
        }
        else
        {
            UpdateCurrentQuestionLabel();
            SetCurrentQuestion();
            ButtonState();
        }

        changePlayerPosition.Invoke();
    }

    public void ChangeQuestion3D(int direction)
    {
        OnOption3DSelected(-1);

        questionCounter += direction;
        QuestionMenu.SetActive(false);

        if (questionCounter == questions.Count)
        {
            instructions.SetActive(false);
            particleModels.SetActive(false);
            answerAll.SetActive(true);
            questionCounter -= 1;
        }
        else
        {
            UpdateCurrentQuestionLabel();
            SetCurrentQuestion3D();
            ButtonState();
        }

        changePlayerPosition.Invoke();
    }


    public void ChangeQuestionInteractive(int direction)
    {
        if(particleSettings.Count > questionCounter)
        {
            particleSettings[questionCounter] = simulation.SaveParticleSetting();
            interestPointValue[questionCounter] = simulation.SaveInterestPointValue();
        }
        else
        {
            particleSettings.Add(simulation.SaveParticleSetting());
            interestPointValue.Add(simulation.SaveInterestPointValue());
        }

        questionCounter += direction;

        if (questionCounter == questions.Count)
        {
            instructions.SetActive(false);
            simulationBox.SetActive(false);
            answerAll.SetActive(true);
            questionCounter -= 1;
        }
        else
        {
            UpdateCurrentQuestionLabel();
            SetCurrentQuestionInteractive();
            ButtonState();
        }

        changePlayerPosition.Invoke();

    }

    public void ValidateResponses()
    {
        switch (condition)
        {
            case Condition.menu:

                for (int i = 0; i < questions.Count; i++)
                {
                    responses.Add(questions[i].selectedAnswer == questions[i].correctImage);
                }

                break;
            case Condition.model:
                for (int i = 0; i < questions.Count; i++)
                {
                    responses.Add(questions[i].models[int.Parse(questions[i].selectedAnswer)] == questions[i].correctModel);
                }
                break;
            case Condition.interactive:

                for (int i = 0; i < questions.Count; i++)
                {
                    responses.Add(ValidateInterestPointValues(questions[i].relation, interestPointValue[i][0], interestPointValue[i][1], interestPointValue[i][2]));
                }


                break;
        }
    }

    public void GoToSurveyWeb()
    {
        if (isLastCondition)
        {
            endExperiment.SetActive(true);
            SurveyController.SaveParticipantData();
        }
        else
        {
            doSurvey.SetActive(true);
        }
    }

    private void SetButtonEvent(int position)
    {
        buttonList[position].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate {
            OnOptionSelected(position);
        });
    }

    private void ResetCurrentSelection()
    {
        if (selectedOption != "" && questions[questionCounter].images.Count > 0)
        {
            if (IsAnswerInOptions() != -1)
            {
                buttonList[questions[questionCounter].images.IndexOf(selectedOption)].GetComponent<Outline>().enabled = false;
            }
        }
    }

    private void UpdateCurrentQuestionLabel()
    {
        string question = "Question #" + (questionCounter + 1);
        currentQuestion.text = question;
    }

    private void SetCurrentQuestion()
    {
        questionLabel.text = questionDescription;
        relationLabel.text = questions[questionCounter].relation;

        LoadOptionImages();
        SetSelectedAnswer();
    }

    private void SetCurrentQuestion3D()
    {
        questionLabel.text = questionDescription;
        relationLabel.text = questions[questionCounter].relation;

        LoadOptionModels();
        SetSelectedAnswer3D();
    }


    private void SetCurrentQuestionInteractive()
    {
        questionLabel.text = questionDescription;
        relationLabel.text = questions[questionCounter].relation;

        SetSelectedAnswerInteractive();
        simulation.ResetParticlesPosition(questionCounter);
    }

    private Sprite LoadImageFromResources(string image)
    {
        return Resources.Load<Sprite>("Survey/Images/" + image);
    }

    private GameObject LoadPrefabModelFromResources(string model)
    {
        return (Resources.Load("Survey/Models/" + model)) as GameObject;
    }

    private List<Vector3> LoadParticleSettings()
    {
        List<Vector3> particles = new List<Vector3>();

        particles.Add(questions[0].setting.GetSetting());
        particles.Add(questions[1].setting.GetSetting());

        return particles;
    }

    private void LoadOptionImages()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].transform.GetChild(0).gameObject.SetActive(false);
            buttonList[i].transform.GetChild(1).gameObject.SetActive(true);

            Debug.Log(questions[questionCounter].images[i]);    
            buttonList[i].transform.GetChild(1).GetComponent<Image>().sprite = LoadImageFromResources(questions[questionCounter].images[i]);
        }
    }


    private void LoadOptionModels()
    {
        if(modelList.Count > 0)
        {
            foreach (var model in modelList)
            {
                Destroy(model);
            }

            modelList.Clear();
        }

        for (int i = 0; i < answerPositionList.Count; i++)
        {
            GameObject model = LoadPrefabModelFromResources(questions[questionCounter].models[i]);
            GameObject answerModel = Instantiate(model, answerPositionList[i].position, Quaternion.identity);
            answerModel.transform.parent = answerPositionList[i];

            modelList.Add(answerModel);
        }
    }

    private void ButtonState()
    {
        MenuButtons[0].interactable = true;
        MenuButtons[1].interactable = true;

        if (questionCounter == 0)
        {
            MenuButtons[0].interactable = false;
        }
  
    }

    private void SetSelectedAnswer()
    {
        Debug.Log("Set selected answer ->" + questions[questionCounter].selectedAnswer);

        if (questions[questionCounter].selectedAnswer != null)
        {
            selectedOption = questions[questionCounter].selectedAnswer;
            buttonList[questions[questionCounter].images.IndexOf(selectedOption)].GetComponent<UnityEngine.UI.Outline>().enabled = true;
            QuestionMenu.SetActive(true);
        }
    }

    private void SetSelectedAnswer3D()
    {
        Debug.Log("Set selected answer ->" + questions[questionCounter].selectedAnswer);

        if (questions[questionCounter].selectedAnswer != null)
        {
            OnOption3DSelected(int.Parse(questions[questionCounter].selectedAnswer));
            QuestionMenu.SetActive(true);
        }
    }

    private void SetSelectedAnswerInteractive()
    {
        Debug.Log("Set selected answer ->" + questions[questionCounter].selectedAnswer);

        if(particleSettings.Count > 0 && particleSettings.Count > questionCounter)
            if (particleSettings[questionCounter] != null)
            {
                simulation.ChangeParticleInitialPosition(particleSettings[questionCounter]);
            }
    }

    private int IsAnswerInOptions()
    {
        return questions[questionCounter].images.IndexOf(selectedOption);
    }


    public bool ValidateInterestPointValues(string relation, int p1, int p2, int p3)
    {
        string numericalRelation = relation.Replace("P1", p1 + "");
        numericalRelation = numericalRelation.Replace("P2", p2 + "");
        numericalRelation = numericalRelation.Replace("P3", p3 + "");

        string[] operators = { "<", ">", "=" };

        bool match = true;

        string[] numbers = Regex.Split(numericalRelation, @"\D+");

        int comparisons = 0;

        foreach (string op in operators)
        {
            string[] operationRepetation = numericalRelation.Split(op);

            if (operationRepetation.Length > 1)
            {
                if (comparisons == 0)
                {
                    match &= EvaluateRelation(numbers[0], numbers[1], op);
                    comparisons++;
                }
                else
                {
                    match &= EvaluateRelation(numbers[1], numbers[2], op);
                    comparisons++;
                }


                if (operationRepetation.Length == 3)
                {
                    match &= EvaluateRelation(numbers[1], numbers[2], op);
                }
            }
        }

        Debug.Log("The relation " + relation +  " -> " + numericalRelation + " is " + match);

        return match;
    }

    public bool EvaluateRelation(string number1, string number2, string op)
    {
        bool result = false;

        Debug.Log("Validating operation -> " + number1 + " " + op + " " + number2);

        int a = int.Parse(number1);
        int b = int.Parse(number2);

        switch (op)
        {
            case "<":
                result = (a < b);
                break;
            case ">":
                result = (a > b);
                break;
            case "=":
                result = (a == b);
                break;
        }

        return result;
    }

}
