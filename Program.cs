using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    internal class Program
    {
        // List of all possible colors
        private static readonly List<ColorChip> Chips = new List<ColorChip>();

        private static void Main(string[] args)
        {
            // Generate Sample Chips
            Chips.Add(new ColorChip(Color.Blue, Color.Yellow));
            Chips.Add(new ColorChip(Color.Red, Color.Green));
            Chips.Add(new ColorChip(Color.Yellow, Color.Red));
            Chips.Add(new ColorChip(Color.Orange, Color.Purple));

            Console.WriteLine("\nChips:\n");
            // Show chips
            foreach (var chip in Chips)
            {
                Console.WriteLine("[" + chip + "]");
            }
            Console.WriteLine(); // Carriage return 

            var chipSequence = new List<ColorChip>();

            // Find sequence of chips that unlocks the panel
            foreach (var chip in Chips.Where(chip => chip.StartColor == Color.Blue))
            {
                // Add chip to sequence and check if a valid sequence is found
                chipSequence.Add(chip);
                if (FindSequenceRecursively(chip, chipSequence))
                {
                    Console.WriteLine("Valid Chip Sequence:\n");
                    // Show sequence
                    foreach (var c in chipSequence)
                    {
                        Console.WriteLine("[" + c + "]");
                    }
                    Console.WriteLine("\nPanel Successfully Unlocked.\n");
                    return;
                }
                // Remove chip if no valid sequence
                chipSequence.Remove(chip);
            }

            Console.WriteLine(Constants.ErrorMessage + "!\n");
        }

        // Recursive function to find a valid chip sequence
        // Returns true if a sequence is found, false otherwise
        private static bool FindSequenceRecursively(ColorChip lastChip, List<ColorChip> chipSequence)
        {
            // Check if sequence is valid
            if (lastChip.EndColor == Color.Green)
            {
                return true;
            }

            // Check if sequence is possible
            foreach (var nextChip in Chips.Where(c => c.StartColor == lastChip.EndColor))
            {
                // Check for duplicates
                if (!chipSequence.Contains(nextChip))
                {
                    // Add chip to sequence
                    chipSequence.Add(nextChip);

                    // Check if sequence is valid
                    if (FindSequenceRecursively(nextChip, chipSequence))
                    {
                        return true;
                    }

                    // Remove chip if not valid
                    chipSequence.Remove(nextChip);
                };

            }

            return false;
        }
    }
}