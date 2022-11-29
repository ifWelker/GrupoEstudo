using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CalculoBonus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public FuncionarioController(ILogger<WeatherForecastController> logger, IMemoryCache cache, IConfiguration configuration)
        {
            _logger = logger;
            _cache = cache;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult BuscarFuncionarios(int id) {

            var funcionario = _cache.Get<Funcionario>(id);

            if (funcionario is null)
                return NotFound();

            return Ok(funcionario);

        }

        [HttpPost]
        public IActionResult CriarFuncionario([FromBody] Funcionario funcionario)
        {
            _cache.Set<Funcionario>(funcionario.Id, funcionario);

            return Ok(funcionario);
        }

        [HttpPut]
        public IActionResult AtualizarFuncionario(int id, [FromBody] Funcionario funcionario)
        {
            _cache.Set<Funcionario>(id,funcionario);

            return Ok(funcionario);
        }

        [HttpDelete]
        public IActionResult DemitirFuncionario(int id) 
        {
            var funcionario = _cache.Get<Funcionario>(id);

            _cache.Remove(id);

            return Ok($"{funcionario.Nome} foi demitido com sucesso");
        }

        [HttpGet("ReajusteSalario")]
        public IActionResult ReajusteSalario(int id, decimal percentualAumento) 
        {
            var funcionario = _cache.Get<Funcionario>(id);

            var salarioReajustado = ((funcionario.Salario * percentualAumento) / 100) + funcionario.Salario;

            return Ok(salarioReajustado);
        }

        [HttpGet("CalcularPLR")]
        public IActionResult CalcularPLR(int id)
        {
            decimal tetoPLR = Convert.ToDecimal(_configuration["TetoPLR"]),
                    qtdSalariosPLR = Convert.ToDecimal(_configuration["QuantidadeSalariosPLR"]);

            var funcionario = _cache.Get<Funcionario>(id);

            var valorPLR = funcionario.CalcularPLR(qtdSalariosPLR, tetoPLR);

            return Ok(valorPLR);
        }

        [HttpGet("CalcularBonus")]
        public IActionResult CalcularBonus(int id)
        {
            var funcionario = _cache.Get<Funcionario>(id);

            decimal tetoPLR = Convert.ToDecimal(_configuration["TetoPLR"]),
                    qtdSalariosPLR = Convert.ToDecimal(_configuration["QuantidadeSalariosPLR"]);

            int qtdBonusMinimo = 0,
                qtdBonusMaximo = 0;

            decimal valorBonusMinimo = 0m,
                    valorBonusMaximo = 0m;

            switch (funcionario.Cargo)
            {
                case Cargo.Junior:
                    qtdBonusMinimo = Convert.ToInt32(_configuration["Bonus:JuniorMinimo"]);
                    qtdBonusMaximo = Convert.ToInt32(_configuration["Bonus:JuniorMaximo"]);
                    valorBonusMinimo = funcionario.CalcularBonusMinimo(qtdSalariosPLR, tetoPLR, qtdBonusMinimo);
                    valorBonusMaximo = funcionario.CalcularBonusMaximo(qtdSalariosPLR, tetoPLR, qtdBonusMaximo);
                    break;
                case Cargo.Pleno:
                    qtdBonusMinimo = Convert.ToInt32(_configuration["Bonus:PlenoMinimo"]);
                    qtdBonusMaximo = Convert.ToInt32(_configuration["Bonus:PlenoMaximo"]);
                    valorBonusMinimo = funcionario.CalcularBonusMinimo(qtdSalariosPLR, tetoPLR, qtdBonusMinimo);
                    valorBonusMaximo = funcionario.CalcularBonusMaximo(qtdSalariosPLR, tetoPLR, qtdBonusMaximo);
                    break;
                case Cargo.Senior:
                    qtdBonusMinimo = Convert.ToInt32(_configuration["Bonus:SeniorMinimo"]);
                    qtdBonusMaximo = Convert.ToInt32(_configuration["Bonus:SeniorMaximo"]);
                    valorBonusMinimo = funcionario.CalcularBonusMinimo(qtdSalariosPLR, tetoPLR, qtdBonusMinimo);
                    valorBonusMaximo = funcionario.CalcularBonusMaximo(qtdSalariosPLR, tetoPLR, qtdBonusMaximo);
                    break;
                case Cargo.Especialista:
                    qtdBonusMinimo = Convert.ToInt32(_configuration["Bonus:EspecialistaMinimo"]);
                    qtdBonusMaximo = Convert.ToInt32(_configuration["Bonus:EspecialistaMaximo"]);
                    valorBonusMinimo = funcionario.CalcularBonusMinimo(qtdSalariosPLR, tetoPLR, qtdBonusMinimo);
                    valorBonusMaximo = funcionario.CalcularBonusMaximo(qtdSalariosPLR, tetoPLR, qtdBonusMaximo);
                    break;
                case Cargo.Coordenador:
                    qtdBonusMinimo = Convert.ToInt32(_configuration["Bonus:CoordenadorMinimo"]);
                    qtdBonusMaximo = Convert.ToInt32(_configuration["Bonus:CoordenadorMaximo"]);
                    valorBonusMinimo = funcionario.CalcularBonusMinimo(qtdSalariosPLR, tetoPLR, qtdBonusMinimo);
                    valorBonusMaximo = funcionario.CalcularBonusMaximo(qtdSalariosPLR, tetoPLR, qtdBonusMaximo);
                    break;
                case Cargo.Gerente:
                    qtdBonusMinimo = Convert.ToInt32(_configuration["Bonus:GerenteMinimo"]);
                    qtdBonusMaximo = Convert.ToInt32(_configuration["Bonus:GerenteMaximo"]);
                    valorBonusMinimo = funcionario.CalcularBonusMinimo(qtdSalariosPLR, tetoPLR, qtdBonusMinimo);
                    valorBonusMaximo = funcionario.CalcularBonusMaximo(qtdSalariosPLR, tetoPLR, qtdBonusMaximo);
                    break;
                case Cargo.Diretor:
                    qtdBonusMinimo = Convert.ToInt32(_configuration["Bonus:DiretorMinimo"]);
                    qtdBonusMaximo = Convert.ToInt32(_configuration["Bonus:DiretorMaximo"]);
                    valorBonusMinimo = funcionario.CalcularBonusMinimo(qtdSalariosPLR, tetoPLR, qtdBonusMinimo);
                    valorBonusMaximo = funcionario.CalcularBonusMaximo(qtdSalariosPLR, tetoPLR, qtdBonusMaximo);
                    break;
                default:
                    break;
            }

            return Ok(new { bonusMinimo = valorBonusMinimo, bonusMaximo = valorBonusMaximo });

        }

        [HttpGet("SalarioAnual")]
        public IActionResult SalarioAnual(int id, int bonus, decimal valorPLR, decimal mesesTrabalhados)
        {
            var funcionario = _cache.Get<Funcionario>(id);

            decimal ferias = (funcionario.Salario / 3);

            decimal calculoBonus = (funcionario.Salario * bonus) - valorPLR;

            decimal decimoTerceiro = (funcionario.Salario / 12) * mesesTrabalhados;

            if (funcionario.Cargo == Cargo.Junior || funcionario.Cargo == Cargo.Senior)
            {
                var salarioAnual = (funcionario.Salario * mesesTrabalhados) + valorPLR + ferias + decimoTerceiro;
                return Ok(salarioAnual);
            }
            else
            {
                var salarioAnual = (funcionario.Salario * mesesTrabalhados) + calculoBonus + valorPLR + ferias + decimoTerceiro;
                return Ok(salarioAnual);
            }
        }


    }
}
