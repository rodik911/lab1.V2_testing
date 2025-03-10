using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;
using System.IO;


public class MusicCollection
{
    private List<MusicTrack> tracks = new List<MusicTrack>();
    private ListView listView;
    public MusicCollection(ListView listView)
    {
        this.listView = listView;
        LoadTracks();
    }
    private void LoadTracks()
    {
        listView.Items.Clear();
        foreach (var track in tracks)
        {
            listView.Items.Add(new ListViewItem(new[] { track.Artist, track.Title,
track.Genre.ToString(), track.Year.ToString() }));
        }
    }
    public void AddTrack(MusicTrack track)
    {
        tracks.Add(track);
        LoadTracks();
        MessageBox.Show("Трек добавлен.");
    }
    public void RemoveTrack(MusicTrack track)
    {
        if (tracks.Contains(track))
        {
            tracks.Remove(track);
            LoadTracks();
            MessageBox.Show("Трек удалён.");
        }
        else
        {
            MessageBox.Show("Трек не найден в коллекции.");
        }
    }
    public void SearchByArtist(string artist)
    {
        var foundTracks = tracks.Where(t => t.Artist.ToLower().Contains(artist.ToLower())).ToList();
        if (foundTracks.Any())
        {
            listView.Items.Clear();
            foreach (var track in foundTracks)
            {
                listView.Items.Add(new ListViewItem(new[] { track.Artist, track.Title,
track.Genre.ToString(), track.Year.ToString() }));
            }
            MessageBox.Show("Найденные треки:");
        }
        else
        {
            MessageBox.Show("Треки не найдены.");
        }
    }
    public void SortByYear()
    {
        var sortedTracks = tracks.OrderBy(t => t.Year).ToList();
        listView.Items.Clear();
        foreach (var track in sortedTracks)
        {
            listView.Items.Add(new ListViewItem(new[] { track.Artist, track.Title,
track.Genre.ToString(), track.Year.ToString() }));
        }
        MessageBox.Show("Сортировка по году выполнена.");
    }
    public void ReloadTracks()
    {
        LoadTracks();
    }

    public void ExportCollection(string filePath)
    {
        var json = JsonConvert.SerializeObject(tracks, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);
        MessageBox.Show("Коллекция экспортирована.");
    }

    public void ImportCollection(string filePath)
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            var importedTracks = JsonConvert.DeserializeObject<List<MusicTrack>>(json);
            if (importedTracks != null)
            {
                tracks.AddRange(importedTracks);
                LoadTracks();
                MessageBox.Show("Коллекция импортирована.");
            }
        }
        else
        {
            MessageBox.Show("Файл не найден.");
        }
    }

    public void BackupCollection(string backupFilePath)
    {
        ExportCollection(backupFilePath);
        MessageBox.Show("Резервная копия коллекции создана.");
    }
}
