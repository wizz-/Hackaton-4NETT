﻿using Application.Services.Cadastros.Dtos;

namespace Application.Services.Especialidades.Interfaces
{
    public interface IEspecialidadesAppService
    {
        IList<EspecialidadeDto> ObterEspecialidades();
    }
}
