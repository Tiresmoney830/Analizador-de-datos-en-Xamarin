using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
using Xamarin.Forms;


namespace Primera_Tarea
{
    public partial class MainPage : ContentPage
    {
        private readonly Dictionary<string, string> _definitions = new Dictionary<string, string>
        {
            { "bool", "✅ Representa un valor lógico: verdadero (true) o falso (false). Ideal para controlar estados y opciones." },
            { "DateTime", "🕒 Almacena una fecha y hora. Perfecto para registrar eventos, fechas de nacimiento o agendar citas." },
            { "char", "🎭 Representa un único carácter (letra, número o símbolo). Se escribe entre comillas simples, ej: 'A'." },
            { "byte", "🧩 Un número entero muy pequeño sin signo, de 0 a 255. Útil para datos binarios o valores como componentes de color (R, G, B)." },
            { "short", "🔍 Un número entero de 16 bits (-32,768 a 32,767). Se usa para ahorrar memoria cuando los números no son muy grandes." },
            { "int", "🧮 El tipo de número entero más común (32 bits). Se usa para contadores, IDs, edades y la mayoría de operaciones numéricas sin decimales." },
            { "long", "🚀 Un número entero enorme (64 bits). Necesario para IDs de bases de datos muy grandes o cálculos con números gigantes." },
            { "decimal", "💰 Un tipo numérico de alta precisión (128 bits). INDISPENSABLE para cálculos financieros (dinero) para evitar errores de redondeo." },
            { "double", "🔢 Un número con decimales de doble precisión (64 bits). El estándar para coordenadas GPS, cálculos científicos y medidas." },
            { "string", "✍️ Una secuencia de caracteres (texto). El tipo de dato más versátil, usado para mostrar cualquier tipo de información textual." }
        };

        public MainPage()
        {
            InitializeComponent();
        }

        private async void AnalyzeButton_Clicked(object sender, EventArgs e)
        {
            string inputText = inputEntry.Text;

          
            if (resultFrame.IsVisible)
            {
                await resultFrame.FadeTo(0, 150, Easing.CubicIn);
                resultFrame.IsVisible = false;
            }

            if (string.IsNullOrWhiteSpace(inputText))
            {
                await DisplayAlert("Entrada Vacía", "Por favor, escribe un valor en el campo de texto para analizar.", "OK");
                return;
            }

            string cleanInput = inputText.Trim();
            string detectedType;

            if (bool.TryParse(cleanInput, out _)) { detectedType = "bool"; }
            else if (DateTime.TryParse(cleanInput, out _)) { detectedType = "DateTime"; }
            else if (cleanInput.Length == 1 && !char.IsDigit(cleanInput[0])) { detectedType = "char"; }
            else if (byte.TryParse(cleanInput, out _)) { detectedType = "byte"; }
            else if (short.TryParse(cleanInput, out _)) { detectedType = "short"; }
            else if (int.TryParse(cleanInput, out _)) { detectedType = "int"; }
            else if (long.TryParse(cleanInput, out _)) { detectedType = "long"; }
            else if (decimal.TryParse(cleanInput, out _)) { detectedType = "decimal"; } // Decimal puede tener o no un punto.
            else if (double.TryParse(cleanInput, out _)) { detectedType = "double"; }
            else { detectedType = "string"; }

         
            ShowResult(detectedType);
        }

        private async void ShowResult(string typeName)
        {
            // Actualizamos el contenido de la tarjeta
            resultTypeLabel.Text = typeName;
            resultDefinitionLabel.Text = _definitions[typeName];

            // Hacemos visible el cuadro (aún con opacidad 0)
            resultFrame.IsVisible = true;

            // Iniciamos la animación de fundido (fade-in)
            // El resultado aparecerá suavemente en 400 milisegundos.
            await resultFrame.FadeTo(1, 400, Easing.CubicOut);
        }
    }
}