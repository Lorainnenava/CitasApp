using MudBlazor;

namespace PortalAdmin.Components
{
    public class ToastService
    {
        private readonly ISnackbar _snackbar;

        public ToastService(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }

        public void Show(string message, Severity severity = Severity.Info, int durationMs = 2000)
        {
            _snackbar.Configuration.VisibleStateDuration = durationMs; 
            _snackbar.Configuration.SnackbarVariant = Variant.Filled;
            _snackbar.Add(message, severity);
        }
    }
}
