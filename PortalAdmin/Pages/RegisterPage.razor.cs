using MudBlazor;
using PortalAdmin.Models;
using PortalAdmin.Validators;

namespace PortalAdmin.Pages
{
    public partial class RegisterPage
    {
        private RegisterModel Model { get; set; } = new RegisterModel();
        private PropertyValidator<RegisterModel> PropertyValidator { get; set; }
        private MudForm form;

        protected override void OnInitialized()
        {
            PropertyValidator = new PropertyValidator<RegisterModel>(new RegisterValidator());
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
