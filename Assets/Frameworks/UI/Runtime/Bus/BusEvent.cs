using System;
using System.Threading;
using System.Threading.Tasks;

namespace EblanDev.ScenarioCore.UIFramework.Bus
{
    /// <summary>
    /// Обработчик событий для шины.
    /// </summary>
    public class BusEvent
    {
        /// <summary>
        /// Событие на которое можно подписаться.
        /// </summary>
        public event Action E;
        
        private bool Invoked = false;

        /// <summary>
        /// Invoke события.
        /// </summary>
        public void Invoke()
        {
            Invoked = true;
            E?.Invoke();
        }

        /// <summary>
        /// Ожидание первого после начала ожидания Invoke.
        /// </summary>
        /// <param name="token">
        /// Токен для сопровождения эвейтера.
        /// </param>
        public async Task Awaiter(CancellationToken token)
        {
            while (Invoked == false)
            {
                await Task.Yield();
                if (token.IsCancellationRequested)
                {
                    Invoked = false;
                    return;
                }
            }
            Invoked = false;
        }
    }
    
    /// <summary>
    /// Обработчик событий для шины.
    /// </summary>
    /// <typeparam name="T">
    /// Тип с которым работает ивент.
    /// </typeparam>
    public class BusEvent<T> : BusEvent
    {
        /// <summary>
        /// Последняя дата с которой инвокалось событие.
        /// </summary>
        public T LastV;
        
        /// <summary>
        /// Инвок события.
        /// </summary>
        /// <param name="data">
        /// Инвок дата
        /// </param>
        public void Invoke(T data)
        {
            LastV = data;
            Invoke();
        }
    }

    /// <summary>
    /// Обработчик событий для шины.
    /// </summary>
    /// <typeparam name="T">
    /// Тип с которым работает ивент.
    /// </typeparam>
    /// <typeparam name="T1">
    /// Тип1 с которым работает ивент.
    /// </typeparam>
    public class BusEvent<T, T1> : BusEvent<T>
    {
        /// <summary>
        /// Последняя дата 1 с которой инвокалось событие.
        /// </summary>
        public T1 LastV1;

        /// <summary>
        /// Инвок события.
        /// </summary>
        /// <param name="data">
        /// Инвок дата
        /// </param>
        /// <param name="data1">
        /// Инвок дата1
        /// </param>
        public void Invoke(T data, T1 data1)
        {
            LastV1 = data1;
            Invoke(data);
        }
    }

    /// <summary>
    /// Обработчик событий для шины.
    /// </summary>
    /// <typeparam name="T">
    /// Тип с которым работает ивент.
    /// </typeparam>
    /// <typeparam name="T1">
    /// Тип1 с которым работает ивент.
    /// </typeparam>
    /// <typeparam name="T2">
    /// Тип2 с которым работает ивент.
    /// </typeparam>
    public class BusEvent<T, T1, T2> : BusEvent<T, T1>
    {
        /// <summary>
        /// Последняя дата 1 с которой инвокалось событие.
        /// </summary>
        public T2 LastV2;

        /// <summary>
        /// Инвок события.
        /// </summary>
        /// <param name="data">
        /// Инвок дата
        /// </param>
        /// <param name="data1">
        /// Инвок дата1
        /// </param>
        /// <param name="data2">
        /// Инвок дата2
        /// </param>
        public void Invoke(T data, T1 data1, T2 data2)
        {
            LastV2 = data2;
            Invoke(data, data1);
        }
    }
}