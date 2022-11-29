namespace CalculoBonus
{

    public class Funcionario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public decimal Salario { get; set; }

        public Cargo Cargo { get; set; }

        public Departamento Departamento { get; set; }
    }
}
