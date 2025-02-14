using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Login;

public class ReqValidarUsuario: IRequest<ResValidarUsuario>
{
    public int Id { get; set; }
    public string UsuarioNombre { get; set; } = String.Empty;
    public string Pass { get; set; } = String.Empty;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

}
