using System;
using System.Collections.Generic;
using System.Linq;
using Code.Extension;
using UnityEngine;


namespace Code.CharacterCustomizing
{
    [Serializable] public class CharacterObjectListsAllGender
    {// classe for keeping the lists organized, allows for organization of the all gender items
        public List<GameObject> all_12_Extra = new List<GameObject>();
        public List<GameObject> all_Hair = new List<GameObject>();
        public List<GameObject> all_Head_Attachment = new List<GameObject>();
        public List<GameObject> back_Attachment = new List<GameObject>();
        public List<GameObject> chest_Attachment = new List<GameObject>();
        public List<GameObject> elbow_Attachment_Left = new List<GameObject>();
        public List<GameObject> elbow_Attachment_Right = new List<GameObject>();
        public List<GameObject> elf_Ear = new List<GameObject>();
        public List<GameObject> headCoverings_Base_Hair = new List<GameObject>();
        public List<GameObject> headCoverings_No_FacialHair = new List<GameObject>();
        public List<GameObject> headCoverings_No_Hair = new List<GameObject>();
        public List<GameObject> hips_Attachment = new List<GameObject>();
        public List<GameObject> knee_Attachement_Left = new List<GameObject>();
        public List<GameObject> knee_Attachement_Right = new List<GameObject>();
        public List<GameObject> shoulder_Attachment_Left = new List<GameObject>();
        public List<GameObject> shoulder_Attachment_Right = new List<GameObject>();

        public List<GameObject> SearchByName(string listNames)
        {
            Dbg.Log($"Search item: {listNames}");
            var names = listNames.Split(new char[]{'|'});
            var results = new List<GameObject>();
            results.AddRange(SearchByName(names, all_12_Extra));
            results.AddRange(SearchByName(names, all_Hair));
            results.AddRange(SearchByName(names, all_Head_Attachment));
            results.AddRange(SearchByName(names, back_Attachment));
            results.AddRange(SearchByName(names, chest_Attachment));
            results.AddRange(SearchByName(names, elbow_Attachment_Left));
            results.AddRange(SearchByName(names, elbow_Attachment_Right));
            results.AddRange(SearchByName(names, elf_Ear));
            results.AddRange(SearchByName(names, headCoverings_Base_Hair));
            results.AddRange(SearchByName(names, headCoverings_No_FacialHair));
            results.AddRange(SearchByName(names, headCoverings_No_Hair));
            results.AddRange(SearchByName(names, hips_Attachment));
            results.AddRange(SearchByName(names, knee_Attachement_Left));
            results.AddRange(SearchByName(names, knee_Attachement_Right));
            results.AddRange(SearchByName(names, shoulder_Attachment_Left));
            results.AddRange(SearchByName(names, shoulder_Attachment_Right));

            Dbg.Log($"Result of search:{results}, counts:{results.Count}");
            return results;
        }

        private static IEnumerable<GameObject> SearchByName(string[] listNames, List<GameObject> source)
        {
            return listNames.Select(name => source.FirstOrDefault(x => x.name == name)).Where(item => item != null).AsEnumerable();
        }
    }
}