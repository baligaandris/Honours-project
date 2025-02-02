﻿/*

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
/// Attach sprite renderer to game object
namespace Yarn.Unity.Example {

    /// Attach SpriteSwitcher to game object
    public class SpriteSwitcher : MonoBehaviour {

        [System.Serializable]
        public struct SpriteInfo {
            public string name;
            public Sprite sprite;
        }

        public SpriteInfo[] sprites;

        /// Create a command to use on a sprite
        [YarnCommand("setsprite")]
        public void UseSprite(string spriteName) {

            Sprite s = null;
            foreach(var info in sprites) {
                if (info.name == spriteName) {
                    s = info.sprite;
                    break;
                }
             }
            if (s == null) {
                Debug.LogErrorFormat("Can't find sprite named {0}!", spriteName);
                return;
            }

            if (GetComponent<SpriteRenderer>() != null) { GetComponent<SpriteRenderer>().sprite = s; }
            if (GetComponent<Image>() != null) { GetComponent<Image>().sprite = s; }
        }
    }

}
