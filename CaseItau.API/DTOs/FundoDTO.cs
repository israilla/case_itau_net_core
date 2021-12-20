﻿namespace CaseItau.API.DTOs
{
    public class FundoDTO
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public int CodigoTipo { get; set; }
        public string NomeTipo { get; set; }
        public decimal? Patrimonio { get; set; }
    }
}