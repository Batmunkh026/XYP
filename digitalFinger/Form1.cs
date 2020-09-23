using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace digitalFinger
{
    public partial class Form1 : Form, DPFP.Capture.EventHandler
    {
        private string TAG = "MAINFORM";
        DPFP.Capture.Capture Capturer;
        public Form1()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        delegate void SetTextCallback(string text);
        private void writeStatusMessage(string input)
        {
            //txtMessageData.Clear();
            try
            {
                if (this.txtMessageData.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(writeStatusMessage);
                    this.Invoke(d, new object[] { input });
                }
                else
                {
                    this.txtMessageData.Text = input;
                }
                //txtMessageData.Text = input;
            }
            catch(Exception ex)
            {
                LogWriter._error(TAG, ex.ToString());
                //txtMessageData.Text = ex.Message;
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblDateData.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();              // Create a capture operation.

                if (null != Capturer)
                {
                    Capturer.EventHandler = this; // Subscribe for capturing events.
                }
                else
                {
                    writeStatusMessage("Хурууны хээний зураг авах боломжгүй байна.");
                }
            }
            catch
            {
                writeStatusMessage("Хурууны хээний зураг авах боломжгүй байна.");
                //MessageBox.Show("Can't initiate capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected virtual void Process(DPFP.Sample Sample)
        {
            // Draw fingerprint sample image.
            DrawPicture(ConvertSampleToBitmap(Sample));
        }
        protected void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    writeStatusMessage("Уншигч төрхөөрөмжинд хурууны хээгээ уншуулна уу ...");
                }
                catch
                {
                    writeStatusMessage("Төхөөрөмж ажиллагааг эхлүүлэх боломжгүй байна.");
                }
            }
        }
        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                    writeStatusMessage("Програм хаах явцад алдаа гарлаа.");
                }
            }
        }
        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();  // Create a sample convertor.
            Bitmap bitmap = null;                                                           // TODO: the size doesn't matter
            Convertor.ConvertToPicture(Sample, ref bitmap);                                 // TODO: return bitmap as a result
            return bitmap;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
            Start();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }
        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }
        #region EventHandler Members:

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            writeStatusMessage("Хурууны хээг амжилттай уншлаа.");
            //writeStatusMessage("Scan the same fingerprint again.");
            Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            writeStatusMessage("Хурууны хээ уншигч төхөөрөмжнөөс хуруугаа холдуулсан байна.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            writeStatusMessage("Хурууны хээ уншигч төхөөрөмжид хуруу хүрсэн байна.");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            writeStatusMessage("Хурууны хээ уншигч төхөөрөмжтэй амжилттай холбогдлоо.");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            writeStatusMessage("Хурууны хээ уншигч төхөөрөмжөөс холболтоо салгалаа.");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
            {
                writeStatusMessage("Хурууны хээний чанар сайн байна.");
            }
            else
            {
                writeStatusMessage("Хурууны хээний чанар муу байна. Дахин дарна уу.");
                //lblStatusText.Text = "Хангалтгүй";
            }
        }
        #endregion
        private void DrawPicture(Bitmap fingerData)
        {
            picFingerPicture.Image = null;
            if(picFingerPicture.Image == null)
            {
                string base64Str = string.Empty;
                picFingerPicture.Image = new Bitmap(fingerData, picFingerPicture.Size);
                //Bitmap sizedMap = new Bitmap(picFingerPicture.Image);
                if(picFingerPicture.Image !=null)
                {
                    if (imagetobase64(fingerData, out base64Str))
                    {
                        LogWriter._error(TAG, base64Str);
                        writeFingerData(base64Str);
                    }
                }
                else
                {
                    writeStatusMessage("Хурууны хээний мэдээлэл харуулахад алдаа гарлаа !!!");
                }
            }
            else
            {
                writeStatusMessage("Хурууны хээний мэдээлэл харуулахад алдаа гарлаа !!!");
            }
        }
        private bool imagetobase64(Bitmap finger, out string base64)
        {
            bool status = false;
            base64 = string.Empty;
            try
            {
                MemoryStream stream = new MemoryStream();
                finger.Save(stream, ImageFormat.Png);
                byte[] fingerBytes = stream.ToArray();
                base64 = Convert.ToBase64String(fingerBytes);
                status = true;
            }
            catch(Exception ex)
            {
                writeStatusMessage(ex.Message);
                status = false;
            }
            return status;
        }

        private void writeFingerData(string input)
        {
            //txtMessageData.Clear();
            try
            {
                if (this.txtFingerData.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(writeFingerData);
                    this.Invoke(d, new object[] { input });
                }
                else
                {
                    this.txtFingerData.Text = input;
                }
            }
            catch (Exception ex)
            {
                LogWriter._error(TAG, ex.ToString());
            }
        }

        private void BtnCopyFingerData_Click(object sender, EventArgs e)
        {
            txtFingerData.Focus();
            txtFingerData.SelectAll();
            Clipboard.SetText(txtFingerData.Text);
        }

        private void BtnResetUI_Click(object sender, EventArgs e)
        {
            Stop();
            picFingerPicture.Image = null;
            txtFingerData.Text = string.Empty;
            txtMessageData.Text = string.Empty;
            Init();
            Start();
        }
    }
}
