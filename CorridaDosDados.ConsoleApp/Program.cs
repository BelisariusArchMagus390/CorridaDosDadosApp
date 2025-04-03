using System;

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
        static Dictionary<string, string[]> eventosEspeciais = new Dictionary<string, string[]>
        {
            { "broken_bridge", ["-2", "Você chegou em uma ponte quebrada e terá que dar a volta, -2 casas."] },
            { "paving_path", ["2", "Você chegou em uma rua pavimentada que facilita muito a sua corrida, +2 casas"] },
            { "ride", ["3", "Você encontrou um carro para te dar carona, +3 casas"] },
            { "congestion", ["-3", "Você encontrou um congestionamento no caminho que te atrasou muito, -3 casas"] },
            { "lost", ["0", "Você se perde no caminho, portanto volta onde parou, volta a posição anterior"] },
            { "+turn", ["+", "Você ultrapassou muito o outro competidor, mais 1 turno"] }
        };
        string[] vetorCaminhoPartida = constroiArrayVetorCaminhoPartida(); 

        static string[] constroiArrayVetorCaminhoPartida()
        {
            // definindo arrays inteiro e string de vetorCaminhoPartida
            int[] vetorCaminhoPartidaInt = Enumerable.Repeat(0, 30).ToArray();
            int arrayTamanho = vetorCaminhoPartidaInt.Length;
            string[] vetorCaminhoPartidaStr = new string[arrayTamanho];

            // construindo array vetorCaminhoPartida
            for (int numero = 0; numero == arrayTamanho; numero++)
                vetorCaminhoPartidaStr[numero] = vetorCaminhoPartidaInt[numero].ToString();

            return vetorCaminhoPartidaStr;
        }

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
        void criaEventosEspeciais()
        {
            // cria lista com o nome dos eventos especiais
            string[] vetorEventosEspeciais = eventosEspeciais.Keys.ToArray();

            // escolhe o número de eventos que podem ocorrer na partida, onde 20 é o máximo
            Random numeroAleatorio = new Random();
            int DeEventosEspeciais = numeroAleatorio.Next(1, 21);

            int posicaoCaminho = 0;
            int posicao = 0;

            // cria o vetor mapeada com o os eventos especiais
            for (int i = 0; i < DeEventosEspeciais; i++)
            {
                posicaoCaminho = numeroAleatorio.Next(0, 31);

                if (vetorCaminhoPartida[posicaoCaminho] == "0")
                {
                    posicao = numeroAleatorio.Next(0, 5);

                    vetorCaminhoPartida[posicaoCaminho] = vetorEventosEspeciais[posicao];
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
