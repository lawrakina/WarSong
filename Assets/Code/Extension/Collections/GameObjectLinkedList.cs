using System;
using System.Collections.Generic;


namespace Code.Extension.Collections
{
    public sealed class GameObjectLinkedList<T1> : LinkedList<LinkedListItem<T1>>
    {
        private readonly LinkedListItem<T1>[] _array;
        private int _position = -1;

        public GameObjectLinkedList(LinkedListItem<T1>[] items)
        {
            _array = items;
            _position = 0;
        }

        public LinkedListItem<T1> Current
        {
            get
            {
                if (_position == -1 || _position >= _array.Length)
                    throw new InvalidOperationException("fuck off");
                return _array[_position];
            }
        }

        public bool MoveNext()
        {
            if (_position < _array.Length - 1)
            {
                _position++;
                _array[_position - 1].Value.SetActive(false);
                _array[_position].Value.SetActive(true);
                return true;
            }

            return false;
        }

        public bool MovePrev()
        {
            if (_position > 0)
            {
                _position--;
                _array[_position + 1].Value.SetActive(false);
                _array[_position].Value.SetActive(true);
                return true;
            }

            return false;
        }
    }
}