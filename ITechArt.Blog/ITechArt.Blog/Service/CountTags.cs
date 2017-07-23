using System;
using System.Linq;
using System.Collections.Generic;

using ITechArt.Blog.Models;


namespace ITechArt.Blog.Service
{
    class PartialComparer : IEqualityComparer<Tag>
    {
        public string GetComparablePart(Tag s)
        {
            return s.Name;
        }
        public bool Equals(Tag x, Tag y)
        {
            return GetComparablePart(x).Equals(GetComparablePart(y));
        }
        public int GetHashCode(Tag obj)
        {
            return GetComparablePart(obj).GetHashCode();
        }
    }
    public class CountTags
    {
        public static Dictionary<string, int> TagsNamesAndFontsSize(List<Tag> tagsList)
        {
            //Словарь для хранения уникальных имен тэгов и их частот.
            Dictionary<string, double> unicTagsDictAndFreq = new Dictionary<string, double>();
            //Словарь для хранения информации для вывода (имя тэга и размер шрифта).
            Dictionary<string, int> tagDict = new Dictionary<string, int>();
            //Список уникальных тэгов.
            List<Tag> unicTagsList = tagsList.Distinct(new PartialComparer()).ToList();
            //Подсчет частоты.
            foreach (var unicTag in unicTagsList)
            {
                int i = 0;
                foreach (var notUnicTag in tagsList)
                {
                    if (unicTag.Name == notUnicTag.Name)
                    {
                        i++;
                    }
                }
                unicTagsDictAndFreq.Add(unicTag.Name, Convert.ToDouble(i));
            }
            double max = Convert.ToDouble(unicTagsDictAndFreq.Values.Max());
            int fontSize = 0;
            double part = 0;
            //Расчет размера шрифта
            foreach (var st in unicTagsDictAndFreq)
            {
                part = ((st.Value / max) * 100);
                if (part >= 98)
                {
                    fontSize = 6;
                }

                else if (part >= 70)
                {
                    fontSize = 5;
                }

                else if (part >= 50)
                {
                    fontSize = 4;
                }

                else if (part >= 30)
                {
                    fontSize = 3;
                }

                else if (part >= 10)
                {
                    fontSize = 2;
                }

                else if (part < 10)
                {
                    fontSize = 1;
                }
                tagDict.Add(st.Key, fontSize);
            }
            return tagDict;
        }
    }
}