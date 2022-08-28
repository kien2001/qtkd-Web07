using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFunc
{
    public static class CommonFunc
    {
        /// <summary>
        /// Xử lý lọc giá trị của các array thành duy nhất 1 giá trị
        /// </summary>
        /// <returns></returns>
        /// Created by LVKIEN 23/08/2022

        public static IEnumerable<string> FormatData(IEnumerable<string> array)
        {
            List<string> resultCareer = new();
            foreach (var item in array)
            {
                if (item.Contains('\n'))
                {
                    string[] splitArray = item.Trim().Split('\n');
                    foreach (var itemSplit in splitArray)
                    {
                        resultCareer.Add(itemSplit.Trim());
                    }
                }
                else
                {
                    resultCareer.Add(item.Trim());
                }
            }

            IEnumerable<string> resultArray = resultCareer.ToArray().Distinct();
            return resultArray;
        }
    }
}
