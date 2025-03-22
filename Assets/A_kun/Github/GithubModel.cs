using System;
using System.Collections.Generic;

[Serializable]
public class HistoryType
{
    public const string COMMIT = "commit";
    public const string MERGE = "merge";
}

[Serializable]
public class BranchInfo
{
    public string name;
    public string base_commit;
    public string base_commit_message;
    public DateTime base_commit_date;
    public string base_commit_author;
    public string base_commit_url;
    public bool is_default;
    public string last_commit;
    public string last_commit_message;
    public DateTime last_commit_date;
    public string last_commit_author;
    public string last_commit_url;
}

[Serializable]
public class TagInfo
{
    public string name;
    public string commit_sha;
    public string message;
    public DateTime created_at;
    public string author;
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
    public string parent_sha;
    public string parent_message;
    public string parent_author;
    public DateTime? parent_date;
    public string parent_url;
}

[Serializable]
public class BranchHistory
{
    public string branch_name;
    public List<HistoryItem> history;
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