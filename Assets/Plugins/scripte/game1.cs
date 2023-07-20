using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;

public class game1 : MonoBehaviour {
    public SqliteConnection con_db;
    public SqliteCommand cmd_db;
    public SqliteDataReader rdr;
    public string path;

	public Text money;
	private string money1;

    public Text determine;
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

    public void LoadScene(string loadName)
    {
		
		if (determine.text == "Easy")
        {
            connection();
            cmd_db = new SqliteCommand("Update Mdas Set Intermediate= '" + 1 + "'", con_db);
            cmd_db.ExecuteNonQuery();
            Disconnect();
			var m = 0;
			m = int.Parse(money.text);
			money1 = (m + 5).ToString();
			connection();
            cmd_db = new SqliteCommand("Update Account Set money = '" + money1 + "',Chapter1 = '" + 1 + "'", con_db);
            cmd_db.ExecuteNonQuery();
            Disconnect();
            Application.LoadLevel(loadName);
        }
		else if (determine.text == "Intermediate")
        {
            connection();
            cmd_db = new SqliteCommand("Update Mdas Set Difficult= '" + 1 + "'", con_db);
            cmd_db.ExecuteNonQuery();
            Disconnect();
			connection();
			var m = 0;
			m = int.Parse(money.text);
			money1 = (m + 5).ToString();
            cmd_db = new SqliteCommand("Update Account Set  money = '" + money1 + "', Chapter1 = '" + 1 + "'", con_db);
            cmd_db.ExecuteNonQuery();
            Disconnect();
            Application.LoadLevel(loadName);
        }
        
      
    }
    public void ChangeScene(string changescene)
    {
        connection();
        cmd_db = new SqliteCommand("Update Logical Set  Unlock = ' " + 1 + "'", con_db);
        cmd_db.ExecuteNonQuery();
        Disconnect();
        Application.LoadLevel(changescene);
    }
}
