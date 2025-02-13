using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Registro.AgregarPersona;

public class ReqRegistrarPersona : IRequest<ResRegistrarPersona>
{
    public Persona obj_persona { get; set; }

}
