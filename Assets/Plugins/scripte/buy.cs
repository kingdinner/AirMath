using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class buy : MonoBehaviour {
	public SqliteConnection con_db;
	public SqliteCommand cmd_db;
	public SqliteDataReader rdr;
	public string path;

	public Text player;
	public Text coins;
	private string dheart;
	private string dfree;
	private string dtime;

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


	void Start () {
		connection ();
		cmd_db = new SqliteCommand ("SELECT * FROM Account", con_db);
		rdr = cmd_db.ExecuteReader ();
		while (rdr.Read ()) {

			player.text = rdr[0].ToString ();
			coins.text = rdr[1].ToString ();
		}
		rdr.Close ();
		Disconnect ();
		connection ();
		cmd_db = new SqliteCommand ("SELECT * FROM market", con_db);
		rdr = cmd_db.ExecuteReader ();
		while (rdr.Read ()) {
			dfree = rdr[1].ToString();
			dtime = rdr[0].ToString();
			dheart = rdr[2].ToString();
		}
		rdr.Close ();
		Disconnect ();
	
	}
	
	public void freeze()
	{
		if (int.Parse (coins.text) > 80) {
			int f,c;
			f = int.Parse (dfree) + 1;
			c = int.Parse (coins.text) - 80;
			coins.text = c.ToString();
			connection ();
			cmd_db = new SqliteCommand ("Update market Set freeze = ' " + f + "', money = ' " + c + "'", con_db);
			cmd_db.ExecuteNonQuery ();
			Disconnect ();
		}
	}
	public void time()
	{
		if (int.Parse (coins.text) > 80) {
			int t, c;
			t = int.Parse (dtime) + 1;
			c = int.Parse (coins.text) - 80;
			coins.text = c.ToString ();
			connection ();
			cmd_db = new SqliteCommand ("Update market Set time = ' " + t + "', money = ' " + c + "'", con_db);
			cmd_db.ExecuteNonQuery ();
			Disconnect ();
		}
	}
	public void heart()
	{
		if (int.Parse (coins.text) > 100) {
			int h,c;
			h = int.Parse (dheart) + 1;
			c = int.Parse (coins.text) - 80;
			coins.text = c.ToString ();
			connection ();
			cmd_db = new SqliteCommand ("Update market Set heart = ' " + h + "', money = ' " + c + "'", con_db);
			cmd_db.ExecuteNonQuery ();
			Disconnect ();
		}
	}
}
