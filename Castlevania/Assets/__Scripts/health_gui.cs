using UnityEngine;
using System.Collections;

public class health_gui : MonoBehaviour {
	public int health = 18;
	public int score = 0;
	public GameObject simon;

	string genTopText(int score, int time, int stage){
		string text = "SCORE-";
		string score_str = score.ToString ();
		while (score_str.Length < 6) {
			score_str = "0" + score_str;
		}
		text += score_str+" TIME ";
		string time_str = time.ToString();
		while (time_str.Length < 4) {
			time_str = "0" + time_str;
		}
		text += time_str + " STAGE " + stage.ToString ();
		return text;
	}

	void OnGUI() {
		// Black background
		Color black = new Color(0, 0, 0);
		Texture2D black_tex = new Texture2D(1, 1);
		black_tex.SetPixel(0, 0, black);
		black_tex.Apply();
		GUI.skin.box.normal.background = black_tex;
		GUI.Box (new Rect (0, 0, Screen.width, 200), GUIContent.none);

		// Top line
		GUI.skin.label.normal.background = black_tex;
		GUI.skin.label.fontSize = 40;
		string top_text = genTopText (score, 200, 1);
		GUI.Label (new Rect (10, 10, Screen.width - 30, 70), top_text);
		GUI.Label (new Rect (10, 50, Screen.width - 30, 70), "PLAYER");

		// Health blocks
		Color red = new Color(1, 0, 0);
		Texture2D red_tex = new Texture2D (1, 1);
		red_tex.SetPixel (0, 0, red);
		red_tex.Apply ();
		Color white = new Color(1, 1, 1);
		Texture2D white_tex = new Texture2D (1, 1);
		white_tex.SetPixel (0, 0, white);
		white_tex.Apply ();

		GUI.skin.box.normal.background = red_tex;
		for (int x=0; x<18; x++) {
			if (x >= health) 
				GUI.skin.box.normal.background = white_tex;
			GUI.Box (new Rect(180+x*20, 70, 10, 15), GUIContent.none);
		}


	}
	void sethealth(int hp) {
		health = hp;
	}

	void setscore(int scor) {
		score = scor;
	}
}
