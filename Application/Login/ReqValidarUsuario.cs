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
    public string UsuarioNombre { get; set; } 
    public string Pass { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

}
