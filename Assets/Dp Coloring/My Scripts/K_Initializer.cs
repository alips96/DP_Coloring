using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dp_Coloring
{
	public class K_Initializer : MonoBehaviour 
	{
        public Text KNumber;

        [HideInInspector]
        public int k = 0;

        public void PressedIncreaseKButton()
        {
            k++;
            KNumber.text = k.ToString();
        }

        public void PressedDecreaseKButton()
        {
            k--;
            KNumber.text = k.ToString();
        }
	}
}

