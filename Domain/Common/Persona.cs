using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common;

public class Persona
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombres { get; set; }

    [Required]
    [MaxLength(100)]
    public string Apellidos { get; set; }

    [Required]
    [MaxLength(50)]
    public string NumeroIdentificacion { get; set; }

    [Required]
    [MaxLength(50)]
    public string TipoIdentificacion { get; set; }

    [Required]
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public string IdentificacionCompleta => $"{TipoIdentificacion}-{NumeroIdentificacion}";

    [NotMapped]
    public string NombreCompleto => $"{Nombres} {Apellidos}";

    [Required]
    [MaxLength(50)]
    public string Usuario { get; set; }

    [Required]
    [MaxLength(255)]
    public string Pass { get; set; }

}
