using Calculator.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Linq.Expressions;
using System.Numerics;
using System.Windows.Controls;

namespace Calculator.Model
{
    public class MainModel : INotifyPropertyChanged
    {
        public bool OperationStatus { get; set; } = false;

        public static string FirstMemory { get; set; } = "0";

        public static string SecondMemory { get; set; } = "0";

        public static string FirstMemoryStatus { get; set; } = " *Активно*";

        public static string SecondMemoryStatus { get; set; } = "";

        public static string ActualMemory { get; set; } = "0";

        public static int MemoryMode { get; set; } = 0;

        public static bool ErrorInOperation = false;


        public string InputFirstOperator (string inputParameter)
        {
            if (ActualMemory == "0") 
            {
                ActualMemory = inputParameter;
                return inputParameter;
            }
            else
            {
                return ActualMemory += inputParameter;
            }
        }

        public void DivisionToSelf (string value)
        {
            double OriginalValue = Convert.ToDouble(value);
            OriginalValue = 1 / OriginalValue;
            ActualMemory = $"{OriginalValue}";
        }

        public void GetFactorial (string value)
        {
            double DoubleValue = Convert.ToDouble(value);
            int IntVlaue = Convert.ToInt32(DoubleValue);
            double ResultValue = 1;
            for (int i = 1; i < IntVlaue + 1; i++)
            {
                ResultValue = ResultValue * i;
            }
            ActualMemory = $"{ResultValue}";
        }

        public void GetLn (string value)
        {
            double ResultValue = Convert.ToDouble(value);
            ResultValue = Math.Log(ResultValue);
            ActualMemory = $"{ResultValue}";
        }

        public void GetCosinus(string value)
        {
            double ResultValue = Convert.ToDouble(value);
            ResultValue = Math.Cos(ResultValue);
            ActualMemory = $"{ResultValue}";
        }

        public void GetSinus(string value)
        {
            double ResultValue = Convert.ToDouble(value);
            ResultValue = Math.Sin(ResultValue);
            ActualMemory = $"{ResultValue}";
        }

        public void GetArcTangens(string value)
        {
            double ResultValue = Convert.ToDouble(value);
            ResultValue = Math.Atan(ResultValue);
            ActualMemory = $"{ResultValue}";
        }

        public void GetTangens(string value)
        {
            double ResultValue = Convert.ToDouble(value);
            ResultValue = Math.Tan(ResultValue);
            ActualMemory = $"{ResultValue}";
        }

        public void PlusToMemory (string value)
        {
            double CurrentValue;
            double NewValue = Convert.ToDouble(value);
            if (MemoryMode == 0)
            {
                CurrentValue = Convert.ToDouble(FirstMemory);
                NewValue = CurrentValue + NewValue;
                FirstMemory = $"{NewValue}";
            }
            else if (MemoryMode == 1)
            {
                CurrentValue = Convert.ToDouble(SecondMemory);
                NewValue = CurrentValue + NewValue;
                SecondMemory = $"{NewValue}";
            }
        }

        public void MinusFromMemory(string value)
        {
            double CurrentValue;
            double NewValue = Convert.ToDouble(value);
            if (MemoryMode == 0)
            {
                CurrentValue = Convert.ToDouble(FirstMemory);
                NewValue = CurrentValue - NewValue;
                FirstMemory = $"{NewValue}";
            }
            else if (MemoryMode == 1)
            {
                CurrentValue = Convert.ToDouble(SecondMemory);
                NewValue = CurrentValue - NewValue;
                SecondMemory = $"{NewValue}";
            }
        }

        public void GetResult (string firstValue, string secondValue, string operation)
        {
            if (secondValue == "" || operation == "" || secondValue == "-")
            {
                ErrorInOperation = true;
                return;
            }
            if (secondValue[secondValue.Length - 1] == ',')
            {
                secondValue += "0";
            }
            if (Convert.ToDouble(secondValue) == 0 && operation == "/")
            {
                MessageBox.Show("∞");
                return;
            }
            double DoubleResult = 0;
            double FirstDouble = Convert.ToDouble(firstValue);
            double SecondDouble = Convert.ToDouble(secondValue);
            if (operation == "+")
            {
                DoubleResult = FirstDouble + SecondDouble;
            }
            else if (operation == "-")
            {
                DoubleResult = FirstDouble - SecondDouble;
            }
            else if (operation == "/")
            {
                DoubleResult = FirstDouble / SecondDouble;
            }
            else if (operation == "*")
            {
                DoubleResult = FirstDouble * SecondDouble;
            }
            else if (operation == "^")
            {
                DoubleResult = FirstDouble;
                int SecondInt = Convert.ToInt32(SecondDouble);
                for (int i = 1;  i < SecondInt; i++) 
                {
                    DoubleResult = DoubleResult * FirstDouble;
                }
            }
            else if (operation == "Exp")
            {
                int SecondInt = Convert.ToInt32(SecondDouble);
                for (int i = 0; i < SecondInt; i++)
                {
                    firstValue += "0";
                }
                DoubleResult = Convert.ToDouble(firstValue);
            }
            else if (operation == "log")
            {
                if (SecondDouble == 0 || FirstDouble == 0)
                {
                    ErrorInOperation = true;
                    return;
                }
                DoubleResult = Math.Log(SecondDouble, FirstDouble);
            }
            ActualMemory = $"{DoubleResult}";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
