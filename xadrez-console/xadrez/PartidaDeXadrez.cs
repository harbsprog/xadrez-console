﻿using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez {

    class PartidaDeXadrez {

        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;


        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino) {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();

        }

        public void validarPosicaoDeOrigem(Posicao pos) {
            if (tab.peca(pos) == null) {
                throw new TabuleiroException("Não Existe Peça na Posição de Origem Escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor) {
                throw new TabuleiroException("A Peça de Origem Escolhida Não é Sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()) {
                throw new TabuleiroException("Não há Movimentos Disponiveis Para a Peça de Origem Escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) {
            if (!tab.peca(origem).podeMoverPara(destino)) {
                throw new TabuleiroException("Posição de Destino Inválida!");
            }
        }

        public void mudaJogador() {
            if(jogadorAtual == Cor.Branca) {
                jogadorAtual = Cor.Preta;
            } else if(jogadorAtual == Cor.Preta) {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() {


            //Criando Peões Brancos
            //tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('a', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('b', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('f', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('g', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez('h', 2).toPosicao());

            //Criando Peões Pretos
            //tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('a', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('b', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('d', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('e', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('f', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('g', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez('h', 7).toPosicao());

            //Criando Torres Brancas
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));

            //Criando Torres Pretas
            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));

            //Criando Cavalos Brancos
            //tab.colocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('b', 1).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('g', 1).toPosicao());

            //Criando Cavalos Pretos
            //tab.colocarPeca(new Cavalo(tab, Cor.Preta), new PosicaoXadrez('b', 8).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Preta), new PosicaoXadrez('g', 8).toPosicao());

            //Criando Bispos Brancos
            //tab.colocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('f', 1).toPosicao());

            //Criando Bispos Pretos
            //tab.colocarPeca(new Bispo(tab, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Preta), new PosicaoXadrez('f', 8).toPosicao());

            //Criando Rei Branco
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            //Criando Rei Preto
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));

            //Criando Rainha/Dama Branca
            //tab.colocarPeca(new Dama(tab, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());

            //Criando Rainha/Dama Preta
            //tab.colocarPeca(new Dama(tab, Cor.Preta), new PosicaoXadrez('e', 8).toPosicao());

        }
    }
}
