using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Dp_Coloring
{
	public class GameManager_Master : MonoBehaviour 
	{

        public delegate void CalculationEventHandler(StringBuilder block,int k);
        public event CalculationEventHandler EventStartCalculation;

        public delegate void DisplayEventHandler(string finalBlock);
        public event DisplayEventHandler EventDisplayResult;

        public void CallEventStartCalculation(StringBuilder block,int k) 
        {
            EventStartCalculation.Invoke(block,k);
        }

        public void CallEventDisplayResult(string finalBlock)
        {
            EventDisplayResult.Invoke(finalBlock);
        }

    }
}

