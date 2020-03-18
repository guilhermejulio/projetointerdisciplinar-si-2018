using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace projetointerdisciplinar
{
    class Voo
    {
        private int numberReservations;
        Dictionary<int, string> seats = new Dictionary<int, string>();
        private Queue waitingLine = new Queue(5);

        public string status { get; private set; } = "Open fligth";

        public Voo()
        {
            for(int i =1; i<=10; i++)
                seats.Add(i, null);
        }

        public bool ReservaAssento(int numberSeat, string passenger_ID)
        {
            seats[numberSeat] = passenger_ID;
            numberReservations++;
            //chamar metodo de atualizar voo lotado

            return true;
        }

        public bool ExcluirReserva(string CPF)
        {
            if (seats.ContainsValue(CPF))
            {
                int assentoPassageiro = AssentoPassageiro(CPF);
                //chamada do metodo que retorna o assento do passageiro

                seats[assentoPassageiro] = null;
                numberReservations--;

                Atualizar_VooLotado(); //atualizando status do voo


                if (waitingLine.Count > 0) //reservando assento para passageiro na lista de espera.
                    ReservarAssento(assentoPassageiro, (string)waitingLine.Dequeue());

                return true;
            }

            return false;
        }
        public bool ReservarNaFila(string passageiro_CPF)
        {
            if (waitingLine.Count < 5) //Regra 3 Inclusão - verificação se a fila está lotada
            {
                if (!waitingLine.Contains(passageiro_CPF))
                {
                    waitingLine.Enqueue(passageiro_CPF);
                    return true;
                }
            }

            return false;
        }

        public bool PossuiReserva(string CPF)
        {
            if (seats.ContainsValue(CPF) || waitingLine.Contains(CPF))
                return true;
            return false;
        }

        public string Imprime()
        {
            return "\nNumero de reservas no voo: " + numberReservations +
            "\n\nRelação de passageiros: \n" + RelacaoDePassageiros() +
            "\n\nTamanho da fila de espera: " + waitingLine.Count +
            "\nPassageiros na fila de espera: \n" + RelacaoFila();

        }

        public string AssentosVagos()
        {
            string response = "";
            foreach (KeyValuePair<int, string> assentoPassageiro in seats)
            {
                if (assentoPassageiro.Value == null)
                {
                    response += "[ " + assentoPassageiro.Key + " ] ";
                }
            }
            return response;
        }

        public bool AssentoVago(int assento)
        {
            if (seats[assento] == null) // se o assento está vago retorna true
                return true;
            return false;
        }

        private string RelacaoFila()
        {
            string relacao = "";

            foreach (string passageiro in waitingLine)
            {
                relacao += "\nCPF do passageiro: " + passageiro;
            }

            return relacao;
        }

        private string RelacaoDePassageiros()
        {
            string relacao = "";

            foreach (KeyValuePair<int, string> assentoPassageiro in seats)
            {
                if (assentoPassageiro.Value == null) // se não possui ninguem no assento.
                    relacao += "\nNumero do assento: [" + assentoPassageiro.Key + "] ##  Assento Vago";
                else
                    relacao += "\nNumero do assento: [" + assentoPassageiro.Key + "] ##  CPF do passageiro: [" + assentoPassageiro.Value + "]";
            }

            return relacao;
        }

        private void ReservarAssento(int assentoPassageiro, string v)
        {
            throw new NotImplementedException();
        }

        private bool Atualizar_VooLotado()
        {
            // se contem assento com Valor nulo = false existe assento vago e o voo não está lotado
            if (seats.ContainsValue(null))
            {
                status = "Existem vagas";
                return false;
            }
            else
            {
                if (waitingLine.Count < 5)
                {
                    status = "Vagas na fila";
                    return false;
                }
            }
            status = "Voo lotado";
            return true;
        }

        private int AssentoPassageiro(string CPF)
        {
            foreach (KeyValuePair<int, string> assentoPassageiro in seats)
            {
                if (assentoPassageiro.Value == CPF)
                {
                    return assentoPassageiro.Key;
                }
            }
            return 0;
        }

        public bool VooLotado()
        {
            if (seats.ContainsValue(null))
                return false;
            return true;
        }

        public int GetNumeroReservas()
        {
            return numberReservations;
        }


    }
}
