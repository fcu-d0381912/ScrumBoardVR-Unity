using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System;
public class Speech : MonoBehaviour {
    Process Processee;
    StreamWriter myStreamWriter;
    public InputField s2;
	Text inputtotext;
    Thread thread;
    bool start = false;
    string stringtemp = "";
	int linetemp;
	TextGenerator ge;

    private void Start()
    {
		s2.lineType = InputField.LineType.MultiLineNewline;
        Processee = new Process();
        string jsonCredentialsPath = Path.Combine(Application.streamingAssetsPath, "application_default_credentials.json");
        Processee.StartInfo.Arguments = jsonCredentialsPath;
        Processee.StartInfo.FileName = Path.Combine(Application.streamingAssetsPath, "ConsoleApplication3.exe");
        thread = new Thread(ProcesswStart);
        thread.IsBackground = true;
        thread.Start();

		inputtotext = s2.GetComponentsInChildren<Text> () [1];
		ge = inputtotext.cachedTextGenerator;

		//input.caretPosition-=1;
		linetemp = 1;

        //UnityEngine.Debug.Log("ss");
        //SmartLogger.LogError(DebugFlags.GoogleStreamingSpeechToText, "This service is only supported on Windows.");
    }

    public  void ProcesswStart()
    {
        Processee.StartInfo.UseShellExecute = false;
        Processee.StartInfo.RedirectStandardInput = true;
        Processee.StartInfo.RedirectStandardOutput = true;
        Processee.StartInfo.StandardOutputEncoding = System.Text.Encoding.Default;
        

        //Processee.StartInfo.CreateNoWindow = true;
        // Processee.Kill();
        //StartCoroutine(Nowplay());
        Processee.Start();
		beginRead();
        //Processee.StandardOutput.ReadToEnd();


        //Processee.StandardOutput.Read();


        //UnityEngine.Debug.Log(Processee.StandardOutput.Read());
        //Processee.WaitForExit();
        //s2.text= Processee.StandardOutput.ReadToEnd().ToString();


    }
    public void speechStop()
    {
        myStreamWriter = Processee.StandardInput;
        myStreamWriter.WriteLine("Stop");
        start = false;

    }
    public void speechStart()
    {
		
        myStreamWriter = Processee.StandardInput;
		s2.ActivateInputField ();
        myStreamWriter.WriteLine("Start");
        start = true;
		UnityEngine.Debug.Log ("speechok");
    }
    public void readText()
    {

        // if (!Processee.StandardOutput.EndOfStream) {
        while (Processee.StandardOutput.EndOfStream != true) {
            stringtemp = Processee.StandardOutput.ReadLine();

            UnityEngine.Debug.Log(stringtemp.Length);
            start = true;
        }
       
        // Processee.Dispose();
        //   }

        //Processee.Kill();
    }
    public  void beginRead()
    {

		 thread = new Thread(readText);
         thread.IsBackground = true;
         thread.Start();

        //s2.text += "ss中文";
        //s2.text;
        // UnityEngine.Debug.Log(Processee.StandardOutput.ReadToEnd());
        //Processee.Close();
        //Processee.Kill();
    }

	public  void cleanText()
	{
		s2.text = "";
		linetemp = 1;
	}

	public  void deleteText()
	{
		s2.text = s2.text.Remove(s2.caretPosition-1,1);
		linetemp = 1;
	}


	public  void caretleft()
	{

		if(linetemp >= 1){
			if(s2.caretPosition == ge.lines[linetemp-1].startCharIdx)
			{
				UnityEngine.Debug.Log ("second   "+linetemp);
				linetemp--;
			}

		}
		s2.caretPosition--;
	}

	public  void caretRight()
	{
		//UnityEngine.Debug.Log ("second   "+linetemp);
		//UnityEngine.Debug.Log ("second   "+ge.lineCount);
		if(linetemp <= ge.lineCount)
		{
			s2.caretPosition++;
			if(linetemp > 1 && s2.caretPosition == ge.lines[linetemp].startCharIdx )
			{
				linetemp++;
			}
		}


	}

	//public  void caretUp()
	//{
	//	if(linetemp>0){
			
	//		linetemp--;
	//		s2.caretPosition = ge.lines[linetemp].startCharIdx-1;
	//		UnityEngine.Debug.Log(linetemp);
	//		UnityEngine.Debug.Log(s2.caretPosition);
	//	}

	//}
	//public  void caretDown()
	//{
		
	//	linetemp++;
	//	s2.caretPosition = ge.lines[linetemp].startCharIdx;
	//	UnityEngine.Debug.Log(linetemp);
	//	UnityEngine.Debug.Log(s2.caretPosition);
	//}


	public void newline()
	{
		s2.text = s2.text.Insert (s2.caretPosition, "\n");
		//UnityEngine.Debug.Log ("Temp"+linetemp);
		s2.caretPosition = ge.lines[linetemp-1].startCharIdx;
	}
    void Update()
    {
        if (start)
        {
			
			s2.text = s2.text.Insert(s2.caretPosition,stringtemp);
			s2.caretPosition += stringtemp.Length;
			stringtemp = "";

        }
        start = false;
    }
}
