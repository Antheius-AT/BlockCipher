// Copyright file="Program.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools
{
    using Cryptools.Actors;
    using Cryptools.Interfaces;
    using Cryptools.Models.CipherModes;
    using System.Text;

    public class Program
    {
        private static async Task Main(string[] args)
        {
            ICipherModeVisitor visitor = new Encryptor();

            var blockFormatter = new BlockFormatter(128);

            var blocks = blockFormatter.TransformText(Encoding.ASCII.GetBytes("Hallo duda das ist ein lä#ngerer Test test text"));

            foreach (var item in blocks)
            {
                var mode = new ElectronicCodebookMode(item);
                await mode.Accept(visitor);
                mode.GetResult();
            }
        }
    }
}