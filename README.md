# Aplicativo de Jogo da Velha

O objetivo desse aplicativo é disponibilizar um jogo da velha (TicTacToe).

## Lógica do Jogo

O primeiro jogador que conseguir fazer uma linha completa na horizontal, vertical ou diagonal, vence o jogo. As jogadas são alternadas entre o jogador 01 (**X**) e o jogador 02 (**O**).

## Funcionalidade Extra (**DEVs**)

Atualmente, é possível definir o número de casas do jogo através de uma variável acessível somente via código. Aqueles que clonarem o repositório podem testar esse recurso, caso considerem interessante.

## Fim de Jogo

Ao término do jogo, ocorrerá uma de três possibilidades. Veja-as a seguir:

| Condição | Resultado |
| :- | :- |
| O jogador 01 (**X**) formou uma linha completa. | O jogador 01 (**X**) é o vencedor. |
| O jogador 02 (**O**) formou uma linha completa. | O jogador 02 (**O**) é o vencedor. |
| Todas as casas do jogo foram preenchidas e nenhuma linha foi formada. | Ocorreu um empate, ou seja, sem vencedor. |
