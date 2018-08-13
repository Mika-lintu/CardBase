using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput: MonoBehaviour
{
    
    void Update ()
    {
        //Player Movement
    }

    
    public void Button1(bool pressed)
    {
        if (pressed)
        {
            print("Button 1 was pressed");
        }
        else
        {
            print("Button 1 was released");
        }
    }

  
    public void Button2(bool pressed)
    {

        if (pressed)
        {
            print("Button 2 was pressed");
        }
        else
        {
            print("Button 2 was released");
        }
    }


    public void Button3(bool pressed)
    {
        if (pressed)
        {
            print("Button 3 was pressed");
        }
        else
        {
            print("Button 3 was released");
        }
    }

    public void Button4(bool pressed)
    {
        if (pressed)
        {
            print("Button 4 was pressed");
        }
        else
        {
            print("Button 4 was released");
        }
    }

}
