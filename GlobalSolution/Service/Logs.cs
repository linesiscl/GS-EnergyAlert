using System;
using System.IO;

namespace GlobalSolution.Service
{
    public static class Logs
    {
        private static readonly string CaminhoLog = "C:\\Users\\Aline\\Desktop\\facul\\3ESR\\c#\\GlobalSolution\\GlobalSolution\\logs.txt";

        public static void Registrar(string mensagem)
        {
            var texto = $"[{DateTime.Now}] {mensagem}{Environment.NewLine}";
            File.AppendAllText(CaminhoLog, texto);
        }
    }
}
