using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Combination
    {
        string comboName;
        int damage;
        public Combination()
        {

        }
        public Combination(int _damage,string _comboName)
        {
            damage = _damage;
            comboName = _comboName;
        }
        public string ComboName
        {
            get
            {
                return comboName;
            }
        }

        public int Damage
        {
            get
            {
                return damage;
            }
        }
        
    }
}
