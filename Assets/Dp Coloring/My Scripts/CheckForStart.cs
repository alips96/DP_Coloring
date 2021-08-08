using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dp_Coloring
{
	public class CheckForStart : MonoBehaviour 
	{
        InputHandler inputHandler;
        K_Initializer kInitializer;
        GameManager_Master gameManagerMaster;

        private void Start()
        {
            SetInitialRefrences();
        }

        private void SetInitialRefrences()
        {
            inputHandler = GetComponent<InputHandler>();
            kInitializer = GetComponent<K_Initializer>();
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        public void GoButtonPressed()
        {
            if(kInitializer.k > 0 && inputHandler.mainBlock.Length > 1)
            {
                if(kInitializer.k <= (inputHandler.mainBlock.Length) - 1)
                {
                    gameManagerMaster.CallEventStartCalculation(inputHandler.mainBlock,kInitializer.k);
				}
            }
        }
    }
}

