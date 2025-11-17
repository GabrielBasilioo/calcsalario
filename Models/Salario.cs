using System;

namespace AppTeste.Models
{
    public class Salario
    {
        public decimal HorasSemana { get; set; }
        public decimal ValorHora { get; set; }

        public decimal HorasMes => HorasSemana * 4.33m;
        public decimal SalarioBruto => HorasMes * ValorHora;

        // INSS
        public decimal DescontoINSS => CalcularINSS(SalarioBruto);

        // Base para IR (SalarioBruto - INSS)
        public decimal BaseIR => SalarioBruto - DescontoINSS;

        // IR
        public decimal DescontoIR => CalcularIR(BaseIR);

        // Salário líquido
        public decimal SalarioLiquido => SalarioBruto - DescontoINSS - DescontoIR;

        private decimal CalcularINSS(decimal salario)
        {
            if (salario > 6433.57m)
                return 751.97m; // teto INSS

            if (salario <= 1100.00m)
                return salario * 0.075m;
            else if (salario <= 2203.48m)
                return salario * 0.09m - 16.50m;
            else if (salario <= 3305.22m)
                return salario * 0.12m - 82.60m;
            else // até 6433.57
                return salario * 0.14m - 148.70m;
        }

        private decimal CalcularIR(decimal baseCalculo)
        {
            if (baseCalculo <= 2259.20m)
                return 0m;
            else if (baseCalculo <= 2826.65m)
                return baseCalculo * 0.075m - 169.44m;
            else if (baseCalculo <= 3751.05m)
                return baseCalculo * 0.15m - 381.44m;
            else if (baseCalculo <= 4664.68m)
                return baseCalculo * 0.225m - 662.77m;
            else
                return baseCalculo * 0.275m - 896.00m;
        }
    }
}
