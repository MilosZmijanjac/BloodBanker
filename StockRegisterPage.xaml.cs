using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BloodBanker.DAO;
using BloodBanker.Model;
using WPFCustomMessageBox;

namespace BloodBanker
{
    /// <summary>
    ///     Interaction logic for StockRegisterPage.xaml
    /// </summary>
    public partial class StockRegisterPage : Page
    {
        private readonly MedicalInstitution medicalInstitution;

        public StockRegisterPage(MedicalInstitution medInst)
        {
            medicalInstitution = medInst;
            InitializeComponent();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            var stock = new Stock(medicalInstitution.MI_ID, "", 0, 0);
            uint result = 0;
            if (!IsFilled()) return;

            foreach (UIElement i in mainGrid.Children)
            {
                if (Grid.GetColumn(i) == 0)
                    stock.B_GRP = ((TextBox) i).Text;
                if (Grid.GetColumn(i) == 1)
                {
                    if (uint.TryParse(((TextBox) i).Text, out result))
                    {
                        stock.QUANTITY = (int) result;
                    }

                    else
                    {
                        CustomMessageBox.ShowOK("", (string) Application.Current.Resources["IncompleteReg"], "OK");
                        return;
                    }
                }

                if (Grid.GetColumn(i) == 2)
                {
                    if (uint.TryParse(((TextBox) i).Text, out result))
                    {
                        stock.RATE = (int) result;
                        try
                        {
                            StockDAO.StoreStock(stock);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show((string) Application.Current.Resources["StockRegistrationPageException"] +
                                            "\n" + ex.Message);
                        }
                    }
                    else
                    {
                        CustomMessageBox.ShowOK("", (string) Application.Current.Resources["IncompleteReg"], "OK");
                        return;
                    }
                }
            }

            NavigationService.Navigate(new LoginPage());
        }

        public bool IsFilled()
        {
            foreach (var i in mainGrid.Children)
                if (i is TextBox && ((TextBox) i).Text.Equals((string) Application.Current.Resources["Enter"]))
                {
                    CustomMessageBox.ShowOK("", (string) Application.Current.Resources["IncompleteReg"], "OK");
                    return false;
                }

            return true;
        }

        private void PositiveAQ_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveAQ.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                positiveAQ.Foreground = Brushes.Black;
                positiveAQ.Clear();
            }
        }

        private void PositiveAQ_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveAQ.Text.Equals(""))
            {
                positiveAQ.Text = (string) Application.Current.Resources["Enter"];
                positiveAQ.Foreground = Brushes.White;
            }
        }

        private void PositiveAR_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveAR.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                positiveAR.Foreground = Brushes.Black;
                positiveAR.Clear();
            }
        }

        private void PositiveAR_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveAR.Text.Equals(""))
            {
                positiveAR.Text = (string) Application.Current.Resources["Enter"];
                positiveAR.Foreground = Brushes.White;
            }
        }

        private void PositiveBQ_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveBQ.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                positiveBQ.Foreground = Brushes.Black;
                positiveBQ.Clear();
            }
        }

        private void PositiveBQ_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveBQ.Text.Equals(""))
            {
                positiveBQ.Text = (string) Application.Current.Resources["Enter"];
                positiveBQ.Foreground = Brushes.White;
            }
        }

        private void PositiveBR_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveBR.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                positiveBR.Foreground = Brushes.Black;
                positiveBR.Clear();
            }
        }

        private void PositiveBR_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveBR.Text.Equals(""))
            {
                positiveBR.Text = (string) Application.Current.Resources["Enter"];
                positiveBR.Foreground = Brushes.White;
            }
        }

        private void PositiveOQ_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveOQ.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                positiveOQ.Foreground = Brushes.Black;
                positiveOQ.Clear();
            }
        }

        private void PositiveOQ_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveOQ.Text.Equals(""))
            {
                positiveAQ.Text = (string) Application.Current.Resources["Enter"];
                positiveAQ.Foreground = Brushes.White;
            }
        }

        private void PositiveOR_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveOR.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                positiveOR.Foreground = Brushes.Black;
                positiveOR.Clear();
            }
        }

        private void PositiveOR_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveOR.Text.Equals(""))
            {
                positiveOR.Text = (string) Application.Current.Resources["Enter"];
                positiveOR.Foreground = Brushes.White;
            }
        }

        private void PositiveABQ_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveABQ.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                positiveABQ.Foreground = Brushes.Black;
                positiveABQ.Clear();
            }
        }

        private void PositiveABQ_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveABQ.Text.Equals(""))
            {
                positiveABQ.Text = (string) Application.Current.Resources["Enter"];
                positiveABQ.Foreground = Brushes.White;
            }
        }

        private void PositiveABR_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveABR.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                positiveABR.Foreground = Brushes.Black;
                positiveABR.Clear();
            }
        }

        private void PositiveABR_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (positiveABR.Text.Equals(""))
            {
                positiveABR.Text = (string) Application.Current.Resources["Enter"];
                positiveABR.Foreground = Brushes.White;
            }
        }

        private void NegativeAQ_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeAQ.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                negativeAQ.Foreground = Brushes.Black;
                negativeAQ.Clear();
            }
        }

        private void NegativeAQ_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeAQ.Text.Equals(""))
            {
                negativeAQ.Text = (string) Application.Current.Resources["Enter"];
                negativeAQ.Foreground = Brushes.White;
            }
        }

        private void NegativeAR_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeAR.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                negativeAR.Foreground = Brushes.Black;
                negativeAR.Clear();
            }
        }

        private void NegativeAR_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeAR.Text.Equals(""))
            {
                negativeAR.Text = (string) Application.Current.Resources["Enter"];
                negativeAR.Foreground = Brushes.White;
            }
        }

        private void NegativeBQ_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeBQ.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                negativeBQ.Foreground = Brushes.Black;
                negativeBQ.Clear();
            }
        }

        private void NegativeBQ_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeBQ.Text.Equals(""))
            {
                negativeBQ.Text = (string) Application.Current.Resources["Enter"];
                negativeBQ.Foreground = Brushes.White;
            }
        }

        private void NegativeBR_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeBR.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                negativeBR.Foreground = Brushes.Black;
                negativeBR.Clear();
            }
        }

        private void NegativeBR_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeBR.Text.Equals(""))
            {
                negativeBR.Text = (string) Application.Current.Resources["Enter"];
                negativeBR.Foreground = Brushes.White;
            }
        }

        private void NegativeOQ_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeOQ.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                negativeOQ.Foreground = Brushes.Black;
                negativeOQ.Clear();
            }
        }

        private void NegativeOQ_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeOQ.Text.Equals(""))
            {
                negativeOQ.Text = (string) Application.Current.Resources["Enter"];
                negativeOQ.Foreground = Brushes.White;
            }
        }

        private void NegativeOR_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeOR.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                negativeOR.Foreground = Brushes.Black;
                negativeOR.Clear();
            }
        }

        private void NegativeOR_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeOR.Text.Equals(""))
            {
                negativeOR.Text = (string) Application.Current.Resources["Enter"];
                negativeOR.Foreground = Brushes.White;
            }
        }

        private void NegativeABQ_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeABQ.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                negativeABQ.Foreground = Brushes.Black;
                negativeABQ.Clear();
            }
        }

        private void NegativeABQ_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeABQ.Text.Equals(""))
            {
                negativeABQ.Text = (string) Application.Current.Resources["Enter"];
                negativeABQ.Foreground = Brushes.White;
            }
        }

        private void NegativeABR_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeABR.Text.Equals((string) Application.Current.Resources["Enter"]))
            {
                negativeABR.Foreground = Brushes.Black;
                negativeABR.Clear();
            }
        }

        private void NegativeABR_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (negativeABR.Text.Equals(""))
            {
                negativeABR.Text = (string) Application.Current.Resources["Enter"];
                negativeABR.Foreground = Brushes.White;
            }
        }
    }
}