namespace CorridaDosDados.ConsoleApp
{
    internal class Program
    {
        // cria as variáveis globais
        int posicaoJogador = 0;
        int posicaoCpu = 0;
        string vitorioso = "";
        int resultadoDado = 0;
        bool turnoAtivo = true;
        // cria dicionário que guarda os eventos especiais da partida
        Dictionary<string, string[]> eventosEspeciais = new Dictionary<string, string[]>
        {
            { "broken_bridge", ["-2", "Você chegou em uma ponte quebrada e terá que dar a volta, -2 casas."] },
            { "paving_path", ["2", "Você chegou em uma rua pavimentada que facilita muito a sua corrida, +2 casas"] },
            { "ride", ["3", "Você encontrou um carro para te dar carona, +3 casas"] },
            { "congestion", ["-3", "Você encontrou um congestionamento no caminho que te atrasou muito, -3 casas"] },
            { "lost", ["0", "Você se perde no caminho, portanto volta onde parou, volta a posição anterior"] },
            { "+turn", ["+", "Você ultrapassou muito o outro competidor, mais 1 turno"] }
        };

        // verifica a entrada do usuário
        static void confirmacaoComando()
        {
            bool comandoApertado = false;
            while (comandoApertado == false)
            {
                Console.Write("\nVez do jogador rolar o dado: ");
                string comando = Console.ReadLine();

                if (comando == "")
                    comandoApertado = true;
                else
                    Console.WriteLine("\nEntrada incorreta");
            }
        }

        // cria o mapameanto dos eventos especiais da partida

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
