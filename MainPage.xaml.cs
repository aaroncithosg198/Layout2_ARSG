using System;
using System.Globalization;

namespace Layout2_ARSG
{
	public partial class MainPage : ContentPage
	{
		double primerNumero = 0;
		string operador = "";
		bool nuevoNumero = true;

		public MainPage()
		{
			InitializeComponent();
		}

		// Botones del 0 al 9
		private void Numero_Clicked(object sender, EventArgs e)
		{
			Button boton = (Button)sender;

			if (nuevoNumero || Display.Text == "Error")
			{
				Display.Text = boton.Text;
				nuevoNumero = false;
			}
			else
			{
				Display.Text += boton.Text;
			}
		}

		// Botones de operaciones
		private void Operador_Clicked(object sender, EventArgs e)
		{
			if (double.TryParse(Display.Text,
				NumberStyles.Any,
				CultureInfo.InvariantCulture,
				out primerNumero))
			{
				Button boton = (Button)sender;
				operador = boton.Text;
				nuevoNumero = true;
			}
		}

		// Botón igual
		private void Igual_Clicked(object sender, EventArgs e)
		{
			if (!double.TryParse(Display.Text,
				NumberStyles.Any,
				CultureInfo.InvariantCulture,
				out double segundoNumero))
				return;

			double resultado = 0;

			switch (operador)
			{
				case "+":
					resultado = primerNumero + segundoNumero;
					break;

				case "−":
				case "-":
					resultado = primerNumero - segundoNumero;
					break;

				case "×":
				case "*":
					resultado = primerNumero * segundoNumero;
					break;

				case "÷":
				case "/":
					if (segundoNumero == 0)
					{
						Display.Text = "Error";
						operador = "";
						nuevoNumero = true;
						return;
					}

					resultado = primerNumero / segundoNumero;
					break;

				default:
					return;
			}

			Display.Text = resultado.ToString(
				"G",
				CultureInfo.InvariantCulture);

			operador = "";
			nuevoNumero = true;
		}

		// Botón C: limpiar todo
		private void Limpiar_Clicked(object sender, EventArgs e)
		{
			Display.Text = "0";
			primerNumero = 0;
			operador = "";
			nuevoNumero = true;
		}

		// Botón DEL: borrar último carácter
		private void Borrar_Clicked(object sender, EventArgs e)
		{
			if (Display.Text == "Error")
			{
				Display.Text = "0";
				nuevoNumero = true;
				return;
			}

			if (nuevoNumero)
				return;

			if (Display.Text.Length > 1)
			{
				Display.Text = Display.Text.Substring(
					0, Display.Text.Length - 1);
			}
			else
			{
				Display.Text = "0";
			}
		}

		// Botón decimal
		private void Decimal_Clicked(object sender, EventArgs e)
		{
			if (nuevoNumero || Display.Text == "Error")
			{
				Display.Text = "0.";
				nuevoNumero = false;
			}
			else if (!Display.Text.Contains("."))
			{
				Display.Text += ".";
			}
		}
	}
}