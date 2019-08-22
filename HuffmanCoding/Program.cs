using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HuffmanCoding
{
    class Program
    {
        static void Main(string[] args)
        {
            const int maxSize = 256;
            const byte action = 0;
            const byte inFile = 1;
            const string outFile = "output.txt";
            const string keyFile = "key.txt";

            // If the number of command line arguments is incorrect
            if (args.Length != 2)
            {
                // Display usage error message and exit
                Console.WriteLine("Usage:  HuffmanCoding.exe compress inFile");
                Environment.Exit(0);
            }

            // If the action route argument is invalid
            if (args[action] != "compress" && args[action] != "decompress")
            {
                // Display usage error message and exit
                Console.WriteLine("Usage: HuffmanCoding.exe action inFile");
                Console.WriteLine("Action can be either \"compress\" or \"decompress\"");
                Environment.Exit(0);
            }

            // If the input file does not exist
            if (!File.Exists(args[inFile]))
            {
                // Display input error message and exit
                Console.WriteLine($"The input file '{args[inFile]}' does not exist");
                Environment.Exit(0);
            }

            // Instantiate an array of character frequency objects
            var cfArray = new CharacterFrequency[maxSize];
            for (int i = 0; i < maxSize; i++)
                cfArray[i] = new CharacterFrequency((char)i);


            if (args[action] == "compress")
            {
                // Open the input file
                var fileReader = new StreamReader(File.OpenRead(args[inFile]));
                int value = fileReader.Read(); // Read a character from the input file

                // While not at the end of the input file
                while (value != -1)
                {
                    // Increment the character frequency count
                    if (value < maxSize)
                        cfArray[value].Increment();
                    value = fileReader.Read(); // Read the next character
                }

                fileReader.Close(); // Close the input file
                Array.Sort(cfArray); // Sort the array

                // Instantiate a sorted linked list of binary tree nodes of type character frequency
                LinkedList<BinaryTree<CharacterFrequency>> tList = new LinkedList<BinaryTree<CharacterFrequency>>();

                int zeroCounter = 0; // Instantiate counter for number of frequencies with a value of zero
                foreach (CharacterFrequency cf in cfArray)
                {
                    // If the frequency of the character frequency object is not zero
                    if (cf.Frequency != 0)
                    {
                        // Insert the character frequency object into a binary tree node
                        BinaryTree<CharacterFrequency> t = new BinaryTree<CharacterFrequency>();
                        t.Insert(cf, BinaryTree<CharacterFrequency>.Relative.Root);
                        tList.AddLast(t); // Add the node to the linked list
                    }
                    else
                        zeroCounter++; // Increment zero counter

                }

                // If the zero counter registers all frequencies being zero
                if (zeroCounter == maxSize)
                {
                    // Display empty input file error and exit
                    Console.WriteLine("Input file is empty");
                    Environment.Exit(0);
                }

                // While more than one node exists in the linked list
                while (tList.Count > 1)
                {
                    // If only two nodes remain in the linked list and the first node is larger than the last
                    if (tList.Count == 2 && tList.First.Value.Current.Data.Frequency > tList.Last.Value.Current.Data.Frequency)
                    {
                        // Swap the first and last node
                        BinaryTree<CharacterFrequency> temp = tList.First();
                        tList.RemoveFirst();
                        tList.AddLast(temp);
                    }

                    // Remove the first two nodes from the linked list
                    BinaryTree<CharacterFrequency> lowNode = tList.First();
                    tList.RemoveFirst();
                    BinaryTree<CharacterFrequency> highNode = tList.First();
                    tList.RemoveFirst();

                    // Construct a new node with a combined frequency of the two removed nodes
                    CharacterFrequency cf = new CharacterFrequency('\0', lowNode.Current.Data.Frequency + highNode.Current.Data.Frequency);
                    BinaryTree<CharacterFrequency> newNode = new BinaryTree<CharacterFrequency>();
                    newNode.Insert(cf, BinaryTree<CharacterFrequency>.Relative.Root);
                    // Assign the two removed nodes to the newly constructed node as the children
                    newNode.Insert(lowNode.Root, BinaryTree<CharacterFrequency>.Relative.LeftChild);
                    newNode.Insert(highNode.Root, BinaryTree<CharacterFrequency>.Relative.RightChild);

                    // If the new node is smaller than all other nodes or no nodes exist in the linked list
                    if (tList.First == null || newNode.Current.Data.Frequency <= tList.First.Value.Current.Data.Frequency)
                        tList.AddFirst(newNode); // Add the new node to the beginning of the linked list
                    else // Else find the first node in the linked list that is larger than the new node
                    {
                        LinkedListNode<BinaryTree<CharacterFrequency>> current = tList.First;
                        while (current.Next != null && (newNode.Current.Data.Frequency >= current.Value.Current.Data.Frequency))
                            current = current.Next;
                        tList.AddBefore(current, newNode); // Add the new node before the found larger node
                    }
                }

                // Construct the encoding table
                tList.First.Value.Encode(tList.First.Value.Root);

                // Instantiate an array of strings representing Huffman codes
                string[] hcArray = tList.First.Value.PathTable;

                // Open the input file
                fileReader = new StreamReader(File.OpenRead(args[inFile]));

                // If the output file exists, delete it
                if (File.Exists(outFile)) File.Delete(outFile);
                var fileWriter = new StreamWriter(File.OpenWrite(outFile)); // Open the output file

                value = fileReader.Read(); // Read a character from the input file
                byte b = 0; // Instantiate empty byte
                int j = 7; // Instantiate counter

                // While not at the end of the input file
                while (value != -1)
                {
                    if (value < maxSize)
                    {
                        // Find assigned Huffman code for the read character
                        string huffmanCode = hcArray[value];

                        // For each bit in the found Huffman code
                        foreach (char c in huffmanCode)
                        {
                            // If a full byte has been constructed
                            if (j == -1)
                            {
                                // Write the constructed byte to the output file
                                fileWriter.Write($"{Convert.ToString(b, 2).PadLeft(8, '0')}");
                                b = 0; // Reset byte
                                j = 7; // Reset counter
                            }

                            // If the current bit in the found Huffman code is a one
                            if (c == '1')
                                // Modify the bit corresponding to the counter-designated place in the byte to be a one
                                b = (byte)(b | (byte)(Math.Pow(2, j)));
                            j--; // Else increment the counter
                        }
                    }
                    value = fileReader.Read(); // Read the next character from the input file
                }

                // Write final byte to the output file
                fileWriter.Write($"{Convert.ToString(b, 2).PadLeft(8, '0')}");
                fileWriter.Close(); // Close the output file

                // If the key file exists, delete it
                if (File.Exists(keyFile)) File.Delete(keyFile);
                fileWriter = new StreamWriter(File.OpenWrite(keyFile)); // Open the key file

                // Write character frequency counts to key file
                foreach (CharacterFrequency cf in cfArray)
                {
                    if (cf.Frequency != 0)
                        fileWriter.WriteLine(cf.ToString());
                }

                fileWriter.Close(); // Close the key file
            }
            else // Decompress file
            {
                // If the key file does not exist
                if (!File.Exists(keyFile))
                {
                    // Display missing key file error message and exit
                    Console.WriteLine("No key file found\nCannot decompress input file");
                    Environment.Exit(0);
                }

                // Open the key file
                var fileReader = new StreamReader(File.OpenRead(keyFile));
                int totalCharacters = 0; // Instantiate total character counter
                var key = fileReader.ReadLine(); // Read the first line from the key file


                string[] cfBuilder = null;
                Regex r = new Regex(@"^.+?[|]\d+?[|]\d+?$"); // https://www.regexpal.com/

                // While the read line is not null
                while (key != null)
                {
                    // Check the read line for matching string representations of a character frequency object
                    if (r.IsMatch(key))
                    {
                        // Modify the character frequency array accordingly to found matches
                        cfBuilder = key.Split('|');
                        cfArray[Convert.ToInt32(cfBuilder[1])].Frequency = Convert.ToInt32(cfBuilder[2]);
                        totalCharacters += Convert.ToInt32(cfBuilder[2]); // Increment total character counter by found frequencies
                    }
                    key = fileReader.ReadLine(); // Read the next line
                }

                fileReader.Close(); // Close the key file

                Array.Sort(cfArray); // Sort the array

                // Instantiate a sorted linked list of binary tree nodes of type character frequency
                LinkedList<BinaryTree<CharacterFrequency>> tList = new LinkedList<BinaryTree<CharacterFrequency>>();

                int zeroCounter = 0; // Instantiate counter for number of frequencies with a value of zero

                foreach (CharacterFrequency cf in cfArray)
                {
                    // If the frequency of the character frequency object is not zero
                    if (cf.Frequency != 0)
                    {
                        // Insert the character frequency object into a binary tree node
                        BinaryTree<CharacterFrequency> t = new BinaryTree<CharacterFrequency>();
                        t.Insert(cf, BinaryTree<CharacterFrequency>.Relative.Root);
                        tList.AddLast(t); // Add the node to the linked list
                    }
                    else
                        zeroCounter++; // Increment zero counter
                }

                // If the zero counter registers all frequencies being zero
                if (zeroCounter == maxSize)
                {
                    // Display empty input file error and exit
                    Console.WriteLine("Input file is empty");
                    Environment.Exit(0);
                }

                // While more than one node exists in the linked list
                while (tList.Count > 1)
                {
                    // If only two nodes remain in the linked list and the first node is larger than the last
                    if (tList.Count == 2 && tList.First.Value.Current.Data.Frequency > tList.Last.Value.Current.Data.Frequency)
                    {
                        // Swap the first and last node
                        BinaryTree<CharacterFrequency> temp = tList.First();
                        tList.RemoveFirst();
                        tList.AddLast(temp);
                    }

                    // Remove the first two nodes from the linked list
                    BinaryTree<CharacterFrequency> lowNode = tList.First();
                    tList.RemoveFirst();
                    BinaryTree<CharacterFrequency> highNode = tList.First();
                    tList.RemoveFirst();

                    // Construct a new node with a combined frequency of the two removed nodes
                    CharacterFrequency cf = new CharacterFrequency('\0', lowNode.Current.Data.Frequency + highNode.Current.Data.Frequency);
                    BinaryTree<CharacterFrequency> newNode = new BinaryTree<CharacterFrequency>();
                    newNode.Insert(cf, BinaryTree<CharacterFrequency>.Relative.Root);
                    // Assign the two removed nodes to the newly constructed node as the children
                    newNode.Insert(lowNode.Root, BinaryTree<CharacterFrequency>.Relative.LeftChild);
                    newNode.Insert(highNode.Root, BinaryTree<CharacterFrequency>.Relative.RightChild);

                    // If the new node is smaller than all other nodes or no nodes exist in the linked list
                    if (tList.First == null || newNode.Current.Data.Frequency <= tList.First.Value.Current.Data.Frequency)
                        tList.AddFirst(newNode); // Add the new node to the beginning of the linked list
                    else // Else find the first node in the linked list that is larger than the new node
                    {
                        LinkedListNode<BinaryTree<CharacterFrequency>> current = tList.First;
                        while (current.Next != null && (newNode.Current.Data.Frequency >= current.Value.Current.Data.Frequency))
                            current = current.Next;
                        tList.AddBefore(current, newNode); // Add the new node before the found larger node
                    }
                }

                // If the output file already exists
                if (File.Exists(outFile))
                    File.Delete(outFile); // Delete the output file

                // Open the input and output file
                fileReader = new StreamReader(File.OpenRead(args[inFile]));
                var fileWriter = new StreamWriter(File.OpenWrite(outFile));
                var value = fileReader.Read(); // Read a character from the input file

                // Instantiate counter and array to measure out full bytes
                char[] s = new char[8];
                int i = 0;

                // While input file data is valid
                while (value == '1' || value == '0')
                {
                    // If a full byte has been measured out
                    if (i == 8)
                    {
                        // Search the binary tree for leaf nodes corresponding to path designated by bits
                        for (int c = 0; c < s.Length; c++)
                        {
                            // If bit is a one
                            if (s[c] == '1')
                                // Traverse the tree to the left
                                tList.First.Value.MoveTo(BinaryTree<CharacterFrequency>.Relative.LeftChild);
                            // If bit is a zero
                            if (s[c] == '0')
                                // Traverse the tree to the right
                                tList.First.Value.MoveTo(BinaryTree<CharacterFrequency>.Relative.RightChild);

                            // If a leaf node is found and total remaining characters is not zero
                            if (tList.First.Value.Current.IsLeaf() == true && totalCharacters != 0)
                            {
                                // Write the leaf node character to the output file
                                fileWriter.Write($"{tList.First.Value.Current.Data.Character}");
                                // Return to the root of the binary tree
                                tList.First.Value.MoveTo(BinaryTree<CharacterFrequency>.Relative.Root);
                                totalCharacters--; // Decrease remaining total characters in compressed file
                            }
                        }

                        // Reset the counter and byte measuring array
                        i = 0;
                        s = new char[8];
                    }
                    else
                    {
                        // Add bit to byte measuring array
                        s[i] = (char)value;
                        i++; // Increment counter
                        value = fileReader.Read(); // Read the next character from the input file
                    }
                }

                // Close input and output files
                fileWriter.Close();
                fileReader.Close();
            }
        }
    }
}