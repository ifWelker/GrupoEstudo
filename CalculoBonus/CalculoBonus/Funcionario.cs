namespace CalculoBonus
{

    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Salario { get; set; }
        public Cargo Cargo { get; set; }
        public Departamento Departamento { get; set; }

        public decimal CalcularPLR(decimal qtdSalariosPLR, decimal tetoPLR) => (Salario * qtdSalariosPLR) > tetoPLR ? tetoPLR : Salario * qtdSalariosPLR;
        public decimal CalcularBonusMinimo(decimal qtdSalariosPLR, decimal tetoPLR, int qtdBonusMinimo) => ((Salario * qtdBonusMinimo) - CalcularPLR(qtdSalariosPLR, tetoPLR));
        public decimal CalcularBonusMaximo(decimal qtdSalariosPLR, decimal tetoPLR, int qtdBonusMaximo) => ((Salario * qtdBonusMaximo) - CalcularPLR(qtdSalariosPLR, tetoPLR));
    }
}
