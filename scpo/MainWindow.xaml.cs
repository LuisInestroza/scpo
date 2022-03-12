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
using System.Windows.Navigation;
using System.Windows.Shapes;
// Libreria para formatos de las fechas.
using System.Globalization;

namespace scpo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            displayCalendar();
        }

        /// <summary>
        /// Función para mostrar el calendario de citas 
        /// </summary>
        private void displayCalendar()
        {
            DateTime now = DateTime.Now;

            // Obtener el mes y el año actual
            int month = now.Month;
            int year = now.Year;

            // Cambiar el formate del mes del numero al nombre.
            String nameMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);

            // Mostrar el mes y el año en el label
            lbMonthYear.Text = nameMonth+", " +year.ToString();

           
            
        }
    }
}
