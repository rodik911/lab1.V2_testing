using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicalCollection
{
    public partial class MusicCollectionForm : Form
    {
        private MusicCollection musicCollection;
        private ListView listView;
        private Button addTrackButton;
        private Button removeTrackButton;
        private Button searchByArtistButton;
        private Button sortByYearButton;
        private Button reloadButton;
        public MusicCollectionForm()
        {
            this.Text = "Управление музыкальной коллекцией";
            this.Width = 670;
            this.Height = 400;
            this.Icon = Icon.ExtractAssociatedIcon("C:\\Users\\329191-23\\Desktop\\lab_4\\lab1.V2_testing\\MusicalCollection\\free-icon-music-7797380.ico");
            this.BackColor = Color.FromName("LightSteelBlue");
            this.CenterToScreen();
            CreateControls();
            musicCollection = new MusicCollection(listView);

        }
        private void CreateControls()
        {
            listView = new ListView
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(630, 300),
                View = View.Details,
                FullRowSelect = true
            };
            listView.Columns.Add("Исполнитель", 200);
            listView.Columns.Add("Название", 200);
            listView.Columns.Add("Жанр", 120);
            listView.Columns.Add("Год", 80);
            addTrackButton = new Button
            {
                Location = new System.Drawing.Point(10, 320),
                Text = "Добавить трек",
                Size = new System.Drawing.Size(100, 25)
            };
            addTrackButton.Click += (sender, e) =>
            {
                var addTrackForm = new AddTrackForm();
                addTrackForm.ShowDialog();
                if (addTrackForm.DialogResult == DialogResult.OK)
                {
                    var track = new MusicTrack(
                    addTrackForm.Artist,
                    addTrackForm.Title,
                    addTrackForm.Genre,
                    addTrackForm.Year);
                    musicCollection.AddTrack(track);
                }
            };
            removeTrackButton = new Button
            {
                Location = new System.Drawing.Point(120, 320),
                Text = "Удалить трек",
                Size = new System.Drawing.Size(100, 25)
            };
            removeTrackButton.Click += (sender, e) =>
            {
                if (listView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Сначала выберите трек для удаления.");
                    return;
                }
                var track = new MusicTrack(
                listView.SelectedItems[0].SubItems[0].Text,
                listView.SelectedItems[0].SubItems[1].Text,
                listView.SelectedItems[0].SubItems[2].Text,
                int.Parse(listView.SelectedItems[0].SubItems[3].Text));
                musicCollection.RemoveTrack(track);
            };
            searchByArtistButton = new Button
            {
                Location = new System.Drawing.Point(230, 320),
                Text = "Поиск по исполнителю",
                Size = new System.Drawing.Size(140, 25)
            };
            searchByArtistButton.Click += (sender, e) =>
            {
                var searchForm = new SearchArtistForm();
                searchForm.ShowDialog();
                if (searchForm.DialogResult == DialogResult.OK)
                {
                    musicCollection.SearchByArtist(searchForm.Artist);
                }
            };
            sortByYearButton = new Button
            {
                Location = new System.Drawing.Point(380, 320),
                Text = "Сортировать по году",
                Size = new System.Drawing.Size(120, 25)
            };
            sortByYearButton.Click += (sender, e) => musicCollection.SortByYear();
            reloadButton = new Button
            {
                Location = new System.Drawing.Point(510, 320),
                Text = "Сбросить фильтрацию",
                Size = new System.Drawing.Size(140, 25)
            };
            reloadButton.Click += (sender, e) => musicCollection.ReloadTracks();
            this.Controls.Add(listView);
            this.Controls.Add(addTrackButton);
            this.Controls.Add(removeTrackButton);
            this.Controls.Add(searchByArtistButton);
            this.Controls.Add(sortByYearButton);
            this.Controls.Add(reloadButton);
        }

    }
}
