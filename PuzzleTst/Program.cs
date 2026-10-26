
string[] fragments = {
    "942517", "605676", "498291", "668826", "357057", "478151", "315629", "007148", "252887",
    "421662", "284505", "467650", "115330", "648206", "207562", "612298", "576885", "294200",
    "847595", "021597", "074878", "801997", "585401", "168510", "385293", "151863", "022142",
    "340350", "976151", "337989", "863284", "488310", "303887", "939173", "331413", "905657",
    "833617", "170794", "094486", "551394", "943693", "147970", "400196", "537505", "367493",
    "117178", "675840", "868721", "519081", "735564", "401733", "915348", "169233", "324651",
    "958675", "368753", "861460", "401341", "343222", "794373", "816374", "535119", "188234",
    "577779", "097792", "729303", "782637", "148159", "830641", "716890", "397853", "871196",
    "277603", "749226", "839595", "131852", "409432", "810698", "456030", "529185", "758823",
    "265024", "051041", "699031", "737269", "139340", "730977", "249786", "039931", "055669",
    "100107", "653178", "279773", "336550", "332847", "685485", "423269", "193536", "890062",
    "377637", "595777", "412134", "322736", "546929", "616370", "767332", "781184", "920944",
    "851005", "258850", "064083", "051202", "427711", "359855", "540928", "314284", "085261",
    "880969", "649699", "064881", "705423", "646927", "252556", "272007", "217511", "620286",
    "229724", "108865", "124636", "231417", "961201", "658432", "775416", "246027", "854036",
    "687762", "389097", "013153", "417085", "919198", "988711", "488665"
};

var startTime = DateTime.Now;

Dictionary<int, List<int>> matches = new Dictionary<int, List<int>>();
for (int i = 0; i < fragments.Length; i++)
{
    matches[i] = new List<int>();
    string suffix = fragments[i].Substring(4, 2);
    for (int j = 0; j < fragments.Length; j++)
    {
        if (i != j && fragments[j].StartsWith(suffix))
        {
            matches[i].Add(j);
        }
    }
}

var endTime = DateTime.Now;

List<int> bestPath = new List<int>();
bool[] visited = new bool[fragments.Length];
List<int> currentPath = new List<int>();

void FindLongestSequence(int node)
{
    visited[node] = true;
    currentPath.Add(node);

    bool isDeadEnd = true;
    foreach (int next in matches[node])
    {
        if (!visited[next])
        {
            isDeadEnd = false;
            FindLongestSequence(next);
        }
    }

    if (isDeadEnd)
    {
        if (currentPath.Count > bestPath.Count)
        {
            bestPath = new List<int>(currentPath);
        }
    }

    currentPath.RemoveAt(currentPath.Count - 1);
    visited[node] = false;
}

for (int i = 0; i < fragments.Length; i++)
{
    FindLongestSequence(i);
}

string result = fragments[bestPath[0]];
for (int i = 1; i < bestPath.Count; i++)
{
    result += fragments[bestPath[i]].Substring(2);
}

Console.WriteLine($"Used fragments: {bestPath.Count}/{fragments.Length}");
Console.WriteLine($"Time taken: {(endTime - startTime).TotalMilliseconds} ms");
Console.WriteLine($"\nLongest sequence:\n{result}");