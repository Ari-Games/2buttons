using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts
{
    class InputBuffer
    {
        int size;
        StringBuilder buffer;
        float lasttime;
        public float LastTime
        {
            get
            {
                return lasttime;
            }
        }
        public InputBuffer(int size)
        {
            this.size = size;
            buffer = new StringBuilder(size+1);
            lasttime = Time.time;
        }
        public void AddBuffer(char input)
        {
            if (input != 'A' && input != 'D')
            {
                return;
            }
            buffer.Append(input);
            if(buffer.Length > size)
            {
                Debug.Log("Rem");
                buffer.Remove(0, 1);// remove the first elem
            }
            lasttime = Time.time;
        }
        public void Clear()
        {
            Debug.Log("Rem");
            buffer.Clear();
        }
        public Combination CheckBuffer(List<Combination> combinations)
        {
            string buffer = this.buffer.ToString();
            foreach (var item in combinations)
            {
                if (buffer.Contains(item.ComboName))
                {
                    this.buffer.Clear();
                    return item;
                }
                
            }
            return null;
        }
    }
}
