using System;
using UnityEngine;


namespace Code.Extension.Collections
{
    [Serializable] public class LinkedListItem
    {
        public int Key;
        public GameObject Value;

        public LinkedListItem(int key, GameObject value)
        {
            Key = key;
            Value = value;
        }
    }

    [Serializable] public class LinkedListItem<T1>
    {
        public T1 Key;
        public GameObject Value;

        public LinkedListItem(T1 key, GameObject value)
        {
            Key = key;
            Value = value;
        }
    }
}