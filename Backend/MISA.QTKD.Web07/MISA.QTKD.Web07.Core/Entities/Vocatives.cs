using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Vocatives
    {
        #region Vocative

        public int Vocative { get; set; } // xưng hô(0- ông, 1- bà, 2- anh, 3-chị, 4- cô, 5- chú, 6- bác, 7- em)

        public string VocativeName
        {
            get
            {
                return Vocative switch
                {
                    0 => "Không chọn",
                    1 => "Ông",
                    2 => "Bà",
                    3 => "Anh",
                    4 => "Chị",
                    5 => "Cô",
                    6 => "Chú",
                    7 => "Bác",
                    8 => "Em",
                    _ => "",
                };
            }
            set { }
        }

        #endregion

        public Vocatives(int vocative, string vocativeName)
        {
            Vocative = vocative;
            VocativeName = vocativeName;
        }

        public Vocatives()
        {
        }
    }
}
