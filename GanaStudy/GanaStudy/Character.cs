﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanaStudy
{
    public class Character
    {
        public string Romaji;
        public string Gana;
        public DateTime TestTime;
        public int correct;
        public Character()
        {

        }
        public Character(string _Gana,string _Romaji)
        {
            Romaji = _Romaji;
            Gana = _Gana;
            correct = 0;
            TestTime = DateTime.Now;
        }
    }
}
