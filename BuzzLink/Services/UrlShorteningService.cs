﻿using Microsoft.EntityFrameworkCore;

namespace BuzzLink.Services
{
    public class UrlShorteningService
    {
        public const int NumberOfCharsInShortLink = 7;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly ApplicationDbContext _dbContext;

        private readonly Random _random = new();

        public UrlShorteningService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GenerateUniqueCode()
        {
            var codeChars = new char[NumberOfCharsInShortLink];

            while (true)
            {
                for (int i = 0; i < NumberOfCharsInShortLink; i++)
                {
                    int randomIndex = _random.Next(Alphabet.Length - 1);
                    codeChars[i] = Alphabet[randomIndex];
                }

                var code = new string(codeChars);

                if (!await _dbContext.ShortenedUrls.AnyAsync(s => s.Code == code))
                {
                    return code;
                }
            }
        }
    }
}
