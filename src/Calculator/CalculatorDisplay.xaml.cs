namespace Project4.Views;
using Calculator;
using Microsoft.Maui.Controls;
using Project4.ViewModel;
using Microsoft.Toolkit.Mvvm.Input;
public partial class CalculatorDisplay : ContentPage
{
	
    string currentEntry = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    string decimalFormat = "N0";
    bool paranthesis = false;
    Stack<String> p = new Stack<String>();
    
    public CalculatorDisplay(CalculatorViewModel calculatorViewModel)
    {
        InitializeComponent();
        this.BindingContext = calculatorViewModel;
    }
    

        void OnSelectNumber(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            string pressed = button.Text;

            currentEntry += pressed;

            if ((this.resultText.Text == "0" && pressed == "0")
                || (currentEntry.Length <= 1 && pressed != "0")
                || currentState < 0)
            {
                this.resultText.Text = "";
                if (currentState < 0)
                    currentState *= -1;
            }

            if (pressed == "." && decimalFormat != "N2")
            {
                decimalFormat = "N2";
            }

            this.resultText.Text += pressed;
        }

    void OnSelectOperator(object sender, EventArgs e)
    {
        if (paranthesis)
        {
            char[] x = { 'X', '÷', '-', '+' };
            Button button = (Button)sender;
            string temp = this.resultText.Text;
            char lastCharacter = temp[temp.Length - 1];
            if (!x.Contains(lastCharacter))
            {
                currentEntry += button.Text;
                this.resultText.Text = currentEntry;
            }
        }
        else
        {
            LockNumberValue(resultText.Text);

            currentState = -2;
            Button button = (Button)sender;
            string pressed = button.Text;
            mathOperator = pressed;
        }
    }
    void OnSquareRoot(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            LockNumberValue(resultText.Text);
            decimalFormat = "N2";
            OnCalculate(this, null);
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
        buttonAction(true, 1.0);
    }

    void OnCalculate(object sender, EventArgs e)
    {
        if (paranthesis)
        {
            double result = Calculator.evaluate(this.resultText.Text);
            this.CurrentCalculation.Text = this.resultText.Text + "";
            this.resultText.Text = result + "";
            paranthesis = false;
            this.cc1.Text = this.CurrentCalculation.Text;
            this.cc2.Text = this.resultText.Text;
            buttonAction(true, 1.0);
            p.Clear();
        }
        else
        {
            if (currentState == 2)
            {
                if (secondNumber == 0)
                    LockNumberValue(resultText.Text);

                double result = Calculator.Calculate(firstNumber, secondNumber, mathOperator);

                this.CurrentCalculation.Text = $"{firstNumber} {mathOperator} {secondNumber}";

                this.resultText.Text = result.ToTrimmedString(decimalFormat);
                this.cc1.Text = this.CurrentCalculation.Text;
                this.cc2.Text = this.resultText.Text;
                firstNumber = result;
                secondNumber = 0;
                currentState = -1;
                currentEntry = string.Empty;
               
            }
            else
            {
                double result = Math.Sqrt(firstNumber);
                this.CurrentCalculation.Text = "√" + firstNumber;
                this.resultText.Text = result.ToTrimmedString(decimalFormat);
                //this.cc1.Text = this.CurrentCalculation.Text;
                //this.cc2.Text = this.resultText.Text;
            }
        }
    }

    void OnNegative(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            secondNumber = -1;
            mathOperator = "×";
            currentState = 2;
            OnCalculate(this, null);
        }
    }

    void OnPercentage(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            LockNumberValue(resultText.Text);
            decimalFormat = "N2";
            secondNumber = 0.01;
            mathOperator = "×";
            currentState = 2;
            OnCalculate(this, null);
        }
    }

    void onParanthesis(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;
        if (pressed == "(")
        {
            string text = this.resultText.Text;
            if (text.Length > 2)
            {
                if ((text[text.Length - 1] == ')') || "0123456789".Contains(text[text.Length - 1]))
                {
                    currentEntry += "";
                }
                else
                {
                    p.Push("(");
                    paranthesis = true;
                    buttonAction(false, 0.3);
                    currentEntry += pressed;
                }
            }
            else
            {
                p.Push("(");
                paranthesis = true;
                buttonAction(false, 0.3);
                currentEntry += pressed;
            }

        }
        else
        {
            if (p.Count > 0)
            {
                p.Pop();
                currentEntry += pressed;
            }
            else
            {
                this.resultText.Text = "";
            }
        }
        this.resultText.Text = currentEntry;
    }
    void buttonAction(bool enable, double opacity)
    {
        modoperator.IsEnabled = enable;
        modoperator.Opacity = opacity;
        sroot.IsEnabled = enable;
        sroot.Opacity = opacity;
        percent.IsEnabled = enable;
        percent.Opacity = opacity;
        dot.IsEnabled = enable;
        dot.Opacity = opacity;
        addsubtract.IsEnabled = enable;
        addsubtract.Opacity = opacity;
    }
}