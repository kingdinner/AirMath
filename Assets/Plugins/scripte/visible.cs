using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;

public class visible : MonoBehaviour {
    // Update is called once per frame
    public SqliteConnection con_db;
    public SqliteCommand cmd_db;
    public SqliteDataReader rdr;
    public string path;


    public Text text;
    public Text text1;
    public Image panel;

    string account;
    string level;
    string money;

    public InputField nacc;
    private void connection()
    {
        try
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
        catch (Exception ex)
        {
            text.text = ex.ToString();
        }
    }

   
    private void Disconnect()
    {
        con_db.Close();
        con_db.Dispose();
    }

    private void accounts()
    {
        connection();
        try
        {
            cmd_db = new SqliteCommand("Select * from Account", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                text.text = rdr[0].ToString();
            }

            Disconnect();

        }
        catch (Exception ex)
        {
            text.text = ex.ToString();
        }
        var x = text.text;
        if (x == "")
        {
            text1.gameObject.SetActive(false);
        }
        else
        {
            text1.gameObject.SetActive(true);
        }
        

    }
    //---------
    void Start()
    {
        //text.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        //----display database
        accounts();
      
    }
   


    public void naccount()
    {
        connection();
        try
        {
            account = nacc.text.ToString();
            cmd_db = new SqliteCommand("Insert Into Account(Player,money,Chapter1,Chapter2) values ('" + account + "','" + "0" + "','" + "1" + "','" + "1" + "' ) ", con_db);
            cmd_db.ExecuteNonQuery();
            Disconnect();
            accounts();
            Disconnect();
        }
        catch (Exception ex)
        {
            text.text = ex.ToString();
        }
        panel.gameObject.SetActive(false);

    }




    //-----------------
    public void ChangeScene(string sceneName)
    {
        var x = text.text;
        if (x == "")
        {
            panel.gameObject.SetActive(true);
        }
       else
        {
            Application.LoadLevel(sceneName);
       
        }
    }
   
}
