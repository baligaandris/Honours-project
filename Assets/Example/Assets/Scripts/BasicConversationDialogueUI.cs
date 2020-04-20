/*

The MIT License (MIT)

Copyright (c) 2015-2017 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Yarn.Unity;
using Yarn;
using Eachb.YarnExtensions;

namespace Eachb.YarnDialogue
{
    /// Displays dialogue lines to the player, and sends
    /// user choices back to the dialogue system.

    /** Note that this is just one way of presenting the
     * dialogue to the user. The only hard requirement
     * is that you provide the RunLine, RunOptions, RunCommand
     * and DialogueComplete coroutines; what they do is up to you.
     */
    [RequireComponent(typeof(DialogueRunner))]
    public class BasicConversationDialogueUI : DialogueUIBehaviour
    {

        /// The object that contains the dialogue and the options.
        /** This object will be enabled when conversation starts, and 
         * disabled when it ends.
         */
        public GameObject dialogueContainer;

        public GameObject dialogueOptionsPanel;

        /// The UI element that displays lines
        public TextMeshProUGUI lineText;

        /// A UI element that appears after lines have finished appearing
        public GameObject continuePrompt;

        /// A UI element that covers the screen when dialogue is completed to conclude the game
        public GameObject dialogueCompletedOverlay;

        /// A delegate (ie a function-stored-in-a-variable) that
        /// we call to tell the dialogue system about what option
        /// the user selected
        private OptionChooser SetSelectedOption;

        /// How quickly to show the text, in seconds per character
        [Tooltip("How quickly to show the text, in seconds per character")]
        public float textSpeed = 0.025f;

        /// The buttons that let the user choose an option
        public List<Button> optionButtons;

        public VariableStorageBehaviour variableStorageReference;


        [Tooltip("Minimum amount of time in seconds before the dialogue runner will accept more skip input")]
        public float inputSpamTime = 0.4f;

        /// <summary>
        /// Boolean this class may access during a text display loop which, when true, skips to the end of the line.
        /// </summary>
        private bool _trySkip;
        /// <summary>
        /// Internal running log of when the last skip input was made so we can tell when the input should be enabled.
        /// </summary>
        private float _lastSkipTime;

        public string CurrentNode { get; private set; } = "";

        public delegate void YarnDialogueNodeCompletion(string completedNodeName);
        public static event YarnDialogueNodeCompletion OnYarnDialogueNodeCompleted;

        public void StartDialogue(string entryNode)
        {
            this.CurrentNode = entryNode;
            GetComponent<DialogueRunner>().StartDialogue(entryNode);
        }

        /// <summary>
        /// This only sets the internal skipping counter to true if more than inputSpamTime seconds have passed since the last skip to filter spammed input.
        /// </summary>
        public bool TrySkip
        {
            //In order to get an active skip the linetext object actually has to be active
            get { if (!lineText.gameObject.activeSelf) { _trySkip = false; _lastSkipTime = 0.0f; } return _trySkip; }
            set
            {
                if (!value || !lineText.gameObject.activeSelf)
                {
                    _lastSkipTime = Time.time;
                    _trySkip = false;
                    return;
                }
                if ((Time.time - _lastSkipTime) > inputSpamTime)
                {
                    _lastSkipTime = Time.time;
                    _trySkip = value;
                }
            }
        }

        void Awake()
        {
            // Start by hiding the container, line and option buttons
            if (dialogueContainer != null)
                dialogueContainer.SetActive(false);
            dialogueOptionsPanel?.SetActive(false);
            lineText.gameObject.SetActive(false);


            // Hide the continue prompt if it exists
            if (continuePrompt != null)
                continuePrompt.SetActive(false);
        }

        /// Show a line of dialogue, gradually
        public override IEnumerator RunLine(Line line)
        {
            TrySkip = false;
            // Show the text
            lineText.gameObject.SetActive(true);
            if (textSpeed > 0.0f)
            {
                // Display the line one character at a time
                YarnTextMeshParser ytmp = new YarnTextMeshParser(line.text, variableStorageReference);
                for (int i = 0; i < ytmp.StringLength; i++)
                {
                    if (TrySkip)
                    {
                        TrySkip = false;
                        lineText.text = ytmp.GetYarnString(ytmp.StringLength - 1);
                        break;
                    }
                    lineText.text = ytmp.GetYarnString(i);
                    //Be warned this has a limit of <framerate> chars/second. We can implement a more sophisticated solution (more than 1 char/frame) if necessary.
                    yield return new WaitForSeconds(textSpeed);
                }
            }
            else
            {
                // Display the line immediately if textSpeed == 0
                lineText.text = line.text;
            }
            // Show the 'press any key' prompt when done, if we have one
            if (continuePrompt != null)
                continuePrompt.SetActive(true);
            // Wait for any user input
            while (Input.anyKeyDown == false)
            {
                yield return null;
            }
            // Hide the text and prompt
            lineText.gameObject.SetActive(true);
            if (continuePrompt != null)
                continuePrompt.SetActive(false);
        }

        /// Show a list of options, and wait for the player to make a selection.
        public override IEnumerator RunOptions(Options optionsCollection,
                                                OptionChooser optionChooser)
        {
            dialogueOptionsPanel?.SetActive(true);
            // Do a little bit of safety checking
            if (optionsCollection.options.Count > optionButtons.Count)
            {
                Debug.LogWarning("There are more options to present than there are" +
                                 "buttons to present them in. This will cause problems.");
            }
            optionButtons.ForEach(x => x.gameObject.SetActive(false));

            // Display each option in a button, and make it visible
            int i = 0;
            foreach (var optionString in optionsCollection.options)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<Text>().text = optionString;
                i++;
            }

            // Record that we're using it
            SetSelectedOption = optionChooser;

            // Wait until the chooser has been used and then removed (see SetOption below)
            while (SetSelectedOption != null)
            {
                yield return null;
            }

            dialogueOptionsPanel?.SetActive(false);
        }

        /// Called by buttons to make a selection.
        public void SetOption(int selectedOption)
        {

            // Call the delegate to tell the dialogue system that we've
            // selected an option.
            SetSelectedOption(selectedOption);

            // Now remove the delegate so that the loop in RunOptions will exit
            SetSelectedOption = null;
        }

        /// Run an internal command.
        public override IEnumerator RunCommand(Command command)
        {
            // "Perform" the command
            Debug.Log("Command: " + command.text);

            yield break;
        }

        /// Called when the dialogue system has started running.
        public override IEnumerator DialogueStarted()
        {
            Debug.Log("Dialogue starting!");

            // Enable the dialogue controls.
            dialogueContainer?.SetActive(true);


            yield break;
        }

        public override IEnumerator NodeComplete(string nextNode)
        {
            if (string.IsNullOrEmpty(this.CurrentNode)) { yield break; }
            OnYarnDialogueNodeCompleted?.Invoke(this.CurrentNode);
            this.CurrentNode = nextNode;
            yield break;
        }
        /// Called when the dialogue system has finished running.
        public override IEnumerator DialogueComplete()
        {
            Debug.Log("Complete!");

            // Hide the dialogue interface.
            if (dialogueContainer != null)
                dialogueContainer.SetActive(false);

            // Show the blank overlay
            if (dialogueCompletedOverlay != null)
            {
                dialogueCompletedOverlay.SetActive(true);
            }

            yield break;
        }
        void Update()
        {
            if (Input.GetButtonDown("Jump") /*|| Input.GetButtonDown("Fire1")*/)
            {
                TrySkip=true;
            }
        }

    }
}
