using HoloToolkit.Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class ScaleManager : Singleton<ScaleManager>
{
    float expandAnimationCompletionTime;
    // Store a bool for whether our scale model is expanded or not.
    bool isModelExpanding = false;

    // KeywordRecognizer object.
    KeywordRecognizer keywordRecognizer;

    // Defines which function to call when a keyword is recognized.
    delegate void KeywordAction(PhraseRecognizedEventArgs args);
    Dictionary<string, KeywordAction> keywordCollection;

    void Start()
    {
        keywordCollection = new Dictionary<string, KeywordAction>();

        // Add keyword Expand Model to call the ExpandModelCommand function.
        keywordCollection.Add("Expand Model", ExpandModelCommand);

        // Add keyword Reset Model to call the ResetModelCommand function.
        keywordCollection.Add("Contract Model", ContractModelCommand);

        // Initialize KeywordRecognizer with the previously added keywords.
        keywordRecognizer = new KeywordRecognizer(keywordCollection.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction keywordAction;

        if (keywordCollection.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke(args);
        }
    }

    private void ContractModelCommand(PhraseRecognizedEventArgs args)
    {
        // Reset local variables.
        isModelExpanding = false;

        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void ExpandModelCommand(PhraseRecognizedEventArgs args)
    {
        PopulateDescription pop = new PopulateDescription();
        pop.populateObject(this.gameObject);
        this.transform.localScale = new Vector3(1.0f, 10.0f, 1.0f);
        //this.transform.localScale.y


        // Set the expand model flag.
        isModelExpanding = true;

        //ExpandModel.Instance.Expand();
    }

    public void Update()
    {
        if (isModelExpanding)
        {
            isModelExpanding = false;

        
        }
    }

}