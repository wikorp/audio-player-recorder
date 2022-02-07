using System;
using System.Windows.Forms;

using System.Diagnostics;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;

namespace AudioPlayer
{
    public partial class FormMain : Form
    {

        //windows media player
        private bool Recording = false;

        private int number = 0;
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.autoStart = false;
        }

        private void fileSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = @"Wave File (*.wav)|*.wav;";
            if (dlg.ShowDialog() != DialogResult)
            {
                var path = dlg.InitialDirectory + dlg.FileName;
                axWindowsMediaPlayer1.URL = path;
                updateHeaderView(path);
            }
        }

        private void recordButton_Click_1(object sender, EventArgs e)
        {
            if (!Recording)
            {
                recordButton.Text = "Stop recording";
                record("open new Type waveaudio Alias recsound", "", 0, 0);
                record("record recsound", "", 0, 0);

            }
            else
            {
                recordButton.Text = "Record";
                OpenFileDialog dlg = new OpenFileDialog();
                var path = dlg.InitialDirectory.ToString() + $"sound{number++}.wav";
                axWindowsMediaPlayer1.URL = path;
                record($"save recsound {path}", "", 0, 0);
                record("close recsound", "", 0, 0);

            }
            Recording = !Recording;
            updateHeaderView("");
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        private void updateHeaderView(string PATH)
        {
            if (PATH == "")
            {
                labelHeader.Text = "";
                return;
            }

            WaveHeader waveHeader = new WaveHeader();

            using (var fileStream = new FileStream(PATH, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(fileStream))
            {
                try
                {
                    waveHeader.riff = reader.ReadBytes(4);
                    waveHeader.fileSize = reader.ReadUInt32();
                    waveHeader.wav = reader.ReadBytes(4);
                    waveHeader.fmt = reader.ReadBytes(4);
                    waveHeader.fmtSize = reader.ReadUInt32();
                    waveHeader.format = reader.ReadUInt16();
                    waveHeader.channels = reader.ReadUInt16();
                    waveHeader.samplingRate = reader.ReadUInt32();
                    waveHeader.dataRate = reader.ReadUInt32();
                    waveHeader.dataBlockSize = reader.ReadUInt16();
                    waveHeader.bitsPerSample = reader.ReadUInt16();
                    waveHeader.data = reader.ReadBytes(4);
                    waveHeader.dataSize = reader.ReadUInt32();

                    labelHeader.Text = waveHeader.ToString();

                }
                finally
                {
                    reader.Close();
                    fileStream.Close();
                }
            }
        }
    }
}