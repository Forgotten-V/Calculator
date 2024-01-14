using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Calculator.View;
using Calculator.Model;

namespace Calculator.ViewModel
{
    public class CalculatorLogic : INotifyPropertyChanged
    {

        MainModel Model = new MainModel();

        

        public void InputValueNumber (string value)
        {
            if (MainActionArea == "")
            {
                FirstInputArea = Model.InputFirstOperator(value);
            }
            else
            {
                if (SecondInputArea == "0" && value == "0")
                {
                    SecondInputArea = "0";
                }
                else if (SecondInputArea == "0")
                {
                    SecondInputArea = value;
                }
                else
                {
                    SecondInputArea += value;
                }
            }
        }

        public void ChangeOperationMode (string operation)
        {
            char LastChar = FirstInputArea[FirstInputArea.Length - 1];
            if (LastChar == ',')
            {
                InputValueNumber("0");
            }
            if (MainActionArea == "")
            {
                MainActionArea = operation;
                SecondInputArea = "0";
            }
            else if (MainActionArea != "")
            {
                VGetResult();
                if (MainModel.ErrorInOperation == true)
                {
                    return;
                }
                MainActionArea = operation;
                SecondInputArea = "0";
            }
        }

        public void ClearSidesFields ()
        {
            SecondInputArea = "";
            MainActionArea = "";
            FirstInputArea = MainModel.ActualMemory;
        }

        public string RemoveChars (string text, char[] chars)
        {
            foreach (char c in chars)
            {
                text = text.Replace(c.ToString(), "");
            }
            return text;
        }

        private string _FirstInputArea = "0";
        public string FirstInputArea
        {
            get { return _FirstInputArea; }
            set
            {
                _FirstInputArea = value;
                OnPropertyChanged(nameof(FirstInputArea));
            }
        }

        private string _SecondInputArea = "";
        public string SecondInputArea
        {
            get { return _SecondInputArea; }
            set
            {
                _SecondInputArea = value;
                OnPropertyChanged(nameof(SecondInputArea));
            }
        }

        private string _MainActionArea = "";
        public string MainActionArea
        {
            get { return _MainActionArea; }
            set
            {
                _MainActionArea = value;
                OnPropertyChanged(nameof(MainActionArea));
            }
        }

        private string _FirstMemoryCell = MainModel.FirstMemory + MainModel.FirstMemoryStatus;
        public string FirstMemoryCell
        {
            get { return _FirstMemoryCell; }
            set
            {
                _FirstMemoryCell = value;
                OnPropertyChanged(nameof(FirstMemoryCell));
            }
        }

        private string _SecondMemoryCell = MainModel.SecondMemory + MainModel.SecondMemoryStatus;
        public string SecondMemoryCell
        {
            get { return _SecondMemoryCell; }
            set
            {
                _SecondMemoryCell = value;
                OnPropertyChanged(nameof(SecondMemoryCell));
            }
        }

        public ICommand OpenProfessionalCalculatorPage
        {
            get { return new NavigateRelayCommand(VProfessionalCalculatorPage); }
        }
        private void VProfessionalCalculatorPage()
        {
            var OpenProfessionalCalculator = new FrProfessionalCalculator();
            App.Current.MainWindow.Content = OpenProfessionalCalculator;
            MainModel.ActualMemory = "0";
        }

        public ICommand OpenCasualCalculatorPage
        {
            get { return new NavigateRelayCommand(VOpenCasualCalculatorPage); }
        }
        private void VOpenCasualCalculatorPage()
        {
            var OpenCasualCalculator = new FrCasualCalculator();
            App.Current.MainWindow.Content = OpenCasualCalculator;
            MainModel.ActualMemory = "0";
        }

        public ICommand PlusToMemory
        {
            get { return new NavigateRelayCommand(VPlusToMemory); }
        }
        private void VPlusToMemory()
        {
            Model.PlusToMemory(FirstInputArea);
            FirstMemoryCell = MainModel.FirstMemory + MainModel.FirstMemoryStatus;
            SecondMemoryCell = MainModel.SecondMemory + MainModel.SecondMemoryStatus;
        }

        public ICommand MinusFromMemory
        {
            get { return new NavigateRelayCommand(VMinusFromMemory); }
        }
        private void VMinusFromMemory()
        {
            Model.MinusFromMemory(FirstInputArea);
            FirstMemoryCell = MainModel.FirstMemory + MainModel.FirstMemoryStatus;
            SecondMemoryCell = MainModel.SecondMemory + MainModel.SecondMemoryStatus;
        }

        public ICommand TakeFromMemory
        {
            get { return new NavigateRelayCommand(VTakeFromMemory); }
        }
        private void VTakeFromMemory()
        {
            if (MainActionArea == "")
            {
                if (MainModel.MemoryMode == 0)
                {
                    MainModel.ActualMemory = MainModel.FirstMemory;
                    FirstInputArea = MainModel.FirstMemory;
                }
                else if (MainModel.MemoryMode == 1)
                {
                    MainModel.ActualMemory = MainModel.SecondMemory;
                    FirstInputArea = MainModel.SecondMemory;
                }
            }
            else
            {
                if (MainModel.MemoryMode == 0)
                {
                    SecondInputArea = MainModel.FirstMemory;
                }
                else if (MainModel.MemoryMode == 1)
                {
                    SecondInputArea = MainModel.SecondMemory;
                }
            }
        }

        public ICommand ClearMemory
        {
            get { return new NavigateRelayCommand(VClearMemory); }
        }
        private void VClearMemory()
        {
            if (MainModel.MemoryMode == 0)
            {
                MainModel.FirstMemory = "0";
                FirstMemoryCell = MainModel.FirstMemory + MainModel.FirstMemoryStatus;
            }
            else if (MainModel.MemoryMode == 1)
            {
                MainModel.SecondMemory = "0";
                SecondMemoryCell = MainModel.SecondMemory + MainModel.SecondMemoryStatus;
            }
        }

        public ICommand ClearActualArea
        {
            get { return new NavigateRelayCommand(VClearActualArea); }
        }
        private void VClearActualArea()
        {
            if (SecondInputArea != "")
            {
                SecondInputArea = "";
                MainActionArea = "";
            }
            else 
            {
                VClearAll();
            }
        }

        public ICommand ClearAll
        {
            get { return new NavigateRelayCommand(VClearAll); }
        }
        public void VClearAll()
        {
            MainModel.ActualMemory = "0";
            ClearSidesFields();
        }

        public ICommand DeleteLastChar
        {
            get { return new NavigateRelayCommand(VDeleteLastChar); }
        }
        public void VDeleteLastChar()
        {
            if (SecondInputArea.Length > 0)
            {
                if (SecondInputArea.Length == 1)
                {
                    SecondInputArea = "0";
                }
                else
                {
                    SecondInputArea = SecondInputArea.Remove(SecondInputArea.Length - 1, 1);
                }
            }
            else if (FirstInputArea.Length == 1 && SecondInputArea == "")
            {
                MainModel.ActualMemory = "0";
                FirstInputArea = "0";
            }
            else if (FirstInputArea != "0" && SecondInputArea == "")
            {
                MainModel.ActualMemory = MainModel.ActualMemory.Remove(MainModel.ActualMemory.Length-1, 1);
                FirstInputArea = FirstInputArea.Remove(FirstInputArea.Length-1, 1);
            }
        }

        public ICommand ChangeOperator
        {
            get { return new NavigateRelayCommand(VChangeOperator); }
        }
        private void VChangeOperator()
        {
            if (SecondInputArea != "")
            {
                SecondInputArea = $"{Convert.ToDouble(SecondInputArea) * (-1)}";
            }
            else if (Convert.ToDouble(FirstInputArea) != 0)
            {
                MainModel.ActualMemory = $"{Convert.ToDouble(FirstInputArea) * (-1)}";
                FirstInputArea = MainModel.ActualMemory;
            }
        }

        public ICommand ChoiceFirstMemory
        {
            get { return new NavigateRelayCommand(VChoiceFirstMemory); }
        }
        private void VChoiceFirstMemory()
        {
            MainModel.FirstMemoryStatus = " *Активно*";
            MainModel.SecondMemoryStatus = "";
            FirstMemoryCell = MainModel.FirstMemory + MainModel.FirstMemoryStatus;
            SecondMemoryCell = MainModel.SecondMemory + MainModel.SecondMemoryStatus;
            MainModel.MemoryMode = 0;
        }

        public ICommand ChoiceSecondMemory
        {
            get { return new NavigateRelayCommand(VChoiceSecondMemory); }
        }
        private void VChoiceSecondMemory()
        {
            MainModel.FirstMemoryStatus = "";
            MainModel.SecondMemoryStatus = " *Активно*";
            FirstMemoryCell = MainModel.FirstMemory + MainModel.FirstMemoryStatus;
            SecondMemoryCell = MainModel.SecondMemory + MainModel.SecondMemoryStatus;
            MainModel.MemoryMode = 1;
        }

        public ICommand AddZero
        {
            get { return new NavigateRelayCommand(VAddZero); }
        }
        private void VAddZero()
        {
            InputValueNumber("0");
        }

        public ICommand AddOne
        {
            get { return new NavigateRelayCommand(VAddOne); }
        }
        private void VAddOne()
        {
            InputValueNumber("1");
        }

        public ICommand AddTwo
        {
            get { return new NavigateRelayCommand(VAddTwo); }
        }
        private void VAddTwo()
        {
            InputValueNumber("2");
        }

        public ICommand AddThree
        {
            get { return new NavigateRelayCommand(VAddThree); }
        }
        private void VAddThree()
        {
            InputValueNumber("3");
        }

        public ICommand AddFour
        {
            get { return new NavigateRelayCommand(VAddFour); }
        }
        private void VAddFour()
        {
            InputValueNumber("4");
        }

        public ICommand AddFive
        {
            get { return new NavigateRelayCommand(VAddFive); }
        }
        private void VAddFive()
        {
            InputValueNumber("5");
        }

        public ICommand AddSix
        {
            get { return new NavigateRelayCommand(VAddSix); }
        }
        private void VAddSix()
        {
            InputValueNumber("6");
        }

        public ICommand AddSeven
        {
            get { return new NavigateRelayCommand(VAddSeven); }
        }
        private void VAddSeven()
        {
            InputValueNumber("7");
        }

        public ICommand AddEight
        {
            get { return new NavigateRelayCommand(VAddEight); }
        }
        private void VAddEight()
        {
            InputValueNumber("8");
        }

        public ICommand AddNine
        {
            get { return new NavigateRelayCommand(VAddNine); }
        }
        private void VAddNine()
        {
            InputValueNumber("9");
        }

        public ICommand AddPoint
        {
            get { return new NavigateRelayCommand(VAddPoint); }
        }
        private void VAddPoint()
        {
            char[] Points = { ',' };
            if (SecondInputArea.Length > 0)
            {
                char LastChar = SecondInputArea[SecondInputArea.Length - 1];
                if (LastChar == ',')
                {
                    SecondInputArea = SecondInputArea.Remove(SecondInputArea.Length - 1, 1);
                }
                else
                {
                    SecondInputArea = RemoveChars(SecondInputArea, Points);
                    SecondInputArea += ",";
                }
            }
            else
            {
                char LastChar = FirstInputArea[FirstInputArea.Length - 1];
                if (LastChar == ',')
                {
                    FirstInputArea = FirstInputArea.Remove(FirstInputArea.Length - 1, 1);
                    MainModel.ActualMemory = MainModel.ActualMemory.Remove(MainModel.ActualMemory.Length - 1, 1);
                }
                else
                {
                    FirstInputArea = RemoveChars(FirstInputArea, Points);
                    MainModel.ActualMemory = RemoveChars(MainModel.ActualMemory, Points);
                    FirstInputArea += ",";
                    MainModel.ActualMemory += ",";
                }
            }
        }

        public ICommand DivisionToSelf
        {
            get { return new NavigateRelayCommand(VDivisionToSelf); }
        }
        private void VDivisionToSelf()
        {
            if (Convert.ToDouble(FirstInputArea) == 0)
            {
                MessageBox.Show("∞");
            }
            else
            {
                Model.DivisionToSelf(FirstInputArea);
                FirstInputArea = MainModel.ActualMemory;
            }
            ClearSidesFields ();
        }

        public ICommand GetLn
        {
            get { return new NavigateRelayCommand(VGetLn); }
        }
        private void VGetLn()
        {
            Model.GetLn(FirstInputArea);
            FirstInputArea = MainModel.ActualMemory;
            ClearSidesFields();
        }

        public ICommand GetFactorial
        {
            get { return new NavigateRelayCommand(VGetFactorial); }
        }
        private void VGetFactorial()
        {
            if (Convert.ToDouble(FirstInputArea) == 0)
            {
                InputValueNumber("1");
            }
            else
            {
                Model.GetFactorial(FirstInputArea);
                FirstInputArea = MainModel.ActualMemory;
            }
            ClearSidesFields();
        }

        public ICommand GetTangens
        {
            get { return new NavigateRelayCommand(VGetTangens); }
        }
        private void VGetTangens()
        {
            Model.GetTangens(FirstInputArea);
            FirstInputArea = MainModel.ActualMemory;
            ClearSidesFields();
            MessageBox.Show("Результат в радианах");
        }

        public ICommand GetArcTangens
        {
            get { return new NavigateRelayCommand(VGetArcTangens); }
        }
        private void VGetArcTangens()
        {
            Model.GetArcTangens(FirstInputArea);
            FirstInputArea = MainModel.ActualMemory;
            ClearSidesFields();
            MessageBox.Show("Результат в радианах");
        }

        public ICommand GetSinus
        {
            get { return new NavigateRelayCommand(VGetSinus); }
        }
        private void VGetSinus()
        {
            Model.GetSinus(FirstInputArea);
            FirstInputArea = MainModel.ActualMemory;
            ClearSidesFields();
            MessageBox.Show("Результат в радианах");
        }

        public ICommand GetCosinus
        {
            get { return new NavigateRelayCommand(VGetCosinus); }
        }
        private void VGetCosinus()
        {
            Model.GetCosinus(FirstInputArea);
            FirstInputArea = MainModel.ActualMemory;
            ClearSidesFields();
            MessageBox.Show("Результат в радианах");
        }
        public ICommand AddQuad
        {
            get { return new NavigateRelayCommand(VAddQuad); }
        }
        private void VAddQuad()
        {
            ChangeOperationMode("^");
        }

        public ICommand AddExp
        {
            get { return new NavigateRelayCommand(VAddExp); }
        }
        private void VAddExp()
        {
            ChangeOperationMode("Exp");
        }

        public ICommand AddPlus
        {
            get { return new NavigateRelayCommand(VAddPlus); }
        }
        private void VAddPlus()
        {
            ChangeOperationMode("+");
        }

        public ICommand AddLog
        {
            get { return new NavigateRelayCommand(VAddLog); }
        }
        private void VAddLog()
        {
            ChangeOperationMode("log");
        }

        public ICommand AddMinus
        {
            get { return new NavigateRelayCommand(VAddMinus); }
        }
        private void VAddMinus()
        {
            ChangeOperationMode("-");
        }

        public ICommand AddMultiply
        {
            get { return new NavigateRelayCommand(VAddMultiply); }
        }
        private void VAddMultiply()
        {
            ChangeOperationMode("*");
        }

        public ICommand AddDivide
        {
            get { return new NavigateRelayCommand(VAddDivide); }
        }
        private void VAddDivide()
        {
            ChangeOperationMode("/");
        }

        public ICommand GetResult
        {
            get { return new NavigateRelayCommand(VGetResult); }
        }
        public void VGetResult()
        {
            Model.GetResult(FirstInputArea, SecondInputArea, MainActionArea);
            if (MainModel.ErrorInOperation == true)
            {
                MessageBox.Show("Завершите выражение для его выполнения");
                MainModel.ErrorInOperation = false;
            }
            else
            {
                ClearSidesFields();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
