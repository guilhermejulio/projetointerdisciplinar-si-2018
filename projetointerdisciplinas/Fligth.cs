using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace projetointerdisciplinar
{
    /// <summary>
    /// Classe que é usada pelo conjunto de Voo da linha aerea, a classe é a representação de 1 Voo
    /// </summary>
    class Fligth
    {
        private int numberReservations;
        Dictionary<int, string> seats = new Dictionary<int, string>();
        private Queue waitingLine = new Queue(5);
        public string status { get; private set; } = "Existem vagas no Voo";

        /// <summary>
        /// Construtor da classe Voo, inicializa todos os assentos com um int como key, e null para o valor,
        /// ou seja, quando o passageiro é nullo não existe passageiro no assento.  
        /// null neste caso é igual a vago
        /// </summary>
        public Fligth()
        {
            for(int i =1; i<=10; i++)
                seats.Add(i, null);
        }

        /// <summary>
        /// Metodo de reserva de assento, retorna a resposta da solicitação por meio de um booleano
        /// </summary>
        /// <param name="numberSeat">Numero do assento</param>
        /// <param name="passenger_ID">Documento do passageiro, CPF</param>
        /// <returns></returns>
        public bool ReserveSeat(int numberSeat, string passenger_ID)
        {
            seats[numberSeat] = passenger_ID;
            numberReservations++;
            Atualizar_VooLotado();

            return true;
        }

        /// <summary>
        /// Metodo que deleta uma reserva no Voo
        /// </summary>
        /// <param name="passenger_ID">Documento do passageiro, CPF</param>
        /// <returns></returns>
        public bool DeleteReservation(string passenger_ID)
        {
            if (seats.ContainsValue(passenger_ID))
            {
                int assentoPassageiro = AssentoPassageiro(passenger_ID);
                //chamada do metodo que retorna o assento do passageiro

                seats[assentoPassageiro] = null;
                numberReservations--;

                Atualizar_VooLotado(); 


                if (waitingLine.Count > 0) //reservando assento para passageiro na lista de espera.
                    ReservarAssento(assentoPassageiro, (string)waitingLine.Dequeue());

                return true;
            }

            return false;
        }

        /// <summary>
        /// Metodo que reserva um passageiro na fila de espera
        /// </summary>
        /// <param name="passenger_ID">Documento do passageiro, CPF</param>
        /// <returns></returns>
        public bool ReservarNaFila(string passenger_ID)
        {
            if (waitingLine.Count < 5) //Regra 3 Inclusão - verificação se a fila está lotada
            {
                if (!waitingLine.Contains(passenger_ID))
                {
                    waitingLine.Enqueue(passenger_ID);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Metodo que retorna se um determinado passageiro possui reserva no Voo
        /// </summary>
        /// <param name="passenger_ID">Documento do passageiro, CPF</param>
        /// <returns></returns>
        public bool PossuiReserva(string passenger_ID)
        {
            if (seats.ContainsValue(passenger_ID) || waitingLine.Contains(passenger_ID))
                return true;
            return false;
        }

        /// <summary>
        /// Metodo que imprime detalhes sobre o Voo
        /// </summary>
        /// <returns></returns>
        public string Imprime()
        {
            return "\nNumero de reservas no voo: " + numberReservations +
            "\n\nRelação de passageiros: \n" + RelacaoDePassageiros() +
            "\n\nTamanho da fila de espera: " + waitingLine.Count +
            "\nPassageiros na fila de espera: \n" + QueueRelationship();

        }

        /// <summary>
        /// Metodo que retorna uma string de todos os assentos vagos
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo que retorna se um determinado assento está vago
        /// </summary>
        /// <param name="seat">Numero do assento</param>
        /// <returns></returns>
        public bool VacantSeat(int seat)
        {
            if (seats[seat] == null) // se o assento está vago retorna true
                return true;
            return false;
        }

        /// <summary>
        /// Metodo que retorna a relação dos passageiros da fila
        /// </summary>
        /// <returns></returns>
        private string QueueRelationship()
        {
            string relacao = "";

            foreach (string passageiro in waitingLine)
            {
                relacao += "\nCPF do passageiro: " + passageiro;
            }

            return relacao;
        }

        /// <summary>
        /// Metodo que retorna a relação de passageiros do voo
        /// </summary>
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

        /// <summary>
        /// Metodo que realiza a reserva de um assento no Voo
        /// </summary>
        /// <param name="assentoPassageiro">Assento escolhido</param>
        /// <param name="passenger_ID">Documento do passageiro</param>
        private bool ReservarAssento(int assentoPassageiro, string passenger_ID)
        {
            seats[assentoPassageiro] = passenger_ID;
            numberReservations++;

            Atualizar_VooLotado(); //atualizar status do voo.


            return true;
        }

        /// <summary>
        /// Metodo de verificação sobre se o voo está lotado
        /// </summary>
        /// <returns></returns>
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

        public int NumeroDeAssentosVagos()
        {
            int assentos_vagos = 0;

            foreach (KeyValuePair<int, string> aux in seats)
            {
                if (aux.Value == null)
                    assentos_vagos++;
            }
            return assentos_vagos;
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

        public int NumeroDeVagasNaFila()
        {
            return 5 - waitingLine.Count;
        }


    }
}
