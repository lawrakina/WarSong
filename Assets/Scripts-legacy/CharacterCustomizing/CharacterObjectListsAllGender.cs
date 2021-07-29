using System;
using System.Collections.Generic;
using UnityEngine;


namespace CharacterCustomizing
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
    }
}