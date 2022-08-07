using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class StartMenu : MonoBehaviour
{
	public TMP_InputField PlayerNameInput;

	public void OnStartClicked()
	{
		PersistenceManager.Instance.PlayerName = PlayerNameInput.text;
		
        SceneManager.LoadScene("Scenes/main");
	}
}
