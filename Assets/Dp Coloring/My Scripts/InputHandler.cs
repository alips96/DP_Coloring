using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Dp_Coloring
{
	public class InputHandler : MonoBehaviour 
	{
        public Button blackButton,brownButton, redButton, orangeButton, yellowButton
            , greenButton, blueButton, purpleButton, greyButton, whiteButton;

        int displayColorCounter = 0; //counts the input elements
        public StringBuilder mainBlock = new StringBuilder();
        public Vector3 currentPos;

        //Materials
        public Material orangeMaterial, brownMaterial;

        public void SwithchColorHandler(string code)
        {
            mainBlock.Append(code);

            GameObject go = Instantiate(Resources.Load<GameObject>("Cube"),
                new Vector3((currentPos.x) + (1.5f * displayColorCounter),currentPos.y,currentPos.z),Quaternion.identity);

            switch (code)
            {
                case "0":
                    go.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 1);
                    break;
                case "1":
                    go.GetComponent<MeshRenderer>().material = brownMaterial;
                    break;
                case "2":
                    go.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0, 1);
                    break;
                case "3":
                    go.GetComponent<MeshRenderer>().material = orangeMaterial;
                    break;
                case "4":
                    go.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    break;
                case "5":
                    go.GetComponent<MeshRenderer>().material.color = Color.green;
                    break;
                case "6":
                    go.GetComponent<MeshRenderer>().material.color = Color.blue;
                    break;
                case "7":
                    go.GetComponent<MeshRenderer>().material.color = new Color(128, 0, 128,1);
                    break;
                case "8":
                    go.GetComponent<MeshRenderer>().material.color = Color.grey;
                    break;
                case "9":
                    go.GetComponent<MeshRenderer>().material.color = Color.white;
                    break;
            }

            displayColorCounter++;
        }
    }
}

