using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Text;
using UnityEngine.Windows.Speech;

public class voiceRec : MonoBehaviour
{
    private KeywordRecognizer keyWordReconizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private Microphone mc;

    // Start is called before the first frame update
    void Start() 
    {
        foreach( var device in Microphone.devices)
        {
            Debug.Log(device);
        }
        Debug.Log("Start");
        actions.Add("forward", Forward);
        actions.Add("up", Up);
        actions.Add("down", Down); 
        actions.Add("back", Back);
        actions.Add("stop", StopRec);

        keyWordReconizer = new KeywordRecognizer(actions.Keys.ToArray());
        keyWordReconizer.OnPhraseRecognized += RecognizedSpeech;
        keyWordReconizer.Start();
        Debug.Log("kwr started");
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Forward()
    {
        transform.Translate(1, 0, 0);
    }

    private void Back()
    {
        transform.Translate(-1, 0, 0);
    }

    private void Up()
    {
        transform.Translate(0, 1, 0);
    }

    private void Down()
    {
        transform.Translate(0, -1, 0);
    }

    private void StopRec()
    {
        keyWordReconizer.Stop();
    }

    // Update is called once per frame
    void Update()
    {
            if(keyWordReconizer.IsRunning)
        {
           // Debug.Log("Still Running");
        }
            
    }
}
