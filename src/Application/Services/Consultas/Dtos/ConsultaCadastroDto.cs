﻿
namespace Application.Services.Consultas.Dtos
{
    public class ConsultaCadastroDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public string Especialidade { get; set; }
        public DateTime DataHorario { get; set; }
    }
}
