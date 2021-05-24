﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Form8snCore.FileFormats;

namespace Form8snCore.DataExtraction
{
    public static class FilterData
    {
        /// <summary>
        /// returns one of ArrayList, string, int, Dictionary&lt;string,object>; For the dict, 'object' must also be one of these.
        /// </summary>
        /// <param name="type">Filter to apply</param>
        /// <param name="parameters">parameters for this filter</param>
        /// <param name="sourcePath">path the filter is pointing to</param>
        /// <param name="otherFilters">Other filters in the project that can be used as sources</param>
        /// <param name="sourceData">complete data set for the template</param>
        /// <param name="repeaterData">if we are mapping data for a repeating page, put the page specific data here</param>
        public static object? ApplyFilter(MappingType type,
            Dictionary<string, string> parameters,
            string[]? sourcePath,
            Dictionary<string, MappingInfo> otherFilters,
            object? sourceData,
            object? repeaterData)
        {
            var redirects = new HashSet<string>(); // for detecting loops in filter-over-filter
            var filterPackage = new FilterPackage
            {
                Type = type,
                Params = parameters,
                SourcePath = sourcePath,
                FilterSet = otherFilters,
                Data = sourceData,
                RepeaterData = repeaterData,
                Redirects = redirects
            };
            return ApplyFilterRecursive(filterPackage);
        }

        private static object? ApplyFilterRecursive(FilterPackage pkg)
        {
            switch (pkg.Type)
            {
                case MappingType.None:
                    return FindDataAtPath(pkg);
                
                case MappingType.FixedValue:
                    return GetFixedValue(pkg);

                case MappingType.SplitIntoN:
                    return SplitIntoMaxCount(pkg);

                case MappingType.TakeWords:
                    return TakeWords(pkg);

                case MappingType.SkipWords:
                    return SkipWords(pkg);

                case MappingType.Total:
                    return SumOfAllOnPath(pkg);
                
                case MappingType.Concatenate:
                    return ConcatenateList(pkg);

                case MappingType.RunningTotal:
                    // TODO...
                    // This should go through a 'Repeat Source'
                    break;
                default: return "Not yet implemented";
            }

            return null;
        }

        private static object? ConcatenateList(FilterPackage pkg)
        {
            if (pkg.Data == null || pkg.SourcePath == null || pkg.SourcePath.Length < 1) return null;

            var target = FindDataAtPath(pkg);
            if (target == null) return null;
            
            var preKey = nameof(JoinMappingParams.Prefix);
            var prefix = pkg.Params.ContainsKey(preKey) ? pkg.Params[preKey] : "";
            var inKey = nameof(JoinMappingParams.Infix);
            var infix = pkg.Params.ContainsKey(inKey) ? pkg.Params[inKey] : "";
            var postKey = nameof(JoinMappingParams.Postfix);
            var postfix = pkg.Params.ContainsKey(postKey) ? pkg.Params[postKey] : "";

            if (target is string str) return $"{prefix}{str}{postfix}";

            if (target is ArrayList list) return JoinAsString(list, prefix, infix, postfix);

            return null;
        }

        private static string JoinAsString(ArrayList list, string prefix, string infix, string postfix)
        {
            var sb = new StringBuilder(prefix);

            var first = true;
            foreach (var item in list)
            {
                if (first) first = false;
                else sb.Append(infix);
                sb.Append(item);
            }
            
            sb.Append(postfix);
            return sb.ToString();
        }


        /// <summary>
        /// If target is string, split on white space and do an array take, then join on 'space'
        /// If target is an array, do an array take and return resulting array
        /// Otherwise return null 
        /// </summary>
        private static object? TakeWords(FilterPackage pkg)
        {
            if (pkg.Data == null || pkg.SourcePath == null || pkg.SourcePath.Length < 1) return null;
            var countKey = nameof(TakeMappingParams.Count);
            var mcs = pkg.Params.ContainsKey(countKey) ? pkg.Params[countKey] : null;
            if (!int.TryParse(mcs, out var count)) return $"Invalid parameter: Count should be an integer, but is {mcs}";


            var target = FindDataAtPath(pkg);

            if (target == null) return null;

            if (target is string str) return SkipTakeFromString(str, 0, count);

            if (target is ArrayList list) return SkipTakeFromArray(list, 0, count);

            return null;
        }

        /// <summary>
        /// If target is string, split on white space and do an array take, then join on 'space'
        /// If target is an array, do an array take and return resulting array
        /// Otherwise return null 
        /// </summary>
        private static object? SkipWords(FilterPackage pkg)
        {
            if (pkg.Data == null || pkg.SourcePath == null || pkg.SourcePath.Length < 1) return null;
            var countKey = nameof(SkipMappingParams.Count);
            var mcs = pkg.Params.ContainsKey(countKey) ? pkg.Params[countKey] : null;
            if (!int.TryParse(mcs, out var count)) return $"Invalid parameter: Count should be an integer, but is {mcs}";

            var target = FindDataAtPath(pkg);

            if (target == null) return null;

            if (target is string str) return SkipTakeFromString(str, count, int.MaxValue);

            if (target is ArrayList list) return SkipTakeFromArray(list, count, int.MaxValue);

            return null;
        }

        /// <summary>
        /// If target is array, split into sub-arrays
        /// If target is a string, split into character groups
        /// If target is a number, stringify and split as a string
        /// Otherwise return null
        /// </summary>
        private static object? SplitIntoMaxCount(FilterPackage pkg)
        {
            if (pkg.Data == null || pkg.SourcePath == null || pkg.SourcePath.Length < 1) return null;
            var countKey = nameof(MaxCountMappingParams.MaxCount);
            var mcs = pkg.Params.ContainsKey(countKey) ? pkg.Params[countKey] : null;
            if (!int.TryParse(mcs, out var count)) return $"Invalid parameter: MaxCount should be an integer, but is {mcs}";

            var target = FindDataAtPath(pkg);

            if (target == null) return null;

            if (target is ArrayList list) return RepackArray(list, count);

            if (target is string str) return RepackString(str, count);

            return RepackString(target.ToString()!, count);
        }

        private static object? SkipTakeFromString(string str, int skip, int take)
        {
            if (take < 1) return null;
            return string.Join(" ", SplitOnWhiteSpace(str).Skip(skip).Take(take));
        }

        private static object? SkipTakeFromArray(ArrayList list, int skip, int take)
        {
            if (take < 1) return null;
            if (take == 1 && list.Count > skip) return list[skip]; // if we're asking for 'first', don't make a 1-long list
            return new ArrayList(list.ToArray().Skip(skip).Take(take).ToArray());
        }

        private static List<string> SplitOnWhiteSpace(string str)
        {
            // string.Split() doesn't have a char-class based option, so...
            var outp = new List<string>();
            var sb = new StringBuilder();

            foreach (var c in str)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (sb.Length > 0) outp.Add(sb.ToString());
                    sb.Clear();
                }
                else sb.Append(c);
            }

            if (sb.Length > 0) outp.Add(sb.ToString());

            return outp;
        }

        private static object RepackString(string str, int count)
        {
            if (str.Length <= count) return str;
            var outp = new ArrayList();
            var chars = str.ToArray();

            var sb = new StringBuilder();
            foreach (var c in chars)
            {
                sb.Append(c);
                if (sb.Length < count) continue;

                outp.Add(sb.ToString());
                sb.Clear();
            }

            if (sb.Length > 0) outp.Add(sb.ToString());

            return outp;
        }

        private static object RepackArray(ArrayList list, int count)
        {
            if (list.Count <= count) return list;
            var outp = new ArrayList();

            var subList = new ArrayList();
            foreach (var item in list)
            {
                subList.Add(item);
                if (subList.Count < count) continue;

                outp.Add(subList);
                subList = new ArrayList();
            }

            if (subList.Count > 0) outp.Add(subList);

            return outp;
        }

        private static object? SumOfAllOnPath(FilterPackage pkg)
        {
            if (pkg.Data == null || pkg.SourcePath == null || pkg.SourcePath.Length < 1) return null;

            var root = pkg.SourcePath[0];
            var data = pkg.Data;
            var pathSkip = 1;

            if (root == "#")
            {
                if (pkg.SourcePath.Length < 2) return null;
                var newFilter = pkg.RedirectFilter(pkg.SourcePath[1]);
                if (newFilter == null) return null;
                data = ApplyFilterRecursive(newFilter);
                if (data == null) return null;
                pathSkip = 2; // root and filter name
            }
            else if (root == "D")
            {
                if (pkg.SourcePath.Length < 1) return null;
                data = pkg.RepeaterData;
                if (data == null) throw new Exception("Found a repeater filter on a page with no repeater data");
                pathSkip = 1; // root
            }
            else if (root != "") throw new Exception($"Unexpected root marker: {root}");

            // Walk the path. Each time we hit an array, recurse down the path
            return SumPathRecursive(pkg.SourcePath.Skip(pathSkip), data);
        }
        
        // TODO: generalise this and remove the version in SumOfAllOnPath
        private static object? FindDataAtPath(FilterPackage pkg)
        {
            var path = pkg.SourcePath;
            var data = pkg.Data;
            if (path == null || data == null) return null;

            var root = path[0];
            var pathSkip = 1;

            if (root == "#")
            {
                if (path.Length < 2) return null;
                var newFilter = pkg.RedirectFilter(path[1]);
                if (newFilter == null) return null;
                data = ApplyFilterRecursive(newFilter);
                if (data == null) return null;
                pathSkip = 2; // root and filter name
            }
            else if (root == "D")
            {
                if (pkg.RepeaterData == null) throw new Exception("Was asked for repeat page data, but none was supplied");
                if (path.Length < 2) return null;
                data = pkg.RepeaterData;
                pathSkip = 1; // root 'D'
            }
            else if (path[0] != "") throw new Exception($"Unexpected root marker: {path[0]}");

            var sb = new StringBuilder();
            var target = data;
            for (int i = pathSkip; i < path.Length; i++)
            {
                var name = path[i];

                sb.Append(".");
                sb.Append(name);
                if (name.StartsWith('[') && name.EndsWith(']'))
                {
                    // array reference.
                    if (!(target is ArrayList list)) return $"Invalid path: expected list, but was not at {sb}";
                    if (!int.TryParse(name.Trim('[', ']'), out var idx)) return $"Invalid path: malformed index at {sb}";
                    if (idx < 0 || idx >= list.Count) return null;

                    target = list[idx];
                }
                else
                {
                    if (!(target is Dictionary<string, object> dict)) return $"Invalid path: expected object, but was not at {sb}";
                    if (dict.ContainsKey(name))
                    {
                        target = dict[name];
                        continue;
                    }

                    return null;
                }
            }

            return target;
        }

        private static decimal? SumPathRecursive(IEnumerable<string> path, object sourceData)
        {
            var sum = 0.0m;
            var target = sourceData;
            var remainingPath = new Stack<string>(path.Reverse());
            while (remainingPath.Count > 0)
            {
                var name = remainingPath.Pop();

                if (name.StartsWith('[') && name.EndsWith(']'))
                {
                    // array reference.
                    if (remainingPath.Count < 1) return null; // didn't find a valid item
                    if (!(target is ArrayList list)) return null; // can't do it

                    // Ignore the actual index, and recurse over every actual item
                    var pathArray = remainingPath.ToArray();
                    foreach (var item in list)
                    {
                        if (item == null) continue;
                        var maybe = SumPathRecursive(pathArray, item);
                        if (maybe != null) sum += maybe.Value;
                    }

                    return sum;
                }

                if (target is Dictionary<string, object> dict)
                {
                    if (!dict.ContainsKey(name)) return null; // data ended before path
                    target = dict[name];
                    if (remainingPath.Count > 0) continue; // otherwise we *require* a single value at target
                }

                // We have a single value. Try to get a number from it
                if (target is double d) return (decimal)d;
                if (target is int i) return i;
                var lastChance = target.ToString();
                if (decimal.TryParse(lastChance, out var d2)) return d2;

                // Not a number-like value:
                return null;
            }

            return sum;
        }

        private static object? GetFixedValue(FilterPackage pkg)
        {
            var key = nameof(TextMappingParams.Text);
            return pkg.Params.ContainsKey(key) ? pkg.Params[key] : null;
        }
    }
}