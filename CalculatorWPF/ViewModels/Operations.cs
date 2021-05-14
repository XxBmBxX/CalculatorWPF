using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWPF
{
    public class Operations
    {
        public static void DigitClickedUpdate(object parameter)
        {
            if (CalculatorViewModel.DecimalClicked && !char.IsDigit(char.Parse(parameter as string)))
                return;
            if (CalculatorViewModel.CalculatorField == "0")
                CalculatorViewModel.DecimalClicked = false;
            if (CalculatorViewModel.CalculatorField == "0")
            {
                CalculatorViewModel.CalculatorField = parameter as string;
            }
            else
            {
                CalculatorViewModel.CalculatorField += parameter as string;
            }
            CalculatorViewModel.AllowOperations = true;
        }
        public static void MemoryClickedUpdate(object parameter)
        {
            switch (parameter as string)
            {
                case "M+":
                    CalculatorViewModel.Memory += decimal.Parse(CalculateString(CalculatorViewModel.CalculatorField));
                    break;
                case "M-":
                    CalculatorViewModel.Memory -= decimal.Parse(CalculateString(CalculatorViewModel.CalculatorField));
                    break;
                case "MR":
                    CalculatorViewModel.CalculatorField = CalculatorViewModel.Memory.ToString();
                    break;
                case "MC":
                    CalculatorViewModel.Memory = 0;
                    break;
                default:
                    break;
            }
        }
        public static void OperationClickedUpdate(object parameter)
        {
            switch (parameter as string)
            {
                case "=":
                    CalculatorViewModel.CalculatorField = CalculateString(CalculatorViewModel.CalculatorField);
                    CalculatorViewModel.DecimalClicked = true;
                    break;
                case "C":
                    CalculatorViewModel.CalculatorField = "0";
                    CalculatorViewModel.DecimalClicked = true;
                    CalculatorViewModel.AllowOperations = false;
                    break;
                case "sqrt":
                    if (decimal.Parse(CalculateString(CalculatorViewModel.CalculatorField)) < 0)
                    {
                        break;
                    }
                    else
                    {
                        CalculatorViewModel.CalculatorField = Math.Sqrt(Convert.ToDouble(CalculateString(CalculatorViewModel.CalculatorField))).ToString();
                        CalculatorViewModel.DecimalClicked = true;
                    }                    
                    break;
                case "x2":
                    CalculatorViewModel.CalculatorField = Math.Pow(Convert.ToDouble(CalculateString(CalculatorViewModel.CalculatorField)), 2).ToString();
                    break;
                case "backspace":
                    if (CalculatorViewModel.CalculatorField.Length > 1)
                    {
                        CalculatorViewModel.CalculatorField = CalculatorViewModel.CalculatorField.Remove(CalculatorViewModel.CalculatorField.Length - 1, 1);
                    }
                    else
                    {
                        CalculatorViewModel.CalculatorField = "0";
                    }
                    break;
                case "1/x":
                    if (CalculatorViewModel.CalculatorField.Length > 0)
                    {
                        CalculatorViewModel.CalculatorField = (1 / Convert.ToDouble(CalculateString(CalculatorViewModel.CalculatorField))).ToString();
                    }
                    break;
                default:
                    break;
            }

            if (!CalculatorViewModel.AllowOperations)
                return;
            switch (parameter as string)
            {
                case "-":
                    CalculatorViewModel.CalculatorField += "-";
                    CalculatorViewModel.DecimalClicked = false;
                    CalculatorViewModel.AllowOperations = false;
                    break;
                case "+":
                    CalculatorViewModel.CalculatorField += "+";
                    CalculatorViewModel.DecimalClicked = false;
                    CalculatorViewModel.AllowOperations = false;
                    break;
                case "×":
                    CalculatorViewModel.CalculatorField += "×";
                    CalculatorViewModel.DecimalClicked = false;
                    CalculatorViewModel.AllowOperations = false;
                    break;
                case "÷":
                    CalculatorViewModel.CalculatorField += "÷";
                    CalculatorViewModel.DecimalClicked = false;
                    CalculatorViewModel.AllowOperations = false;
                    break;
                case ".":
                    if (CalculatorViewModel.DecimalClicked || !char.IsDigit(CalculatorViewModel.CalculatorField[CalculatorViewModel.CalculatorField.Length - 1]) || !CalculatorViewModel.AllowOperations)
                    {
                        break;
                    }
                    else
                    {
                        CalculatorViewModel.CalculatorField += ".";
                        CalculatorViewModel.AllowOperations = false;
                        CalculatorViewModel.DecimalClicked = true;
                        break;
                    }                   
                default:
                    break;
            }
        }

        public static string CalculateString(string mathProblem)
        {
            if (mathProblem.Length == 1)
                return mathProblem;
            char[] delimiterChars = { '+', '-', '×', '÷' };
            string[] numbers = mathProblem.Split(delimiterChars);
            char[] operations = new string(mathProblem.Where(c => c < '0' || c > '9').ToArray()).ToCharArray();

            if (numbers.Length == 1)
                return numbers[0].ToString();
            decimal result = 0;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (result == 0)
                {
                    switch (operations[i])
                    {
                        case '+':
                            result = decimal.Parse(numbers[i]) + decimal.Parse(numbers[i + 1]);
                            break;
                        case '-':
                            result = decimal.Parse(numbers[i]) - decimal.Parse(numbers[i + 1]);
                            break;
                        case '×':
                            result = decimal.Parse(numbers[i]) * decimal.Parse(numbers[i + 1]);
                            break;
                        case '÷':
                            result = decimal.Parse(numbers[i]) / decimal.Parse(numbers[i + 1]);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (operations[i])
                    {
                        case '+':
                            result += decimal.Parse(numbers[i + 1]);
                            break;
                        case '-':
                            result -= decimal.Parse(numbers[i + 1]);
                            break;
                        case '×':
                            result *= decimal.Parse(numbers[i + 1]);
                            break;
                        case '÷':
                            result /= decimal.Parse(numbers[i + 1]);
                            break;
                        default:
                            break;
                    }
                }
            }
            return result.ToString();
        }
    }
}
