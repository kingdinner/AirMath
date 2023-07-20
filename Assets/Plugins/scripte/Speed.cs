using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class Speed : MonoBehaviour {

    public Rigidbody2D rb1;
    public Rigidbody2D rb2;
    public Rigidbody2D rb3;
    public Rigidbody2D rb4;
    public Rigidbody2D rb5;
    public Rigidbody2D rb6;
    public Rigidbody2D rb7;
    public Rigidbody2D rb8;
    public Rigidbody2D rb9;
    public Rigidbody2D rb10;

	public float ballForce = 0;
    //speed
    public Text speed;

	private string dfree;
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


	void Start()
    {
		connection ();
		cmd_db = new SqliteCommand ("SELECT * FROM market", con_db);
		rdr = cmd_db.ExecuteReader ();
		while (rdr.Read ()) {
			dfree = rdr[1].ToString();


		}
		rdr.Close ();
		Disconnect ();
if (speed.text == "Intermediate")
        {
            rb1.gravityScale = 5;
            rb2.gravityScale = 5;
            rb3.gravityScale = 5;
            rb4.gravityScale = 5;
            rb5.gravityScale = 5;
            rb6.gravityScale = 5;
            rb7.gravityScale = 5;
            rb8.gravityScale = 5;
            rb9.gravityScale = 5;
            rb10.gravityScale = 5;
        }
        else if (speed.text == "Difficult")
        {
            rb1.gravityScale = 10;
            rb2.gravityScale = 10;
            rb3.gravityScale = 10;
            rb4.gravityScale = 10;
            rb5.gravityScale = 10;
            rb6.gravityScale = 10;
            rb7.gravityScale = 10;
            rb8.gravityScale = 10;
            rb9.gravityScale = 10;
            rb10.gravityScale = 10;
        }
    }
	public void free () {
		if (int.Parse (dfree) > 0) {
			rb1.isKinematic = true;
			rb2.isKinematic = true;
			rb3.isKinematic = true;
			rb4.isKinematic = true;
			rb5.isKinematic = true;
			rb6.isKinematic = true;
			rb7.isKinematic = true;
			rb8.isKinematic = true;
			rb9.isKinematic = true;
			rb10.isKinematic = true;
				int f;
				f = int.Parse (dfree) - 1;
				connection ();
				cmd_db = new SqliteCommand ("Update market Set free = ' " + f + "'", con_db);
				cmd_db.ExecuteNonQuery ();
				Disconnect ();
		}
		}

	// Update is called once per frame

}
