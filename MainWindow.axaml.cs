using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace Sr1
{
    public partial class MainWindow : Window
    {
        private TextBox _startRangeA;
        private TextBox _endRangeB;
        private TextBox _resultTask1;
        private Button _calculateTask1Button;
        
        private TextBox _number1;
        private TextBox _number2;
        private TextBox _number3;
        private TextBox _resultTask2;
        private Button _calculateTask2Button;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            // Отримання посилань на елементи інтерфейсу
            _startRangeA = this.FindControl<TextBox>("StartRangeA");
            _endRangeB = this.FindControl<TextBox>("EndRangeB");
            _resultTask1 = this.FindControl<TextBox>("ResultTask1");
            _calculateTask1Button = this.FindControl<Button>("CalculateTask1Button");

            _number1 = this.FindControl<TextBox>("Number1");
            _number2 = this.FindControl<TextBox>("Number2");
            _number3 = this.FindControl<TextBox>("Number3");
            _resultTask2 = this.FindControl<TextBox>("ResultTask2");
            _calculateTask2Button = this.FindControl<Button>("CalculateTask2Button");

            // Додавання обробників подій
            _calculateTask1Button.Click += CalculateTask1_Click;
            _calculateTask2Button.Click += CalculateTask2_Click;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void CalculateTask1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(_startRangeA.Text, out int a) && int.TryParse(_endRangeB.Text, out int b))
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

                    _resultTask1.Text = count.ToString();
                }
                else
                {
                    _resultTask1.Text = "Введіть коректні числові значення";
                }
            }
            catch (Exception ex)
            {
                _resultTask1.Text = $"Помилка: {ex.Message}";
            }
        }

        private void CalculateTask2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.TryParse(_number1.Text, out double num1) && 
                    double.TryParse(_number2.Text, out double num2) && 
                    double.TryParse(_number3.Text, out double num3))
                {
                    double result;
                    
                    // Перевіряємо, чи є хоча б одне додатне число
                    if (num1 > 0 || num2 > 0 || num3 > 0)
                    {
                        // Квадрат суми
                        double sum = num1 + num2 + num3;
                        result = sum * sum;
                        _resultTask2.Text = $"{result} (квадрат суми)";
                    }
                    else
                    {
                        // Сума квадратів
                        result = (num1 * num1) + (num2 * num2) + (num3 * num3);
                        _resultTask2.Text = $"{result} (сума квадратів)";
                    }
                }
                else
                {
                    _resultTask2.Text = "Введіть коректні числові значення";
                }
            }
            catch (Exception ex)
            {
                _resultTask2.Text = $"Помилка: {ex.Message}";
            }
        }
    }
}