using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttoncontroller: MonoBehaviour
{


    public ButtonVR3D buttonOpt1Script;
    public ButtonVR3D buttonOpt2Script;
    public ButtonVR3D buttonOpt3Script;
    public ButtonVR3D buttonOpt4Script;
    public GameObject UIChangeInfo;
    public int tableId;
    // Start is called before the first frame update
    void Start()
    {

        //UI CHANGE INFO DEFINE SCRIPT
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void verifychanges(int id)
    {
        // CHANGE INFO IN THE UI 
        //butoncontrollerAllTables.changeValue(tableId, id);
        if (id == 0)
        {
            buttonOpt2Script.changeButton(false);
            buttonOpt3Script.changeButton(false);
            buttonOpt4Script.changeButton(false);
        }
        else if (id == 1)
        {
            buttonOpt1Script.changeButton(false);
            buttonOpt3Script.changeButton(false);
            buttonOpt4Script.changeButton(false);
        }
        else if (id == 2)
        {
            buttonOpt2Script.changeButton(false);
            buttonOpt1Script.changeButton(false);
            buttonOpt4Script.changeButton(false);
        }
        else if (id == 3)
        {
            buttonOpt2Script.changeButton(false);
            buttonOpt3Script.changeButton(false);
            buttonOpt1Script.changeButton(false);
        }

    }
}
