using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawler.Utils
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public class AsyncStreamWriter
        : TextWriter
    {
        private Stream stream;
        private Encoding encoding;

        public AsyncStreamWriter(Stream stream, Encoding encoding)
        {
            this.stream = stream;
            this.encoding = encoding;
        }

        public override void Write(char[] value, int index, int count)
        {
            byte[] textAsBytes = this.Encoding.GetBytes(value, index, count);

            Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, textAsBytes, 0, textAsBytes.Length, null);
        }

        public override void Write(char value)
        {
            this.Write(new[] { value });
        }

        public static void InjectAsConsoleOut()
        {
            Console.SetOut(new AsyncStreamWriter(Console.OpenStandardOutput(), Console.OutputEncoding));
        }

        public override Encoding Encoding
        {
            get
            {
                return this.encoding;
            }
        }
    }
}
