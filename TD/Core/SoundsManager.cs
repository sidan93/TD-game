using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.Multimedia;
using SharpDX.XAudio2;
using SharpDX.IO;

namespace TD.Core
{
    static class SoundsManager
    {
        static Dictionary<string, Tuple<WaveFormat, AudioBuffer, uint[], XAudio2>> _audios;

        static public void init()
        {
            _audios = new Dictionary<string, Tuple<WaveFormat, AudioBuffer, uint[], XAudio2>>();
        }

        static public Tuple<WaveFormat, AudioBuffer, uint[], XAudio2> getAudio(string file)
        {
            // если данного ключа нет в массиве, то попробуем его скачать, если это не получится, то создадим объект и еще раз попробуем
            if (!_audios.ContainsKey(file))
            {
                string pathFile = "Assets/sounds/" + file + ".wav";
                try
                {
                    _audios.Add(file, loadFile(pathFile));
                }
                catch (Exception e)
                {
                    Helpers.DecodeAudioToWav(file);
                    _audios.Add(file, loadFile(pathFile));
                }
            }
            return _audios[file];
        }

        static public NativeFileStream getAudioStream(string file)
        {
            string pathFile = "Assets/sounds/" + file + ".wav";
            try
            {
                loadFile(pathFile);
                return new NativeFileStream(pathFile, NativeFileMode.Open, NativeFileAccess.Read);
            }
            catch (Exception e)
            {
                Helpers.DecodeAudioToWav(file); 
                return new NativeFileStream(pathFile, NativeFileMode.Open, NativeFileAccess.Read);
            }
        }

        static private Tuple<WaveFormat, AudioBuffer, uint[], XAudio2> loadFile(string path)
        {
            var XAudio = new XAudio2();
            var MasteringVoice = new MasteringVoice(XAudio);
            var stream = new SoundStream(File.OpenRead(path));
            var waveFormat = stream.Format;
            var buffer = new AudioBuffer
                        {
                            Stream = stream.ToDataStream(),
                            AudioBytes = (int)stream.Length,
                            Flags = BufferFlags.EndOfStream
                        };
            stream.Close();
            return new Tuple<WaveFormat, AudioBuffer, uint[], XAudio2>(waveFormat, buffer, stream.DecodedPacketsInfo, XAudio);
        }
    }
}
