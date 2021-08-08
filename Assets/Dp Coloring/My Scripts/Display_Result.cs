using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dp_Coloring
{
	public class Display_Result : MonoBehaviour 
	{
        GameManager_Master gameManagerMaster;
        public GameObject displayOperationsText;
        public GameObject operationCounterText;
        public Vector3 currentPos;
        int displayColorCounter = 0;

        //Materials
        public Material orangeMaterial, brownMaterial;

        private void OnEnable()
        {
            SetInitialRefrences();
            gameManagerMaster.EventDisplayResult += DisplayResult;
        }

        private void OnDisable()
        {
            gameManagerMaster.EventDisplayResult -= DisplayResult;
        }

        private void SetInitialRefrences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();

            if(gameManagerMaster == null)
            {
                Debug.LogWarning("gameManagerMaster is null. you might hav forgotten to attach this script to gameManager");
            }
        }

        void DisplayResult(string block)
        {
            DisplayOperations(block);
            DisplayOutputBlock(block);
        }

        private void DisplayOutputBlock(string block)
        {
            int kIndex = block.Length / 2;

            for (int i = 0; i < block.Length; i++)
            {
                if (i == kIndex)
                    continue;

                GameObject go = Instantiate(Resources.Load<GameObject>("Cube"),
                    new Vector3((currentPos.x) + (1.5f * displayColorCounter), currentPos.y, currentPos.z), Quaternion.identity);

                switch (block[i])
                {
                    case '0':
                        go.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 1);
                        break;
                    case '1':
                        go.GetComponent<MeshRenderer>().material = brownMaterial;
                        break;
                    case '2':
                        go.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0, 1);
                        break;
                    case '3':
                        go.GetComponent<MeshRenderer>().material = orangeMaterial;
                        break;
                    case '4':
                        go.GetComponent<MeshRenderer>().material.color = Color.yellow;
                        break;
                    case '5':
                        go.GetComponent<MeshRenderer>().material.color = Color.green;
                        break;
                    case '6':
                        go.GetComponent<MeshRenderer>().material.color = Color.blue;
                        break;
                    case '7':
                        go.GetComponent<MeshRenderer>().material.color = new Color(128, 0, 128, 1);
                        break;
                    case '8':
                        go.GetComponent<MeshRenderer>().material.color = Color.grey;
                        break;
                    case '9':
                        go.GetComponent<MeshRenderer>().material.color = Color.white;
                        break;
                }

                displayColorCounter++;
            }
        }

        private void DisplayOperations(string block)
        {
            int kIndex = block.Length / 2;

            displayOperationsText.SetActive(true);
            operationCounterText.GetComponent<Text>().text = block[kIndex].ToString();
            operationCounterText.SetActive(true);
        }
    }
}

