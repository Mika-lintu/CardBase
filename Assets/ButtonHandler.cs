using System;
using UnityEngine;


namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {

        public string Name;
        public int buttonID;


        void OnEnable()
        {

        }

        public void SetDownState()
        {
            //If(!cooldown)
            CrossPlatformInputManager.SetButtonDown(Name);
            NetworkClientUI.SendButtonInfo( 1, buttonID);

            
            //Add Cooldown
            print("I was Clicked");
        }


        public void SetUpState()
        {
            CrossPlatformInputManager.SetButtonUp(Name);
            NetworkClientUI.SendButtonInfo( 0, buttonID);
            
            print("I was Released");
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void Update()
        {

        }
    }
}
