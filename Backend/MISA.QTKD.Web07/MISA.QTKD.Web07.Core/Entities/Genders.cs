using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Genders
    {
        // Id giới tính
        public int Gender { get; set; }

        // giới tính: 1-nam, 2-nữ, 3- khác
        public string? GenderName
        {
            get
            {
                return Gender switch
                {
                    1 => "Nam",
                    2 => "Nữ",
                    3 => "Khác",
                    _ => ""
                };
            }
            set { }
        }

        public Genders(int gender, string? genderName)
        {
            Gender = gender;
            GenderName = genderName;
        }

        public Genders()
        {
        }
    }
}
