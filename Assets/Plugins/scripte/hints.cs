using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using UnityEngine.SceneManagement;


public class hints : MonoBehaviour {
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
	// Use this for initialization
	void Start(){
		connection ();
		cmd_db = new SqliteCommand ("SELECT * FROM market", con_db);
		rdr = cmd_db.ExecuteReader ();
		while (rdr.Read ()) {
			dfree = rdr[1].ToString();
			dtime = rdr[0].ToString();
		
		}
		rdr.Close ();
		Disconnect ();
	}

	
	// Update is called once per frame
	void time () {
	
	}
}
