using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Login;

public class ResValidarUsuario
{
    // Identificador del usuario, puede estar presente si las credenciales son correctas
    public int? Id { get; set; }

    // Nombre de usuario (si es necesario devolverlo)
    //public string UsuarioNombre { get; set; }

    // Mensaje que indica si las credenciales son correctas o si hubo algún error
    public string Mensaje { get; set; }
}
