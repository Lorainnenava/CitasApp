using MudBlazor;
using PortalAdmin.Models;
using PortalAdmin.Validators;

namespace PortalAdmin.Pages
{
    public partial class LoginPage
    {
        private LoginModel Model { get; set; } = new LoginModel();
        private PropertyValidator<LoginModel> PropertyValidator { get; set; }
        private MudForm form;

        protected override void OnInitialized()
        {
            PropertyValidator = new PropertyValidator<LoginModel>(new LoginValidator());
        }

        private async Task Submit()
        {
            await form.Validate();

            if (form.IsValid)
            {
                Toast.Show("Formulario enviado correctamente.", Severity.Success);
            }
            else
            {
                Toast.Show("Por favor corrige los errores.", Severity.Error);
            }
        }
    }
}
