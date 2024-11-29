```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.4894/22H2/2022Update)
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.200
  [Host]     : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX2 [AttachedDebugger]
  Job-GQTGTK : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX2
  Job-NZOAAP : .NET 8.0.2 (8.0.224.6711), X64 RyuJIT AVX2

InvocationCount=1  UnrollFactor=1  

```
| Method              | Job        | IterationCount | Mean        | Error      | StdDev      | Median      | Allocated |
|-------------------- |----------- |--------------- |------------:|-----------:|------------:|------------:|----------:|
| ShowTupleTypeMemory | Job-GQTGTK | 1              |  4,600.0 ns |         NA |     0.00 ns |  4,600.0 ns |    2136 B |
| ShowHashtableMemory | Job-GQTGTK | 1              | 27,450.0 ns |         NA |     0.00 ns | 27,450.0 ns |   12496 B |
| SearchTupleType     | Job-NZOAAP | Default        | 11,045.8 ns | 1,032.2 ns | 2,978.20 ns | 10,200.0 ns |     400 B |
| SearchHashTable     | Job-NZOAAP | Default        |    777.8 ns |   178.5 ns |   497.58 ns |    600.0 ns |     400 B |
