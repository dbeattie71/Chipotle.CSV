using BenchmarkDotNet.Attributes;
using Chipotle.CSV;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Benchmarks
{
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [MemoryDiagnoser]
    public class ChipotleCsvBenchmark
    {
        public ChipotleCsvBenchmark()
        {
        }

        [Benchmark]
        public async Task<Csv> Parse2KB()
        {
            return await ParseCsvFile(Resources.FileSize.KB1);
        }

        [Benchmark]
        public async Task<Csv> Parse4KB()
        {
            return await ParseCsvFile(Resources.FileSize.KB4);
        }

        [Benchmark]
        public async Task<Csv> Parse8KB()
        {
            return await ParseCsvFile(Resources.FileSize.KB8);
        }

        [Benchmark]
        public async Task<Csv> Parse16KB()
        {
            return await ParseCsvFile(Resources.FileSize.KB16);
        }

        [Benchmark]
        public async Task<Csv> Parse32KB()
        {
            return await ParseCsvFile(Resources.FileSize.KB32);
        }

        //[Benchmark]
        //public async Task<Csv> Parse1MB()
        //{
        //    return await ParseCsvFile("Import_User_Sample_en_Duplicated.csv");
        //}

        //[Benchmark]
        //public async Task<Csv> Parse4MB()
        //{
        //    return await ParseCsvFile("FL_insurance_sample.csv");
        //}

        private static async Task<Csv> ParseCsvFile(Resources.FileSize size)
        {
            using (var stream = Resources.GetStream(size))
            using (var csv = Csv.Parse(stream))
            {
                Debug.WriteLine($"Reading file, size = {stream.Length}");

                Row<byte> row;
                int count = 0;
                do
                {
                    row = await csv.GetNextAsync();
                    count++;
                }
                while (row != null);

                Debug.WriteLine($"Read {count} rows");

                return csv;
            }
        }
    }
}