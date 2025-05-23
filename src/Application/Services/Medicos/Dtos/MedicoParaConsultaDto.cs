﻿namespace Application.Services.Medicos.Dtos
{
    public class MedicoParaConsultaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CrmNumero { get; set; }
        public string CrmUf { get; set; }
        public string Especialidade { get; set; }
        public decimal? ValorDaConsulta { get; set; }
    }
}
