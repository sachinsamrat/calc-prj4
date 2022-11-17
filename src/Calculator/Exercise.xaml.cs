

using Calculator.Exercise;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
namespace Calculator;

public partial class Exercise1 : ContentPage
{
    List<ExerciseDTO> _exercises;
    int counter = 0;
    int correctAnswers = 0;
    public Exercise1()
    {
        InitializeComponent();
        FetchContent();
        LoadQuestion();
    }

    private void FetchContent()
    {
        var apiUrl = "https://localhost:7115/Exercises";
        correctAnswers = 0;
        _exercises = GetExercises(apiUrl);
    }

    private void LoadQuestion()
    {
        if (counter >= _exercises.Count) {
            Complete();
            return;
        }
        if(counter < 0)
        {
            counter = 0;
        }
        var item = _exercises[counter];

        Question.Text = "Q" + (counter+1) + " : " + item.Question;
        IsCorrect.Text = "";
        Val1.Text = item.Options[0];
        Val2.Text = item.Options[1];
        Val3.Text = item.Options[2];
        //if (counter == 0)
        //{
        //    Previous.IsEnabled = false;
        //}
        //else
        //{
        //    Previous.IsEnabled = true;
        //}
        //if (counter >= _exercises.Count-1)
        //{
        //    Next.IsEnabled = false;
        //}
        //else
        //{
        //    Next.IsEnabled = true;
        //}
    }

    private void Complete()
    {
        Question.Text = "That's all Folks!!";
        IsCorrect.Text = correctAnswers + "/" + _exercises.Count;
        Val1.IsVisible = false;
        Val2.IsVisible = false;
        Val3.IsVisible = false;
        IsCorrect.IsVisible = true;
        NewQuiz.IsVisible = true;
    }

    private List<ExerciseDTO> GetExercises(string apiUrl)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(apiUrl);

        // Add an Accept header for JSON format.
        client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

        // List data response.
        HttpResponseMessage response = client.GetAsync(apiUrl).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
        if (response.IsSuccessStatusCode)
        {
            // Parse the response body.
            var dataObjects = response.Content.ReadAsAsync<IEnumerable<ExerciseDTO>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            return dataObjects.ToList();
        }
        else
        {
            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        }

        // Make any other calls using HttpClient here.

        // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
        client.Dispose();

        return null;
    }


    private void Val1Clicked(object sender, EventArgs e)
    {
        CheckVal(0);
    }

    private void Val3Clicked(object sender, EventArgs e)
    {
        CheckVal(2);
    }

    private void CheckVal(int v)
    {
        if (_exercises[counter].Options[v] == _exercises[counter].CorrectAnswer)
        {
            IsCorrect.Text = "That is correct :)";
            ++correctAnswers;
            Dispatcher.DispatchDelayed(new TimeSpan(0,0,1),()=>{ ++counter; LoadQuestion(); });
        }
        else
        {
            IsCorrect.Text = "Incorrect answer :(";
            TryAgain.IsVisible = true;
            SkipToNext.IsVisible = true;
            Val1.IsEnabled = false;
            Val2.IsEnabled = false;
            Val3.IsEnabled = false;
        }
    }

    private void Val2Clicked(object sender, EventArgs e)
    {
        CheckVal(1);
    }

    //private void Previous_Clicked(object sender, EventArgs e)
    //{
    //    --counter;
    //    LoadQuestion();
    //}

    private void TryAgain_Clicked(object sender, EventArgs e)
    {
        IsCorrect.Text = "";
        Val1.IsEnabled = true;
        Val2.IsEnabled = true;
        Val3.IsEnabled = true;
        TryAgain.IsVisible = false;
        SkipToNext.IsVisible = false;
    }

    private void SkipToNext_Clicked(object sender, EventArgs e)
    {
        Val1.IsEnabled = true;
        Val2.IsEnabled = true;
        Val3.IsEnabled = true;
        TryAgain.IsVisible = false;
        SkipToNext.IsVisible = false;
        ++counter;
        LoadQuestion();
    }

    private void NewQuiz_Clicked(object sender, EventArgs e)
    {
        Question.Text = "";
        Val1.IsVisible = true;
        Val2.IsVisible = true;
        Val3.IsVisible = true;
        IsCorrect.IsVisible = true;
        NewQuiz.IsVisible = false;
        counter = 0;
        FetchContent();
        LoadQuestion();
    }
}
