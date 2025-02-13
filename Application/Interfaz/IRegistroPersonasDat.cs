using Application.Login;
using Application.Registro.AgregarPersona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaz;

public interface IRegistroPersonasDat
{
    Task<ResRegistrarPersona> AgregarPersona(ReqRegistrarPersona request);
}
