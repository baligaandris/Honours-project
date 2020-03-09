using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class BossEnablerScript : PlayableBehaviour
{
  [SerializeField]
  private GameObject enabledControlPanel;

  [SerializeField]
  Interactable bossThing;

}
