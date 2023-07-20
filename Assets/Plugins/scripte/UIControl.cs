using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	

public class UIControl : MonoBehaviour 
{
	public float hSliderValue;

    public void ChangeScene ()
	{
		SceneManager.LoadScene("ChapterOne");
	}
   
	public void LoadScene ()
	{
		SceneManager.LoadScene ("two");
	}
	public void MainMenu ()
	{
		SceneManager.LoadScene ("Mainmenu");
	}
	public void one ()
	{
		SceneManager.LoadScene ("ChapterOneEasy");
	}
	public void two ()
	{
		SceneManager.LoadScene ("two");
	}
	public void three ()
	{
		SceneManager.LoadScene ("three");
	}
	public void cone ()
	{
		SceneManager.LoadScene ("logic1");
	}
	public void ctwo ()
	{
		SceneManager.LoadScene ("four");
	}
	public void cthree ()
	{
		SceneManager.LoadScene ("five");
	}
	public void Howtoplay ()
	{
		SceneManager.LoadScene ("Howtoplay");
	}
	public void Setting()
	{
		SceneManager.LoadScene ("Settings");
	}
	public void Market()
	{
		SceneManager.LoadScene ("market");
	}
	public void chapter2()
	{
		SceneManager.LoadScene ("ChapterTwo");
	}
	public void quit1()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) { 
			Application.Quit();
		} else {
			Application.Quit();
		}
	}
public void AudioOptions()
	{
		AudioListener.volume = hSliderValue/10.0f;
	}
}

















//	public void VolumeControl (float volumeControl)
//	{
//		GetComponent<AudioSource>().volume = volumeControl;
//		PlayerPrefs.SetFloat ("CurVol", GetComponent<AudioSource>().volume);
//	}

//	void Awake()
//	{
//		GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("CurVol");

//		if (slider) 
//		{
//			slider.value = GetComponent<AudioSource> ().volume;
//		}

//	}


