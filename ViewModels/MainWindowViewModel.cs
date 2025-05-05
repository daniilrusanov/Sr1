using System;
using System.Windows.Input;

namespace Sr1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _startRangeA = "1";
        private string _endRangeB = "1000";
        private string _resultTask1 = "";
        
        private string _number1 = "0";
        private string _number2 = "0";
        private string _number3 = "0";
        private string _resultTask2 = "";

        public MainWindowViewModel()
        {
            CalculateTask1Command = new RelayCommand(_ => CalculateTask1());
            CalculateTask2Command = new RelayCommand(_ => CalculateTask2());
        }

        public string StartRangeA
        {
            get => _startRangeA;
            set => SetField(ref _startRangeA, value);
        }

        public string EndRangeB
        {
            get => _endRangeB;
            set => SetField(ref _endRangeB, value);
        }

        public string ResultTask1
        {
            get => _resultTask1;
            set => SetField(ref _resultTask1, value);
        }

        public string Number1
        {
            get => _number1;
            set => SetField(ref _number1, value);
        }

        public string Number2
        {
            get => _number2;
            set => SetField(ref _number2, value);
        }

        public string Number3
        {
            get => _number3;
            set => SetField(ref _number3, value);
        }

        public string ResultTask2
        {
            get => _resultTask2;
            set => SetField(ref _resultTask2, value);
        }

        public ICommand CalculateTask1Command { get; }
        public ICommand CalculateTask2Command { get; }

        private void CalculateTask1()
        {
            try
            {
                if (int.TryParse(StartRangeA, out int a) && int.TryParse(EndRangeB, out int b))
                {
                    if (a > b)
                    {
                        // Swap values if a > b
                        int temp = a;
                        a = b;
                        b = temp;
                    }

                    int count = 0;
                    for (int i = a; i <= b; i++)
                    {
                        // Перевіряємо умови:
                        // 1. Кратне 29
                        // 2. Парне (ділиться на 2 без остачі)
                        // 3. При діленні на 5 дає в остачі 2
                        if (i % 29 == 0 && i % 2 == 0 && i % 5 == 2)
                        {
                            count++;
                        }
                    }

                    ResultTask1 = count.ToString();
                }
                else
                {
                    ResultTask1 = "Введіть коректні числові значення";
                }
            }
            catch (Exception ex)
            {
                ResultTask1 = $"Помилка: {ex.Message}";
            }
        }

        private void CalculateTask2()
        {
            try
            {
                if (double.TryParse(Number1, out double num1) && 
                    double.TryParse(Number2, out double num2) && 
                    double.TryParse(Number3, out double num3))
                {
                    double result;
                    
                    // Перевіряємо, чи є хоча б одне додатне число
                    if (num1 > 0 || num2 > 0 || num3 > 0)
                    {
                        // Квадрат суми
                        double sum = num1 + num2 + num3;
                        result = sum * sum;
                        ResultTask2 = $"{result} (квадрат суми)";
                    }
                    else
                    {
                        // Сума квадратів
                        result = (num1 * num1) + (num2 * num2) + (num3 * num3);
                        ResultTask2 = $"{result} (сума квадратів)";
                    }
                }
                else
                {
                    ResultTask2 = "Введіть коректні числові значення";
                }
            }
            catch (Exception ex)
            {
                ResultTask2 = $"Помилка: {ex.Message}";
            }
        }
    }

    // Простий клас команди для заміни ReactiveCommand
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);
    }

    // Простий клас CommandManager для обробки CanExecuteChanged
    public static class CommandManager
    {
        public static event EventHandler RequerySuggested;

        public static void InvalidateRequerySuggested()
        {
            RequerySuggested?.Invoke(null, EventArgs.Empty);
        }
    }
}