using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticipantSurveyModel 
{
    public int id;
    public float timeInTutorial;
    public float timeInLesson;
    public List<RoomData> rooms;
    
    public ParticipantSurveyModel(int id)
    {
        this.id = id;
        this.timeInTutorial = 0;
        this.timeInLesson = 0;
        this.rooms = new List<RoomData>();
    }

}

[System.Serializable]
public class RoomData
{
    public int condition;
    public float time;
    public float move;
    public int clicks;
    public bool response1;
    public bool response2;
}
