using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        // Boruların hareket hızını ve kuşun yer çekimi hızını tanımlıyoruz
        int pipeSpeed = 7; // Boruların sola hareket hızı
        int gravity = 11; // Kuşun aşağıya hareket hızı
        int score = 0; // Oyun skorunu tutar
        bool gameOver = false;  // Oyunun bittiğini kontrol eden değişken

        public Form1()
        {
            InitializeComponent(); // Form bileşenlerini başlatır
        }

        // Oyun sırasında gerçekleşen ana olaylar burada işlenir
        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity; // Kuşun yer çekiminden etkilenmesini sağlar
            pipeBottom.Left -= pipeSpeed; // Alt boruyu sola hareket ettirir
            pipeTop.Left -= pipeSpeed; // Üst boruyu sola hareket ettirir
            ScoreText.Text = "Score : " + score; // Skoru ekranda günceller

            // Eğer boru ekranın solundan çıktıysa, tekrar sağdan görünmesini sağlar
            if (pipeBottom.Left < -150)
            {
                pipeBottom.Left = 800; // Alt boruyu sağdan başlat
                score++; // Skoru artır
            }
            if (pipeTop.Left < -180)
            {
                pipeTop.Left = 950; // Üst boruyu sağdan başlat
                score++; // Skoru artır
            }

            // Kuş herhangi bir boruya, zemine veya üst sınırdan çıkarsa oyunu sonlandırır
            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) || flappyBird.Top < -25
                )
            {
                endGame(); // Oyunu sonlandırma fonksiyonunu çağır
            }

            // Skor belli bir seviyeye ulaştığında boruların hızını artır
            if (score > 5)
            {
                pipeSpeed = 15;
            }
        }

        // Klavye tuşuna basıldığında (örneğin, Space) çalışır
        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -11; // Kuşun yukarı hareket etmesi için gravity negatif yapılıyor
            }
        }

        // Klavye tuşu bırakıldığında çalışır
        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 11; // Kuşun tekrar aşağı düşmesi için gravity pozitif yapılıyor
            }

            if (e.KeyCode == Keys.Enter && gameOver)
            {
                // Eğer oyun bitmişse ve Enter'a basılırsa oyunu yeniden başlat
                RestartGame();
            }
        }

        // Oyun sonlandığında yapılacak işlemler
        private void endGame()
        {
            gameTimer.Stop(); // Oyun zamanlayıcısını durdurur
            ScoreText.Text += " Game Over !!! Press Enter to Retry"; // "Game Over" mesajını gösterir
            gameOver = true; // Oyun bitiş durumunu işaretler
        }

        // Oyunu yeniden başlatmak için gereken işlemler
        private void RestartGame()
        {
            gameOver = false; // Oyunun bittiğini sıfırlar
            flappyBird.Location = new Point(42, 223); // Kuşun başlangıç konumunu ayarlar
            pipeTop.Left = 800; // Üst boruyu başlangıç pozisyonuna getirir
            pipeBottom.Left = 1200; // Alt boruyu başlangıç pozisyonuna getirir
            score = 0; // Skoru sıfırlar
            pipeSpeed = 8; // Boru hızını başlangıç hızına ayarlar
            ScoreText.Text = "Score: 0 "; // Skoru ekranda sıfırlar
            gameTimer.Start(); // Oyunu yeniden başlatır
        }
    }
}
