using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.Multimedia;
using SharpDX.XAudio2;

namespace TD.Core
{
    static class SoundsManager
    {
        static public XAudio2 XAudio;
        static public MasteringVoice MasteringVoice;

        static Dictionary<string, Tuple<WaveFormat, AudioBuffer, uint[]>> _audios;

        static public void init()
        {
            _audios = new Dictionary<string, Tuple<WaveFormat, AudioBuffer, uint[]>>();
            XAudio = new XAudio2();
            MasteringVoice = new MasteringVoice(XAudio);

            Helpers.DecodeAudioToWav("shoot01.mp3");
        }

        static public Tuple<WaveFormat, AudioBuffer, uint[]> getAudio(string file)
        {
            // если данного ключа нет в массиве, то попробуем его скачать, если это не получится, то создадим объект и еще раз попробуем
            if (!_audios.ContainsKey(file))
            {
                string pathFile = "Assets/sounds/" + file + ".wav";
                _audios.Add(file, loadFile(file));
            }
            return _audios[file];

        }

        static private Tuple<WaveFormat, AudioBuffer, uint[]> loadFile(string path)
        {
            var stream = new SoundStream(File.OpenRead(path));
            var waveFormat = stream.Format;
            var buffer = new AudioBuffer
                        {
                            Stream = stream.ToDataStream(),
                            AudioBytes = (int)stream.Length,
                            Flags = BufferFlags.EndOfStream
                        };
            stream.Close();
            return new Tuple<WaveFormat, AudioBuffer, uint[]>(waveFormat, buffer, stream.DecodedPacketsInfo);
        }
    }
}
