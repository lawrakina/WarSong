using System;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    [Serializable] public class CharacterObjectGroups
    {// classe for keeping the lists organized, allows for simple switching from male/female objects
        public List<GameObject> arm_Lower_Left = new List<GameObject>();
        public List<GameObject> arm_Lower_Right = new List<GameObject>();
        public List<GameObject> arm_Upper_Left = new List<GameObject>();
        public List<GameObject> arm_Upper_Right = new List<GameObject>();
        public List<GameObject> eyebrow = new List<GameObject>();
        public List<GameObject> facialHair = new List<GameObject>();
        public List<GameObject> hand_Left = new List<GameObject>();
        public List<GameObject> hand_Right = new List<GameObject>();
        public List<GameObject> headAllElements = new List<GameObject>();
        public List<GameObject> headNoElements = new List<GameObject>();
        public List<GameObject> hips = new List<GameObject>();
        public List<GameObject> leg_Left = new List<GameObject>();
        public List<GameObject> leg_Right = new List<GameObject>();
        public List<GameObject> torso = new List<GameObject>();
    }
}