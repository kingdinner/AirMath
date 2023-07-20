using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class Chapter2 : MonoBehaviour {
	bool timeups;
	public SqliteConnection con_db;
	public SqliteCommand cmd_db;
	public SqliteDataReader rdr;
	public string path;
	public int x = 4;
	//addition
	public Text text;

	public Text final;

	//level check if easy, inter, diff
	public Text level;
	private string level1;
	public Text money;
	private string money1;
	public Text deter;
	public Button b1;
	public Text name1;
	//heart or lives
	public Image image;
	public Image image1;
	public Image image2;

	//random letter
	public Text a;
	public Text b;
	public Text c;
	public Text d;
	public Text e;
	public Text f;
	public Text g;
	public Text h;
	public Text i;
	public Text j;
	private int quest;
	// next level
	public Image image4;
	public Text wl;

	string z;
	//time
	public Text text6;
	float timeLeft = 15.0f;
	private string dtime;
	public Text pause;
	private void connection()
	{

		// para sa windows or application type at para sa path
		if (Application.platform != RuntimePlatform.Android)
		{
			path = Application.dataPath + "/Plugins/database/airdatabase.bytes.db";
		}
		else
		{
			path = Application.persistentDataPath + "/Plugins/database/airdatabase.bytes.db";
			if (!File.Exists(path))
			{
				WWW load = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "Plugins/database/airdatabase.bytes.db");
				while (!load.isDone) { }
				File.WriteAllBytes(path, load.bytes);
			}
		}
		// para sa pagopen ng detabase
		con_db = new SqliteConnection("URI=file:" + path);
		con_db.Open();
	}


	private void Disconnect()
	{
		con_db.Close();
		con_db.Dispose();
	}

	void start1()
	{
		a.text = Random.Range(0, 50).ToString();
		b.text = Random.Range(0, 50).ToString();
		c.text =Random.Range(0, 50).ToString();
		d.text = Random.Range(0, 50).ToString();
		e.text = Random.Range(0, 50).ToString();
		f.text = Random.Range(0, 50).ToString();
		g.text = Random.Range(0, 50).ToString();
		h.text = Random.Range(0, 50).ToString();
		i.text = Random.Range(0, 50).ToString();
		char letter = (char)('a' + Random.Range(0, 9));
		var x = (letter).ToString();

		if (x == "a")
		{
			a.text = final.text;
		}
		if (x == "b")
		{
			b.text = final.text;
		}
		if (x == "c")
		{
			c.text = final.text;
		}
		if (x == "d")
		{
			d.text = final.text;
		}
		if (x == "e")
		{
			e.text = final.text;
		}
		if (x == "f")
		{
			f.text = final.text;
		}
		if (x == "g")
		{
			g.text = final.text;
		}
		if (x == "h")
		{
			h.text = final.text;
		}
		if (x == "i")
		{
			i.text = "";
			i.text = final.text;
		}
		if (x == "j")
		{
			j.text = final.text;
		}
		image4.gameObject.SetActive(false);
	}

	void chapter()
	{
		if (deter.text == "Easy") {
			quest = 0;
		} else if (deter.text == "Intermediate") {
			quest = 1;
		}else if (deter.text == "Difficult") {
			quest = 2;
		}
		connection ();
		cmd_db = new SqliteCommand ("SELECT * FROM Question ORDER BY RANDOM() LIMIT 1", con_db);
		rdr = cmd_db.ExecuteReader ();
		while (rdr.Read ()) {

			z = rdr[quest].ToString ();
		
		}
		rdr.Close ();
		Disconnect ();

		int idx = z.LastIndexOf("!");
		if (idx > 2) {
			text.text = z.Remove (idx);
			final.text = z.Substring(idx);
			final.text = final.text.Remove (0, 1);
		}
	
	}
	void Start()

	{


		connection();
		cmd_db = new SqliteCommand("Select * from Account", con_db);
		rdr = cmd_db.ExecuteReader();
		while (rdr.Read())
		{
			money.text = rdr[1].ToString();
			level.text = rdr[3].ToString();
		}
		rdr.Close();
		Disconnect();

		if (deter.text == "Easy") {

			chapter();
			start1 ();
		} else if (deter.text == "Intermediate") {

			chapter();
			start1 ();
		} else if (deter.text == "Difficult") {

			chapter();
			start1 ();
		}






	}
	//lose
	void loss()
	{

		x = x - 1;
		if (x == 3)
		{
			image2.gameObject.SetActive(false);

		}
		if (x == 2)
		{
			image1.gameObject.SetActive(false);

		}
		if (x == 1)
		{
			image.gameObject.SetActive(false);
			wl.text = "Sorry";
			b1.gameObject.SetActive(false);
			image4.gameObject.SetActive(true);
			timeups = true;
			name1.text = "Main Menu";
		}

	}
	//winner
	void play ()
	{
		image4.gameObject.SetActive(true);
		text6.text = "LOCKED";
		timeups = true;
		wl.text = "Next";
		name1.text = "Okay";

	}
	public void time () {
		if (int.Parse (dtime) >= 1) {
			timeLeft = timeLeft + 10;
			text6.text = timeLeft.ToString ();
			int f;
			f = int.Parse (dtime) - 1;
			connection ();
			cmd_db = new SqliteCommand ("Update market Set time = ' " + f + "'", con_db);
			cmd_db.ExecuteNonQuery ();
			Disconnect ();
		}
	}
	public void pause1()
	{
		if (pause.text == "ll") {
			Time.timeScale = 0;
			pause.text = ">";
		} else {
			pause.text = "ll";
			Time.timeScale = 1;
		}
	}
	void Update()
	{
		timeLeft -= Time.deltaTime;
		text6.text = Mathf.Round(timeLeft).ToString();
		if (timeLeft < 0)
		{
			if (timeups == false) {
				text6.text = "Time UP";
				wl.text = "Try Again";
				image4.gameObject.SetActive(true);
				name1.text = "Okay";
			}
		}

		if(timeups == true)
		{
			text6.text = "Locked";
			timeLeft -= Time.time;
		}

	}

	public void n1()
	{

		if (a.text == final.text)
		{
			play();
		}
		else
		{
			loss();
		}
	}
	public void n2()
	{

		if (b.text == final.text)
		{
			play();
		}
		else
		{
			loss();
		}
	}
	public void n3()
	{

		if (c.text == final.text)
		{
			play();
		}
		else
		{
			loss();
		}
	}
	public void n4()
	{
		if (d.text == final.text)
		{
			play();
		}
		else
		{
			loss();
		}
	}
	public void n5()
	{

		if (e.text == final.text)
		{
			play();
		}
		else
		{
			loss();
		}
	}
	public void n6()
	{

		if (f.text == final.text)
		{
			play();
		}
		else
		{
			loss();
		}
	}
	public void n7()
	{

		if (g.text == final.text)
		{
			play();
		}
		else
		{
			loss();
		}
	}
	public void n8()
	{
		if (h.text == final.text)
		{
			play();
		}
		else
		{
			loss();
		}
	}
	public void n9()
	{

		if (i.text == final.text)
		{
			play();
		}
		else
		{
			loss();
		}
	}
	public void n10()
	{

		if (j.text == final.text)
		{
			play();
		}
		else
		{
			loss();
		}
	}
	public void lvlone()
	{
		if (wl.text == "Next") {
			var z = 0;
			var m = 0;
			m = int.Parse (money.text);
			z = int.Parse (level.text);
			level1 = (z + 1).ToString ();
			money1 = (m + 5).ToString ();
			if (z != 25 & deter.text == "Easy") {
				connection ();
				cmd_db = new SqliteCommand ("Update Account Set money = ' " + money1 + "', Chapter2 = ' " + level1 + "'", con_db);
				cmd_db.ExecuteNonQuery ();
				Disconnect ();
				SceneManager.LoadScene ("logic1");
			}else if (z != 25 & deter.text == "Intermediate") {
				connection ();
				cmd_db = new SqliteCommand ("Update Account Set money = ' " + money1 + "', Chapter2 = ' " + level1 + "'", con_db);
				cmd_db.ExecuteNonQuery ();
				Disconnect ();
				SceneManager.LoadScene ("four");
			}else if (z != 25 & deter.text == "Difficult") {
				connection ();
				cmd_db = new SqliteCommand ("Update Account Set money = ' " + money1 + "', Chapter2 = ' " + level1 + "'", con_db);
				cmd_db.ExecuteNonQuery ();
				Disconnect ();
				SceneManager.LoadScene ("five");
			}
				
			else if (deter.text == "Easy")
			{
				connection();
				cmd_db = new SqliteCommand("Update Logical Set Intermediate= '" + 1 + "'", con_db);
				cmd_db.ExecuteNonQuery();
				Disconnect();
				var p = 0;
				p = int.Parse(money.text);
				money1 = (p + 5).ToString();
				connection();
				cmd_db = new SqliteCommand("Update Account Set money = '" + money1 + "',Chapter2 = '" + 1 + "'", con_db);
				cmd_db.ExecuteNonQuery();
				Disconnect();
				SceneManager.LoadScene("selectchapter");
			}else if (deter.text == "Intermediate")
			{
				connection();
				cmd_db = new SqliteCommand("Update Logical Set Difficult= '" + 1 + "'", con_db);
				cmd_db.ExecuteNonQuery();
				Disconnect();
				connection();
				var p = 0;
				p = int.Parse(money.text);
				money1 = (p + 5).ToString();
				cmd_db = new SqliteCommand("Update Account Set  money = '" + money1 + "', Chapter2 = '" + 1 + "'", con_db);
				cmd_db.ExecuteNonQuery();
				Disconnect();
				SceneManager.LoadScene("selectchapter");
			}else if (deter.text == "Difficult")
			{
				connection();
				var p = 0;
				p = int.Parse(money.text);
				money1 = (p + 5).ToString();
				cmd_db = new SqliteCommand("Update Account Set  money = '" + money1 + "', Chapter2 = '" + 1 + "'", con_db);
				cmd_db.ExecuteNonQuery();
				Disconnect();
				SceneManager.LoadScene("final");
			}
		} 
		else if (wl.text == "Try Again") {
			loss ();
			timeLeft = 15.0f;
			image4.gameObject.SetActive(false);
		}
		else if (wl.text == "Sorry") {
			image4.gameObject.SetActive(false);

			SceneManager.LoadScene ("Mainmenu");
		}

		
	}
}

