﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using MachineLearningGames.FaceLock.Classes;

namespace MachineLearningGames.FaceLock.Tabs
{
    public partial class Train : UserControl
    {
        public Train()
        {
            InitializeComponent();
        }

        SingletonFL db = SingletonFL.GetInstance();

        private int Dialog(BunifuCards cards, string title)
        {
            cards.Controls.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FilterIndex = 10;
            ofd.Title = title;
            ofd.Multiselect = true;
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (ofd.FileNames.Length <= 18)
                {
                    string[] fileNames = ofd.FileNames;
                    int x = 5, y = 15, maxHeight = -1;
                    foreach (string img in fileNames)
                    {
                        PictureBox pb = new PictureBox();
                        pb.Image = Image.FromFile(img);
                        pb.Location = new Point(x, y);
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pb.Size = new Size(60, 60);
                        x += pb.Width + 10;
                        maxHeight = Math.Max(pb.Height, maxHeight);
                        if (x > cards.Width - 50)
                        {
                            x = 5;
                            y += maxHeight + 10;
                        }
                        cards.Controls.Add(pb);
                    }
                }
                else
                    MessageBox.Show("21 Adet Resim Seçilebilir");
            }
            return ofd.FileNames.Count();
        }

        private void btnUploadGranted_Click(object sender, EventArgs e)
        {
            Dialog(cardGranted, "Lütfen Geçerli Yüz Resimlerini Seçiniz");
            db.upload = true;
        }

        private void btnUploadDenied_Click(object sender, EventArgs e)
        {
            Dialog(cardDenied, "Lütfen Geçersiz Yüz Resimlerini Seçiniz");
            db.upload = true;
        }
    }
}
