using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfMath.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            gb_updateCT.Visibility = Visibility.Hidden;
            gb_Kytudacbiet.Visibility = Visibility.Hidden;

        }


        //Thêm các ký tự đặc biệt khác và các biểu diễn Unicode 
        private string ConvertSpecialCharacters(string input)
        {
            Dictionary<string, string> specialCharacters = new Dictionary<string, string>()
            {
                { "√", @"\sqrt" },
                { "α", @"\alpha" },
                { "β", @"\beta" },
                { "δ", @"\delta" },
                { "ϵ", @"\epsilon" },
                { "ζ", @"\zeta" },
                { "η", @"\eta" },
                { "θ", @"\theta" },
                { "ι", @"\iota" },
                { "κ", @"\kappa" },
                { "λ", @"\lambda" },
                { "μ", @"\mu" },
                { "σ", @"\sigma" },
                { "ω", @"\omega" },
                { "π", @"\pi" },


            };

            foreach (var character in specialCharacters)
            {
                input = input.Replace(character.Key, character.Value);
            }

            return input;
        }

        // sửa kí hiệu
        private void Button_Click(object sender, RoutedEventArgs e)
        {

           

            // RichTextBox does not contain text

            TextRange equationRange = new TextRange(EquationRichTextbox.Document.ContentStart, EquationRichTextbox.Document.ContentEnd);
            string equation = equationRange.Text.Trim();

            try
                {
               
                // Clear the RichTextBox
                formulaRichTextBox.Document.Blocks.Clear();

                    // Convert special characters to their corresponding Unicode representation
                    equation = ConvertSpecialCharacters(equation);

                    // Create a formula string
                    string formulaString = $" {equation} ";

                    // Create a formula control
                    var formulaControl = new FormulaControl
                    {
                        Formula = formulaString,
                        Foreground = Brushes.Black,
                        FontSize = 18
                    };

                    // Render the formula control
                    formulaControl.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));// xác định kích thước chiều cao k giới hạn
                formulaControl.Arrange(new System.Windows.Rect(formulaControl.DesiredSize));//sắpx control trên GUI, bằng cách đặt control vào vị trí
                formulaControl.UpdateLayout();

                    // Create a bitmap render target
                    var renderBitmap = new RenderTargetBitmap((int)formulaControl.ActualWidth, (int)formulaControl.ActualHeight, 96, 96, PixelFormats.Default);
                    renderBitmap.Render(formulaControl); // vẽ formula vào bitmap

                    // Create an Image element to display the formula
                    var image = new System.Windows.Controls.Image
                    {
                        Source = renderBitmap,
                        Stretch = Stretch.None//hình ảnh sẽ không được kéo giãn hoặc thu nhỏ để phù hợp với kích thước của Image
                    };

                // Tạo một InlineUIContainer để chứa phần tử Image
                var inlineContainer = new InlineUIContainer(image);

                    // Create a new paragraph and add the InlineUIContainer
                    var paragraph = new Paragraph(inlineContainer);

                    // Add the paragraph to the RichTextBox
                    formulaRichTextBox.Document.Blocks.Add(paragraph);
                }
                catch (Exception ex)
                {
                // Xử lý lỗi phân tích công thức
                MessageBox.Show($"Error: {ex.Message}");
                }
            

        }
        
           
        

        // đóng cửa sổ sửa kí hiệu toán học
        private void close_gb_updateCT_Click(object sender, RoutedEventArgs e)
        {
            gb_updateCT.Visibility = Visibility.Hidden;
        }

        // đóng chương trình
        private void btn_Thoat_Click(object sender, RoutedEventArgs e)
        {

            // Hiển thị hộp thoại Yes/No
            MessageBoxResult result = MessageBox.Show("Bạn muốn đóng chương trình không?", "Xác Minh", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                Application.Current.Shutdown();
            }
            else
            {
                // Không làm gì cả
            }
        }




        private void formulaRichTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
        
           
        }

        //hiển thị chèn ktdb
        private void btn_ktuDB_Click(object sender, RoutedEventArgs e)
        {
            gb_Kytudacbiet.Visibility = Visibility.Visible;
            //btn_add();
            //btn_add_img();
        }
        private void Button_Click()
        {


        }
        //private void btn_add_img()
        //{

        //    Button[] buttons = new Button[3]; // khai báo mảng Button với độ dài là 3
        //    string[] buttonNames = { "/pi","Button 2", "Button 3" }; // mảng chứa các tên của các nút button
        //    for (int i = 0; i < buttons.Length; i++)
        //    {
        //        Button button = new Button();
        //        button.Content = buttonNames[i];
        //        button.Margin = new Thickness(10 * i, 10 * i, 0, 0); // vị trí của nút button trên giao diện người dùng
        //        button.Click += Button_Click; // thêm xử lý sự kiện cho nút button
        //        // thêm nút button vào mảng
        //                             // Thêm nút button vào một đối tượng Grid, StackPanel hoặc bất kỳ đối tượng nào khác tương ứng với kiểu dự án của bạn
        //        cv_Ktu.Items.Add(button); // thêm nút button vào một StackPanel có tên là myStackPanel
        //    }

        //}




        //đóng cửa sổ chèn kí tự dặc biệt 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gb_Kytudacbiet.Visibility = Visibility.Hidden;
        }

    

        //Chèn kí hiệu toán học

        private void cbb_KyHieutoanhoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbb_KyHieutoanhoc.SelectedItem != null)
            {
                gb_updateCT.Visibility = Visibility.Visible;
                string selectedItem = ((ComboBoxItem)cbb_KyHieutoanhoc.SelectedItem).Content.ToString();
                // Sử dụng giá trị của selectedItem ở đây
                EquationRichTextbox.AppendText(selectedItem);
                try
                {

                    // Clear the RichTextBox


                    // Convert special characters to their corresponding Unicode representation
                    selectedItem = ConvertSpecialCharacters(selectedItem);

                    // Create a formula string
                    string formulaString = $" {selectedItem} ";

                    // Create a formula control
                    var formulaControl = new FormulaControl
                    {
                        Formula = formulaString,
                        Foreground = Brushes.Black,
                        FontSize = 18
                    };

                    // Render the formula control
                    formulaControl.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
                    formulaControl.Arrange(new System.Windows.Rect(formulaControl.DesiredSize));
                    formulaControl.UpdateLayout();

                    // Create a bitmap render target
                    var renderBitmap = new RenderTargetBitmap((int)formulaControl.ActualWidth, (int)formulaControl.ActualHeight, 96, 96, PixelFormats.Default);
                    renderBitmap.Render(formulaControl);

                    // Create an Image element to display the formula
                    var image = new System.Windows.Controls.Image
                    {
                        Source = renderBitmap,
                        Stretch = Stretch.None
                    };

                    // Create an InlineUIContainer to hold the Image element
                    var inlineContainer = new InlineUIContainer(image);

                    // Create a new paragraph and add the InlineUIContainer
                    var paragraph = new Paragraph(inlineContainer);

                    // Add the paragraph to the RichTextBox
                    formulaRichTextBox.Document.Blocks.Add(paragraph);
                    cbb_KyHieutoanhoc.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    // Handle formula parsing error
                    MessageBox.Show($"Error: {ex.Message}");
                }

            }
        }
        //chèn kí tự đặc biệt 

        private void cv_Ktu_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cv_Ktu.SelectedItem != null)
            {
                string SelectedItem = ((Button)cv_Ktu.SelectedItem).Content.ToString();
                gb_updateCT.Visibility = Visibility.Visible;
                EquationRichTextbox.AppendText(SelectedItem);
                try
                {

                    // Clear the RichTextBox


                    // Convert special characters to their corresponding Unicode representation
                    SelectedItem = ConvertSpecialCharacters(SelectedItem);

                    // Create a formula string
                    string formulaString = $" {SelectedItem} ";

                    // Create a formula control
                    var formulaControl = new FormulaControl
                    {
                        Formula = formulaString,
                        Foreground = Brushes.Black,
                        FontSize = 18
                    };

                    // Render the formula control
                    formulaControl.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
                    formulaControl.Arrange(new System.Windows.Rect(formulaControl.DesiredSize));
                    formulaControl.UpdateLayout();

                    // Create a bitmap render target
                    var renderBitmap = new RenderTargetBitmap((int)formulaControl.ActualWidth, (int)formulaControl.ActualHeight, 96, 96, PixelFormats.Default);
                    renderBitmap.Render(formulaControl);

                    // Create an Image element to display the formula
                    var image = new System.Windows.Controls.Image
                    {
                        Source = renderBitmap,
                        Stretch = Stretch.None
                    };

                    // Create an InlineUIContainer to hold the Image element
                    var inlineContainer = new InlineUIContainer(image);

                    // Create a new paragraph and add the InlineUIContainer
                    var paragraph = new Paragraph(inlineContainer);

                    // Add the paragraph to the RichTextBox
                    formulaRichTextBox.Document.Blocks.Add(paragraph);
                    cbb_KyHieutoanhoc.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    // Handle formula parsing error
                    MessageBox.Show($"Error: {ex.Message}");
                }



            }
        }
    }
}

