using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVMCalculator.ViewModels
{
    public class ViewModelCalculator : ViewModelBase
    {
        #region Propiedades
        int currentState = 1;

        string mathOperator;
        public string MathOperator
        {
            get { return mathOperator; }
            set
            {
                if (mathOperator != value)
                {
                    mathOperator = value;
                    OnPropertyChanged();
                }
            }
        }

        double firstNumber;
        public double FirstNumber
        {
            get { return firstNumber; }
            set
            {
                if (firstNumber != value)
                {
                    firstNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        double secondNumber;
        public double SecondNumber
        {
            get { return secondNumber; }
            set
            {
                if (secondNumber != value)
                {
                    secondNumber = value;
                    OnPropertyChanged();
                }
            }
        }


        string result;
		public string Result
		{
			get { return result; }
			set
			{
				if (result != value)
				{
					result = value;
					OnPropertyChanged();
				}
			}
		}

        #endregion
        public ICommand OnSelectNumber { protected set; get; }
        public ICommand OnClear { protected set; get; }
        public ICommand OnSelectOperator { protected set; get; }
        public ICommand OnCalculate { protected set; get; }


        public ViewModelCalculator()
        {
            OnClear = new Command(() =>
            {
                firstNumber = 0;
                secondNumber = 0;
                currentState = 1;
                this.Result = "0";
                this.MathOperator = "";
            });

            OnCalculate = new Command(() =>
            {
                if (currentState == 2)
                {
                    var result = SimpleCalculator.Calculate(firstNumber, secondNumber, mathOperator);

                    Result = result.ToString();
                    firstNumber = result;
                    currentState = -1;
                }
            });


            OnSelectOperator = new Command<string>(
            
                execute: (String parameter) =>
                {
                    currentState = -2;
                    string pressed = parameter;
                    MathOperator = pressed;
                }
                
            );


            OnSelectNumber = new Command<string>(
               execute: (string parameter) =>
               {
				   
				   string pressed = parameter;

				   if (Result == "0" || currentState < 0)
				   {
					   Result = "";
					   if (currentState < 0)
						   currentState *= -1;
				   }

				   Result += pressed;

				   double number;
				   if (double.TryParse(Result, out number))
				   {
					   Result = number.ToString("N0");
					   if (currentState == 1)
					   {
						   firstNumber = number;
					   }
					   else
					   {
						   secondNumber = number;
					   }
				   }
			   });

                


        }


    }
}
