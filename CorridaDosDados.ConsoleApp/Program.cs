using System;

namespace CorridaDosDados.ConsoleApp
{
    internal class Program
    {
        // cria as variáveis globais
        static int posicaoJogador = 0;
        static int posicaoCpu = 0;
        static string vitorioso = "";
        static int resultadoDado = 0;
        static bool turnoAtivo = true;
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
        static string[] vetorCaminhoPartida = new string[30];

        static void constroiArrayVetorCaminhoPartida()
        {
            var program = new Program();
            // definindo arrays inteiro e string de vetorCaminhoPartida
            int[] vetorCaminhoPartidaInt = Enumerable.Repeat(0, 30).ToArray();
            int arrayTamanho = vetorCaminhoPartidaInt.Length;

            // construindo array vetorCaminhoPartida
            for (int numero = 0; numero < arrayTamanho; numero++)
                vetorCaminhoPartida[numero] = vetorCaminhoPartidaInt[numero].ToString();
        }

        // verifica a entrada do usuário
        static void confirmacaoComando()
        {
            bool comandoApertado = false;
            while (comandoApertado == false)
            {
                Console.Write("\n Vez do jogador rolar o dado: ");
                string comando = Console.ReadLine();

                if (comando == "")
                    comandoApertado = true;
                else
                    Console.WriteLine("\n Entrada incorreta");
            }
        }

        // cria o mapameanto dos eventos especiais da partida
        static void criaEventosEspeciais()
        {
            var program = new Program();
            // cria lista com o nome dos eventos especiais
            string[] vetorEventosEspeciais = eventosEspeciais.Keys.ToArray();

            // escolhe o número de eventos que podem ocorrer na partida, onde 20 é o máximo
            Random numeroAleatorio = new Random();
            int DeEventosEspeciais = numeroAleatorio.Next(1, 21);

            int posicaoCaminho = 0;
            int posicao = 0;

            // cria o vetor mapeado com o os eventos especiais
            for (int i = 0; i < DeEventosEspeciais; i++)
            {
                posicaoCaminho = numeroAleatorio.Next(0, 30);
                
                if (vetorCaminhoPartida[posicaoCaminho] == "0")
                {
                    posicao = numeroAleatorio.Next(0, 5);

                    vetorCaminhoPartida[posicaoCaminho] = vetorEventosEspeciais[posicao];
                }
            }
        }

        // verifica de se ocorreu ou não um evento e a aplicação do seu efeito caso seja detectado
        static void ifEventoEspecial(int posicao, string turno)
        {
            var program = new Program();
            string evento = "";
            string descricao = "";
            int efeito = 0;
            string[] arrayEfeito = new string[2];

            if (resultadoDado == 6)
            {
                arrayEfeito = eventosEspeciais[evento];
                evento = "+turn";
                // string[] descricaoArray = eventosEspeciais[evento];
                descricao = arrayEfeito[1];

                Console.WriteLine($" Evento especial! {descricao}");
                turnoAtivo = false;
            }
            else if ((posicao <= 29) && (vetorCaminhoPartida[posicao] != "0"))
            {
                evento = vetorCaminhoPartida[posicao];

                arrayEfeito = eventosEspeciais[evento];

                efeito = int.Parse(arrayEfeito[0]);
                descricao = arrayEfeito[1];

                Console.WriteLine($" Evento especial! {descricao}");

                if (turno == "jogador")
                {
                    if (efeito == 0)
                        posicaoJogador -= resultadoDado;
                    else
                        posicaoJogador += efeito;
                }
                else if (turno == "cpu")
                {
                    if (efeito == 0)
                        posicaoCpu -= resultadoDado;
                    else
                        posicaoCpu += efeito;
                }
            }
        }

        // rola o dado de 6 lados
        static int dado()
        {
            Random numeroAleatorio = new Random();
            return numeroAleatorio.Next(1, 7);
        }

        // verifica a condição de vitória
        static bool condicaoVitoria(int posicao)
        {
            if (posicao == 30 || posicao > 30)
                return true;
            else
                return false;
        }

        // faz a visualização da resolução da partida
        static void visualizarVitoriaDerrota(string turno)
        {
            var program = new Program();

            if (turno == "player")
            {
                Console.Clear();
                Console.WriteLine(" ----- O JOGADOR VENCEU!!! -----\n");
            }
            else if (turno == "cpu")
            {
                Console.Clear();
                Console.WriteLine(" ----- A CPU VENCEU!!! -----\n");
            }
                
            posicaoJogador = 0;
            posicaoCpu = 0;

            Console.WriteLine(" Obrigado por jogar!");

            Console.Write("\n Você quer jogar de novo? (S/N): ");
            char opcao = Console.ReadLine()[0];

            opcao = char.ToUpper(opcao);

            if (opcao == 'S')
                Main(new string[] { });
            else if (opcao == 'N')
                Environment.Exit(0);

        }

        static void Main(string[] args)
        {
            var program = new Program();

            constroiArrayVetorCaminhoPartida();
            criaEventosEspeciais();

            bool partida = false;

            // executa os turnos do jogador e da CPU (computador)
            while (partida == false)
            {
                int resultadoDado = 0;
                // executa o turno do jogador

                turnoAtivo = true;
                while (turnoAtivo == true)
                {
                    confirmacaoComando();

                    resultadoDado = dado();
                    Console.WriteLine($"\n Dado do jogador: {resultadoDado}");

                    posicaoJogador += resultadoDado;

                    if (posicaoJogador <= 30)
                        ifEventoEspecial(posicaoJogador, "jogador");

                    if (posicaoJogador < 30)
                        Console.WriteLine($" Posição do jogador: {posicaoJogador}");

                    if (turnoAtivo == false)
                        turnoAtivo = true;
                    else
                        turnoAtivo = false;
                }

                if (condicaoVitoria(posicaoJogador) == true)
                {
                    vitorioso = "jogador";
                    break;
                }

                // executa o turno da CPU
                turnoAtivo = true;
                while (turnoAtivo == true)
                {
                    resultadoDado = dado();
                    Console.WriteLine($"\n Dado da CPU: {resultadoDado}");

                    posicaoCpu += resultadoDado;

                    if (posicaoCpu <= 30)
                        ifEventoEspecial(posicaoCpu, "cpu");

                    if (posicaoCpu < 30)
                        Console.WriteLine($" Posição da CPU: {posicaoCpu}");

                    if (turnoAtivo == false)
                        turnoAtivo = true;
                    else
                        turnoAtivo = false;
                }

                if (condicaoVitoria(posicaoCpu) == true)
                {
                    vitorioso = "cpu";
                    break;
                }
            }

            visualizarVitoriaDerrota(vitorioso);
        }
    }
}
