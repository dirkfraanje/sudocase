using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuDoCase
{
    public partial class MainPage : ContentPage
    {
        Dictionary<int, HashSet<int>> AllRows = new Dictionary<int, HashSet<int>>();
        Dictionary<int, HashSet<int>> AllColumns = new Dictionary<int, HashSet<int>>();
        Dictionary<int, HashSet<int>> AllGrids = new Dictionary<int, HashSet<int>>();
        HashSet<int> r1 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> r2 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> r3 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> r4 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> r5 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> r6 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> r7 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> r8 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> r9 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        HashSet<int> c1 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> c2 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> c3 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> c4 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> c5 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> c6 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> c7 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> c8 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> c9 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        HashSet<int> g1 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> g2 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> g3 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> g4 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> g5 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> g6 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> g7 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> g8 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<int> g9 = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        HashSet<SuDoCaseEntry> entriesToAddAndRemoveFrom = new HashSet<SuDoCaseEntry>();
        List<SuDoCaseEntry> staticEntries = new List<SuDoCaseEntry>();
        List<SuDoCaseLabel> labels = new List<SuDoCaseLabel>();
        int columnDependency = 1;
        int rowDependency = 1;
        int gridNumber = 1;
        public MainPage(int? i = 0)
        {
            InitializeComponent();
            FillDictionaries();
            CreateEntryFields();
            SolvedLabel.SetOnAppTheme(Label.TextColorProperty, Color.Black, Color.FromHex("#46b5d1"));
            appLabel.SetOnAppTheme(Label.TextColorProperty, Color.Black, Color.FromHex("#46b5d1"));
            resetButton.SetOnAppTheme(Button.TextColorProperty, Color.Black, Color.FromHex("#46b5d1"));
            prefillButton.SetOnAppTheme(Button.TextColorProperty, Color.Black, Color.FromHex("#46b5d1"));
            SolveButton.SetOnAppTheme(Button.TextColorProperty, Color.Black, Color.FromHex("#46b5d1"));
            if (i != 0)
                SetPrefill(i);
        }

        private async void ResetButton_Clicked(object sender, EventArgs e)
        {
            var main = new MainPage();
            await Navigation.PushAsync(main);

        }

        private async void Prefill_Clicked(object sender, EventArgs e)
        {
            var choosePage = new ChoosePrefill();
            await Navigation.PushAsync(choosePage);

        }

        private void StartResolve(object sender, EventArgs e)
        {
            var timer = new Stopwatch();
            timer.Start();
            Resolve();
            timer.Stop();
            var notSolvedAll = entriesToAddAndRemoveFrom.Any(x => string.IsNullOrWhiteSpace(x.Text));
            if (notSolvedAll)
            {
                SolvedLabel.Text = $"Partly solved in {timer.ElapsedMilliseconds} milliseconds. Could not solve all";
                SolvedLabel.IsVisible = true;
            }
            else
            {
                SolvedLabel.Text = $"Solved in {timer.ElapsedMilliseconds} milliseconds";
                SolvedLabel.IsVisible = true;
            }
        }

        private void Resolve()
        {
            var allEntriesEmpty = staticEntries.All(x => string.IsNullOrWhiteSpace(x.Text));
            if (allEntriesEmpty)
                return;

            TryToResolvePuzzle(false, false, false);

            var notSolvedAll = entriesToAddAndRemoveFrom.Any(x => string.IsNullOrWhiteSpace(x.Text));
            if (notSolvedAll)
                StartGambleLoop();
        }

        private void StartGambleLoop()
        {
            var solved = false;

            while (!solved)
            {

                RestoreGamble();
                var firstNotFilledEntryWithHints = staticEntries.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Text) && x.Hint.Count > 0 && x.Hint.Count > x.GamblesTried.Count);
                if (firstNotFilledEntryWithHints == null)
                    return;
                HashSet<int> posibilities = new HashSet<int>(firstNotFilledEntryWithHints.Hint);
                posibilities.ExceptWith(firstNotFilledEntryWithHints.GamblesTried);
                var gambleValue = posibilities.First();
                firstNotFilledEntryWithHints.Text = gambleValue.ToString();
                firstNotFilledEntryWithHints.GamblesTried.Add(gambleValue);
                firstNotFilledEntryWithHints.Gamble = true;
                solved = StartGambleResolving(false, false);
            }
        }

        private bool StartGambleResolving(bool gambleLevel2, bool gambleLevel3)
        {
            TryToResolvePuzzle(true, gambleLevel2, gambleLevel3);
            //Check if all are resolved
            var notSolvedAll = entriesToAddAndRemoveFrom.Any(x => string.IsNullOrWhiteSpace(x.Text));
            if (!notSolvedAll)
            {
                return true;
            }
            //Check for dead ends
            var anyFalseFound = entriesToAddAndRemoveFrom.Any(x => string.IsNullOrWhiteSpace(x.Text) && x.Hint.Count() == 0);
            if (anyFalseFound)
            {
                if (!gambleLevel2)
                {
                    RestoreGamble2();
                    var firstNotFilledEntryWithHints2 = staticEntries.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Text) && x.Hint.Count > 0 && x.Hint.Count > x.GamblesTriedLevel2.Count);
                    if (firstNotFilledEntryWithHints2 == null)
                        return false;
                    HashSet<int> posibilities = new HashSet<int>(firstNotFilledEntryWithHints2.Hint);
                    posibilities.ExceptWith(firstNotFilledEntryWithHints2.GamblesTriedLevel2);
                    var gambleValue2 = posibilities.First();
                    firstNotFilledEntryWithHints2.Text = gambleValue2.ToString();
                    firstNotFilledEntryWithHints2.GamblesTriedLevel2.Add(gambleValue2);
                    firstNotFilledEntryWithHints2.GambleLevel2 = true;
                    return StartGambleResolving(true, false);
                }
                else if (!gambleLevel3)
                {
                    var anyfalsefoundvalue = entriesToAddAndRemoveFrom.Where(x => string.IsNullOrWhiteSpace(x.Text) && x.Hint.Count() == 0);
                    RestoreGamble3();
                    var firstNotFilledEntryWithHints3 = staticEntries.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Text) && x.Hint.Count > 0 && x.Hint.Count > x.GamblesTriedLevel3.Count);
                    if (firstNotFilledEntryWithHints3 == null)
                        return false;
                    HashSet<int> posibilities = new HashSet<int>(firstNotFilledEntryWithHints3.Hint);
                    posibilities.ExceptWith(firstNotFilledEntryWithHints3.GamblesTriedLevel3);
                    var gambleValue3 = posibilities.First();
                    firstNotFilledEntryWithHints3.Text = gambleValue3.ToString();
                    firstNotFilledEntryWithHints3.GamblesTriedLevel3.Add(gambleValue3);
                    firstNotFilledEntryWithHints3.GambleLevel3 = true;
                    return StartGambleResolving(true, true);
                }
                else
                    return false;
            }

            var firstNotFilledEntryWithHints = staticEntries.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.Text) && x.Hint.Count > 0);
            if (firstNotFilledEntryWithHints == null)
                return false;
            var gambleValue = firstNotFilledEntryWithHints.Hint.First();
            firstNotFilledEntryWithHints.Text = gambleValue.ToString();
            firstNotFilledEntryWithHints.GamblesTriedLevel2.Add(gambleValue);
            firstNotFilledEntryWithHints.GambleLevel2 = true;
            return StartGambleResolving(gambleLevel2, gambleLevel3);

        }

        private void RestoreGamble3()
        {
            foreach (var item in staticEntries.Where(x => x.GambleLevel3))
            {
                item.Text = "";
                item.GambleLevel3 = false;
            }
        }

        private void RestoreGamble2()
        {
            foreach (var item in staticEntries.Where(x => x.GambleLevel2))
            {
                item.Text = "";
                item.GambleLevel2 = false;
            }
        }

        private void RestoreGamble()
        {
            foreach (var item in staticEntries.Where(x => x.Gamble))
            {
                item.Text = "";
                item.Gamble = false;
            }
        }


        private void TryToResolvePuzzle(bool gamble, bool gambleLevel2, bool gambleLevel3)
        {
            if (TryEasyResolve(gamble, gambleLevel2, gambleLevel3))
                TryToResolvePuzzle(gamble, gambleLevel2, gambleLevel3);
            var rowResolve = TryRowResolve(gamble, gambleLevel2, gambleLevel3);
            var columnResolve = TryColumnResolve(gamble, gambleLevel2, gambleLevel3);
            var gridResolve = TryGridResolve(gamble, gambleLevel2, gambleLevel3);
            if (rowResolve == true || columnResolve == true || gridResolve == true)
                TryToResolvePuzzle(gamble, gambleLevel2, gambleLevel3);
        }
        private bool TryRowResolve(bool gamble, bool gambleLevel2, bool gambleLevel3)
        {
            var returnValue = false;
            foreach (var item in AllRows)
            {
                var rowNumber = item.Key;
                var allEntriesInRowWithEmptyValue = entriesToAddAndRemoveFrom.Where(x => x.RowNumber == rowNumber);
                List<int> listWithUniqueEntry = new List<int>();
                foreach (var entryInColumn in allEntriesInRowWithEmptyValue)
                {
                    foreach (var value in entryInColumn.Hint)
                    {
                        listWithUniqueEntry.Add(value);
                    }
                }
                if (!listWithUniqueEntry.Any())
                    continue;
                var lowestUsed = listWithUniqueEntry.GroupBy(i => i)
                .Where(x => x.Count() != 27)
                .OrderBy(grp => grp.Count()).FirstOrDefault();
                if (lowestUsed == null)
                    return false;
                if (lowestUsed.Count() == 1)
                {
                    var entryWithLowestUsed = allEntriesInRowWithEmptyValue.First(x => x.Hint.Contains(lowestUsed.Key));
                    entryWithLowestUsed.Text = $"{lowestUsed.Key}";
                    entryWithLowestUsed.Gamble = gamble;
                    entryWithLowestUsed.GambleLevel2 = gambleLevel2;
                    entryWithLowestUsed.GambleLevel3 = gambleLevel3;
                    returnValue = true;
                }
            }
            return returnValue;
        }
        private bool TryColumnResolve(bool gamble, bool gambleLevel2, bool gambleLevel3)
        {
            var returnValue = false;
            foreach (var item in AllColumns)
            {
                var columnNumber = item.Key;
                var allEntriesInColumnWithEmptyValue = entriesToAddAndRemoveFrom.Where(x => x.ColumnNumber == columnNumber);
                List<int> listWithUniqueEntry = new List<int>();
                foreach (var entryInColumn in allEntriesInColumnWithEmptyValue)
                {
                    foreach (var value in entryInColumn.Hint)
                    {
                        listWithUniqueEntry.Add(value);
                    }
                }
                if (!listWithUniqueEntry.Any())
                    continue;
                var lowestUsed = listWithUniqueEntry.GroupBy(i => i)
                .Where(x => x.Count() != 27)
                .OrderBy(grp => grp.Count()).FirstOrDefault();
                if (lowestUsed == null)
                    return false;
                if (lowestUsed.Count() == 1)
                {
                    var entryWithLowestUsed = allEntriesInColumnWithEmptyValue.First(x => x.Hint.Contains(lowestUsed.Key));
                    entryWithLowestUsed.Text = $"{lowestUsed.Key}";
                    entryWithLowestUsed.Gamble = gamble;
                    entryWithLowestUsed.GambleLevel2 = gambleLevel2;
                    entryWithLowestUsed.GambleLevel3 = gambleLevel3;
                    returnValue = true;
                }


            }
            return returnValue;

        }

        private bool TryGridResolve(bool gamble, bool gambleLevel2, bool gambleLevel3)
        {
            var returnValue = false;
            foreach (var item in AllGrids)
            {
                var gridNumber = item.Key;
                var allEntriesInGridWithEmptyValue = entriesToAddAndRemoveFrom.Where(x => x.GridNumber == gridNumber);
                List<int> listWithUniqueEntry = new List<int>();
                foreach (var entryInGrid in allEntriesInGridWithEmptyValue)
                {
                    foreach (var value in entryInGrid.Hint)
                    {
                        listWithUniqueEntry.Add(value);
                    }
                }
                if (!listWithUniqueEntry.Any())
                    continue;
                var lowestUsed = listWithUniqueEntry.GroupBy(i => i)
                .Where(x => x.Count() != 27)
                .OrderBy(grp => grp.Count()).FirstOrDefault();
                if (lowestUsed == null)
                    return false;
                if (lowestUsed.Count() == 1)
                {
                    var entryWithLowestUsed = allEntriesInGridWithEmptyValue.First(x => x.Hint.Contains(lowestUsed.Key));
                    entryWithLowestUsed.Text = $"{lowestUsed.Key}";
                    entryWithLowestUsed.Gamble = gamble;
                    entryWithLowestUsed.GambleLevel2 = gambleLevel2;
                    entryWithLowestUsed.GambleLevel3 = gambleLevel3;
                    returnValue = true;
                }
            }
            return returnValue;
        }



        private bool TryEasyResolve(bool gamble, bool gambleLevel2, bool gambleLevel3)
        {
            var emptyEntriesWithOneHint = entriesToAddAndRemoveFrom.Where(x => x.Hint.Count() == 1);
            if (emptyEntriesWithOneHint.Count() == 0)
                return false;
            foreach (var entry in emptyEntriesWithOneHint.ToArray())
            {
                var x = entry.Hint.FirstOrDefault();
                if (x == 0)
                    continue;
                entry.Text = entry.Hint.First().ToString();
                entry.Gamble = gamble;
                entry.GambleLevel2 = gambleLevel2;
                entry.GambleLevel3 = gambleLevel3;
            }
            return true;
        }

        private void FillDictionaries()
        {
            AllRows.Add(1, r1);
            AllRows.Add(2, r2);
            AllRows.Add(3, r3);
            AllRows.Add(4, r4);
            AllRows.Add(5, r5);
            AllRows.Add(6, r6);
            AllRows.Add(7, r7);
            AllRows.Add(8, r8);
            AllRows.Add(9, r9);


            AllColumns.Add(1, c1);
            AllColumns.Add(2, c2);
            AllColumns.Add(3, c3);
            AllColumns.Add(4, c4);
            AllColumns.Add(5, c5);
            AllColumns.Add(6, c6);
            AllColumns.Add(7, c7);
            AllColumns.Add(8, c8);
            AllColumns.Add(9, c9);

            AllGrids.Add(1, g1);
            AllGrids.Add(2, g2);
            AllGrids.Add(3, g3);
            AllGrids.Add(4, g4);
            AllGrids.Add(5, g5);
            AllGrids.Add(6, g6);
            AllGrids.Add(7, g7);
            AllGrids.Add(8, g8);
            AllGrids.Add(9, g9);
        }

        private void SetGridNumber()
        {
            if (columnDependency == 3 || columnDependency == 6)
                gridNumber++;
            if (columnDependency == 9)
            {
                columnDependency = 0;
                rowDependency++;
                switch (rowDependency)
                {
                    case 1:
                    case 2:
                    case 3:
                        gridNumber = 1;
                        break;
                    case 4:
                    case 5:
                    case 6:
                        gridNumber = 4;
                        break;
                    case 7:
                    case 8:
                    case 9:
                        gridNumber = 7;
                        break;
                    default:
                        break;
                }
            }
            columnDependency++;
        }

        private void SetPrefill(int? difficulty = 0)
        {
            switch (difficulty)
            {
                case 1:
                    findEntry(1,1,1).Text = "1";
                    findEntry(1,5,2).Text = "3";
                    findEntry(1,6,2).Text = "4";
                    findEntry(1,9,3).Text = "8";

                    findEntry(2,2,1).Text = "7";
                    findEntry(2,4,2).Text = "6";
                    findEntry(2,5,2).Text = "8";
                    findEntry(2,8,3).Text = "3";

                    findEntry(3,3,1).Text = "8";
                    findEntry(3,4,2).Text = "2";
                    findEntry(3,5,2).Text = "1";
                    findEntry(3,7,3).Text = "7";
                    findEntry(3,9,3).Text = "4";

                    findEntry(4,2,4).Text = "5";
                    findEntry(4,3,4).Text = "4";
                    findEntry(4,5,5).Text = "9";
                    findEntry(4,7,6).Text = "6";
                    findEntry(4,8,6).Text = "8";

                    findEntry(5,1,4).Text = "9";
                    findEntry(5,2,4).Text = "1";
                    findEntry(5,4,5).Text = "5";
                    findEntry(5,6,5).Text = "8";
                    findEntry(5,8,6).Text = "2";

                    findEntry(6,2,4).Text = "8";
                    findEntry(6,4,5).Text = "3";
                    findEntry(6,9,6).Text = "5";

                    findEntry(7,1,7).Text = "3";
                    findEntry(7,3,7).Text = "5";
                    findEntry(7,4,8).Text = "9";
                    findEntry(7,6,8).Text = "6";
                    findEntry(7,7,9).Text = "8";
                    findEntry(7,8,9).Text = "7";
                    findEntry(7,9,9).Text = "1";

                    findEntry(8,3,7).Text = "6";
                    findEntry(8,8,9).Text = "4";

                    findEntry(9,3,7).Text = "1";
                    findEntry(9,5,8).Text = "7";
                    findEntry(9,7,9).Text = "2";


                    break;
                case 2:
                    findEntry(1, 3, 1).Text = "3";
                    findEntry(1, 5, 2).Text = "1";
                    findEntry(1, 6, 2).Text = "9";
                    findEntry(2, 1, 1).Text = "9";
                    findEntry(2, 4, 2).Text = "8";
                    findEntry(3, 2, 1).Text = "1";
                    findEntry(3, 4, 2).Text = "6";
                    findEntry(3, 5, 2).Text = "2";
                    findEntry(3, 9, 3).Text = "9";
                    findEntry(4, 2, 4).Text = "8";
                    findEntry(4, 8, 6).Text = "4";
                    findEntry(4, 9, 6).Text = "1";
                    findEntry(5, 1, 4).Text = "5";
                    findEntry(5, 2, 4).Text = "2";
                    findEntry(5, 3, 4).Text = "6";
                    findEntry(5, 7, 6).Text = "9";
                    findEntry(5, 8, 6).Text = "8";
                    findEntry(5, 9, 6).Text = "7";
                    findEntry(6, 1, 4).Text = "4";
                    findEntry(6, 2, 4).Text = "9";
                    findEntry(6, 8, 6).Text = "6";
                    findEntry(7, 1, 7).Text = "6";
                    findEntry(7, 5, 8).Text = "8";
                    findEntry(7, 6, 8).Text = "3";
                    findEntry(7, 8, 9).Text = "9";
                    findEntry(8, 6, 8).Text = "2";
                    findEntry(8, 9, 9).Text = "3";
                    findEntry(9, 4, 8).Text = "4";
                    findEntry(9, 5, 8).Text = "6";
                    findEntry(9, 7, 9).Text = "8";
                    break;
                case 3:
                    findEntry(1, 1, 1).Text = "7";
                    findEntry(1, 2, 1).Text = "8";
                    findEntry(1, 4, 2).Text = "1";
                    findEntry(1, 7, 3).Text = "2";
                    findEntry(2, 1, 1).Text = "2";
                    findEntry(2, 9, 3).Text = "8";
                    findEntry(3, 1, 1).Text = "3";
                    findEntry(3, 6, 2).Text = "6";
                    findEntry(3, 8, 3).Text = "9";
                    findEntry(4, 4, 5).Text = "8";
                    findEntry(4, 5, 5).Text = "1";
                    findEntry(4, 6, 5).Text = "5";
                    findEntry(4, 9, 6).Text = "2";
                    findEntry(5, 4, 5).Text = "6";
                    findEntry(6, 4, 5).Text = "7";
                    findEntry(6, 6, 5).Text = "9";
                    findEntry(6, 7, 6).Text = "5";
                    findEntry(6, 8, 6).Text = "8";
                    findEntry(7, 9, 9).Text = "5";
                    findEntry(8, 5, 8).Text = "9";
                    findEntry(8, 7, 9).Text = "7";
                    findEntry(9, 1, 7).Text = "1";
                    findEntry(9, 2, 7).Text = "5";
                    findEntry(9, 9, 9).Text = "6";
                    break;
                case 4:
                    findEntry(1, 2, 1).Text = "3";
                    findEntry(1, 6, 2).Text = "1";
                    findEntry(1, 7, 3).Text = "9";
                    findEntry(2, 1, 1).Text = "1";
                    findEntry(2, 3, 1).Text = "2";
                    findEntry(3, 2, 1).Text = "8";
                    findEntry(4, 3, 4).Text = "9";
                    findEntry(4, 4, 5).Text = "4";
                    findEntry(5, 1, 4).Text = "6";
                    findEntry(5, 2, 4).Text = "1";
                    findEntry(5, 3, 4).Text = "8";
                    findEntry(5, 8, 6).Text = "4";
                    findEntry(5, 9, 6).Text = "2";
                    findEntry(6, 1, 4).Text = "4";
                    findEntry(6, 7, 6).Text = "5";
                    findEntry(7, 4, 8).Text = "6";
                    findEntry(7, 6, 8).Text = "4";
                    findEntry(7, 9, 9).Text = "9";
                    findEntry(8, 1, 7).Text = "3";
                    findEntry(8, 4, 8).Text = "9";
                    findEntry(8, 6, 8).Text = "2";
                    findEntry(8, 8, 9).Text = "5";
                    findEntry(9, 2, 7).Text = "9";
                    findEntry(9, 4, 8).Text = "5";
                    findEntry(9, 5, 8).Text = "1";
                    findEntry(9, 6, 8).Text = "8";
                    findEntry(9, 8, 9).Text = "2";

                    break;
                default:
                    break;
            }
        }
        private SuDoCaseEntry findEntry(int row, int column, int grid)
        {
            return staticEntries.First(x => x.RowNumber == row && x.ColumnNumber == column && x.GridNumber == grid);
        }

        private void CreateEntryFields()
        {
            var entryGrids = new List<Grid>();
            CreateContainerAndSubGrids(entryGrids);
            //Rows
            for (int r = 1; r < 10; r++)
            {
                //Columns
                for (int c = 1; c < 10; c++)
                {
                    var colStack = new StackLayout();
                    var gestureRecognizer = new TapGestureRecognizer();
                    colStack.GestureRecognizers.Add(gestureRecognizer);
                    gestureRecognizer.Tapped += GestureRecognizer_Tapped;
                    colStack.Orientation = StackOrientation.Vertical;
                    colStack.HorizontalOptions = LayoutOptions.Fill;
                    colStack.Spacing = 0;

                    var label = new SuDoCaseLabel(r, c, gridNumber) { Text = "123456789", FontSize = 7 };

                    label.SetOnAppTheme(Label.TextColorProperty, Color.Black, Color.FromHex("#12b4dd"));
                    colStack.Children.Add(label);
                    labels.Add(label);

                    var newEntry = new SuDoCaseEntry(r, c, gridNumber);
                    newEntry.HorizontalTextAlignment = TextAlignment.Center;
                    newEntry.VerticalTextAlignment = TextAlignment.Center;
                    switch (gridNumber)
                    {
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 9:
                            label.SetOnAppTheme(BackgroundColorProperty, Color.LightGray, Color.FromHex("#071e32"));
                            newEntry.SetOnAppTheme(BackgroundColorProperty, Color.LightGray, Color.FromHex("#071e32"));
                            break;
                        default:
                            label.SetOnAppTheme(BackgroundColorProperty, Color.White, Color.Black);
                            newEntry.SetOnAppTheme(BackgroundColorProperty, Color.White, Color.Black);

                            break;
                    }
                    newEntry.SetOnAppTheme(Entry.TextColorProperty, Color.Black, Color.FromHex("#46b5d1"));
                    newEntry.HeightRequest = 36;
                    newEntry.FontSize = 14;
                    newEntry.Keyboard = Keyboard.Numeric;
                    newEntry.TextChanged += entry_TextChanged;
                    entriesToAddAndRemoveFrom.Add(newEntry);
                    staticEntries.Add(newEntry);
                    colStack.Children.Add(newEntry);
                    addEntryToGrid(entryGrids, colStack, r, gridNumber, c);
                    SetGridNumber();
                }
            }
        }
        private Grid CreateEntryGrid(string classid)
        {
            var grid = new Grid()
            {
                ClassId = classid,
                Margin = new Thickness(1, 1, 1, 1),
                ColumnSpacing = 0,
                RowSpacing = 0,

            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(33, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(33, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(33, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(33, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(33, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(33, GridUnitType.Star) });

            return grid;
        }

        private void RemoveValueFromHints(int value, int row, int column, int grid, SuDoCaseEntry entry)
        {
            var allRowsColumnsAndGrids = labels.Where(x => x.RowNumber == row || x.ColumnNumber == column || x.GridNumber == grid);
            foreach (var label in allRowsColumnsAndGrids.ToArray())
            {
                var originalValue = label.Text;
                var newHint = originalValue.Replace($"{value}", "");
                label.Text = newHint;
                var entryFound = findEntry(label.RowNumber, label.ColumnNumber, label.GridNumber);
                if (entryFound != null)
                    entryFound.Hint.Remove(value);
            }
            entry.Hint.Clear();
            entriesToAddAndRemoveFrom.Remove(entry);
            var labelBelongingToEntry = labels.First(x => x.RowNumber == row && x.ColumnNumber == column && x.GridNumber == grid);
            labelBelongingToEntry.Text = "";
        }

        private void HintRestoreGridField(int row, int column, int grid)
        {
            var rowHash = AllRows.TryGetValue(row, out HashSet<int> rowHashSet);
            var columHash = AllColumns.TryGetValue(column, out HashSet<int> columnHashSet);
            var gridHash = AllGrids.TryGetValue(grid, out HashSet<int> gridHashSet);

            HashSet<int> newHintForGrid = new HashSet<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            newHintForGrid.IntersectWith(rowHashSet);
            newHintForGrid.IntersectWith(columnHashSet);
            newHintForGrid.IntersectWith(gridHashSet);
            if (newHintForGrid.Count == 0)
                return;

            SortedSet<int> set = new SortedSet<int>(newHintForGrid);
            StringBuilder builder = new StringBuilder();
            foreach (var item in set)
            {
                builder.Append(item);
            }
            var newHint = builder.ToString();
            var gridLabel = labels.FirstOrDefault(x => x.RowNumber == row && x.ColumnNumber == column && x.GridNumber == grid);

            if (gridLabel == null)
                return;
            var entry = findEntry(row, column, grid);
            if (!string.IsNullOrWhiteSpace(entry.Text))
                return;
            gridLabel.Text = newHint;

            entry.Hint = newHintForGrid;
            entriesToAddAndRemoveFrom.Add(entry);
        }
        private void GestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var stackLayoutTapped = (StackLayout)sender;
            var entry = stackLayoutTapped.Children.First(x => x is SuDoCaseEntry);
            entry.Focus();
        }
        private void CreateContainerAndSubGrids(List<Grid> entryGrids)
        {
            var entryGrid1 = CreateEntryGrid("1");
            var entryGrid2 = CreateEntryGrid("2");
            var entryGrid3 = CreateEntryGrid("3");
            var entryGrid4 = CreateEntryGrid("4");
            var entryGrid5 = CreateEntryGrid("5");
            var entryGrid6 = CreateEntryGrid("6");
            var entryGrid7 = CreateEntryGrid("7");
            var entryGrid8 = CreateEntryGrid("8");
            var entryGrid9 = CreateEntryGrid("9");
            entryGrids.Add(entryGrid1);
            entryGrids.Add(entryGrid2);
            entryGrids.Add(entryGrid3);
            entryGrids.Add(entryGrid4);
            entryGrids.Add(entryGrid5);
            entryGrids.Add(entryGrid6);
            entryGrids.Add(entryGrid7);
            entryGrids.Add(entryGrid8);
            entryGrids.Add(entryGrid9);

            foreach (var item in entryGrids)
            {
                var containerGrid = new Grid();
                containerGrid.Children.Add(new BoxView() { BackgroundColor = Color.Black });
                containerGrid.Children.Add(item);
                switch (item.ClassId)
                {
                    case "1":
                        row1.Children.Add(containerGrid, 0, 0);
                        break;
                    case "2":
                        row1.Children.Add(containerGrid, 1, 0);
                        break;
                    case "3":
                        row1.Children.Add(containerGrid, 2, 0);
                        break;
                    case "4":
                        row2.Children.Add(containerGrid, 0, 0);
                        break;
                    case "5":
                        row2.Children.Add(containerGrid, 1, 0);
                        break;
                    case "6":
                        row2.Children.Add(containerGrid, 2, 0);
                        break;
                    case "7":
                        row3.Children.Add(containerGrid, 0, 0);
                        break;
                    case "8":
                        row3.Children.Add(containerGrid, 1, 0);
                        break;
                    case "9":
                        row3.Children.Add(containerGrid, 2, 0);
                        break;
                    default:
                        break;
                }
            }
        }

        private void addEntryToGrid(List<Grid> grids, StackLayout colstack, int r, int g, int c)
        {
            var grid = grids.First(x => x.ClassId == g.ToString());
            AddToGrid(grid, colstack, r, c);
        }

        private void AddToGrid(Grid grid, StackLayout colstack, int r, int c)
        {
            var gridRow = 0;
            var gridColumn = 0;
            switch (r)
            {
                case 1:
                case 4:
                case 7:
                    gridRow = 0;
                    break;
                case 2:
                case 5:
                case 8:
                    gridRow = 1;
                    break;
                case 3:
                case 6:
                case 9:
                    gridRow = 2;
                    break;
                default:
                    break;
            }
            switch (c)
            {
                case 1:
                case 4:
                case 7:
                    gridColumn = 0;
                    break;
                case 2:
                case 5:
                case 8:
                    gridColumn = 1;
                    break;
                case 3:
                case 6:
                case 9:
                    gridColumn = 2;
                    break;
                default:
                    break;
            }
            grid.Children.Add(colstack, gridColumn, gridRow);
        }
        private void entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender is SuDoCaseEntry))
                return;
            var entry = (SuDoCaseEntry)sender;

            var row = entry.RowNumber;
            var column = entry.ColumnNumber;
            var grid = entry.GridNumber;

            if (e.NewTextValue == "")
            {
                if (entry.CanRestoreHint)
                {
                    if (int.TryParse(e.OldTextValue, out int result))
                    {


                        var rowHash = AllRows.TryGetValue(row, out HashSet<int> rowHashSet);
                        var columHash = AllColumns.TryGetValue(column, out HashSet<int> columnHashSet);
                        var gridHash = AllGrids.TryGetValue(grid, out HashSet<int> gridHashSet);
                        if (!rowHash || !columHash || !gridHash)
                            return;
                        rowHashSet.Add(result);
                        columnHashSet.Add(result);
                        gridHashSet.Add(result);
                        var allRowsColumnsAndGrids2 = labels.Where(x => x.RowNumber == row || x.ColumnNumber == column || x.GridNumber == grid);
                        foreach (var property in allRowsColumnsAndGrids2)
                        {
                            HintRestoreGridField(property.RowNumber, property.ColumnNumber, property.GridNumber);
                        }

                        entry.CanRestoreHint = false;
                    }
                }
            }
            else
            {
                if (!int.TryParse(e.NewTextValue, out int result))
                {
                    entry.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                    return;
                }
                if (!(result >= 1 && result <= 9))
                {
                    entry.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(e.OldTextValue))
                {

                }
                if (!CheckAndRemove(row, column, grid, result, entry))
                    entry.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                else
                {
                    entry.CanRestoreHint = true;
                }
            }
        }

        private bool CheckAndRemove(int row, int column, int grid, int value, SuDoCaseEntry entry)
        {
            var rowHash = AllRows.TryGetValue(row, out HashSet<int> rowHashSet);
            var columHash = AllColumns.TryGetValue(column, out HashSet<int> columnHashSet);
            var gridHash = AllGrids.TryGetValue(grid, out HashSet<int> gridHashSet);
            if (!rowHash || !columHash || !gridHash)
                return false;
            HashSet<int> all = new HashSet<int>(rowHashSet);
            all.IntersectWith(columnHashSet);
            all.IntersectWith(gridHashSet);
            if (!all.Contains(value))
                return false;
            rowHashSet.Remove(value);
            columnHashSet.Remove(value);
            gridHashSet.Remove(value);
            RemoveValueFromHints(value, row, column, grid, entry);
            return true;

        }
    }
}
