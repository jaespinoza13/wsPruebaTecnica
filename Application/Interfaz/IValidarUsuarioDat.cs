using Application.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaz;

public interface IValidarUsuarioDat
{
    Task<ResValidarUsuario> GetValidacionUsuario(ReqValidarUsuario request);
}
