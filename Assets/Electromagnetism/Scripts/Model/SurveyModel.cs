using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SurveyModel
{
    public List<string> questionDescription;
    public List<QuestionRelation> relation;
}

[System.Serializable]
public class QuestionRelation
{
    public string relation;
    public Setting setting;
    public List<string> images;
    public List<string> models;
    public string selectedAnswer;
    public string correctImage;
    public string correctModel;
}

[System.Serializable]
public partial class Setting
{
    public float x;
    public float y;
    public float z;


    public Vector3 GetSetting()
    {
        return new Vector3(x, y, z);
    }
}
