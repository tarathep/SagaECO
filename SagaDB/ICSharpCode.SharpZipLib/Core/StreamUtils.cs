namespace ICSharpCode.SharpZipLib.Core
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="StreamUtils" />.
    /// </summary>
    public sealed class StreamUtils
    {
        /// <summary>
        /// The ReadFully.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        public static void ReadFully(Stream stream, byte[] buffer)
        {
            StreamUtils.ReadFully(stream, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// The ReadFully.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public static void ReadFully(Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0 || offset > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0 || offset + count > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(count));
            while (count > 0)
            {
                int num = stream.Read(buffer, offset, count);
                if (num <= 0)
                    throw new EndOfStreamException();
                offset += num;
                count -= num;
            }
        }

        /// <summary>
        /// The Copy.
        /// </summary>
        /// <param name="source">The source<see cref="Stream"/>.</param>
        /// <param name="destination">The destination<see cref="Stream"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        public static void Copy(Stream source, Stream destination, byte[] buffer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (buffer.Length < 128)
                throw new ArgumentException("Buffer is too small", nameof(buffer));
            bool flag = true;
            while (flag)
            {
                int count = source.Read(buffer, 0, buffer.Length);
                if (count > 0)
                {
                    destination.Write(buffer, 0, count);
                }
                else
                {
                    destination.Flush();
                    flag = false;
                }
            }
        }

        /// <summary>
        /// The Copy.
        /// </summary>
        /// <param name="source">The source<see cref="Stream"/>.</param>
        /// <param name="destination">The destination<see cref="Stream"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="progressHandler">The progressHandler<see cref="ProgressHandler"/>.</param>
        /// <param name="updateInterval">The updateInterval<see cref="TimeSpan"/>.</param>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public static void Copy(Stream source, Stream destination, byte[] buffer, ProgressHandler progressHandler, TimeSpan updateInterval, object sender, string name)
        {
            StreamUtils.Copy(source, destination, buffer, progressHandler, updateInterval, sender, name, -1L);
        }

        /// <summary>
        /// The Copy.
        /// </summary>
        /// <param name="source">The source<see cref="Stream"/>.</param>
        /// <param name="destination">The destination<see cref="Stream"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="progressHandler">The progressHandler<see cref="ProgressHandler"/>.</param>
        /// <param name="updateInterval">The updateInterval<see cref="TimeSpan"/>.</param>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="fixedTarget">The fixedTarget<see cref="long"/>.</param>
        public static void Copy(Stream source, Stream destination, byte[] buffer, ProgressHandler progressHandler, TimeSpan updateInterval, object sender, string name, long fixedTarget)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (buffer.Length < 128)
                throw new ArgumentException("Buffer is too small", nameof(buffer));
            if (progressHandler == null)
                throw new ArgumentNullException(nameof(progressHandler));
            bool flag1 = true;
            DateTime now = DateTime.Now;
            long processed = 0;
            long target = 0;
            if (fixedTarget >= 0L)
                target = fixedTarget;
            else if (source.CanSeek)
                target = source.Length - source.Position;
            ProgressEventArgs e1 = new ProgressEventArgs(name, processed, target);
            progressHandler(sender, e1);
            bool flag2 = true;
            while (flag1)
            {
                int count = source.Read(buffer, 0, buffer.Length);
                if (count > 0)
                {
                    processed += (long)count;
                    flag2 = false;
                    destination.Write(buffer, 0, count);
                }
                else
                {
                    destination.Flush();
                    flag1 = false;
                }
                if (DateTime.Now - now > updateInterval)
                {
                    flag2 = true;
                    now = DateTime.Now;
                    ProgressEventArgs e2 = new ProgressEventArgs(name, processed, target);
                    progressHandler(sender, e2);
                    flag1 = e2.ContinueRunning;
                }
            }
            if (flag2)
                return;
            ProgressEventArgs e3 = new ProgressEventArgs(name, processed, target);
            progressHandler(sender, e3);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="StreamUtils"/> class from being created.
        /// </summary>
        private StreamUtils()
        {
        }
    }
}
