using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CadFuncionario.Core.DomainObjects;

namespace CadFuncionario.Domain.Entities
{
    public class Profissao : Entity
    {
        public Profissao(Guid profissaoId, string descricao, decimal salarioBase)
        {
            ProfissaoId = profissaoId == Guid.Empty
                ? Guid.NewGuid()
                : profissaoId;
            Descricao = descricao;
            SalarioBase = salarioBase;
        }

        public Guid ProfissaoId { get; private set; }
        public string Descricao { get; private set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalarioBase { get; private set; }

        public virtual ICollection<StepProfissao> StepProfissoes { get; set; }

        public void AlterarSalarioBase(decimal salarioBase)
        {
            SalarioBase = salarioBase;
        }

        public void AlterarDescricao(string descricao)
        {
            Descricao = descricao;
        }
    }
}