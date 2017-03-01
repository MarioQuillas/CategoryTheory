using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GeneralThings.ApplicativeFunctor;
using GeneralThings.Monads;

namespace GeneralThings.MoreMonads
{
    public static partial class WriterExtensions
    {
        // SelectMany: (Writer<TEntry, TSource>, TSource -> Writer<TEntry, TSelector>, (TSource, TSelector) -> TResult) -> Writer<TEntry, TResult>
        public static Writer<TEntry, TResult> SelectMany<TEntry, TSource, TSelector, TResult>(
            this Writer<TEntry, TSource> source,
            Func<TSource, Writer<TEntry, TSelector>> selector,
            Func<TSource, TSelector, TResult> resultSelector) =>
            new Writer<TEntry, TResult>(() =>
            {
                Writer<TEntry, TSelector> result = selector(source.Value);
                return new Tuple<IEnumerable<TEntry>, TResult>(source.Monoid.Multiply(source.Content, result.Content),
                    resultSelector(source.Value, result.Value));
            });

        // Wrap: TSource -> Writer<TEntry, TSource>
        public static Writer<TEntry, TSource> Writer<TEntry, TSource>(this TSource value) =>
            new Writer<TEntry, TSource>(value);

        // Select: (Writer<TEnvironment, TSource>, TSource -> TResult) -> Writer<TEnvironment, TResult>
        public static Writer<TEntry, TResult> Select<TEntry, TSource, TResult>(
            this Writer<TEntry, TSource> source, Func<TSource, TResult> selector) =>
            source.SelectMany(value => selector(value).Writer<TEntry, TResult>(), (value, result) => result);

        public static Writer<string, TSource> LogWriter<TSource>(this TSource value, Func<TSource, string> logFactory)
            =>
                new Writer<string, TSource>(
                    () => new Tuple<IEnumerable<string>, TSource>(logFactory(value).Enumerable(), value));

        internal static void Workflow()
        {
            Writer<string, string> query = from filePath in Console.ReadLine().LogWriter(value =>
                                                $"File path: {value}") // Writer<string, string>.
                                           from encodingName in Console.ReadLine().LogWriter(value =>
                                                $"Encoding name: {value}") // Writer<string, string>.
                                           from encoding in Encoding.GetEncoding(encodingName).LogWriter(value =>
                                                $"Encoding: {value}") // Writer<string, Encoding>.
                                           from fileContent in File.ReadAllText(filePath, encoding).LogWriter(value =>
                                                $"File content length: {value.Length}") // Writer<string, string>.
                                           select fileContent; // Define query.
            string result = query.Value; // Execute query.
            query.Content.WriteLines();
            // File path: D:\File.txt
            // Encoding name: utf-8
            // Encoding: System.Text.UTF8Encoding
            // File content length: 76138
        }
    }
}