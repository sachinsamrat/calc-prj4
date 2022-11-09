using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Exercise
{
    public class ExerciseDTO
    {
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> Options { get; set; }
    }
}
