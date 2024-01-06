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

        public void DivisionToSelf (string Value)
        {
            double OriginalValue = Convert.ToDouble(Value);
            OriginalValue = 1 / OriginalValue;
            ActualMemory = $"{OriginalValue}";
        }

        public void GetFactorial (string Value)
        {
            double DoubleValue = Convert.ToDouble(Value);
            int IntVlaue = Convert.ToInt32(DoubleValue);
            double ResultValue = 1;
            for (int i = 1; i < IntVlaue + 1; i++)
            {
                ResultValue = ResultValue * i;
            }
            ActualMemory = $"{ResultValue}";
        }

        public void GetLn (string Value)
        {
            double ResultValue = Convert.ToDouble(Value);
            ResultValue = Math.Log(ResultValue);
            ActualMemory = $"{ResultValue}";
        }

        public void GetCosinus(string Value)
        {
            double ResultValue = Convert.ToDouble(Value);
            ResultValue = Math.Cos(ResultValue);
            ActualMemory = $"{ResultValue}";
        }

        public void GetSinus(string Value)
        {
            double ResultValue = Convert.ToDouble(Value);
            ResultValue = Math.Sin(ResultValue);
            ActualMemory = $"{ResultValue}";
        }

        public void GetArcTangens(string Value)
        {
            double ResultValue = Convert.ToDouble(Value);
            ResultValue = Math.Atan(ResultValue);
            ActualMemory = $"{ResultValue}";
        }

        public void GetTangens(string Value)
        {
            double ResultValue = Convert.ToDouble(Value);
            ResultValue = Math.Tan(ResultValue);
            ActualMemory = $"{ResultValue}";
        }

        public void PlusToMemory (string Value)
        {
            double CurrentValue;
            double NewValue = Convert.ToDouble(Value);
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

        public void MinusFromMemory(string Value)
        {
            double CurrentValue;
            double NewValue = Convert.ToDouble(Value);
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

        public void GetResult (string FirstValue, string SecondValue, string Operation)
        {
            if (SecondValue == "" || Operation == "" || SecondValue == "-")
            {
                ErrorInOperation = true;
                return;
            }
            if (SecondValue[SecondValue.Length - 1] == ',')
            {
                SecondValue += "0";
            }
            if (Convert.ToDouble(SecondValue) == 0 && Operation == "/")
            {
                MessageBox.Show("∞");
                return;
            }
            double DoubleResult = 0;
            double FirstDouble = Convert.ToDouble(FirstValue);
            double SecondDouble = Convert.ToDouble(SecondValue);
            if (Operation == "+")
            {
                DoubleResult = FirstDouble + SecondDouble;
            }
            else if (Operation == "-")
            {
                DoubleResult = FirstDouble - SecondDouble;
            }
            else if (Operation == "/")
            {
                DoubleResult = FirstDouble / SecondDouble;
            }
            else if (Operation == "*")
            {
                DoubleResult = FirstDouble * SecondDouble;
            }
            else if (Operation == "^")
            {
                DoubleResult = FirstDouble;
                int SecondInt = Convert.ToInt32(SecondDouble);
                for (int i = 1;  i < SecondInt; i++) 
                {
                    DoubleResult = DoubleResult * FirstDouble;
                }
            }
            else if (Operation == "Exp")
            {
                int SecondInt = Convert.ToInt32(SecondDouble);
                for (int i = 0; i < SecondInt; i++)
                {
                    FirstValue += "0";
                }
                DoubleResult = Convert.ToDouble(FirstValue);
            }
            else if (Operation == "log")
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
