using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Common
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; } // Código de estado HTTP
        public string ErrorMessage { get; set; } = string.Empty; // Mensaje principal del error
        public string Details { get; set; } = string.Empty; // Detalles adicionales (opcional)
        public List<string>? Errors { get; set; } // Lista de errores específicos (opcional)
    }

}
