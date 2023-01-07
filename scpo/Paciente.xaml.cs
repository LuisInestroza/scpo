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
using System.Windows.Shapes;
using scpo.Pacientes;

namespace scpo
{
    /// <summary>
    /// Lógica de interacción para Paciente.xaml
    /// </summary>
    public partial class Paciente : Window
    {

        public int idPacienteUpdate;
        public Paciente()
        {
            InitializeComponent();
            List_Paciente();
            btn_Update_Paciente.IsEnabled = false;

            


        }

        public void List_Paciente()
        {
            // Llamar la clase
            Pacientes.Paciente nuevo = new Pacientes.Paciente();
            // Crear la lista
            List<Pacientes.Paciente> lista = Pacientes.Paciente.ListarPaciente();

            lbListaPacientes.Items.Clear();
            // Mostrar todos los pacientes
            if (lista.Any())
            {
                lista.ForEach(paciente => lbListaPacientes.Items.Add(paciente.nombrePaciente.ToString()));
    
            }
            
        }

        private void Clear_Inputs()
        {
            List_Paciente();
            txtNombreCompleto.Text = "";
            txtNumeroIdentidad.Text = "";
            dpFechaNaciemiento.Text = "";
            txtedad.Text = "";
            txtNumero.Text = "";
            cbSexo.Text = "";
            cbEstadoCivil.Text = "";
            txtOcupacion.Text = "";
            txtDireccion.Text = "";
            btn_Add_Paciente.IsEnabled= true;
            btn_Update_Paciente.IsEnabled = false;
            txtBuscarPaciente.Text = "";

        }

        private void btn_Add_Paciente_Click(object sender, RoutedEventArgs e)
        {
            // Validar cada campo para evitar inserciones vacias
            if (txtNombreCompleto.Text == "")
            {
                MessageBox.Show("Ingresar el Nombre del paciente", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txtNumeroIdentidad.Text == "")
            {
                MessageBox.Show("Ingresar la Identidad", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txtedad.Text == "")
            {
                MessageBox.Show("Ingresar la Edad", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txtDireccion.Text == "")
            {
                MessageBox.Show("Ingresar la Direccion", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txtNumero.Text == "")
            {
                MessageBox.Show("Ingresar los Telfonos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (cbEstadoCivil.Text == "")
            {
                MessageBox.Show("Selecciona el Estado Civil", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            else if (cbSexo.Text == "")
            {
                MessageBox.Show("Seleciona el Sexo", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (dpFechaNaciemiento.Text == "")
            {
                MessageBox.Show("Ingresa la Fecha de Nacimiento", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txtOcupacion.Text == "")
            {
                MessageBox.Show("Ingresa la Ocupación", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Pacientes.Paciente addPaciente = new Pacientes.Paciente();

                addPaciente.nombrePaciente = txtNombreCompleto.Text;
                addPaciente.identidadPaciente = txtNumeroIdentidad.Text;
                addPaciente.fechaNacimiento = Convert.ToDateTime(dpFechaNaciemiento.Text);
                addPaciente.edad = Convert.ToInt32(txtedad.Text);
                addPaciente.numeroTelefono = txtNumero.Text;
                addPaciente.genero = cbSexo.Text.ToString();
                addPaciente.estadoCivil = cbEstadoCivil.Text.ToString();
                addPaciente.ocupacion = txtOcupacion.Text;
                addPaciente.direccion = txtDireccion.Text;

                if (Pacientes.Paciente.Add_Paciente(addPaciente))
                {
                    MessageBox.Show("Los Datos Han Sido Registrados", "Registro Guardado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Clear_Inputs();
                }
                else
                {
                    MessageBox.Show("Los Datos No Han Sido Registrados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Clear_Inputs(); 
                }
            }
            
        }

        private void txtBuscarPaciente_TextChanged(object sender, TextChangedEventArgs e)
        {
            Pacientes.Paciente paciente = Pacientes.Paciente.List_Datos_Paciente_Identidad(txtBuscarPaciente.Text);

            idPacienteUpdate = paciente.idPaciente;
            txtNombreCompleto.Text = paciente.nombrePaciente;
            txtNumeroIdentidad.Text = paciente.identidadPaciente;
            dpFechaNaciemiento.Text = Convert.ToString(paciente.fechaNacimiento);
            txtedad.Text = Convert.ToString(paciente.edad);
            txtNumero.Text = paciente.numeroTelefono;
            cbSexo.Text = paciente.genero;
            cbEstadoCivil.Text = paciente.estadoCivil;
            txtOcupacion.Text = paciente.ocupacion;
            txtDireccion.Text = paciente.direccion;

            if(txtNumeroIdentidad.Text != "")
            {
                btn_Update_Paciente.IsEnabled = true;
                btn_Add_Paciente.IsEnabled = false;
            }


        }

        private void btn_Update_Paciente_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro de modificar el paciente?", "Modificar Paciente", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
            
                Pacientes.Paciente updatePaciente = new Pacientes.Paciente();
                updatePaciente.idPaciente = idPacienteUpdate;
                updatePaciente.nombrePaciente = txtNombreCompleto.Text;
                updatePaciente.identidadPaciente = txtNumeroIdentidad.Text;
                updatePaciente.fechaNacimiento = Convert.ToDateTime(dpFechaNaciemiento.Text);
                updatePaciente.edad = Convert.ToInt32(txtedad.Text);
                updatePaciente.numeroTelefono = txtNumero.Text;
                updatePaciente.genero = cbSexo.Text.ToString();
                updatePaciente.estadoCivil = cbEstadoCivil.Text.ToString();
                updatePaciente.ocupacion = txtOcupacion.Text;
                updatePaciente.direccion = txtDireccion.Text;
                
                if (Pacientes.Paciente.Update_Paciente(updatePaciente))
                {
                    
                    MessageBox.Show("Los Datos Se Han Actuzalizado", "Actulizacion De Datos", MessageBoxButton.OK, MessageBoxImage.Information);
                    Clear_Inputs();
                }
                else
                {
                    MessageBox.Show("Los Datos No Se Han Actuzalizado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Clear_Inputs();
            }
            
        }
    }
}
