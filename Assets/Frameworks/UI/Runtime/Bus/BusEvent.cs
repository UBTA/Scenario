using System;
using System.Threading;
using System.Threading.Tasks;

namespace EblanDev.ScenarioCore.UIFramework.Bus
{
    /// <summary>
    /// Обработчик событий для шины.
    /// </summary>
    /// <typeparam name="T">
    /// Тип с которым работает ивент.
    /// </typeparam>
    public sealed class BusEvent<T>
    {
        /// <summary>
        /// Событие на которое можно подписаться.
        /// </summary>
        public event Action<T> E;
        /// <summary>
        /// Последняя дата с которой инвокалось событие.
        /// </summary>
        public T LastInvokeData;

        private bool Invoked = false;

        /// <summary>
        /// Invoke события.
        /// </summary>
        public void Invoke()
        {
            Invoked = true;
            E?.Invoke(default);
        }

        /// <summary>
        /// Invoke события.
        /// </summary>
        /// <param name="data"></param>
        public void Invoke(T data)
        {
            LastInvokeData = data;
            Invoked = true;
            E?.Invoke(data);
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
    /// Первый тип с которым работает ивент.
    /// </typeparam>
    /// <typeparam name="T1">
    /// Второй тип с которым работает ивент.
    /// </typeparam>
    public sealed class BusEvent<T, T1>
    {
        /// <summary>
        /// Событие на которое можно подписаться.
        /// </summary>
        public event Action<T, T1> E;

        private bool Invoked = false;

        /// <summary>
        /// Последняя первая дата с которой инвокалось событие.
        /// </summary>
        public T LastInvokeData;
        /// <summary>
        /// Последняя вторая дата с которой инвокалось событие.
        /// </summary>
        public T1 LastInvokeData1;

        /// <summary>
        /// Invoke события.
        /// </summary>
        public void Invoke()
        {
            Invoked = true;
            E?.Invoke(default, default);
        }

        /// <summary>
        /// Invoke события.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="data1"></param>
        public void Invoke(T data, T1 data1)
        {
            LastInvokeData = data;
            LastInvokeData1 = data1;
            Invoked = true;
            E?.Invoke(data, data1);
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
    /// Первый тип с которым работает ивент.
    /// </typeparam>
    /// <typeparam name="T1">
    /// Второй тип с которым работает ивент.
    /// </typeparam>
    /// <typeparam name="T2">
    /// Третий тип с которым работает ивент.
    /// </typeparam>
    public sealed class BusEvent<T, T1, T2>
    {
        /// <summary>
        /// Событие на которое можно подписаться.
        /// </summary>
        public event Action<T, T1, T2> E;

        private bool Invoked = false;

        /// <summary>
        /// Последняя первая дата с которой инвокалось событие.
        /// </summary>
        public T LastInvokeData;
        /// <summary>
        /// Последняя вторая дата с которой инвокалось событие.
        /// </summary>
        public T1 LastInvokeData1;
        /// <summary>
        /// Последняя третья дата с которой инвокалось событие.
        /// </summary>
        public T2 LastInvokeData2;

        /// <summary>
        /// Invoke события.
        /// </summary>
        public void Invoke()
        {
            Invoked = true;
            E?.Invoke(default, default, default);
        }

        /// <summary>
        /// Invoke события.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        public void Invoke(T data, T1 data1, T2 data2)
        {
            LastInvokeData = data;
            LastInvokeData1 = data1;
            LastInvokeData2 = data2;
            Invoked = true;
            E?.Invoke(data, data1, data2);
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
}