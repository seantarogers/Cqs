namespace Cqs.Infrastructure.Dapper
{
    using System;
    using System.Collections.Generic;

    public interface IGridReader
    {
        IList<object> Read(bool buffered = true);

        IList<T> Read<T>(bool buffered = true);

        IList<TReturn> Read<TFirst, TSecond, TReturn>(Func<TFirst, TSecond, TReturn> func, string splitOn = "id");

        IList<TReturn> Read<TFirst, TSecond, TThird, TReturn>(
            Func<TFirst, TSecond, TThird, TReturn> func,
            string splitOn = "id");

        IList<TReturn> Read<TFirst, TSecond, TThird, TFourth, TReturn>(
            Func<TFirst, TSecond, TThird, TFourth, TReturn> func,
            string splitOn = "id");

        void Dispose();
    }
}

 
 