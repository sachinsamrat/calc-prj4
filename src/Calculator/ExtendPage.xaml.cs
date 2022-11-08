using System.Data;

namespace Calculator;

public partial class ExtendPage : ContentPage
{
    public ExtendPage()
    {
        InitializeComponent();
    }

    string currentEntry = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    string decimalFormat = "N0";



    void OnSelectNumber(object sender, EventArgs e)
    {

        Button button = (Button)sender;
        string pressed = button.Text;
        if (pressed == "mod")
        {
            currentEntry += "%";
        }
        else if (pressed == "×")
        {
            currentEntry += "*";
        }
        else if (pressed == "÷")
        {
            currentEntry += "/";
        }
        else if (pressed == "%")
        {
            currentEntry += "*0.01";
        }
        else
        {
            currentEntry += pressed;
        }

        resultText.Text = currentEntry;
    }


    void OnSelectOperator(object sender, EventArgs e)
    {
        LockNumberValue(resultText.Text);

        currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        if (pressed == "mod")
        {
            mathOperator = "%";
        }
        else
        {
            mathOperator = pressed;
        }
    }

    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }

            currentEntry = string.Empty;
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentState = 1;
        decimalFormat = "N0";
        this.resultText.Text = "0";
        currentEntry = string.Empty;
    }

    void OnCalculate(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        var v = dt.Compute(currentEntry, "");
        this.resultText.Text = v.ToString();
        currentState = -1;
        currentEntry = v.ToString();
    }

    void OnNegative(object sender, EventArgs e)
    {
        var v = float.Parse(currentEntry) * -1;
        this.resultText.Text = v.ToString();
        currentState = -1;
        currentEntry = this.resultText.Text;
    }

    void OnPercentage(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            LockNumberValue(resultText.Text);
            decimalFormat = "N2";
            secondNumber = 0.01;
            mathOperator = "�";
            currentState = 2;
            OnCalculate(this, null);
        }
    }

    void OnRoot(object sender, EventArgs e)
    {
        var sq = Math.Sqrt(int.Parse(currentEntry));
        this.resultText.Text = sq.ToString();
        currentState = -1;
        currentEntry = sq.ToString();


    }
}