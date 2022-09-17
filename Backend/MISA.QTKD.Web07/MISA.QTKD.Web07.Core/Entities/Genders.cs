using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Object về giới tính
    /// </summary>
    /// Created by LVKIEN 13/09/2022
    public class Genders
    {
        #region Gender

        // Id giới tính
        public Enums.Gender? Gender { get; set; }

        // giới tính: 1-nam, 2-nữ, 3- khác
        public string? GenderName
        {
            get
            {
                return Gender switch
                {
                    Enums.Gender.Male => "Nam",
                    Enums.Gender.Female => "Nữ",
                    Enums.Gender.Other => "Khác",
                    _ => ""
                };
            }
            set { }
        }

        #endregion

        public Genders(Enums.Gender? gender, string? genderName)
        {
            Gender = gender;
            GenderName = genderName;
        }

        public Genders()
        {
        }
    }
}
