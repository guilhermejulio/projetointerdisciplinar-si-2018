using System.Collections;
using System.Collections.Generic;

namespace projetointerdisciplinar
{
    /// <summary>
    /// Classe que é usada pelo conjunto de Voo da linha aerea, a classe é a representação de 1 Voo
    /// </summary>
    class Fligth
    {
        #region Atributos
        private int numberReservations;
        Dictionary<int, string> seats = new Dictionary<int, string>();
        private Queue waitingLine = new Queue(5);
        public string status { get; private set; } = "Existem vagas no Voo";
        #endregion

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

        #region Metodos Principais
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
            UpdateFligth();

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
                int passengerSeat = PassengerSeat(passenger_ID);
                //chamada do metodo que retorna o assento do passageiro

                seats[passengerSeat] = null;
                numberReservations--;

                UpdateFligth();


                if (waitingLine.Count > 0) //reservando assento para passageiro na lista de espera.
                    ReserveSeat(passengerSeat, (string)waitingLine.Dequeue());

                return true;
            }

            return false;
        }

        /// <summary>
        /// Metodo que reserva um passageiro na fila de espera
        /// </summary>
        /// <param name="passenger_ID">Documento do passageiro, CPF</param>
        /// <returns></returns>
        public bool BookInLine(string passenger_ID)
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
        /// Metodo que retorna uma string de todos os assentos vagos
        /// </summary>
        /// <returns></returns>
        public string VacantSeats()
        {
            string response = "";
            foreach (KeyValuePair<int, string> passengerSeat in seats)
            {
                if (passengerSeat.Value == null)
                {
                    response += "[ " + passengerSeat.Key + " ] ";
                }
            }
            return response;
        }

        #endregion

        #region Metodos extra
        /// <summary>
        /// Metodo que retorna se um determinado passageiro possui reserva no Voo
        /// </summary>
        /// <param name="passenger_ID">Documento do passageiro, CPF</param>
        /// <returns></returns>
        public bool hasReservation(string passenger_ID)
        {
            if (seats.ContainsValue(passenger_ID) || waitingLine.Contains(passenger_ID))
                return true;
            return false;
        }

        /// <summary>
        /// Metodo que imprime detalhes sobre o Voo
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return "\nNumero de reservas no voo: " + numberReservations +
            "\n\nRelação de passageiros: \n" + PassengerList() +
            "\n\nTamanho da fila de espera: " + waitingLine.Count +
            "\nPassageiros na fila de espera: \n" + QueueRelationship();

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
            string relationship = "";

            foreach (string passenger in waitingLine)
            {
                relationship += "\nCPF do passageiro: " + passenger;
            }

            return relationship;
        }

        /// <summary>
        /// Metodo que retorna a relação de passageiros do voo
        /// </summary>
        private string PassengerList()
        {
            string relationship = "";

            foreach (KeyValuePair<int, string> passengerSeat in seats)
            {
                if (passengerSeat.Value == null) // se não possui ninguem no assento.
                    relationship += "\nNumero do assento: [" + passengerSeat.Key + "] ##  Assento Vago";
                else
                    relationship += "\nNumero do assento: [" + passengerSeat.Key + "] ##  CPF do passageiro: [" + passengerSeat.Value + "]";
            }

            return relationship;
        }

        /// <summary>
        /// Metodo que retorna o numero do assento de um determinado passageiro
        /// </summary>
        /// <param name="passenger_ID"></param>
        /// <returns></returns>
        private int PassengerSeat(string passenger_ID)
        {
            foreach (KeyValuePair<int, string> passengerSeat in seats)
            {
                if (passengerSeat.Value == passenger_ID)
                {
                    return passengerSeat.Key;
                }
            }
            return 0;
        }

        /// <summary>
        /// Metodo que retorna o numero de assentos vagos do Voo
        /// </summary>
        /// <returns></returns>
        public int EmptySeats()
        {
            int emptySeats = 0;

            foreach (KeyValuePair<int, string> aux in seats)
            {
                if (aux.Value == null)
                    emptySeats++;
            }
            return emptySeats;
        }

        /// <summary>
        /// Metodo que retorna o numero total de vagas na fila de espera
        /// </summary>
        /// <returns></returns>
        public int NumberSeatsQueue()
        {
            return 5 - waitingLine.Count;
        }

        /// <summary>
        /// Metodo que retorna o numero total de reservas do Voo
        /// </summary>
        /// <returns></returns>
        public int GetNumberReservations()
        {
            return numberReservations;
        }

        /// <summary>
        /// Metodo que retorna se o Voo está lotado
        /// </summary>
        /// <returns></returns>
        public bool FullFligth()
        {
            if (seats.ContainsValue(null))
                return false;
            return true;
        }
        #endregion

        #region Metodos auxiliares - usados apenas na classe
        /// <summary>
        /// Metodo de verificação sobre se o voo está lotado
        /// </summary>
        /// <returns></returns>
        private bool UpdateFligth()
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

        #endregion

    }
}
