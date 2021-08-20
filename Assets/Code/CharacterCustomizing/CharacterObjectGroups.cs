using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Code.CharacterCustomizing
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

        public List<GameObject> SearchByName(string listNames)
        {
            var names = listNames.Split(new char[]{'|'});
            var results = new List<GameObject>();
            results.AddRange(SearchByName(names, arm_Lower_Left));
            results.AddRange(SearchByName(names, arm_Lower_Right));
            results.AddRange(SearchByName(names, arm_Upper_Left));
            results.AddRange(SearchByName(names, arm_Upper_Right));
            results.AddRange(SearchByName(names, eyebrow));
            results.AddRange(SearchByName(names, facialHair));
            results.AddRange(SearchByName(names, hand_Left));
            results.AddRange(SearchByName(names, hand_Right));
            results.AddRange(SearchByName(names, headAllElements));
            results.AddRange(SearchByName(names, headNoElements));
            results.AddRange(SearchByName(names, hips));
            results.AddRange(SearchByName(names, leg_Left));
            results.AddRange(SearchByName(names, leg_Right));
            results.AddRange(SearchByName(names, torso));

            return results;
        }

        private static IEnumerable<GameObject> SearchByName(string[] listNames, List<GameObject> source)
        {
            return listNames.Select(name => source.FirstOrDefault(x => x.name == name)).Where(item => item != null).AsEnumerable();
        }
    }
}