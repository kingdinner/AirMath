using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;


public class selectchapter : MonoBehaviour {
	public SqliteConnection con_db;
	public SqliteCommand cmd_db;
	public SqliteDataReader rdr;
	public string path;

	public Image image;
	string x;
	// Use this for initialization
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

	void Update()
	{
		image.enabled = false;
		connection();
		cmd_db = new SqliteCommand("Select * from Logical", con_db);
		rdr = cmd_db.ExecuteReader();
		while (rdr.Read())
		{
			x = rdr[0].ToString();
		}
		rdr.Close();
		Disconnect();

		if (x == "1") {
			image.enabled = true;
		} 

	}
}
