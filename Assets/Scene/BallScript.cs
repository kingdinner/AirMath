using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour {

	public Rigidbody2D rb;
	public float ballForce;
	// Use this for initialization
	private string dfree, dtime;
	public SqliteConnection con_db;
	public SqliteCommand cmd_db;
	public SqliteDataReader rdr;
	public string path;

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
	void Start () 
	{
		rb.AddForce (new Vector2 (ballForce, ballForce));

		connection ();
		cmd_db = new SqliteCommand ("SELECT * FROM market", con_db);
		rdr = cmd_db.ExecuteReader ();
		while (rdr.Read ()) {

			dtime = rdr[0].ToString();
			dfree = rdr[1].ToString();

		}
		rdr.Close ();
		Disconnect ();

	}

	public void free () {
		if (int.Parse(dfree) > 0) {
			rb.AddForce (new Vector2 (0, 0));
		}

	}

	// Update is called once per frame
	void time () {

	}
}

