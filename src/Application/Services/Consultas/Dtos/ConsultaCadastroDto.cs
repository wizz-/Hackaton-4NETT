﻿
namespace Application.Services.Consultas.Dtos
{
    public class ConsultaCadastroDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public DateOnly Dia { get; set; }
        public int MedicoId { get; set; }
        public int EspecialidadeId { get; set; }
        public TimeOnly Inicio { get; set; }
        public TimeOnly Fim { get; set; }
    }
}
