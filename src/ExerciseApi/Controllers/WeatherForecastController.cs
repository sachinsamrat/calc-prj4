using Microsoft.AspNetCore.Mvc;

namespace ExerciseApi.Controllers
{
    [ApiController]
    [Route("Weather")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetExercises")]
        public List<ExerciseDTO> GetExerciseItems()
        {
            return GenerateExerciseItems();
        }

        private List<ExerciseDTO> GenerateExerciseItems()
        {
            List<ExerciseDTO> items = new List<ExerciseDTO>();
            for (var counter = 0; counter < 10; counter++)
            {
                var item = new ExerciseDTO();
                Random rnd = new Random();
                int operator1 = rnd.Next(0, 4);
                int val1 = rnd.Next(0, 100);
                int val2 = rnd.Next(0, 100);

                int correctAnswerPosition = rnd.Next(0, 3);
                switch (operator1)
                {
                    case 0:
                        item.Question = val1 + "+" + val2 + "=?";
                        item.CorrectAnswer = (val1 + val2).ToString();
                        break;
                    case 1:
                        if (val2 > val1)
                        {
                            var temp = val2;
                            val2 = val1;
                            val1 = temp;
                        }
                        item.Question = val1 + "-" + val2 + "=?";
                        item.CorrectAnswer = (val1 - val2).ToString();
                        break;
                    case 2:
                        item.Question = val1 + "*" + val2 + "=?";
                        item.CorrectAnswer = (val1 * val2).ToString();
                        break;
                    case 3:
                    default:
                        val2 = ((val2 == 0) ? 1 : val2);
                        if(val2 > val1)
                        {
                            var temp = val2;
                            val2 = val1;
                            val1 = temp;
                        }
                        item.Question = val1 + "/" + ((val2 == 0) ? 1 : val2) + "=?";
                        item.CorrectAnswer = (val1 / val2).ToString();
                        break;
                }
                if (correctAnswerPosition <= 0)
                {
                    item.Options.Add(item.CorrectAnswer);
                    item.Options.Add((Convert.ToInt32(item.CorrectAnswer) + rnd.Next(1, 25)).ToString());
                    item.Options.Add((Convert.ToInt32(item.CorrectAnswer) + rnd.Next(1, 25)).ToString());
                }
                else if (correctAnswerPosition == 1)
                {
                    item.Options.Add((Convert.ToInt32(item.CorrectAnswer) + rnd.Next(1, 25)).ToString());
                    item.Options.Add(item.CorrectAnswer);
                    item.Options.Add((Convert.ToInt32(item.CorrectAnswer) + rnd.Next(1, 25)).ToString());
                }
                else if (correctAnswerPosition >= 2)
                {
                    item.Options.Add((Convert.ToInt32(item.CorrectAnswer) + rnd.Next(1, 25)).ToString());
                    item.Options.Add((Convert.ToInt32(item.CorrectAnswer) + rnd.Next(1, 25)).ToString());
                    item.Options.Add(item.CorrectAnswer);
                }
                items.Add(item);
            }
            return items;
        }
    }
}