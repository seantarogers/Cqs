namespace Cqs.Infrastructure.Dapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::Dapper;

    public class GridReader : IGridReader
    {
        private readonly SqlMapper.GridReader gridReader;

        public GridReader(SqlMapper.GridReader gridReader)
        {
            this.gridReader = gridReader;
        }

        public IList<dynamic> Read(bool buffered = true)
        {
            return gridReader.Read<dynamic>()
                .ToList();
        }

        public IList<T> Read<T>(bool buffered = true)
        {
            return gridReader.Read<T>()
                .ToList();
        }

        public IList<TReturn> Read<TFirst, TSecond, TReturn>(Func<TFirst, TSecond, TReturn> func, string splitOn = "id")
        {
            return gridReader.Read(func, splitOn)
                .ToList();
        }

        public IList<TReturn> Read<TFirst, TSecond, TThird, TReturn>(
            Func<TFirst, TSecond, TThird, TReturn> func,
            string splitOn = "id")
        {
            return gridReader.Read(func, splitOn)
                .ToList();
        }

        public IList<TReturn> Read<TFirst, TSecond, TThird, TFourth, TReturn>(
            Func<TFirst, TSecond, TThird, TFourth, TReturn> func,
            string splitOn = "id")
        {
            return gridReader.Read(func, splitOn)
                .ToList();
        }

        public void Dispose()
        {
            gridReader.Dispose();
        }
    }
}