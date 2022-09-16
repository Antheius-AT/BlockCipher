// Copyright file="Program.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools
{
    using Cryptools.Actors;
    using System.Text;

    public class Program
    {
        private static void Main(string[] args)
        {
            var blockFormatter = new BlockFormatter(128);

            var blocks = blockFormatter.TransformText(Encoding.ASCII.GetBytes("Hallo duda das ist ein lä#ngerer Test test text"));
        }
    }
}