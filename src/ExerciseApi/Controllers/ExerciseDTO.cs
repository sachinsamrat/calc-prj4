﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseApi.Controllers
{
    public class ExerciseDTO
    {
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> Options { get; set; }
        public ExerciseDTO()
        {
            Options = new List<string>();
        }
    }
}
