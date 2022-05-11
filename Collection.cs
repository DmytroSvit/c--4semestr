using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace Praktyka1
{
    class Collection:IEnumerable
    {
        private List<Advertisement> _advertisements = new List<Advertisement>();
        public string Name { get; set; }
        public Advertisement this[int i]
        {
            get { return _advertisements[i]; }
        }
        public Collection(string name)
        {
            Name = name;
        }
        public IEnumerator GetEnumerator()
        {
            return _advertisements.GetEnumerator();
        }

        public void Sort()
        {
            _advertisements.Sort();
        }
        public void Search(string s)
        {
            foreach (var ad in _advertisements)
            {
                if (ad.ToString().ToLower().Contains(s.ToLower()))
                    Console.WriteLine(ad);
            }
        }
        public void Add(Advertisement a) 
        {
            if (a.ErrorDict.Count == 0)
            {
                _advertisements.Add(a);
                Console.WriteLine($"Advertisement {a.Title} added successfully");
                PrintToFile();
            }
            else 
            {
                Console.WriteLine($"Advertisement {a.Title} did not added. List of errors: ");
                foreach (var error in a.ErrorDict)
                {
                    Console.WriteLine(error);
                }
            }
        }

        public void Delete(string id)
        {
            bool success = false;

            for (int i = 0; i < _advertisements.Count; i++)
            {
                if (_advertisements[i].ID == id)
                {                   
                    Console.WriteLine($"Advertisement {_advertisements[i].Title} removed successfully");
                    _advertisements.RemoveAt(i);
                    success = true;
                    PrintToFile();
                }
            }

            if(!success)
                Console.WriteLine($"Advertisement with id: {id}, do not exist");
        }

        public void Change(string id, Advertisement a)
        {
            bool success = false;

            for (int i = 0; i < _advertisements.Count; i++)
            {
                if (_advertisements[i].ID == id)
                {
                    Console.WriteLine($"Advertisement {_advertisements[i].Title} changed successfully");
                    _advertisements.RemoveAt(i);
                    _advertisements.Add(a);
                    success = true;
                    PrintToFile();
                }
            }

            if (!success)
                Console.WriteLine($"Advertisement with id: {id}, do not exist");
        }

        private void PrintToFile()
        {
            using (StreamWriter sw = File.CreateText($"{Name}.txt"))
            {
                foreach (var ad in _advertisements)
                {
                    sw.WriteLine(ad);
                }
            }
        }

    }
}
