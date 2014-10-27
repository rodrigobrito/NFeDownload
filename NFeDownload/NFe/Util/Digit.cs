using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFeDownload.NFe.Util
{
    public class Digit
    {
        private static CultureInfo _ptBr = CultureInfo.GetCultureInfo("pt-BR");

        public static int Modulo11(string chaveNFE)
        {
            if (chaveNFE.Length != 43)
            {
                throw new Exception("Chave inválida, não é possível calcular o digito verificador");
            }
            const string baseCalculo = "4329876543298765432987654329876543298765432";
            var somaResultados = 0;

            for (var i = 0; i <= chaveNFE.Length - 1; i++)
            {
                var numNF = Convert.ToInt32(chaveNFE[i].ToString(_ptBr));
                var numBaseCalculo = Convert.ToInt32(baseCalculo[i].ToString(_ptBr));

                somaResultados += (numBaseCalculo * numNF);
            }

            var restoDivisao = (somaResultados % 11);
            var dv = 11 - restoDivisao;
            if ((dv == 0) || (dv > 9))
            {
                return 0;
            }
            return dv;
        }
    }
}
