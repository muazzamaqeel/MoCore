using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoCore_1_0.Views.Calendar
{
    public partial class CalendarMainWindow : Window
    {
        private DateTime _currentDate;

        public CalendarMainWindow()
        {
            InitializeComponent();
            _currentDate = DateTime.Now;
            DisplayCalendar(_currentDate);
        }

        private void DisplayCalendar(DateTime date)
        {
            // Update the month and year text
            MonthYearText.Text = date.ToString("MMMM yyyy");

            // Clear previous calendar buttons
            CalendarGrid.Children.Clear();

            // Get first day of the month and total days in the month
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            int firstDayOfWeek = (int)firstDayOfMonth.DayOfWeek; // 0 = Sunday, 1 = Monday, etc.

            // Add empty cells for days before the 1st of the month
            for (int i = 0; i < firstDayOfWeek; i++)
            {
                CalendarGrid.Children.Add(new TextBlock()); // Empty placeholder for days before the 1st
            }

            // Add buttons for each day in the month
            for (int day = 1; day <= daysInMonth; day++)
            {
                Button dayButton = new Button
                {
                    Content = day.ToString(),
                    Background = Brushes.Transparent,
                    Foreground = Brushes.White,
                    BorderBrush = Brushes.Transparent,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(5),
                    Cursor = System.Windows.Input.Cursors.Hand
                };

                dayButton.Click += (sender, e) =>
                {
                    MessageBox.Show($"You clicked on {dayButton.Content} {MonthYearText.Text}");
                };

                CalendarGrid.Children.Add(dayButton);
            }

            // Add empty cells for remaining days to fill the calendar grid (for a clean look)
            int totalCells = firstDayOfWeek + daysInMonth;
            int remainingCells = 7 - (totalCells % 7);
            if (remainingCells < 7) // Only add if there's space left
            {
                for (int i = 0; i < remainingCells; i++)
                {
                    CalendarGrid.Children.Add(new TextBlock()); // Empty placeholders for remaining cells
                }
            }
        }

        private void PreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(-1); // Go to previous month
            DisplayCalendar(_currentDate);
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(1); // Go to next month
            DisplayCalendar(_currentDate);
        }
    }
}
