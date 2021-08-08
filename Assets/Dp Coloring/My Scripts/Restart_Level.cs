using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dp_Coloring
{
	public class Restart_Level : MonoBehaviour 
	{
		public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Just restarting!
        }
	}
}

