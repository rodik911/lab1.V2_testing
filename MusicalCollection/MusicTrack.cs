using System;
using System.Collections.Generic;
using System.Windows.Forms;
public class MusicTrack
{
    public string Artist { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }
    public MusicTrack(string artist, string title, string genre, int year)
    {
        Artist = artist;
        Title = title;
        Genre = genre;
        Year = year;
    }
    public override string ToString()
    {
        return $"{Artist} - {Title} ({Year}) [{Genre}]";
    }
    public override bool Equals(object obj)
    {
        if (obj is MusicTrack other)
        {
            return Artist == other.Artist && Title == other.Title && Genre == other.Genre && Year == other.Year;
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + (Artist?.GetHashCode() ?? 0);
        hash = hash * 23 + (Title?.GetHashCode() ?? 0);
        hash = hash * 23 + Genre.GetHashCode();
        hash = hash * 23 + Year.GetHashCode();
        return hash;
    }

}