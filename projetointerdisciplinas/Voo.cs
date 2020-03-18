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

        private void ReservarAssento(int assentoPassageiro, string v)
        {
            throw new NotImplementedException();
        }

        private void Atualizar_VooLotado()
        {
            throw new NotImplementedException();
        }

        private int AssentoPassageiro(string cPF)
        {
            throw new NotImplementedException();
        }
    }
}
