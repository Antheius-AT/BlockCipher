using Cryptools;
using System.Collections;
using System.Text;

var blockFormatter = new BlockFormatter(128);

var blocks = blockFormatter.TransformText(Encoding.ASCII.GetBytes("Hallo duda das ist ein lä#ngerer Test test text"));