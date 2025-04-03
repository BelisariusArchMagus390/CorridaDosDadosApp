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
            for (int numero = 0; numero < arrayTamanho; numero++)
                vetorCaminhoPartidaStr[numero] = vetorCaminhoPartidaInt[numero].ToString();

            return vetorCaminhoPartidaStr;
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

                if (program.vetorCaminhoPartida[posicaoCaminho] == "0")
                {
                    posicao = numeroAleatorio.Next(0, 6);

                    program.vetorCaminhoPartida[posicaoCaminho] = vetorEventosEspeciais[posicao];
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

            if (program.resultadoDado == 6)
            {
                evento = "+turn";
                // string[] descricaoArray = eventosEspeciais[evento];
                descricao = eventosEspeciais[evento][1];

                Console.WriteLine($" Evento especial! {descricao}");
                program.turnoAtivo = false;
            }
            else if (program.vetorCaminhoPartida[posicao] != "0")
            {
                evento = program.vetorCaminhoPartida[posicao];

                arrayEfeito = eventosEspeciais[evento];
                efeito = int.Parse(arrayEfeito[0]);

                descricao = arrayEfeito[1];

                Console.WriteLine($" Evento especial! {descricao}");

                if (turno == "jogador")
                {
                    if (efeito == 0)
                        program.posicaoJogador -= program.resultadoDado;
                    else
                        program.posicaoJogador += efeito;
                }
                else if (turno == "cpu")
                {
                    if (efeito == 0)
                        program.posicaoCpu -= program.resultadoDado;
                    else
                        program.posicaoCpu += efeito;
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
                Console.Clear();
                Console.WriteLine(" ----- A CPU VENCEU!!! -----\n");

            program.posicaoJogador = 0;
            program.posicaoCpu = 0;

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

            criaEventosEspeciais();

            bool partida = false;

            // executa os turnos do jogador e da CPU (computador)
            while (partida == false)
            {
                int resultadoDado = 0;
                // executa o turno do jogador

                program.turnoAtivo = true;
                while (program.turnoAtivo == true)
                {
                    confirmacaoComando();

                    resultadoDado = dado();
                    Console.WriteLine($"\n Dado do jogador: {resultadoDado}");

                    program.posicaoJogador += resultadoDado;

                    if (program.posicaoJogador <= 30)
                        ifEventoEspecial(program.posicaoJogador, "jogador");

                    if (program.posicaoJogador < 30)
                        Console.WriteLine($" Posição do jogador: {program.posicaoJogador}");

                    if (program.turnoAtivo == false)
                        program.turnoAtivo = true;
                    else
                        program.turnoAtivo = false;
                }

                if (condicaoVitoria(program.posicaoJogador) == true)
                {
                    program.vitorioso = "jogador";
                    break;
                }

                // executa o turno da CPU
                program.turnoAtivo = true;
                while (program.turnoAtivo == true)
                {
                    resultadoDado = dado();
                    Console.WriteLine($"\n Dado da CPU: {resultadoDado}");

                    program.posicaoCpu += resultadoDado;

                    if (program.posicaoCpu <= 30)
                        ifEventoEspecial(program.posicaoCpu, "cpu");

                    if (program.posicaoCpu < 30)
                        Console.WriteLine($" Posição da CPU: {program.posicaoCpu}");

                    if (program.turnoAtivo == false)
                        program.turnoAtivo = true;
                    else
                        program.turnoAtivo = false;
                }

                if (condicaoVitoria(program.posicaoCpu) == true)
                {
                    program.vitorioso = "cpu";
                    break;
                }
            }

            visualizarVitoriaDerrota(program.vitorioso);
        }
    }
}
