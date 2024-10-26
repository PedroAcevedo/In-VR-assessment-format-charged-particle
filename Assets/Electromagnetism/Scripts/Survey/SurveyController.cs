using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyController : MonoBehaviour
{
    public static ParticipantSurveyModel participantData;
    public static int roomClicks;

    public GameObject[] rooms;
    public int participant;
    public GameObject player;
    public SurveyInteractionController[] surveyInteraction;

    private int currentRoom;
    private List<int> roomSequence;
    private CharacterController characterController;
    private Transform trackingSpace;
    private SurveyModel surveyModel;
    private List<Vector2> questions;
    private float currentRoomXpos;

    // STATS
    private float initialRoomTime;
    private float timeInRoom;
    private int selectedCondition;
    private Vector3 currentPlayerPosition;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        currentRoom = 0;
        characterController = player.GetComponent<CharacterController>();
        trackingSpace = player.transform.GetChild(1).GetChild(0);

        participantData = new ParticipantSurveyModel(participant);

        StartSequence();
        ActivateRoom();
    }

    public void NextRoom()
    {
        currentRoom++;

        if (currentRoom > 1)
        {
            selectedCondition = roomSequence[currentRoom] - 2;

            Debug.Log("The next room is " + selectedCondition);

            surveyInteraction[selectedCondition].questions = GetQuestionList();
            surveyInteraction[selectedCondition].isLastCondition = (currentRoom == roomSequence.Count - 1);
            surveyInteraction[selectedCondition].questionDescription = surveyModel.questionDescription[selectedCondition];
        }

        ActivateRoom();
    }

    public void ChangePlayerPosition()
    {
        characterController.enabled = false;
        player.transform.position = new Vector3(currentRoomXpos, player.transform.position.y, -19f);
        player.transform.rotation = Quaternion.identity;
        trackingSpace.rotation = Quaternion.identity;
        characterController.enabled = true;
        currentPlayerPosition = player.transform.position;
    }

    public void SaveRoomData()
    {
        timeInRoom = Time.time - initialRoomTime;
        CancelInvoke();

        if (currentRoom < 2)
        {
            if(currentRoom == 0)
            {
                participantData.timeInTutorial = timeInRoom;
            }
            else
            {
                participantData.timeInLesson = timeInRoom;
            }

            roomClicks = 0;
            distance = 0;
        }
        else
        {
            RoomData roomData = new RoomData();

            roomData.clicks = roomClicks;
            roomData.condition = selectedCondition;
            roomData.time = timeInRoom;
            roomData.move = distance;
            roomData.response1 = surveyInteraction[selectedCondition].responses[0];
            roomData.response2 = surveyInteraction[selectedCondition].responses[1];
            
            participantData.rooms.Add(roomData);

            roomClicks = 0;
            distance = 0;
        }



    }

    private void ActivateRoom()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].SetActive(i == roomSequence[currentRoom]);
        }

        currentRoomXpos = rooms[roomSequence[currentRoom]].transform.position.x;

        Debug.Log(currentRoomXpos);

        ChangePlayerPosition();
        initialRoomTime = Time.time;
        InvokeRepeating("ReportUser", 2.0f, 2.0f);
    }

    public void LoadParticipantData()
    {
        string destination = Application.dataPath + "/Resources/Data/participant-survey-condition-latin-square.csv";

        string[] lines = System.IO.File.ReadAllLines(destination);

        string[] participantData = lines[participant].Split(",");

        for (int i = 1; i < 4; i++)
        {
            roomSequence.Add(int.Parse(participantData[i]) + 1);
        }

        var jsonValue = Resources.Load<TextAsset>("Data/survey-questions");
        surveyModel = JsonUtility.FromJson<SurveyModel>(jsonValue.text);

        questions = new List<Vector2>();

        for (int i = 4; i < participantData.Length; i += 2)
        {
            questions.Add(new Vector2(int.Parse(participantData[i]), int.Parse(participantData[i + 1])));
        }
    }

    public static void SaveParticipantData()
    {
        string data = JsonUtility.ToJson(participantData);
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/Log/participant-" + participantData.id + "-" + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".json", data);
    }

    private void StartSequence()
    {
        roomSequence = new List<int>();

        roomSequence.Add(0);
        roomSequence.Add(1);
        LoadParticipantData();
    }

    private List<QuestionRelation> GetQuestionList()
    {
        List<QuestionRelation> currentQuestions = new List<QuestionRelation>();

        currentQuestions.Add(surveyModel.relation[(int)questions[currentRoom - 2].x - 1]);
        currentQuestions.Add(surveyModel.relation[(int)questions[currentRoom - 2].y - 1]);

        return currentQuestions;
    }

    public void ReportUser()
    {
        distance += Utils.VectorDistance(currentPlayerPosition, player.transform.position);
        currentPlayerPosition = player.transform.position;
    }
}
