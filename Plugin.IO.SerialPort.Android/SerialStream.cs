using System;
using System.IO;
using Java.IO;


namespace Plugin.IO.SerialPort
{
    public class SerialStream : Stream
    {
        readonly FileDescriptor descriptor;
        readonly FileOutputStream output;
        readonly FileInputStream input;


        public SerialStream(FileDescriptor descriptor)
        {
            this.descriptor = descriptor;
            this.input = new FileInputStream(descriptor);
            this.output = new FileOutputStream(descriptor);
        }


        public override void Flush()
        {
            throw new NotImplementedException();
        }


        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }


        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }


        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.input.Read(buffer, offset, count);
        }


        public override void Write(byte[] buffer, int offset, int count)
        {
            this.output.Write(buffer, offset, count);
        }


        public override bool CanRead { get; } = true;
        public override bool CanSeek { get; } = false;
        public override bool CanWrite { get; } = true;
        public override long Length { get; } = -1;
        public override long Position { get; set; } = -1;
    }
}