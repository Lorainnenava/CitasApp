using Microsoft.JSInterop;
using MudBlazor;

namespace PortalAdmin.Components.Layout
{
    public partial class MainLayout
    {
        private bool DrawerOpen { get; set; } = true;
        private bool IsDarkMode { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            IsDarkMode = await JS.InvokeAsync<bool>("themeStorage.getTheme");
        }

        private async void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;
            await JS.InvokeVoidAsync("themeStorage.saveTheme", IsDarkMode);
        }

        private void DrawerToggle()
        {
            DrawerOpen = !DrawerOpen;
        }

        private readonly MudTheme MyCustomTheme = new()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#2D5D9F",             // Azul profesional (confianza y estabilidad)
                Secondary = "#4DB6AC",           // Verde azulado suave (sanación y frescura)
                Tertiary = "#81C784",            // Verde esperanza (optimismo)
                AppbarBackground = "#EEEEEE",    // Verde pálido muy claro (sensación de calma)
                Background = "#F5F7FA",          // Gris azulado muy claro (limpio, técnico)
                Surface = "#FFFFFF",             // Blanco puro para contenido limpio
                DrawerBackground = "#24497A",    // Azul profundo para navegación lateral
                TextPrimary = "#1A1A1A",         // Gris oscuro-neutro, legible pero suave
                TextSecondary = "#607D8B",       // Azul grisáceo para subtítulos y apoyo visual
                ActionDefault = "#B0BEC5",       // Neutro claro para elementos inactivos
                AppbarText = "#1B365D",          // Azul marino elegante en navbar
                Success = "#388E3C",             // Verde éxito
                Info = "#1976D2",                // Azul informativo sobrio
                Warning = "#FBC02D",             // Amarillo alerta cálido
                Error = "#C62828"                // Rojo alerta profesional
            },

            PaletteDark = new PaletteDark()
            {
                Primary = "#1A2B4C",             // Azul oscuro sofisticado
                Secondary = "#26A69A",           // Verde menta claro (saludable)
                Tertiary = "#A5D6A7",            // Verde esperanza claro
                AppbarBackground = "#1E1E1E",    // Negro suave para barra superior
                Background = "#121212",          // Fondo general oscuro
                Surface = "#1F1F1F",             // Contenido sobre fondo oscuro
                DrawerBackground = "#0D47A1",    // Azul intenso en sidebar
                TextPrimary = "#ECEFF1",         // Casi blanco
                TextSecondary = "#90A4AE",       // Gris claro para subtítulos
                ActionDefault = "#78909C",       // Gris neutro activo
                Success = "#66BB6A",             // Verde éxito suave
                Info = "#29B6F6",                // Azul informativo brillante
                Warning = "#FFA726",             // Naranja alerta
                Error = "#EF5350"                // Rojo alerta vibrante
            }
        };
    }
}
