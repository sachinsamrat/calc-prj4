namespace Calculator;

public static class Calculator
{
    public static List<HistoryItem> historyItems = new List<HistoryItem>();
    public static double Calculate(double value1, double value2, string mathOperator)
    {
        double result = 0;
        switch (mathOperator)
        {
            case "÷":
                result = value1 / value2;
                break;
            case "×":
                result = value1 * value2;
                break;
            case "+":
                result = value1 + value2;
                break;
            case "-":
                result = value1 - value2;
                break;
        }

        string question = value1.ToString() + mathOperator + value2.ToString();
        historyItems.Add(new HistoryItem() { QuestionAnswer = question + " = " + result.ToString() });
        return result;
    }
}

public static class DoubleExtensions
{
    public static string ToTrimmedString(this double target, string decimalFormat)
    {
        string strValue = target.ToString(decimalFormat); //Get the stock string

        //If there is a decimal point present
        if (strValue.Contains("."))
        {
            //Remove all trailing zeros
            strValue = strValue.TrimEnd('0');

            //If all we are left with is a decimal point
            if (strValue.EndsWith(".")) //then remove it
                strValue = strValue.TrimEnd('.');
        }

        return strValue;
    }
}
