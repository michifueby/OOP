namespace WPFAnimation.View
{
    using System.Linq;
    using System.Windows.Controls;

    /// <summary>
    /// Interaktionslogik für Clock.xaml
    /// </summary>
    public partial class Clock : UserControl
    {
        public Clock()
        {
            InitializeComponent();

            var angle = Enumerable.Range(0, 6).Select(p => p * 30);
            Control.ItemsSource = angle;
        }
    }
}
