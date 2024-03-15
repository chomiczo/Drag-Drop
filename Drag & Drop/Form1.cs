using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drag___Drop
{
    public partial class Form1 : Form
    {
        object dragDropSource = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox lbSender = sender as ListBox;
            dragDropSource = sender;
            //int indeks = lbSender.IndexFromPoint(e.Location);
            if(e.Button == MouseButtons.Left && lbSender.SelectedIndices.Count > 0)
            {
                DragDropEffects operacja;
                operacja = /*lbSender.DoDragDrop(lbSender.Items[indeks], DragDropEffects.Copy | DragDropEffects.Move);*/
                    lbSender.DoDragDrop(lbSender.SelectedItems, DragDropEffects.Copy | DragDropEffects.Move);
                if (operacja == DragDropEffects.Move)
                {
                    //lbSender.Items.RemoveAt(indeks);
                    for(int i = lbSender.SelectedIndices.Count - 1; i >= 0; i--)
                    {
                        lbSender.Items.Remove(lbSender.SelectedItems[i]);
                    }
                }
                dragDropSource = null;
            }
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            if(dragDropSource == sender)
                e.Effect = DragDropEffects.None;
            else
            if((e.KeyState & 8) == 8)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.Move;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            ListBox lbSender = sender as ListBox;
            ListBox lbSource = dragDropSource as ListBox;
            int indeks = lbSender.IndexFromPoint(lbSender.PointToClient(new Point(e.X, e.Y)));
            if (indeks == -1) indeks = lbSender.Items.Count;
            for(int i = lbSource.SelectedIndices.Count - 1; i >= 0;i--)
            {
                lbSender.Items.Insert(indeks, lbSource.Items[lbSource.SelectedIndices[i]]);
            }
            //lbSender.Items.Insert(indeks, e.Data.GetData(DataFormats.Text));
        }
    }
}
