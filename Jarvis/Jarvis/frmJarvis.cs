﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Speech.Recognition; //Namespace do projeto

namespace Jarvis
{
    public partial class frmJarvis : Form
    {
        private SpeechRecognitionEngine engine; //Engine de reconhecimento

        public frmJarvis()
        {
            InitializeComponent();
        }

        private void LoadSpeech()
        {
            try
            {
                engine = new SpeechRecognitionEngine();
                engine.SetInputToDefaultAudioDevice(); //Entrada de microfone

                string[] words = { "Olá", "Boa noite" };

                //Carregamento da gramática
                engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(words))));

                //Evento do reconhecimento
                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(rec);
                //Nível do audio
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(audioLevel);

                //Inicia o reconhecimento
                engine.RecognizeAsync(RecognizeMode.Multiple);

                Speaker.Speak("Estou carregando os arquivos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro no LoadSpeech" + ex.Message);
            }
        }

        private void frmJarvis_Load(object sender, EventArgs e)
        {
            LoadSpeech();
        }

        //Método chamado quando algo é reconhecido
        private void rec(object s, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show(e.Result.Text);
        }

        //Método do nível de áudio
        private void audioLevel(object s, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;
        }
    }
}
