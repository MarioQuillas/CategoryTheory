using System;
using System.IO;
using GeneralThings.Monoids;

namespace GeneralThings.MoreMonads
{
    public static partial class ReaderExtensions
    {
        // SelectMany: (Reader<TEnvironment, TSource>, TSource -> Reader<TEnvironment, TSelector>, (TSource, TSelector) -> TResult) -> Reader<TEnvironment, TResult>
        public static ReaderMonad.Reader<TEnvironment, TResult> SelectMany<TEnvironment, TSource, TSelector, TResult>(
            this ReaderMonad.Reader<TEnvironment, TSource> source,
            Func<TSource, ReaderMonad.Reader<TEnvironment, TSelector>> selector,
            Func<TSource, TSelector, TResult> resultSelector) =>
            environment =>
            {
                TSource value = source(environment);
                return resultSelector(value, selector(value)(environment));
            };

        // Wrap: TSource -> Reader<TEnvironment, TSource>
        public static ReaderMonad.Reader<TEnvironment, TSource> Reader<TEnvironment, TSource>(this TSource value) =>
            environment => value;

        // Select: (Reader<TEnvironment, TSource>, TSource -> TResult) -> Reader<TEnvironment, TResult>
        public static ReaderMonad.Reader<TEnvironment, TResult> Select<TEnvironment, TSource, TResult>(
            this ReaderMonad.Reader<TEnvironment, TSource> source, Func<TSource, TResult> selector) =>
            source.SelectMany(value => selector(value).Reader<TEnvironment, TResult>(), (value, result) => result);

        private static ReaderMonad.Reader<IConfiguration, FileInfo> DownloadHtml(Uri uri) =>
            configuration => default(FileInfo);

        private static ReaderMonad.Reader<IConfiguration, FileInfo> ConverToWord(FileInfo htmlDocument,
            FileInfo template) =>
            configuration => default(FileInfo);

        private static ReaderMonad.Reader<IConfiguration, Unit> UploadToOneDrive(FileInfo file) =>
            configuration => default(Unit);

        internal static void Workflow(IConfiguration configuration, Uri uri, FileInfo template)
        {
            ReaderMonad.Reader<IConfiguration, Tuple<FileInfo, FileInfo>> query =
                from htmlDocument in DownloadHtml(uri) // Reader<IConfiguration, FileInfo>.
                from wordDocument in ConverToWord(htmlDocument, template) // Reader<IConfiguration, FileInfo>.
                from unit in UploadToOneDrive(wordDocument) // Reader<IConfiguration, Unit>.
                select new Tuple<FileInfo, FileInfo>(htmlDocument, wordDocument); // Define query.
            Tuple<FileInfo, FileInfo> result = query(configuration); // Execute query.
        }
    }

    internal interface IConfiguration
    {
    }
}