﻿

int[] rodadas = new int[99];
int qtd_jogadores, qtd_cartelas;
do
{
    Console.WriteLine("Digite a quantidade de jogadores: ");
    qtd_jogadores = int.Parse(Console.ReadLine()); 
    if(qtd_jogadores < 2)
    {
        Console.WriteLine("Minimo de 2 jogadores para inicar");
    }
} while(qtd_jogadores < 2);
do
{
    Console.WriteLine("Digite a quantidade de cartelas: ");
    qtd_cartelas = int.Parse(Console.ReadLine());
    if (qtd_cartelas < 1)
    {
        Console.WriteLine("Minimo de 1 cartela para inicar");
    }
} while (qtd_jogadores < 1);
int[,][,]cartelas_jogadores = new int[qtd_jogadores,qtd_cartelas][,]; //Todas as Cartelas de Todos os jogadores
int[,][,]matriz_aux = new int[qtd_jogadores,qtd_cartelas][,]; //Matrizes Inicializadas em 0 porem quando o numero sorteado existir no espelho dela troca pra 1
int[] pontos_jogadores = new int[qtd_jogadores];
int contador_rodadas = 0;
int rodada_ganhou_linha = 0;
int rodada_ganhou_coluna = 0;
int jogador_ganhou_linha = 0;
int jogador_ganhou_coluna = 0;
int jogador_ganhou_bingo = 0;
int rodada_ganhou_bingo = 0;
bool bingo = false, ganhou_coluna = false,ganhou_linha = false;


void Imprimir_Matrizes(int[,][,] jogadores_cartelas, int[,][,] matriz_aux)
{
    for (int jogadores = 0; jogadores < qtd_jogadores; jogadores++)
    {
        Console.WriteLine($"Jogador {jogadores + 1}");

        for (int i = 0; i < 5; i++)
        {
            for (int cartelas = 0; cartelas < qtd_cartelas; cartelas++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (j == 0)
                    {
                        Console.Write("| ");
                    }

                    if (matriz_aux[jogadores, cartelas][i, j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("{0:00} ", jogadores_cartelas[jogadores, cartelas][i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("{0:00} ", jogadores_cartelas[jogadores, cartelas][i, j]);
                    }

                    if (j == 4)
                    {
                        Console.Write(" |");
                    }
                }

                if (cartelas != qtd_cartelas - 1)
                {
                    Console.Write("   "); // Espaço entre as cartelas
                }
            }

            

            if (i != 4)
            {
                Console.WriteLine();
            }
            
        }

        Console.WriteLine();
    }
}

int[] gerar_vetores()
{
    int[] popular_matrizes = new int[99];
    for (int i = 1; i < popular_matrizes.Length; i++)
    {
        popular_matrizes[i] = i;
    }
    return popular_matrizes;
}
int[] Numeros_Sorteados(int tamanho_vetor)
{
    int indice = 0;
    int[] numeros_sorteados = new int[tamanho_vetor];
    while (indice < tamanho_vetor)
    {
        int sorteio = new Random().Next(1, 100);
        bool numero_repetido = false;
        for (int i = 0; i < indice; i++)
        {

            if (sorteio == numeros_sorteados[i])
            {
                numero_repetido = true;
                break;
            }
        }
        if (!numero_repetido)
        {
            numeros_sorteados[indice] = sorteio;
            indice++;
        }
    }
    //for (int i = 0; i < numeros_sorteados.Length; i++)
    //    Console.Write(numeros_sorteados[i] + " ");
    return numeros_sorteados;
}
int[,][,] Matriz_Jogador_Cartela()
{
    int[,][,] cartelas_jogadores = new int[qtd_jogadores, qtd_cartelas][,];

    for (int jogadores = 0; jogadores < qtd_jogadores; jogadores++)
    {
        for (int cartelas = 0; cartelas < qtd_cartelas; cartelas++)
        {
            //Criar as cartelas pra cada jogador
            int[] popular_matrizes = gerar_vetores();
            int linha = 0, coluna = 0;

            cartelas_jogadores[jogadores, cartelas] = new int[5, 5];
            //Preencher todas as linhas e colunas
            while (linha < 5)
            {
                int j = new Random().Next(0, popular_matrizes.Length);
                if (popular_matrizes[j] != 0)
                {
                    cartelas_jogadores[jogadores, cartelas][linha, coluna] = popular_matrizes[j];
                    popular_matrizes[j] = 0;
                    coluna++;
                    if (coluna > 4)
                    {
                        linha++;
                        coluna = 0;
                    }
                }
            }

            matriz_aux[jogadores, cartelas] = new int[5, 5];
        }
    }

    return cartelas_jogadores;
}
cartelas_jogadores = Matriz_Jogador_Cartela();
//Imprimir_Matrizes(cartelas_jogadores, matriz_aux);
rodadas = Numeros_Sorteados(99);
Console.WriteLine(rodadas.Length);
void Verificar_Vetores(int[,][,] matriz, ref int[,][,] matriz_auxiliar_1, int[] pontos)
{
    
    
    int[] vencedor = new int[qtd_jogadores];
    for (int contador_rodadas = 0; contador_rodadas < rodadas.Length && !bingo; contador_rodadas++)
    {
         
        for (int jogadores = 0; jogadores < qtd_jogadores; jogadores++)
        {
            for (int cartelas = 0; cartelas < qtd_cartelas; cartelas++)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {

                        if (rodadas[contador_rodadas] == matriz[jogadores, cartelas][i, j])
                        {
                            matriz_auxiliar_1[jogadores, cartelas][i, j] = 1;

                        }
                    }
                }
            }
        }
        for (int jogadores = 0; jogadores < qtd_jogadores && !bingo; jogadores++)
        {
            for (int cartelas = 0; cartelas < qtd_cartelas && !bingo; cartelas++)
            {
                int contador = 0;
                bool cartela_cheia = false;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (matriz_auxiliar_1[jogadores, cartelas][i, j] == 1)
                        {
                            contador++;
                        }
                        if (contador == 25)
                        {
                            cartela_cheia = true;
                            break;
                        }
                        

                    }
                }
                if (cartela_cheia)
                {
                    pontos[jogadores] += 5;
                    rodada_ganhou_bingo = contador_rodadas + 1;
                    jogador_ganhou_bingo = jogadores + 1;
                    bingo = true;
                    Console.WriteLine($"Jogador {jogador_ganhou_bingo} fez bingo na rodada {rodada_ganhou_bingo}");
                
                    break;
                    
                }
            }
        }
        for (int jogadores = 0; jogadores < qtd_jogadores && !ganhou_coluna; jogadores++)
        {
            for (int cartelas = 0; cartelas < qtd_cartelas && !ganhou_coluna; cartelas++)
            {
                for (int i = 0; i < 5; i++)
                {
                    bool completou_coluna = false;
                    for (int j = 0; j < 5; j++)
                    {
                        if (matriz_auxiliar_1[jogadores, cartelas][j, i] != 1)
                        {
                            break;
                        }
                        if (j == 4)
                        {
                            completou_coluna = true;
                        }
                    }
                    if (completou_coluna)
                    {
                        jogador_ganhou_coluna = jogadores + 1;
                        pontos[jogadores] += 1;
                        rodada_ganhou_coluna = contador_rodadas + 1;
                        ganhou_coluna = true;
                        Console.WriteLine($"Jogador {jogadores} ganhou Coluna na rodada {rodada_ganhou_coluna}");
                        break;
                    }
                }
            }
        }

        for (int jogadores = 0; jogadores < qtd_jogadores && !ganhou_linha; jogadores++)
        {
            for (int cartelas = 0; cartelas < qtd_cartelas && !ganhou_linha; cartelas++)
            {
                for (int i = 0; i < 5; i++)
                {
                    bool completou_linha = false;
                    for (int j = 0; j < 5; j++)
                    {
                        if (matriz_auxiliar_1[jogadores, cartelas][i, j] != 1)
                        {
                            break;
                        }
                        if (j == 4)
                        {
                            completou_linha = true;
                        }
                    }
                    if (completou_linha)
                    {
                        jogador_ganhou_linha = jogadores + 1;
                        pontos[jogadores] += 1;
                        rodada_ganhou_linha = contador_rodadas + 1;
                        ganhou_linha = true;
                        Console.WriteLine($"Jogador {jogadores} ganhou Linha na rodada {rodada_ganhou_linha}");
                        break;
                    }
                }
            }
        }

     
        Console.WriteLine($"{contador_rodadas + 1}° Rodada\nNumero Sorteado -> {rodadas[contador_rodadas]}");
        Imprimir_Matrizes(matriz, matriz_auxiliar_1);
        
       
    }
    Console.WriteLine("============= BINGOOOOOOOOOOOOO =============");
}

Verificar_Vetores(cartelas_jogadores, ref matriz_aux,pontos_jogadores);
Console.WriteLine($"O Jogador {jogador_ganhou_linha} ganhou linha na rodada {rodada_ganhou_linha}");
Console.WriteLine($"O Jogador {jogador_ganhou_coluna } ganhou coluna na rodada {rodada_ganhou_coluna}");
Console.WriteLine($"O Jogador {jogador_ganhou_bingo} fez bingo na rodada {rodada_ganhou_bingo}");
for (int i = 0; i < qtd_jogadores; i++)
{
    Console.WriteLine(pontos_jogadores[i]);
}

//for (contador_rodadas = 0; contador_rodadas < rodadas.Length; contador_rodadas++)
//{
//    for (int i = 0; i < qtd_jogadores; i++)
//    {
//        for (int j = 0; j < qtd_cartelas; j++)
//        {
//            Verificar_Vetores(vetor_cartelas[i][j], ref matriz_aux[i][j]);
//            Console.WriteLine("Jogador " + i + " cartela " + j);
//        }
//    }
//    Console.ReadKey();
//}



//Console.ReadKey();

//for (int i =0; i < qtd_jogadores;i++)
//{
//    for (int j = 0;j < qtd_cartelas; j++)
//    {
//        Console.WriteLine($"Jogador {i + 1} -- Cartela {j + 1} ");
//        Imprimir_Matrizes(vetor_cartelas[i][j], matriz_aux[i][j]);
//    }

//    Console.WriteLine();
//}
