using CalculoBonus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcularBonus.Testes
{
    public class FuncionarioTests
    {
        [Fact]
        public void CalcularPLR_DeveRetornar10000Bonus()
        {
            var funcFabo = new Funcionario()
            {
                Id = 1,
                Cargo = Cargo.Gerente,
                Departamento = Departamento.Tecnologia,
                Nome = "Fabo",
                Salario = 10000m
            };

            var plr = funcFabo.CalcularPLR(2, 10000);

            Assert.Equal(10000m, plr);
        }
    }
}
