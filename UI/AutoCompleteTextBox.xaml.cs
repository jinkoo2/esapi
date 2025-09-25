using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace esapi.UI
{
    public partial class AutoCompleteTextBox : UserControl
    {
        public event EventHandler<string> SelectedItemChanged;

        // ItemsSource (list of options)
        public ObservableCollection<string> ItemsSource
        {
            get => (ObservableCollection<string>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(ObservableCollection<string>), typeof(AutoCompleteTextBox),
                new PropertyMetadata(new ObservableCollection<string>()));

        // SelectedItem (two-way bindable)
        public string SelectedItem
        {
            get => (string)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(string), typeof(AutoCompleteTextBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public AutoCompleteTextBox()
        {
            InitializeComponent();
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = InputTextBox.Text.ToLower();
            var matches = ItemsSource
                .Where(s => s.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
                .Take(10)
                .ToList();

            SuggestionsListBox.ItemsSource = matches;
            SuggestionsListBox.Visibility = matches.Any() ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SuggestionsListBox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SuggestionsListBox.SelectedItem is string selected)
            {
                InputTextBox.Text = selected;
                SelectedItem = selected;
                SuggestionsListBox.Visibility = Visibility.Collapsed;

                // Raise the event
                SelectedItemChanged?.Invoke(this, selected);
            }
        }
    }
}
