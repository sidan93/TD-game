using SharpDX.Multimedia;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Core
{
    class Sound
    {
        WaveFormat _wave;
        AudioBuffer _buffer;
        uint[] _decodePackets;

        SourceVoice _sourceVoice;

        public Sound(string sound)
        {
            var tuple = SoundsManager.getAudio(sound);
            _wave = tuple.Item1;
            _buffer = tuple.Item2;
            _decodePackets = tuple.Item3;

            _sourceVoice = new SourceVoice(SoundsManager.XAudio, _wave, true);
            _sourceVoice.SubmitSourceBuffer(_buffer, _decodePackets);
        }

        public void Start()
        {
            _sourceVoice.Start();
        }
        public void Stop()
        {
            _sourceVoice.Stop();
        }
    }
}
