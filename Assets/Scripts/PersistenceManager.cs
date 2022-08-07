using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PersistenceManager : MonoBehaviour
{
	public static PersistenceManager Instance;
	public string PlayerName;

	public Dictionary<string, int> HiScores = new Dictionary<string, int>();

	private string HiScoresPath;



	private void Awake()
	{
		HiScoresPath = Application.persistentDataPath + "/hiscores.txt";

		if (Instance != null) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		LoadHiScores();
	}

/*
	[System.Serializable]
	public class HiScores : System.Collections.ObjectModel.KeyedCollection<string, int>
	{
		protected override string GetKeyForItem(cCulture item)
		{
			return item.culture;
		}
	}
*/

	public int GetHiScore()
	{
		if (HiScores.ContainsKey(PlayerName))  //FIXME use syntactic sugar instead
			return HiScores[PlayerName];
		else
			return 0;
	}


	public void UpdateHiScore(int score)
	{
		if (score > GetHiScore()) {
			HiScores[PlayerName] = score;
			WriteHiScores();
		}
	}


	public void LoadHiScores()
	{
		string line;
		HiScores = new Dictionary<string, int>();

		if (File.Exists(HiScoresPath)) {
			StreamReader sr = new StreamReader(HiScoresPath);
			while ((line = sr.ReadLine()) != null) {
				string[] tokens = line.Split('=');
				HiScores[tokens[0]] = int.Parse(tokens[1]);
			}

			sr.Close();
		}
	}


	public void WriteHiScores()
	{
		StreamWriter sw = new StreamWriter(HiScoresPath);

		 foreach(KeyValuePair<string, int> scoreItem in HiScores) {
			sw.WriteLine(scoreItem.Key + "=" + scoreItem.Value);
		}

		sw.Close();
	}

}
