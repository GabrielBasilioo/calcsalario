using System;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppTeste.Models;

namespace MauiApp1.ViewModels
{
    public class SalarioViewModel : ObservableObject
    {
        private readonly Salario _salario = new Salario();

        private decimal _salarioBruto;
        private decimal _descontoINSS;
        private decimal _descontoIR;
        private decimal _salarioLiquido;

        public SalarioViewModel()
        {
            CalcularSalarioCommand = new RelayCommand(
                execute: CalcularSalario,
                canExecute: PodeCalcular
            );

            HorasSemanaString = string.Empty;
            ValorHoraString = string.Empty;
        }

        public IRelayCommand CalcularSalarioCommand { get; }

        private string _horasSemanaString;
        public string HorasSemanaString
        {
            get => _horasSemanaString;
            set
            {
                if (SetProperty(ref _horasSemanaString, value))
                {
                    if (TryParseDecimal(value, out var parsed))
                        _salario.HorasSemana = parsed;
                    else
                        _salario.HorasSemana = 0m;

                    CalcularSalarioCommand.NotifyCanExecuteChanged();
                    AtualizarResultados();
                }
            }
        }

        private string _valorHoraString;
        public string ValorHoraString
        {
            get => _valorHoraString;
            set
            {
                if (SetProperty(ref _valorHoraString, value))
                {
                    if (TryParseDecimal(value, out var parsed))
                        _salario.ValorHora = parsed;
                    else
                        _salario.ValorHora = 0m;

                    CalcularSalarioCommand.NotifyCanExecuteChanged();
                    AtualizarResultados();
                }
            }
        }

        public decimal SalarioBruto
        {
            get => _salarioBruto;
            private set => SetProperty(ref _salarioBruto, value);
        }

        public decimal DescontoINSS
        {
            get => _descontoINSS;
            private set => SetProperty(ref _descontoINSS, value);
        }

        public decimal DescontoIR
        {
            get => _descontoIR;
            private set => SetProperty(ref _descontoIR, value);
        }

        public decimal SalarioLiquido
        {
            get => _salarioLiquido;
            private set => SetProperty(ref _salarioLiquido, value);
        }

        private void CalcularSalario()
        {
            SalarioBruto = _salario.SalarioBruto;
            DescontoINSS = _salario.DescontoINSS;
            DescontoIR = _salario.DescontoIR;
            SalarioLiquido = _salario.SalarioLiquido;
        }

        private void AtualizarResultados()
        {
            SalarioBruto = _salario.SalarioBruto;
            DescontoINSS = _salario.DescontoINSS;
            DescontoIR = _salario.DescontoIR;
            SalarioLiquido = _salario.SalarioLiquido;
        }

        private bool PodeCalcular()
        {
            return _salario.HorasSemana > 0m && _salario.ValorHora > 0m;
        }

        private bool TryParseDecimal(string s, out decimal result)
        {
            result = 0m;
            if (string.IsNullOrWhiteSpace(s))
                return false;

            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out result))
                return true;
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
                return true;

            var cleaned = s.Trim();
            return decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.CurrentCulture, out result)
                || decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }
    }
}
