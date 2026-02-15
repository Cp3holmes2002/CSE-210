using System;
using System.Collections.Generic;

class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        _random = new Random();

        string[] splitWords = text.Split(" ");

        for (int i = 0; i < splitWords.Length; i++)
        {
            Word word = new Word(splitWords[i]);
            _words.Add(word);
        }
    }

    public void Display()
    {
        Console.WriteLine(_reference.GetDisplayText());
        Console.WriteLine();

        for (int i = 0; i < _words.Count; i++)
        {
            Console.Write(_words[i].GetDisplayText() + " ");
        }

        Console.WriteLine();
    }

    public void HideRandomWords(int numberToHide)
    {
        int hiddenCount = 0;

        while (hiddenCount < numberToHide)
        {
            int index = _random.Next(_words.Count);

            if (!_words[index].IsHidden())
            {
                _words[index].Hide();
                hiddenCount++;
            }
        }
    }

    public bool AllWordsHidden()
    {
        for (int i = 0; i < _words.Count; i++)
        {
            if (!_words[i].IsHidden())
            {
                return false;
            }
        }

        return true;
    }
}
