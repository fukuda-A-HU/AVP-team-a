using System;
[Serializable]
public class HistoryType
{
    public const string COMMIT = "commit";
    public const string MERGE = "merge";
}

[Serializable]
public class CommitInfo
{
    public string sha;
    public string message;
    public string author;
    public DateTime date;
    public string url;
}

[Serializable]
public class MergeInfo
{
    public int number;
    public string title;
    public string state;
    public DateTime created_at;
    public DateTime? merged_at;
    public string url;
}

[Serializable]
public class HistoryItem
{
    public string type;
    public DateTime date;
    public string title;
    public string description;
    public string author;
    public string url;
    public string sha;
    public int? number;
    public string branch;
} 