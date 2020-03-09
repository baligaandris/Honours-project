using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System;

[Serializable]
public class EnableBoss : PlayableAsset, ITimelineClipAsset
{
    [SerializeField]
    private BossEnablerScript template = new BossEnablerScript();

    public ClipCaps clipCaps
    { get { return ClipCaps.None; } }
    

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<BossEnablerScript>.Create(graph, template);
    }
}